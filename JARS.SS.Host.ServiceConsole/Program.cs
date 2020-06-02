using JARS.Business.Bootstrap;
using JARS.Core;
using JARS.Core.Utils;
using ServiceStack;
using ServiceStack.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JARS.SS.Host.ServiceConsole
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
            var skey = AesUtils.CreateKey();
            var keyXml = skey.ToXml();
            var kUrl = skey.ToBase64UrlSafe();

            JarsCore.Container = MEFBusinessLoader.Init();
            Logger.Info("MEF Loaded!");

            //add license
            string licPath = "~/ServiceStackLicense.txt".MapAbsolutePath();
            Logger.Info($"Registering ServiceStack Licence looking for:{licPath}");
            try
            {
                Licensing.RegisterLicenseFromFileIfExists(licPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to close");
                Console.ReadLine();
                return;

            }
            //setup service
            var listeningOn = args.Length == 0 ? GetHostUrl() : args;
            //start setting up the service stack host
            JarsServiceAppHost appHost = new JarsServiceAppHost(AssemblyLoaderUtil.ServicesAssemblies.ToArray());
            appHost.OnConnect = (evtSub, dictVal) => { Console.WriteLine($"OnConnect - Connection UserId:{evtSub.UserId} UserName: {evtSub.UserName} dictVals:{dictVal.Values}"); };
            appHost.OnSubscribe = (evtSub) => { Console.WriteLine($"OnSubscribe - sub:{evtSub.UserId}"); };
            appHost.OnPublish = (sub, res, msg) => { if (!msg.Contains("cmd.onHeartbeat")) Console.WriteLine($"Publish - DisplayName:{sub.DisplayName} Res.Status:{res.StatusCode} MsgLen:{msg.Length}"); };
            appHost.OnUnsubscribe = (evtSub) => { Console.WriteLine($"OnUnsubscribe - sub:{evtSub.UserId}"); };
            appHost.LimitToAuthenticatedUser = true;
            try
            {   //start the service
                appHost.Init().Start(listeningOn);
            }
            catch (Exception ex)
            {
                Logger.Info($"Error:{ex.Message}");
                Console.WriteLine("\r\n Press key to close app");
                Console.ReadLine();
                return;
            }

            string listeningOnVals = "";
            listeningOn.ToList().ForEach(s => listeningOnVals += $"ip: {s.ToString()}{Environment.NewLine}");

            Console.WriteLine($"AppHost Created at {DateTime.Now}, listening on: {Environment.NewLine}{listeningOnVals}");

            Console.WriteLine("write 'exit' to close, 'notify' to send notification to all subscribers, test to notify 'test' channel or sub-[chanel name] to notify that channel");

            //resolve the events plugin loaded in the configuration.
            IServerEvents se = appHost.TryResolve<IServerEvents>();

            int i = 0;
            string key = "start";
            while (key != "exit")
            {
                Console.Write("Subs: {0}", se.GetAllSubscriptionInfos().Count);
                key = Console.ReadLine();

                if (key == "notify")
                    se.NotifyAll($"Notify All count({i})");

                if (key == "test")
                    se.NotifyChannel("test", $"Notify all in test channel : count({i})");

                if (key.Contains("sub"))
                    se.NotifyChannel(key.Substring(key.IndexOf("-")), $"Notify channel {key.Substring(key.IndexOf("-"))} : count({i})");

                if (key.Contains("infos"))
                {
                    List<SubscriptionInfo> infs = se.GetAllSubscriptionInfos();

                    foreach (var si in infs)
                    {
                        Console.Write($"DisplayName:{si.DisplayName}, Auth:{si.IsAuthenticated}, SubId:{si.SubscriptionId}, UserId:{si.UserId}, UserName:{si.UserName}");
                        Console.WriteLine();
                    }
                }
                if (key.Contains("login"))
                {
                    ServerEventsClient client = new ServerEventsClient("http://localhost:3337/", "test");
                    AuthenticateResponse aResp = client.Authenticate(new Authenticate() { UserName = "Admin", Password = "Jars@dm1n", RememberMe = true });
                    Console.Write($"Auth DisplayName:{aResp.DisplayName}, BT:{aResp.BearerToken}, Un:{aResp.UserName}, UserId:{aResp.UserId}");

                }
                if (key.Contains("req"))
                {

                }
                i++;
            }

        }

        static string[] GetHostUrl()
        {

            string[] retUrl = new string[] { "http://localhost:3022/", "" };
            //retUrl[0] = ConfigurationManager.AppSettings["RemoteServiceUrl"];
            //retUrl[1] = ConfigurationManager.AppSettings["RemoteServiceUrl_SSL"];
            retUrl[0] = AppSettings.GetString("RemoteServiceUrl");
            retUrl[1] = AppSettings.GetString("RemoteServiceUrl_SSL");
            return retUrl;
        }
    }
}
