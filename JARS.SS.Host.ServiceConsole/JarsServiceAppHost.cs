using Funq;
using JARS.Core;
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
using System.Reflection;

namespace JARS.SS.Host.ServiceConsole
{
    /// <summary>
    /// This is the host (server side) for the JaRS service.
    /// </summary>
    public class JarsServiceAppHost : AppSelfHostBase
    {
        [Import]
        IJarsUserRepository _JarsUserRepository;

        //we only have to supply the assembly of one of the services, as any other services will be discovered.
        public JarsServiceAppHost(params Assembly[] assembliesWithServices) : base("Jars Service Host", assembliesWithServices)
        {
            if (JarsCore.Container != null)
                JarsCore.Container.SatisfyImportsOnce(this);

            if (AppSettings == null)
                AppSettings = new AppSettings();
        }

        public JarsServiceAppHost(IJarsUserRepository jarsUserRepository, params Assembly[] assembliesWithServices) : this(assembliesWithServices)
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

            Plugins.Add(new OpenApiFeature
            {
                //DisableAutoDtoInBodyParam = true,
                UseBasicSecurity = false,
                UseBearerSecurity = true,
                ApiDeclarationFilter = api =>
                {
                    var exludePaths = new[] {                       
                        api.Paths["/auth"],
                        api.Paths["/auth/{provider}"],
                        api.Paths["/assignroles"],
                        api.Paths["/unassignroles"],
                        api.Paths["/apikeys"],
                        api.Paths["/apikeys/{Environment}"],
                        api.Paths["/apikeys/regenerate"],
                        api.Paths["/apikeys/regenerate/{Environment}"],
                        api.Paths["/session-to-token"],
                        api.Paths["/access-token"],
                    };
                    foreach (var path in exludePaths)
                    {
                        path.Get = path.Put = path.Delete = path.Post = null;
                    }
                }
            }); //the open api feature

            //enable the app to use the server events functionality make it 
            //responsive to events between clients
            Plugins.Add(new ServerEventsFeature()
            {
                HeartbeatInterval = TimeSpan.FromSeconds(20),
                LimitToAuthenticatedUsers = LimitToAuthenticatedUser,
                //OnInit //Fired when the server receives the initial HTTP connection. This callback can be used to customize any HTTP Headers that are sent back to the client.
                //OnCreated = (sub,req)=> { sub.Meta["from"] = sub.SubscriptionId;},//Fired when the server IEventSubscription is created but before it becomes Connected.
                OnConnect = OnConnect, //Fired when the IEventSubscription is about to be connected. This callback can be used to modify the connection info arguments the client receives (client is sent onConnect)
                OnSubscribe = OnSubscribe, //Fired after the subscription is registered. This callback can be used to send any custom messages to the client
                OnUnsubscribe = OnUnsubscribe, //
                OnPublish = OnPublish //// Fired when message is published                
            });

            ////implement the custom auth provider
            if (LimitToAuthenticatedUser)
            {
                Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                    new IAuthProvider[] {
                        new ApiKeyAuthProvider(AppSettings),
                        new JwtAuthProvider(AppSettings), //<--when not using Microsoft.Identity.Client to get JWT token.
                        //new AadJwtAuthProvider(AppSettings)
                    }));

                //use the jars auth repository and cache client for now
                container.Register<IAuthRepository>(c => new OrmLiteAuthRepository(dbFactory));
                container.Register<ICacheClient>(new OrmLiteCacheClient() { DbFactory = dbFactory });
                container.Resolve<IAuthRepository>().InitSchema();
                container.Resolve<ICacheClient>().InitSchema();
            }
        }
    }
}
