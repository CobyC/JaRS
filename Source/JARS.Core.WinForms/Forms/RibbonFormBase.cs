using DevExpress.XtraBars.Ribbon;
using JARS.Core.Client;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.Interfaces.Rules;
using JARS.Core.Interfaces.Security;
using JARS.Core.Security;
using JARS.Entities;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Forms
{
    public class RibbonFormBase : RibbonForm
    {
        //%USERPROFILE%\JaRs\layouts.txt
        public DevExpress.Utils.Behaviors.BehaviorManager behaviorManager;
        //private System.ComponentModel.IContainer components;

        protected DevExpress.XtraBars.Alerter.AlertControl alertControl;

        [Import]
        protected IPluginFactory pluginFactory;
        protected DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidator;
        private System.ComponentModel.IContainer components;
        protected DevExpress.Utils.DefaultToolTipController defaultToolTipController;

        //[Import]
        //protected IJarsRulesEvaluator entityRulesEvaluator;
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

        public RibbonFormBase()
        {
            InitializeComponent();

            if (JarsCore.Container != null)
                JarsCore.Container.SatisfyImportsOnce(this);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RibbonFormBase));
            this.behaviorManager = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.alertControl = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.dxValidator = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.defaultToolTipController = new DevExpress.Utils.DefaultToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).BeginInit();
            this.SuspendLayout();
            // 
            // RibbonFormBase
            // 
            this.defaultToolTipController.SetAllowHtmlText(this, DevExpress.Utils.DefaultBoolean.Default);
            this.ClientSize = new System.Drawing.Size(748, 433);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RibbonFormBase";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RibbonFormBase_FormClosed);
            this.Load += new System.EventHandler(this.RibbonFormBase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).EndInit();
            this.ResumeLayout(false);

        }

        private void RibbonFormBase_Load(object sender, EventArgs e)
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

        private void AppendMetaData(Dictionary<string, string> meta)
        {
            if (meta == null)
                meta = new Dictionary<string, string>();

            if (!meta.ContainsKey("fromUser"))
                meta.Add("fromUser", SSEventClient.SubscriptionId);
            else
                meta["fromUser"] = SSEventClient.SubscriptionId;
        }

        /// <summary>
        /// This Method sets the controls enabled status.
        /// It will automatically look for controls with the ctrl_ prefix.
        /// </summary>
        /// <param name="parentControl">the parent control who's children will be enabled or disabled</param>
        /// <param name="state">The bool setting the Enabled property to true or false</param>
        public void SetControlEnabledState(Control parentControl, bool state)
        {
            foreach (Control ctrl in parentControl.Controls)
            {
                if (ctrl.Name.StartsWith("ctrl_"))
                    ctrl.Enabled = state;
                if (ctrl.Controls.Count > 0 && !ctrl.Name.StartsWith("ctrl_"))
                    SetControlEnabledState(ctrl, state);
            }
        }

        public virtual void OnMessageEvent(ServerEventMessage msg)
        {
            if (msg.Channel == Name)
                Logger.Debug($"OnMessageEvent() - {msg.Selector} - {msg.Channel}");
        }

        public virtual void OnCommandEvent(ServerEventMessage cm)
        {
            if (cm.Selector != "cmd.onJoin" && cm.Channel == Name)
            {
                Logger.Debug($"OnCommandEvent() - {Name} - {cm.Selector}");
            }
        }

        public virtual Action OnReconnect()
        {
            //Logger.Info($"OnReconnect() - {Name}");
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

        private void RibbonFormBase_FormClosed(object sender, FormClosedEventArgs e)
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
    }

}
