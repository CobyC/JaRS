using System;
using System.Security.Principal;
using System.Threading;
using Funq;
using JARS.Business.Bootstrap;
using JARS.Core;
using JARS.Core.Security;
using JARS.SS.Auth;
using JARS.SS.DTOs;
using JARS.SS.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Configuration;

namespace JARS.Test.SS.Services
{
    [TestClass]
    public class Auth_And_Roles_Test
    {
        const string BaseUri = "http://localhost:2211/";
        private readonly ServiceStackHost appHost;

        /// <summary>
        /// This is the SelfHost class, it acts as the host of the services
        /// </summary>
        class AppHost : AppSelfHostBase
        {
            private bool LimitToAuthenticatedUser { get; set; } = true;
            //for this test we will only use the basic classes that will ship with jars, and not the extension classes and services.
            public AppHost() : base(nameof(Auth_And_Roles_Test), typeof(JarsUserService).Assembly) { }

            public override void Configure(Container container)
            {
                //because we use service events, we need to register the plugin for it.
                Plugins.Add(new ServerEventsFeature());

                container.Register<IAuthRepository>(c => new JarsAuthRepository());
                container.Register<ICacheClient>(new JarsCacheClient());

                Plugins.Add(new RegistrationFeature() { AllowUpdates = true });
                container.Resolve<IAuthRepository>().InitSchema();
                container.Resolve<ICacheClient>().InitSchema();

                //implement the custom auth provider
                if (LimitToAuthenticatedUser)
                {
                    Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                        new IAuthProvider[] {
                        //new AspNetWindowsAuthProvider(this)  //<-- this can only be used when hoisting in IIS!!!
                        //{
                        //    LoadUserAuthFilter = LoadUserAuthInfo,
                        //    AllowAllWindowsAuthUsers =true,
                        //    PersistSession = true
                        //},
                        new CredentialsAuthProvider()
                        }));
                }

                var authRepo = container.Resolve<IAuthRepository>();
                authRepo.InitSchema();
                if (authRepo.GetUserAuthByUserName("Admin") == null)
                {
                    authRepo.CreateUserAuth(new UserAuth()
                    {
                        DisplayName = "JaRS Admin",
                        UserName = RoleNames.Admin,
                        Email = "admin@jars.com",
                        Roles = { RoleNames.Admin },
                    }, "Jars@dm1n");

                    var adminUser = authRepo.GetUserAuthByUserName(RoleNames.Admin);
                    authRepo.AssignRoles(adminUser, new[] { RoleNames.Admin });
                }

