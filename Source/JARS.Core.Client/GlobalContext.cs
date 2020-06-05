using JARS.Core.Authentication;
using JARS.Entities;
using JARS.SS.DTOs;
using ServiceStack;
using ServiceStack.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace JARS.Core.Client
{
    public class GlobalContext
    {
        public GlobalContext()
        { }

        /// <summary>
        /// Pass in the serviceClients to use for the global context,
        /// </summary>
        /// <param name="serviceClient"></param>
        /// <param name="authClient"></param>
        public GlobalContext(IJsonServiceClient serviceClient, IJsonServiceClient authClient)
        {
            _ServiceClient = serviceClient;
            _AuthClient = authClient;
            _Instance = this;
        }

        static object syncObj = new object();
        static GlobalContext _Instance;
        /// <summary>
        /// The current instance of this class.
        /// </summary>
        public static GlobalContext Instance
        {
            get
            {
                lock (syncObj)
                {
                    if (_Instance == null)
                        _Instance = new GlobalContext();

                    return _Instance;
                }
            }
            private set
            {
                _Instance = value;
            }
        }

        // This can be useful when sending notifications to other clients and this client receives the same notification,
        // this can then be used to determine if the same notification needs to be processed because it cam from this client.
        /// <summary>
        /// This GUID represents the instance of the client (application) while it is running.
        /// </summary>
        public Guid ClientInstanceGuid { get; set; }


        AppSettings _AppSettings;
        /// <summary>
        /// The app settings as in the app.config file in the &lt;appSettings&gt; section.
        /// </summary>
        AppSettings AppSettings
        {
            get
            {
                if (_AppSettings == null)
                    _AppSettings = new AppSettings();
                return _AppSettings;
            }
            set
            {
                _AppSettings = value;
            }
        }

        public string PlatformCode = "WinFrm";

        string _AppEnvironment;
        public string AppEnvironment
        {
            get
            {
                //adda property into the config files and transform via xslt
                //https://social.msdn.microsoft.com/Forums/vstudio/en-US/86f1543f-f2ec-4840-a2fd-2ecb24c42cc2/determine-current-runtime-configuration?forum=csharpgeneral
                string appState = AppSettings.GetString("AppState").ToString();

                if (appState == "DEBUG")
                    _AppEnvironment = $" Debug Environment";
                if (appState == "DEV")
                    _AppEnvironment = $" Development Environment";
                if (appState == "LIVE")
                    _AppEnvironment = $" Live Environment";
                if (appState == "RELEASE")
                    _AppEnvironment = $" Release Environment";

                return _AppEnvironment;
            }
        }

        public string ConnectionStatus { get; set; }
        /// <summary>
        /// Use this to send messages to the remote service (synchronization)
        /// If this value is empty the InitializeSSEClient(AuthenticationResult) needs to be called        
        /// </summary>
        public ServerEventsClient SSEventClient { get; private set; }

        /// <summary>
        /// Keeps the result of tha latest authentication request.
        /// </summary>
        AuthenticateResponse _AuthResponse { get; set; }

        public void SetAuthenticationResponse(AuthenticateResponse authResponse)
        {
            _AuthResponse = authResponse;
        }

        JarsUser _LoggedInUser;
        /// <summary>
        /// Holds the JarsUser that has logged on/in
        /// </summary>
        public JarsUser LoggedInUser
        {
            get
            {
                if (_LoggedInUser is null)
                {
                    JarsUserResponse resp = ServiceClient.Get(new GetJarsUser { EmailOrUserName = _AuthResponse.UserName, FetchEagerly = true });
                    _LoggedInUser = resp.UserAccount.ConvertTo<JarsUser>();
                }
                return _LoggedInUser;
            }
        }

        /// <summary>
        /// Call this method when the form is ready to send and receive commands from other clients.
        /// Give a chanel name that the client will listen to.
        /// </summary>        
        public string RegisterEventsServer()
        {
            try
            {
                if (SSEventClient == null)
                    ConnectionStatus = "NOT CONNECTED - NULL";
                else
                    ConnectionStatus = SSEventClient.Status;
            }
            catch (WebException wex)
            {
                ConnectionStatus = $"FAILED - {wex.Message}";
                Logger.Fatal(ConnectionStatus, wex);
                throw wex;
            }
            catch (Exception ex)
            {
                ConnectionStatus = $"FAILED - {ex.Message}";
                Logger.Fatal("InitializeSSClient Exception", ex);
                throw ex;
            }
            return ConnectionStatus;
        }

        public bool InitializeSSEClient()
        {
            bool connected = false;
            try
            {
                SSEventClient = CreateServerEventsClient("Jars");
                SSEventClient.EventStreamRequestFilter =
                    req =>
                    {
                        req.AddBearerToken(_AuthResponse.BearerToken);
                    };
                //also need to assign the credentials to the underlying ServiceClient.
                SSEventClient.ServiceClient.BearerToken = _AuthResponse.BearerToken;
                SSEventClient.Start();
                ConnectionStatus = SSEventClient.Status;
                connected = true;
            }
            catch (WebException wex)
            {
                ConnectionStatus = $"FAILED - {wex.Message}";
                Logger.Fatal(ConnectionStatus, wex);
                SSEventClient.Stop();
            }
            catch (Exception ex)
            {
                Logger.Fatal("InitializeSSClient Exception", ex);
                SSEventClient.Stop();
            }
            return connected;
        }

        public ServerEventsClient CreateServerEventsClient(params string[] channels)
        {
            ServicePointManager.DefaultConnectionLimit = 10;
            var client = new ServerEventsClient(GetHostUrl(), channels)
            {
                OnConnect = evt => connectMsg = evt,
                OnCommand = ServerEventCommands.Add,
                OnMessage = ServerEventMessages.Add,
                OnException = ServerEventErrors.Add,
                //OnReconnect = Reconnects.Add,
                //OnUpdate = Updates.Add,   
                ServiceClient = ServiceClient,
            };

            return client;
        }

        #region event messages
        ServerEventConnect connectMsg = null;
        List<ServiceStack.ServerEventMessage> _ServerEventMessages;
        public List<ServiceStack.ServerEventMessage> ServerEventMessages
        {
            get
            {
                if (_ServerEventMessages == null)
                    _ServerEventMessages = new List<ServiceStack.ServerEventMessage>();
                return _ServerEventMessages;
            }
            set => _ServerEventMessages = value;
        }

        List<ServiceStack.ServerEventMessage> _ServerEventCommands;
        public List<ServiceStack.ServerEventMessage> ServerEventCommands
        {
            get
            {
                if (_ServerEventCommands == null)
                    _ServerEventCommands = new List<ServiceStack.ServerEventMessage>();
                return _ServerEventCommands;
            }
            set => _ServerEventCommands = value;
        }

        List<Exception> _ServerEventErrors;
        public List<Exception> ServerEventErrors
        {
            get
            {
                if (_ServerEventErrors == null)
                    _ServerEventErrors = new List<Exception>();
                return _ServerEventErrors;
            }
            set => _ServerEventErrors = value;
        }
        #endregion

        /// <summary>
        /// Get the cached ServiceClient that is used to communicate with the Url defined in the config file.
        /// </summary>
        public IServiceClient CreateCachedServiceClient()
        {
            return new CachedServiceClient(ServiceClient as ServiceClientBase);
        }

        IJsonServiceClient _AuthClient;
        public IJsonServiceClient AuthClient
        {
            get
            {
                if (_AuthClient == null)
                {
                    _AuthClient = new JsonServiceClient(GetAuthHostUrl())
                    {
                        OnAuthenticationRequired = () =>
                        {

                            //Authenticator auth = new Authenticator(_AuthClient);
                            //auth.TryAuthenticateWithLastSavedProvider();
                        }
                    };
                }
                return _AuthClient;
            }
        }


        IJsonServiceClient _ServiceClient;
        public IJsonServiceClient ServiceClient
        {
            get
            {
                if (_ServiceClient == null)
                {
                    _ServiceClient = new JsonServiceClient(GetHostUrl())
                    {
                        OnAuthenticationRequired = () =>
                        {
                            Authenticator authr = new Authenticator(AuthClient);
                            _AuthResponse = authr.TryAuthenticateWithLastSavedProvider().Result;
                            _ServiceClient.BearerToken = _AuthResponse.BearerToken;
                            if (SSEventClient != null)
                                SSEventClient.EventStreamRequestFilter =
                                req =>
                                {
                                    req.AddBearerToken(_AuthResponse.BearerToken);
                                };
                            var x = _ServiceClient.Post(new Authenticate());
                        }
                    };
                }
                return _ServiceClient;
            }
        }


        string GetHostUrl()
        {
            bool useSSL = AppSettings.Get<bool>("UseSSL", true);
            string retUrl = "";
            if (useSSL)
                retUrl = AppSettings.GetString("RemoteServiceUrl_SSL");
            else
                retUrl = AppSettings.GetString("RemoteServiceUrl");
            return retUrl;
        }

        string GetAuthHostUrl()
        {
            bool useSSL = AppSettings.Get<bool>("UseSSL", true);
            string retUrl = "";
            if (useSSL)
                retUrl = AppSettings.GetString("RemoteAuthServiceUrl_SSL");
            else
                retUrl = AppSettings.GetString("RemoteAuthServiceUrl");
            return retUrl;
        }

    }
}
