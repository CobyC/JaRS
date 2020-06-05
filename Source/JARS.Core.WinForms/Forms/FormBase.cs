using DevExpress.XtraEditors;
using JARS.Core.Client;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.Interfaces.Security;
using JARS.Core.Rules.Utils;
using JARS.Core.Security;
using JARS.Entities;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace JARS.Core.WinForms.Forms
{
    /// <summary>
    /// Indicate the state of the form, helps with enabling the buttons on forms inheriting from BaseInfoForm class.
    /// </summary>
    public enum FormEditStates
    {
        Search,
        Adding,
        Editing,
        Remove,
        Browsing,//add edit delete
        NoRecords,
        BrowseOnly,//no edit available, just view
        BrowseAddOnly,//can only add and not edit again..
        BrowseAddEditOnly, 
        BrowseEditOnly,
        Refreshing,
    }

    public partial class FormBase : XtraForm
    {
        [Import]
        protected IPluginFactory pluginFactory;       

        //[Import]
        //protected EntityRulesEvaluator entityRulesEvaluator;
        //[Import]
        //protected IJarsProcessorFactory processorFactory;

        protected IJsonServiceClient ServiceClient { get { return GlobalContext.Instance.ServiceClient; } }

        protected ServerEventsClient SSEventClient { get { return GlobalContext.Instance.SSEventClient; } }

        public JarsUser LoggedInUser { get { return GlobalContext.Instance.LoggedInUser; } }

        public GlobalContext Context { get { return GlobalContext.Instance; } }


        IRolesAndPermissions _RolesAndOrPermissions;
        protected IRolesAndPermissions RolesAndOrPermissions
        {
            get
            {
                if (_RolesAndOrPermissions == null)
                {
                    _RolesAndOrPermissions = new RolesAndPermissions(Context.LoggedInUser);
                }
                return _RolesAndOrPermissions;
            }
            private set
            {
                _RolesAndOrPermissions = value;
            }
        }

        public FormBase()
        {
            InitializeComponent();

            if (JarsCore.Container != null)
                JarsCore.Container.SatisfyImportsOnce(this);
        }

        private void FormBase_Load(object sender, System.EventArgs e)
        {
            if (!this.DesignMode)
                if (SSEventClient != null)
                {
                    if (SSEventClient.Status == "Started")
                    {
                        SSEventClient.SubscribeToChannels(Name);
                        SSEventClient.OnConnect = (subscription) => AppendMetaData(subscription.Meta);
                        SSEventClient.OnCommand += new Action<ServerEventMessage>(OnCommandEvent);
                        SSEventClient.OnReconnect += new Action(OnReconnect());
                        SSEventClient.OnUpdate += new Action<ServerEventUpdate>(OnUpdateEvent);
                        SSEventClient.OnException += new Action<Exception>(OnExceptionEvent);
                        SSEventClient.OnMessage += new Action<ServerEventMessage>(OnMessageEvent);
                        SSEventClient.OnCommand += new Action<ServerEventMessage>(OnCommandEvent);
                    }
                }
        }

        private void FormBase_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (SSEventClient == null)
                return;

            if (SSEventClient.Status == "Started")
            {
                // AppGlobals.SSEventClient.UnsubscribeFromChannels(Name);
                SSEventClient.OnCommand -= new Action<ServerEventMessage>(OnCommandEvent);
                SSEventClient.OnReconnect -= new Action(OnReconnect());
                SSEventClient.OnUpdate -= new Action<ServerEventUpdate>(OnUpdateEvent);
                SSEventClient.OnException -= new Action<Exception>(OnExceptionEvent);
                SSEventClient.OnMessage -= new Action<ServerEventMessage>(OnMessageEvent);
                SSEventClient.OnCommand -= new Action<ServerEventMessage>(OnCommandEvent);
                SSEventClient.UnsubscribeFromChannels(Name);

            }
        }
        private void AppendMetaData(Dictionary<string, string> meta)
        {
            if (meta == null)
                meta = new Dictionary<string, string>();

            if (!meta.ContainsKey("fromUser"))
                meta.Add("fromUser", SSEventClient.SubscriptionId);
            else
                meta["fromUser"] = SSEventClient.SubscriptionId;
        }

        public virtual void OnMessageEvent(ServerEventMessage msg)
        {
            Logger.Debug($"OnMessageEvent() - {msg.Selector} - {msg.Channel}");
        }

        public virtual void OnCommandEvent(ServerEventMessage cm)
        {
            //if (cm.Selector != "cmd.onJoin" && cm.Channel == Name)
            //{
            Logger.Debug($"OnCommandEvent() - {Name} - {cm.Selector}");
            //}
        }

        public virtual Action OnReconnect()
        {
            Logger.Info($"OnReconnect() - {Name}");
            return () => { };
        }

        public virtual void OnExceptionEvent(Exception ex)
        {
            Logger.Error($"OnExceptionEvent() - {Name} - { ex.Message}", ex);

        }

        public virtual void OnUpdateEvent(ServerEventUpdate up)
        {
            if (up.Channel == Name)
            {
                Logger.Debug($"OnUpdateEvent() - {up.Channel} {up.UserId}");
            }
        }

    }
}
