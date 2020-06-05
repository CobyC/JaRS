using DevExpress.XtraSplashScreen;
using JARS.Client.Bootstrap;
using JARS.Core;
using JARS.Core.Client;
using JARS.Core.Rules;
using JARS.Core.WinForms.Forms;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Text;
using System.Windows.Forms;

namespace JARS.WinForms
{
    static class Program
    {
        public static GlobalContext Context { get => GlobalContext.Instance; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            Application.ApplicationExit += Application_ApplicationExit;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();

            if (CheckSecurity())
            {
                LoginRegisterForm frm = new LoginRegisterForm(Context.AuthClient);
                AuthenticateResponse authResult = frm.LoginOrRegister();

                if (authResult != null && authResult.BearerToken != null)
                {
                    Context.SetAuthenticationResponse(authResult);
                    //now we can register the app to the service stack client because we have all the auth info required
                    if (!Context.InitializeSSEClient())
                    {
                        MessageBox.Show($"Unable to establish connection to server \r\nPlease review log file \r\n{Context.ConnectionStatus}", "Unable to connect to server", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Application.Exit();
                        return;
                    }

                    SplashScreenManager.ShowForm(typeof(SplashForm), true, true);
                    SplashScreenManager.Default.SendCommand(null, "Setting Up JaRS.");
                    //SplashScreenManager.Default.SendCommand(null, "Checking License...");
                    //CheckLicence()    
                    SplashScreenManager.Default.SendCommand(null, "Loading Plugins and Extensions...");
                    List<ComposablePartCatalog> parts = new List<ComposablePartCatalog>() { new AssemblyCatalog(typeof(MainForm).Assembly) };
                    JarsCore.Container = MEFClientLoader.Init(parts);
                    SplashScreenManager.Default.SendCommand(null, "Register Client To Service...");
                    Context.RegisterEventsServer();
                    MainForm mainForm = new MainForm();

                    Application.Run(mainForm);
                }
                else
                {
                    MessageBox.Show("Unable to authenticate user", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show($"You are not authorized to use JaRS. {Environment.NewLine}If you require access to JaRS please contact your administrator.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }
        }

        //private static void OnException(Exception ex)
        //{
        //    if (ex is TokenException)
        //    {
        //        Authenticator athr = new Authenticator(Context.AuthClient);
        //        TokenException tex = ex as TokenException;
        //        AuthenticateResponse authResult = athr.TryAuthenticateWithLastSavedProvider().Result;
        //        Context.SSEventClient.EventStreamRequestFilter =
        //            req =>
        //            {
        //                req.AddBearerToken(authResult.BearerToken);
        //                //req.Credentials = CredentialCache.DefaultCredentials;
        //            };

        //        //also need to assign the credentials to the underlying ServiceClient.
        //        Context.SSEventClient.ServiceClient.BearerToken = authResult.BearerToken;                
        //    }
        //    else
        //    { }
        //}

        static bool CheckSecurity()
        {

            //AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            //WindowsPrincipal MyPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;

            //if (MyPrincipal.IsInRole(JarsRoles.Admin))
            return true;
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            StringBuilder errString = new StringBuilder();
            errString.AppendLine("Error Info:");
            if (e.Exception != null)
            {
                errString.AppendLine("-");
                errString.AppendLine(e.Exception.Message);

                if (e.Exception.InnerException != null)
                {
                    errString.AppendLine("--");
                    errString.AppendLine(e.Exception.InnerException.Message);

                    if (e.Exception.InnerException.InnerException != null)
                    {
                        errString.AppendLine("---");
                        errString.AppendLine(e.Exception.InnerException.InnerException.Message);
                    }
                }
            }
            errString.AppendLine("Please see log file for more details");
            Logger.Fatal(errString.ToString(), e.Exception);

            MessageBox.Show(errString.ToString(), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (Context.SSEventClient != null)
            {
                Context.SSEventClient.RemoveAllRegistrations();
                Context.SSEventClient.Stop();
            }
        }
    }
}