                if (authRepo.GetUserAuthByUserName("TestUser") == null)
                {
                    authRepo.CreateUserAuth(new UserAuth()
                    {
                        DisplayName = "Test User",
                        UserName = "TestUser",
                        Email = "testuser@jars.com",
                        Roles = { "JarsUser" },
                    }, "testuser");

                    var testUser = authRepo.GetUserAuthByUserName("TestUser");
                    authRepo.AssignRoles(testUser, new[] { JarsRoles.User });
                }
            }
        }

        /// <summary>
        /// Start up the service host in the constructor.
        /// </summary>
        public Auth_And_Roles_Test()
        {
            JarsCore.Container = MEFBusinessLoader.Init();
            //MEFLoader.Init();
            string licPath = "~/ServiceStackLicense.txt".MapAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licPath);
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);
        }

        [TestCleanup]
        public void OneTimeTearDown()
        {
            appHost.Dispose();
            JarsCore.Container.Dispose();
        }

        /// <summary>
        /// This methods creates an event client instance
        /// </summary>
        /// <returns>returns an event service stack client instance.</returns>
        public ServerEventsClient CreateEventClient() => new ServerEventsClient(BaseUri, channels: "test");

        /// <summary>
        /// This methods creates a client instance (jSonServiceClient)
        /// </summary>
        /// <returns>returns a service stack client instance.</returns>
        public IServiceClient CreateClient() => new JsonServiceClient(BaseUri);

        [TestMethod]
        private void Auth_and_Assign_Roles_To_Win_Principal()
        {
            var client = CreateClient();
            var eventClient = CreateEventClient().Start();

            //Authenticate
            AuthenticateResponse authR = client.Post(new Authenticate { UserName = "Admin", Password = "Jars@dm1n", RememberMe = true });


            AuthenticateResponse evAuthR = eventClient.Authenticate(new Authenticate { UserName = "testuser", Password = "test", RememberMe = true });

            //get the user from the database
            JarsUserResponse juResp = client.Post(new GetJarsUser { EmailOrUserName = authR.UserId });

            JarsUserResponse juEvResp = eventClient.ServiceClient.Get(new GetJarsUser { EmailOrUserName = evAuthR.UserId });


            //assign user roles to the current windows user
            //this is for single use validation
            WindowsIdentity MySingleUseIdentity = WindowsIdentity.GetCurrent();
            WindowsPrincipal MySingleUsePrincipal = new WindowsPrincipal(MySingleUseIdentity);
            if (MySingleUsePrincipal.IsInRole(JarsRoles.Admin))
                Assert.IsTrue(true);


            //This is for repeated validation use, it sets the principal object on the app domain
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            WindowsPrincipal MyPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;
            if (MyPrincipal.IsInRole(JarsRoles.Admin))
                Assert.IsTrue(true);
            //if (MyPrincipal.IsInRole(JarsRoles.JarsAdministrators))
            //    Assert.IsTrue(true);
            //if (MyPrincipal.IsInRole(JarsRoles.JarsPowerUsers))
            //    Assert.IsTrue(true);
            //if (MyPrincipal.IsInRole(JarsRoles.JarsUsers))
            //    Assert.IsTrue(true);

            //Create a generic (non windows account related) Identity and principal
            GenericIdentity MyGenericIdentity = new GenericIdentity("JarsUser");
            String[] MyRolesArray = { "Manager", "Teller" };
            GenericPrincipal MyGenericPrincipal = new GenericPrincipal(MyGenericIdentity, MyRolesArray);
            //then we can attach it to the current thread (AppDomain?)
            Thread.CurrentPrincipal = MyGenericPrincipal;

            //or use the method below to add the roles of the windows user to the generic identity (as suggested by microsoft)
            GenericPrincipal genWinBasePrincipal = GetGenericPrincipalFromWindowsIdentity(MyRolesArray);
            AppDomain.CurrentDomain.SetThreadPrincipal(genWinBasePrincipal);

        }
        // Create a generic principal based on values from the current
        // WindowsIdentity.
        private static GenericPrincipal GetGenericPrincipalFromWindowsIdentity(string[] additionalRoles = null)
        {
            // Use values from the current WindowsIdentity to construct
            // a set of GenericPrincipal roles.
            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            string[] roles;
            if (additionalRoles != null)
                roles = new string[5 + additionalRoles.Length];
            else
                roles = new string[5];

            if (windowsIdentity.IsAuthenticated)
            {
                // Add custom NetworkUser role.
                roles[0] = "NetworkUser";
            }

            if (windowsIdentity.IsGuest)
            {
                // Add custom GuestUser role.
                roles[1] = "GuestUser";
            }

            if (windowsIdentity.IsSystem)
            {
                // Add custom SystemUser role.
                roles[2] = "SystemUser";
            }

            //add any additional roles to the principal.
            if (additionalRoles != null)
            {
                for (int i = 0; i < additionalRoles.Length; i++)
                {
                    roles[i + 2] = additionalRoles[i];
                }
            }

            // Construct a GenericIdentity object based on the current Windows
            // identity name and authentication type.
            string authenticationType = windowsIdentity.AuthenticationType;
            string userName = windowsIdentity.Name;
            GenericIdentity genericIdentity = new GenericIdentity(userName, authenticationType);

            // Construct a GenericPrincipal object based on the generic identity
            // and custom roles for the user.
            GenericPrincipal genericPrincipal = new GenericPrincipal(genericIdentity, roles);

            return genericPrincipal;
        }

    }
}
