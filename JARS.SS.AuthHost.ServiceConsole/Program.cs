using JARS.Business.Bootstrap;
using JARS.Core;
using ServiceStack;
using ServiceStack.Configuration;
using System;
using System.Linq;

namespace JARS.SS.AuthHost.ServiceConsole
{
    class Program
    {
        private static IAppSettings _AppSettings;
        public static IAppSettings AppSettings
        {
            get
            {
                if (_AppSettings == null)
                    _AppSettings = new AppSettings();
                return _AppSettings;
            }
        }

        static void Main(string[] args)
        {
            //var pkey = AesUtils.CreateKey().ToXml();

            JarsCore.Container = MEFBusinessLoader.Init();
            Logger.Info("MEF Loaded!");

            ////add license
            //string licPath = "~/ServiceStackLicense.txt".MapAbsolutePath();
            //Logger.Info($"Registering ServiceStack Licence looking for:{licPath}");

            //setup services
            var listeningAuthOn = GetAuthHostUrl();
            //set up the authenticatio service, this is used for authentication only.
            JarsAuthServiceAppHost appAuthHost = new JarsAuthServiceAppHost();
            try
            {
                appAuthHost.Init().Start(listeningAuthOn);
            }
            catch (Exception ex)
            {
                Logger.Info($"Auth Error:{ex.Message}");
                Console.WriteLine("\r\n Authentication Service Error:" + ex.Message);
            }
            string listeningOnVals = "";
            listeningAuthOn.ToList().ForEach(s => listeningOnVals += $"ip: {s.ToString()}{Environment.NewLine}");
            Console.WriteLine($"AuthAppHost Created at {DateTime.Now}, listening on: {Environment.NewLine}{listeningOnVals}");

            Console.ReadLine();
        }

        static string[] GetAuthHostUrl()
        {
            string[] retUrl = new string[] { "http://localhost:3011/", "" };
            retUrl[0] = AppSettings.GetString("RemoteAuthServiceUrl");
            retUrl[1] = AppSettings.GetString("RemoteAuthServiceUrl_SSL");
            return retUrl;
        }
    }
}
