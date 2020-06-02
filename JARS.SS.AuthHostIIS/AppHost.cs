﻿using Funq;
using JARS.Core;
using JARS.Core.Security;
using JARS.Data.NH.Jars.Interfaces;
using ServiceStack;
using ServiceStack.Api.OpenApi;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace JARS.SS.AuthHostIIS
{
    public class AppHost : AppHostBase
    {
        [Import]
        IJarsUserRepository _JarsUserRepository;
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("Jars IIS Service Host", typeof(SecureService).Assembly)
        {
            if (JarsCore.Container != null)
                JarsCore.Container.SatisfyImportsOnce(this);

            if (AppSettings == null)
                AppSettings = new AppSettings();
        }

        public AppHost(IJarsUserRepository jarsUserRepository) : this()
        {
            _JarsUserRepository = jarsUserRepository;
        }


        //indicate if only authenticated users will be able to use the service
        public bool LimitToAuthenticatedUser { get; set; }

        //The action that will be executed when the server publishes a response 
        //to subscribed clients
        public Action<IEventSubscription, IResponse, string> OnPublish { get; set; }

        //the action triggered on a new subscription.
        public Action<IEventSubscription> OnSubscribe { get; set; }

        //Action triggered on a connection being made
        public Action<IEventSubscription, Dictionary<string, string>> OnConnect { get; set; }

        //Action triggered on a client unsubscribing
        public Action<IEventSubscription> OnUnsubscribe { get; set; }

        /// <summary>
        /// Configure the service with plugins and features.
        /// </summary>        
        public override void Configure(Container container)
        {
            //set up orm connection factory, mainly used for auth stuff
            var dbFactory = new OrmLiteConnectionFactory(AppSettings.GetConnectionString("AUTH_DB"), SqlServer2012Dialect.Provider);
            container.Register<IDbConnectionFactory>(c => dbFactory);

            Plugins.Add(new OpenApiFeature()); //the open api feature
            //implement the custom auth provider
            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                new IAuthProvider[] {
                        //new AspNetWindowsAuthProvider(this), //<-- this can only be used when hoisting in IIS!!!
                        new ApiKeyAuthProvider(AppSettings),
                        new CredentialsAuthProvider(AppSettings),
                        new JwtAuthProvider(AppSettings), //<--when not using Microsoft.Identity.Client to get JWT token.
                        //new AadJwtAuthProvider(AppSettings),
                    //new MicrosoftGraphAuthProvider(AppSettings),
                    //new IdentityServerAuthFeature(AppSettings), 
                    //new CustomCredentialsAuthProvider(), //<-- we will use this to look at active directory, although should be changed to something else
                    //other providers can be used for external connections.                        
                }));

            //allso add the registration feature, where we can register users, roles and permissions
            Plugins.Add(new RegistrationFeature());
            //add a memory cache client to the container.
            //container.Register<ICacheClient>(new MemoryCacheClient()); //<-- to do this in memory while in dev.
            //container.RegisterAs<OrmLiteCacheClient, ICacheClient>();
            //container.Register<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["AUTH_DB"].ConnectionString, SqlServer2012Dialect.Provider));
            //container.Register<IAuthRepository>(c => new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>())
            //{ UseDistinctRoleTables = true });
            //container.Resolve<IAuthRepository>().InitSchema();
            //container.Resolve<ICacheClient>().InitSchema();
            //container.Register<ICacheClient>(new JarsCacheClient());
            //container.Register<IAuthRepository>(c => new JarsAuthRepository());
            container.Register<ICacheClient>(new OrmLiteCacheClient() { DbFactory = dbFactory });
            container.Resolve<ICacheClient>().InitSchema();
            container.Register<IAuthRepository>(c => new OrmLiteAuthRepository(dbFactory));
            container.Resolve<IAuthRepository>().InitSchema();
            //new SaltedHash().GetHashAndSaltString("password", out string hash, out string salt);
            //then we need to add a user to authenticate to the user repository.
                       
            var authRepo = container.Resolve<IAuthRepository>();
            if (authRepo.GetUserAuthByUserName("JarsAdminUser") == null)
            {
                authRepo.CreateUserAuth(new UserAuth()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    DisplayName = "Jars Admin",
                    UserName = "jarsadminuser",
                    Email = "jarsadminuser@jars.com",
                    Roles = { RoleNames.Admin },
                    Permissions = { JarsPermissions.Full }
                }, "J@r5@dm1nU53r");
            }

            if (authRepo.GetUserAuthByUserName("JarsMobileApp") == null)
            {
                authRepo.CreateUserAuth(new UserAuth()
                {
                    FirstName = "JarsMobile",
                    LastName = "App",
                    DisplayName = "JarsMobile App",
                    UserName = "jarsmobileapp",
                    //Email = "manageruser@jars.com",                   
                    Roles = { JarsRoles.MobileApp },
                    Permissions = { JarsPermissions.CanView, JarsPermissions.CanAdd, JarsPermissions.CanEdit, JarsPermissions.CanDelete }
                }, "Th1sN33ds70Ch@ng£");
            }

            if (authRepo.GetUserAuthByUserName("AdminUser") == null)
            {
                authRepo.CreateUserAuth(new UserAuth()
                {
                    FirstName = "AdminTest",
                    LastName = "AdminTest",
                    DisplayName = "Jars Admin Test",
                    UserName = "adminuser",
                    Email = "adminuser@jars.com",
                    Roles = { RoleNames.Admin },
                    Permissions = { JarsPermissions.Full }
                }, "adminuser");
            }

            if (authRepo.GetUserAuthByUserName("guestuser") == null)
            {
                authRepo.CreateUserAuth(new UserAuth()
                {
                    FirstName = "Guest",
                    LastName = "User",
                    DisplayName = "Guest User",
                    UserName = "guestuser",
                    Email = "guestuser@jars.com",
                    Roles = { JarsRoles.Guest },
                }, "guestuser");

                //get the user from the database and give it some roles
                var testUser = authRepo.GetUserAuthByUserName("guestuser");
                authRepo.AssignRoles(testUser, new[] { JarsRoles.Guest });
            }

            if (authRepo.GetUserAuthByUserName("TestUser") == null)
            {
                authRepo.CreateUserAuth(new UserAuth()
                {
                    FirstName = "Test",
                    LastName = "User",
                    DisplayName = "Test User",
                    UserName = "testuser",
                    Email = "testuser@jars.com",
                    Roles = { JarsRoles.User },
                }, "testuser");

                //get the user from the database and give it some roles
                var testUser = authRepo.GetUserAuthByUserName("testuser");
                authRepo.AssignRoles(testUser, new[] { JarsRoles.User }, new[] { JarsPermissions.CanView, JarsPermissions.CanAddAppointment });
            }

            if (authRepo.GetUserAuthByUserName("TestUser") == null)
            {
                authRepo.CreateUserAuth(new UserAuth()
                {
                    FirstName = "Power",
                    LastName = "User",
                    DisplayName = "Power User",
                    UserName = "poweruser",
                    Email = "poweruser@jars.com",
                    Roles = { JarsRoles.PowerUser },
                }, "poweruser");

                //get the user from the database and give it some roles
                var testUser = authRepo.GetUserAuthByUserName("poweruser");
                authRepo.AssignRoles(testUser, new[] { JarsRoles.PowerUser });
            }


            if (authRepo.GetUserAuthByUserName("TestUser") == null)
            {
                authRepo.CreateUserAuth(new UserAuth()
                {
                    FirstName = "Manager",
                    LastName = "User",
                    DisplayName = "Manager User",
                    UserName = "manageruser",
                    Email = "manageruser@jars.com",
                    Roles = { JarsRoles.Manager },
                }, "manageruser");

                //get the user from the database and give it some roles
                var testUser = authRepo.GetUserAuthByUserName("manageruser");
                authRepo.AssignRoles(testUser, new[] { JarsRoles.Manager });
            }
            
            AfterInitCallbacks.Add(host =>
            {
                var authProvider = (ApiKeyAuthProvider)
                    AuthenticateService.GetAuthProvider(ApiKeyAuthProvider.Name);
                using (var db = host.TryResolve<IDbConnectionFactory>().Open())
                {
                    var userWithKeysIds = db.Column<string>(db.From<ApiKey>()
                        .SelectDistinct(x => x.UserAuthId)).Map(int.Parse);

                    var userIdsMissingKeys = db.Column<string>(db.From<UserAuth>()
                        .Where(x => userWithKeysIds.Count == 0 || !userWithKeysIds.Contains(x.Id))
                        .Select(x => x.Id));

                    var aRepo = (IManageApiKeys)host.TryResolve<IAuthRepository>();
                    foreach (var userId in userIdsMissingKeys)
                    {
                        var apiKeys = authProvider.GenerateNewApiKeys(userId.ToString());
                        aRepo.StoreAll(apiKeys);
                    }
                }
            });
        }
    }
}