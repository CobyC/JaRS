using JARS.Business.Bootstrap;
using JARS.Core;
using ServiceStack;
using System;

namespace JARS.SS.AuthHostIIS
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            JarsCore.Container = MEFBusinessLoader.Init();

            AppHost appHost = new AppHost();
            appHost.OnConnect = (evtSub, dictVal) => { Console.WriteLine($"OnConnect - Connection UserId:{evtSub.UserId} UserName: {evtSub.UserName} dictVals:{dictVal.Values}"); };
            appHost.OnSubscribe = (evtSub) => { Console.WriteLine($"OnSubscribe - sub:{evtSub.UserId}"); };
            appHost.OnPublish = (sub, res, msg) => { if (!msg.Contains("cmd.onHeartbeat")) Console.WriteLine($"Publish - DisplayName:{sub.DisplayName} Res.Status:{res.StatusCode} MsgLen:{msg}"); };
            appHost.OnUnsubscribe = (evtSub) => { Console.WriteLine($"OnUnsubscribe - sub:{evtSub.UserId}"); };
            appHost.LimitToAuthenticatedUser = true;
            //start the service
            appHost.Init();

            //resolve the events plugin loaded in the configuration.
            IServerEvents se = appHost.TryResolve<IServerEvents>();
            Logger.Info("Jars IIS App Started");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Logger.Error("Jars IIS App Error");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            Logger.Info("Jars IIS App Stopped");
        }
    }
}