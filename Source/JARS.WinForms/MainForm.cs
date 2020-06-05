using DevExpress.LookAndFeel;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraScheduler.Forms;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using JARS.Core;
using JARS.Core.Authentication;
using JARS.Core.Exceptions;
using JARS.Core.Extensions;
using JARS.Core.Interfaces.Attributes;
using JARS.Core.Interfaces.Attributes.UI;
using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.Interfaces.Processors;
using JARS.Core.Interfaces.Rules;
using JARS.Core.Interfaces.Rules.Attributes;
using JARS.Core.Rules;
using JARS.Core.Security;
using JARS.Core.Utils;
using JARS.Core.WinForms.Controls;
using JARS.Core.WinForms.Extensions;
using JARS.Core.WinForms.Forms;
using JARS.Core.WinForms.Interfaces.Plugins;
using JARS.Core.WinForms.Interfaces.Processors;
using JARS.Core.WinForms.Plugins;
using JARS.Entities;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JARS.WinForms
{

    public partial class MainForm : RibbonFormBase
    {
        Random rand = new Random(7);
        internal bool _IsFirstLoad;

        //we use MEF here to get all the plugins that will be loaded in the form..
        [ImportMany(typeof(IPluginBarItemToRibbon))] //this will only import the ...ToRibbon contracts, not all the IJarsPlugin contracts       
        IEnumerable<Lazy<IPluginBarItemToRibbon, IPluginToMainRibbonMetadata>> _RibbonPlugins;

        [ImportMany(typeof(IPluginWinForms))] //this will only import the ...ExternalPanel contracts.
        IEnumerable<Lazy<UserControlBasePlugin, IPluginToTabControlMetadata>> _ExternalEntityPlugins;

        [ImportMany(typeof(IPluginAsViewOption))] //this will only import the ...view options
        IEnumerable<Lazy<IPluginAsViewOption, IPluginAsViewOptionMetadata>> _ViewOptionPlugins;

        [ImportMany(typeof(IPluginAsBehaviour))] //this will only import the ...behaviour plugins
        IEnumerable<Lazy<IPluginAsBehaviour, IPluginAsBehaviourMetadata>> _BehaviourPlugins;

        [ImportMany(typeof(IProcessor))]//this will hold all the processors available in the system.
        IEnumerable<Lazy<IProcessor, IProcessorMetadata>> _JarsProcessors;

        private IPluginAsViewOption _ActiveViewOption;
        /// <summary>
        /// Holds the active view option that can be used to for view manipulation throughout the form.
        /// </summary>
        public IPluginAsViewOption ActiveViewOption
        {
            get { return _ActiveViewOption; }

            set { _ActiveViewOption = value; }
        }


        //public string[] defaultPermissions { get => new[] { "Admin", "Editor" }; }
        //public string[] defaultRoles { get => new[] { "Admin", "Editor" }; }
        public string[] DefaultRoles { get => new[] { JarsRoles.Admin, JarsRoles.User, JarsRoles.Manager, JarsRoles.PowerUser }; }
        public string[] DefaultPermissions { get => null; }

        /// <summary>
        /// list for conditions placed on entities
        /// </summary>
        public List<IJarsRule> JarsRules { get; set; }

        Image resourceInfoImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/support/info_32x32.png");

        public MainForm(
            IEnumerable<Lazy<IPluginBarItemToRibbon, IPluginToMainRibbonMetadata>> ribbonPlugins,
            IEnumerable<Lazy<UserControlBasePlugin, IPluginToTabControlMetadata>> externalEntityPlugins,
            IEnumerable<Lazy<IPluginAsViewOption, IPluginAsViewOptionMetadata>> viewOptionPlugins,
            IEnumerable<Lazy<IPluginAsBehaviour, IPluginAsBehaviourMetadata>> behaviourPlugins,
            IEnumerable<Lazy<IProcessor, IProcessorMetadata>> jarsProcessors)
            : base()
        {
            _RibbonPlugins = ribbonPlugins;
            _ExternalEntityPlugins = externalEntityPlugins;
            _ViewOptionPlugins = viewOptionPlugins;
            _BehaviourPlugins = behaviourPlugins;
            _JarsProcessors = jarsProcessors;
        }

        public MainForm()
        {
            _IsFirstLoad = true;
            InitializeComponent();
            barWorkingProgress.Visibility = BarItemVisibility.Always;

            SkinHelper.InitSkinGallery(rgbiSkins, true);
            SplashScreenManager.Default.SendCommand(null, "Loading Skins..");
            //load the skin
            JarsSetting skinSet = LoggedInUser.Settings.FirstOrDefault(s => s.Platform == Context.PlatformCode && s.PartName == $"{this.Name}_Skin");
            if (skinSet != null)
                UserLookAndFeel.Default.SkinName = new string(skinSet.SettingData.Select(Convert.ToChar).ToArray());

            //AppointmentCustomFieldMapping entityMap = new AppointmentCustomFieldMapping("ENTITY", "");
            //schedulerDataStorage.Appointments.CustomFieldMappings.Add(entityMap);
            //AppointmentCustomFieldMapping stateMap = new AppointmentCustomFieldMapping("STATE", "");
            //schedulerDataStorage.Appointments.CustomFieldMappings.Add(stateMap);

            SetDefaultAppointmentStatusesAndLabels();

            #region BarItemPlugins
            SplashScreenManager.Default.SendCommand(null, "Loading Form Plugins..");
            //load the serverEventClient plugins
            ribbonControl.SuspendLayout();
            _RibbonPlugins = _RibbonPlugins.OrderBy(r => r.Metadata.PluginText);
            IList<Lazy<IPluginBarItemToRibbon, IPluginToMainRibbonMetadata>> remRibPlugins = new List<Lazy<IPluginBarItemToRibbon, IPluginToMainRibbonMetadata>>();
            foreach (var pluginItem in _RibbonPlugins)
            {
                try
                {
                    string[] reqRoles = null;
                    string[] reqPerms = null;
                    if (pluginItem.Value is IPluginRequiresPermission pi)
                    {
                        reqRoles = pi.RequiredRoles;
                        reqPerms = pi.RequiredPermissions;
                    }

                    RolesAndOrPermissions.ExecuteAction(reqRoles, reqPerms, () =>
                    {
                        IPluginToMainRibbonMetadata pluginMetaData = pluginItem.Metadata as IPluginToMainRibbonMetadata;
                        LinkPluginWithControls(pluginItem.Value);
                        AddItemToRibbon(pluginMetaData, pluginItem.Value.BarItem);
                    });

                }
                catch (UserRoleOrPermissionException pEx)
                {
                    remRibPlugins.Add(pluginItem);
                    Logger.Debug("Plugin not loaded.", pEx);
                }
                catch (CompositionException cx)
                {
                    if (cx.RootCauses.Count > 0)
                    {
                        Exception rcause = cx.RootCauses.FirstOrDefault(rc => rc.InnerException is SecurityException);
                        if (rcause != null)
                            remRibPlugins.Add(pluginItem);
                    }
                }

            }
            if (remRibPlugins.Count > 0)
                _RibbonPlugins = _RibbonPlugins.Except(remRibPlugins);
            ribbonControl.ResumeLayout();
            #endregion

            #region ExternalDataPlugins
            SplashScreenManager.Default.SendCommand(null, "Loading External Data Plugins..");
            tabControlExternalEntities.SuspendLayout();
            _ExternalEntityPlugins = _ExternalEntityPlugins.OrderBy(x => x.Metadata.PositionIndex);
            IList<Lazy<UserControlBasePlugin, IPluginToTabControlMetadata>> remExtPlugins = new List<Lazy<UserControlBasePlugin, IPluginToTabControlMetadata>>();
            foreach (var pluginItem in _ExternalEntityPlugins)
            {
                try
                {
                    string[] reqRoles = null;
                    string[] reqPerms = null;
                    if (pluginItem.Value is IPluginRequiresPermission pi)
                    {
                        reqRoles = pi.RequiredRoles;
                        reqPerms = pi.RequiredPermissions;
                    }

                    RolesAndOrPermissions.ExecuteAction(reqRoles, reqPerms, () =>
                    {
                        IPluginToTabControlMetadata pluginMetaData = pluginItem.Metadata as IPluginToTabControlMetadata;
                        UserControlBasePlugin plugin = pluginItem.Value as UserControlBasePlugin;
                        LinkPluginWithControls(pluginItem.Value);
                        AddTabPageToTabControl(pluginMetaData, plugin);
                    });
                }
                catch (UserRoleOrPermissionException pEx)
                {
                    remExtPlugins.Add(pluginItem);
                    Logger.Debug("Plugin not loaded.", pEx);
                }
                catch (CompositionException cx)
                {
                    if (cx.RootCauses.Count > 0)
                    {
                        Exception rcause = cx.RootCauses.FirstOrDefault(rc => rc.InnerException is SecurityException);
                        if (rcause != null)
                            remExtPlugins.Add(pluginItem);
                    }
                }
            }
            if (remExtPlugins.Count > 0)
                _ExternalEntityPlugins = _ExternalEntityPlugins.Except(remExtPlugins);

            tabControlExternalEntities.SelectedTabPageIndex = 0;
            tabControlExternalEntities.ResumeLayout();
            #endregion

            #region ViewOptionPlugins
            SplashScreenManager.Default.SendCommand(null, "Loading View Option Plugins..");
            ribbonControl.SuspendLayout();
            _ViewOptionPlugins = _ViewOptionPlugins.OrderBy(v => v.Metadata.PluginText);
            IList<Lazy<IPluginAsViewOption, IPluginAsViewOptionMetadata>> remViewOptPlugins = new List<Lazy<IPluginAsViewOption, IPluginAsViewOptionMetadata>>();
            foreach (var pluginItem in _ViewOptionPlugins)
            {
                try
                {
                    string[] reqRoles = null;
                    string[] reqPerms = null;
                    if (pluginItem.Value is IPluginRequiresPermission pi)
                    {
                        reqRoles = pi.RequiredRoles;
                        reqPerms = pi.RequiredPermissions;
                    }

                    RolesAndOrPermissions.ExecuteAction(reqRoles, reqPerms, () =>
                    {
                        IPluginAsViewOptionMetadata pluginMetaData = pluginItem.Metadata as IPluginAsViewOptionMetadata;
                        LinkPluginWithControls(pluginItem.Value);
                        AddItemToRibbon(pluginMetaData, pluginItem.Value.BarCheckItem);
                        //link option view with the option view event
                        pluginItem.Value.Selected += ViewOptionPlugin_OnSelect;
                        pluginItem.Value.Unselected += ViewOptionPlugin_OnUnselect;
                    });

                }
                catch (UserRoleOrPermissionException pEx)
                {
                    remViewOptPlugins.Add(pluginItem);
                    Logger.Debug("Plugin not loaded.", pEx);
                }
                catch (CompositionException cx)
                {
                    if (cx.RootCauses.Count > 0)
                    {
                        Exception rcause = cx.RootCauses.FirstOrDefault(rc => rc.InnerException is SecurityException);
                        if (rcause != null)
                            remViewOptPlugins.Add(pluginItem);
                    }
                }
            }
            //remove the plugins that failed to load from above process
            if (remViewOptPlugins.Count > 0)
                _ViewOptionPlugins = _ViewOptionPlugins.Except(remViewOptPlugins);
            ribbonControl.ResumeLayout();
            #endregion

            #region BehaviourPlugins
            SplashScreenManager.Default.SendCommand(null, "Loading Behaviour Plugins..");
            ribbonControl.SuspendLayout();
            _BehaviourPlugins = _BehaviourPlugins.OrderBy(v => v.Metadata.PluginText);
            IList<Lazy<IPluginAsBehaviour, IPluginAsBehaviourMetadata>> rembehaviourPlugins = new List<Lazy<IPluginAsBehaviour, IPluginAsBehaviourMetadata>>();
            foreach (var pluginItem in _BehaviourPlugins)
            {
                try
                {
                    string[] reqRoles = null;
                    string[] reqPerms = null;
                    if (pluginItem.Value is IPluginRequiresPermission pi)
                    {
                        reqRoles = pi.RequiredRoles;
                        reqPerms = pi.RequiredPermissions;
                    }

                    RolesAndOrPermissions.ExecuteAction(reqRoles, reqPerms, () =>
                    {
                        IPluginAsBehaviourMetadata pluginMetaData = pluginItem.Metadata as IPluginAsBehaviourMetadata;
                        LinkPluginWithControls(pluginItem.Value);
                        AddItemToRibbon(pluginMetaData, pluginItem.Value.BarItem);
                        //link behaviour with the bar  event
                        pluginItem.Value.OnActivate += BehaviourPlugin_OnActivated;
                        pluginItem.Value.OnDeactivate += BehaviourPlugin_OnDeactivated;
                    });

                }
                catch (UserRoleOrPermissionException pEx)
                {
                    rembehaviourPlugins.Add(pluginItem);
                    Logger.Debug("Plugin not loaded.", pEx);
                }
                catch (CompositionException cx)
                {
                    if (cx.RootCauses.Count > 0)
                    {
                        Exception rcause = cx.RootCauses.FirstOrDefault(rc => rc.InnerException is SecurityException);
                        if (rcause != null)
                            rembehaviourPlugins.Add(pluginItem);
                    }
                }
            }
            //remove the plugins that failed to load from above process
            if (rembehaviourPlugins.Count > 0)
                _BehaviourPlugins = _BehaviourPlugins.Except(rembehaviourPlugins);
            ribbonControl.ResumeLayout();
            #endregion

            #region WorkspaceLayout
            SplashScreenManager.Default.SendCommand(null, "Loading Workspace..");
            // load the workspace
            JarsSetting resSet = Context.LoggedInUser.Settings.FirstOrDefault(s => s.Platform == Context.PlatformCode && s.PartName == Name);
            if (resSet != null)
            {
                using (MemoryStream msUncompressed = new MemoryStream())
                {
                    using (MemoryStream msCompressed = new MemoryStream(resSet.SettingData))
                    using (GZipStream gzDecomp = new GZipStream(msCompressed, CompressionMode.Decompress))
                    {
                        msCompressed.Position = 0;
                        gzDecomp.CopyTo(msUncompressed);
                    }
                    msUncompressed.Position = 0;
                    workspaceManagerMain.LoadWorkspace(resSet.PartName, msUncompressed, false);
                    workspaceManagerMain.ApplyWorkspace(resSet.PartName);
                }
            }
            #endregion

            #region ResourceViewSettings
            SplashScreenManager.Default.SendCommand(null, "Loading Resource Setting..");
            //set the way resources are shown 
            JarsSetting resGrpSet = Context.LoggedInUser.Settings.FirstOrDefault(s => s.Platform == Context.PlatformCode && s.PartName == ckBtnGroupResources.Name);
            if (resGrpSet != null)
                ckBtnGroupResources.Checked = Convert.ToBoolean(resGrpSet.SettingData[0]);
            #endregion

            barWorkingProgress.Visibility = BarItemVisibility.Never;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {


            Text = $"{Context.AppEnvironment} + {Context.LoggedInUser.UserName}";
            barWorkingProgress.Visibility = BarItemVisibility.Always;
            SplashScreenManager.Default.SendCommand(null, "Attaching Processors");

            foreach (var processor in _JarsProcessors)
            {
                #region AttachProcessorsToControls
                if (processor.Value is IPluginWinForms plugin)
                    LinkPluginWithControls(plugin);
                #endregion
            }

            SplashScreenManager.Default.SendCommand(null, "Loading Data");

            if (!_isEntityRulesLoading)
                await LoadEntityRules();

            if (!_isResourcesLoading)
                await LoadResourcesAsync(ckBtnGroupResources.Checked, _IsFirstLoad);

            if (!_isLoadOrRefreshProcessorsDataLoading)
                await LoadOrRefreshProcessorsDataAsync();

            //await LoadAppointmentsAsync(true);
            //build the resource tree
            BuildResourceTree(ckBtnGroupResources.Checked, _IsFirstLoad);
            resourceTree.Refresh();

            //execute the plugins that are set to execute automatically
            SplashScreenManager.Default.SendCommand(null, "Loading External Plugins");
            if (!_isInitExecuteAndLoadStateForExternalPluginsLoading)
                await InitExecuteAndLoadStateForExternalPlugins();

            //load plugin settings
            SplashScreenManager.Default.SendCommand(null, "Loading Ribbon Plugins");
            if (!_isLoadingRibbonPlugins)
                await LoadingRibbonPlugins();


            //load view option plugins
            SplashScreenManager.Default.SendCommand(null, "Loading View Option Plugins");
            foreach (var plugin in _ViewOptionPlugins)
            {
                IPluginAsViewOption plgn = plugin.Value;

                if (plgn is IPluginWithInitialize)
                    (plgn as IPluginWithInitialize).Init();

                if (plgn is IPluginWithStateInfo)
                    LoadPluginState(plgn as IPluginWithStateInfo);

                if (plgn is IPluginWithStateInfoAsync)
                    await LoadPluginStateAsync(plgn as IPluginWithStateInfoAsync);

                if (plgn.BarCheckItem.Checked == true)
                    ActiveViewOption = plgn;
            }
            if (ActiveViewOption == null)
            {
                ActiveViewOption = _ViewOptionPlugins.FirstOrDefault(p => p.Metadata.ApplyToEntityInterfaceType == typeof(IEntityWithStatusLabels)).Value;//.PluginText == "Default").Value;
                ActiveViewOption.BarCheckItem.Checked = true;
            }

            SplashScreenManager.Default.SendCommand(null, "Loading Behavior Plugins");
            foreach (var plugin in _BehaviourPlugins)
            {
                IPluginAsBehaviour plgn = plugin.Value;

                if (plgn is IPluginWithStateInfo)
                    LoadPluginState(plgn);

                if (plgn is IPluginWithStateInfoAsync plgnAsync)
                    await LoadPluginStateAsync(plgnAsync);
            }

            //mark the resource tree resources
            JarsSetting resTreeSet = LoggedInUser.Settings.FirstOrDefault(s => s.Platform == Context.PlatformCode && s.PartName == resourceTree.Name);
            if (resTreeSet != null)
            {
                using (MemoryStream msDecompressed = new MemoryStream())
                {
                    using (MemoryStream msCompressed = new MemoryStream(resTreeSet.SettingData))
                    using (GZipStream gzDecomp = new GZipStream(msCompressed, CompressionMode.Decompress))
                    {
                        msCompressed.Position = 0;
                        gzDecomp.CopyTo(msDecompressed);
                    }
                    BinaryFormatter formatter = new BinaryFormatter();
                    msDecompressed.Position = 0;
                    if (formatter.Deserialize(msDecompressed) is List<string> checkedList)
                        LoadResourceTreeLayout(checkedList);
                }
            }
            //ready
            ckBtnGroupResources.ImageOptions.Image = ckBtnGroupResources.Checked ? ckBtnGroupResources.AppearancePressed.Image : ckBtnGroupResources.Appearance.Image;

            schedulerControl.Refresh();
            schedulerControl.Start = DateTime.Now;

            if (Context.ConnectionStatus == "Started")
            {
                barBtnStatusInfo.ImageIndex = 15;
                barBtnStatusInfo.Caption = "Connected";
                SplashScreenManager.Default.SendCommand(null, "Subscribing to Channels");
                foreach (var processor in _JarsProcessors)
                    SSEventClient.SubscribeToChannels(processor.Metadata.LinkedEntityType.Name);
            }
            else
            {
                barBtnStatusInfo.ImageIndex = 17;
                barBtnStatusInfo.Caption = "Not Connected";
            }
            barWorkingProgress.Visibility = BarItemVisibility.Never;
            SplashScreenManager.CloseForm();
            _IsFirstLoad = false;
            SetAppointmentCreationOption();
        }

        bool _isLoadingRibbonPlugins;
        private async Task LoadingRibbonPlugins()
        {
            try
            {
                _isLoadingRibbonPlugins = true;
                foreach (var plugin in _RibbonPlugins)
                {
                    IPluginBarItemToRibbon plgn = plugin.Value;

                    if (plgn is IPluginWithInitialize pgnInit)
                        pgnInit.Init();

                    if (plgn is IPluginWithStateInfo pgnState)
                        LoadPluginState(pgnState);

                    if (plgn is IPluginWithStateInfoAsync pgnStateAsync)
                        await LoadPluginStateAsync(pgnStateAsync);
                }
            }
            finally
            {
                _isLoadingRibbonPlugins = false;
            }
        }

        private void ViewOptionPlugin_OnUnselect(object sender, EventArgs e)
        {
            try
            {
                barWorkingProgress.Visibility = BarItemVisibility.Always;
                schedulerControl.BeginUpdate();
                if (sender is IPluginAsViewOption)
                {
                    IPluginAsViewOption vop = sender as IPluginAsViewOption;
                    UnCheckViewOptions(vop, false);
                    if (vop.SchedulerLabels == null)
                        return;

                    SchedulerDataStorage _sDataStorage = schedulerControl.DataStorage as SchedulerDataStorage;

                    AppointmentLabelDataStorage lStore = _sDataStorage.Labels;
                    foreach (ApptLabel label in vop.SchedulerLabels)
                    {
                        //string id = $"{label.ViewName}_{label.SortIndex}_{label.Id}";
                        string id = $"{label.Id}";
                        AppointmentLabel lbl = lStore.Items.Find(l => l.Id.ToString() == id);
                        if (lbl != null && !lbl.Id.ToString().Contains("DEFAULT"))
                        {
                            lStore.Remove(lbl);
                            //lbl.Dispose();
                        }
                    }

                    AppointmentStatusDataStorage sStore = _sDataStorage.Statuses;
                    foreach (ApptStatus status in vop.SchedulerStatuses)
                    {
                        //string id = $"{status.ViewName}_{status.SortIndex}_{status.Id}";
                        string id = $"{status.Id}";
                        AppointmentStatus sts = sStore.Items.Find(s => s.Id.ToString() == id);
                        if (sts != null && !sts.Id.ToString().Contains("DEFAULT"))
                        {
                            sStore.Remove(sts);
                            //sts.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
#if DEBUG
                throw ex;
#endif
            }
            finally
            {
                schedulerControl.EndUpdate();
                schedulerControl.Refresh();
                barWorkingProgress.Visibility = BarItemVisibility.Never;
            }
        }

        private void ViewOptionPlugin_OnSelect(object sender, EventArgs e)
        {
            try
            {
                barWorkingProgress.Visibility = BarItemVisibility.Always;
                schedulerControl.BeginUpdate();
                if (sender is IPluginAsViewOption)
                {
                    IPluginAsViewOption vop = sender as IPluginAsViewOption;
                    ActiveViewOption = vop;
                    UnCheckViewOptions(vop, true);

                    SchedulerDataStorage _sDataStorage = schedulerControl.DataStorage as SchedulerDataStorage;

                    AppointmentLabelDataStorage lStore = _sDataStorage.Labels;
                    foreach (ApptLabel label in vop.SchedulerLabels)
                    {
                        //string id = label.AutoUId;//$"{label.ViewType}_{label.SortIndex}_{label.ID}";
                        if (lStore.Items.Find(l => l.Id.ToString() == label.Id.ToString()) == null)
                        {
                            string displayName = $"{label.LabelName}({label.LabelCriteria})";
                            AppointmentLabel lbl = lStore.Items.CreateNewLabel(label.Id, displayName);
                            lbl.Color = Color.FromArgb(label.ColourRGB);
                            lStore.Add(lbl);
                        }

                    }

                    AppointmentStatusDataStorage sStore = _sDataStorage.Statuses;//.Appointments.Statuses;
                    foreach (ApptStatus status in vop.SchedulerStatuses)
                    {
                        if (sStore.Items.Find(s => s.Id.ToString() == status.Id.ToString()) == null)
                        {
                            string displayName = $"{status.StatusName}({status.StatusCriteria})";
                            AppointmentStatus sts = sStore.Items.CreateNewStatus(status.Id, displayName);
                            sts.SetBrush(new SolidBrush(Color.FromArgb(status.ColourRGB)));
                            sts.Type = AppointmentStatusType.Custom;
                            sStore.Add(sts);
                        }
                    }

                    //look fo any control that might implement the IPluginToViewOption interface on the external data tab control
                    UpdateActiveViewLinkedControls(tabControlExternalEntities, ActiveViewOption);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
#if DEBUG
                throw ex;
#endif
            }
            finally
            {
                schedulerControl.EndUpdate();
                schedulerControl.Refresh();
                barWorkingProgress.Visibility = BarItemVisibility.Never;
            }
        }

        void UpdateActiveViewLinkedControls(Control parentControl, IPluginAsViewOption activeViewOption)
        {
            if (parentControl is IPluginToViewOption)
            {
                (parentControl as IPluginToViewOption).SetViewOptionPlugin(activeViewOption);
            }
            foreach (Control childControl in parentControl.Controls)
            {
                UpdateActiveViewLinkedControls(childControl, activeViewOption);
            }
            //if (tabControlExternalEntities.SelectedTabPage != null)
            //    if (tabControlExternalEntities.SelectedTabPage.Controls.Count > 0)
            //        foreach (var childControl in tabControlExternalEntities.SelectedTabPage.Controls)
            //        {
            //            if (childControl is IPluginToViewOption)
            //            {
            //                (childControl as IPluginToViewOption).SetViewOptionPlugin(ActiveViewOption);
            //            }
            //        }
        }

        private void BehaviourPlugin_OnActivated(object sender, EventArgs e)
        {
            try
            {
                if (sender is IPluginAsBehaviour)
                {
                    IPluginAsBehaviour bp = sender as IPluginAsBehaviour;
                    bp.Activate();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
#if DEBUG
                throw ex;
#endif
            }
            finally
            {

            }
        }

        private void BehaviourPlugin_OnDeactivated(object sender, EventArgs e)
        {
            try
            {
                if (sender is IPluginAsBehaviour)
                {
                    IPluginAsBehaviour bp = sender as IPluginAsBehaviour;
                    bp.Deactivate();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
#if DEBUG
                throw ex;
#endif
            }
            finally
            {

            }
        }


        /// <summary>
        /// Uncheck the bar items in the view options group.
        /// </summary>
        /// <param name="sender">the ViewOption plugin that triggered the event</param>
        /// <param name="ignoreSender">Indicate if the sending View option also needs to be unchecked. 
        /// if set to true the sender will not be unchecked.
        /// if set to false the sender will also be unchecked.
        /// </param>
        internal void UnCheckViewOptions(IPluginAsViewOption sender, bool ignoreSender)
        {
            if (!sender.BarCheckItem.Checked)
                return;
            RibbonPageGroup viewOptPgGrp = ribbonControl.GetGroupByName(sender.BarCheckItem.Tag.ToString());
            if (viewOptPgGrp != null)
            //uncheck all the other items.
            {
                foreach (var item in viewOptPgGrp.ItemLinks)
                {
                    if (item is BarCheckItemLink)
                    {
                        BarCheckItem baritem = ((BarCheckItemLink)item).Item as BarCheckItem;
                        if (ignoreSender)
                        {
                            if (baritem != sender.BarCheckItem)
                                baritem.Checked = false;
                        }
                        else
                            baritem.Checked = false;
                    }
                }
            }

        }

        /// <summary>
        /// Links the plugin to the main application controls.
        /// This will link according to the interfaces implemented by the Plugin
        /// </summary>
        /// <param name="pluginValue">the plugin to be linked</param>
        private void LinkPluginWithControls(IPluginWinForms pluginValue)
        {
            if (pluginValue is IPluginToSchedulerControl)
                ((IPluginToSchedulerControl)pluginValue).schedulerControl = schedulerControl;
            if (pluginValue is IPluginToSchedulerStorage)
                ((IPluginToSchedulerStorage)pluginValue).schedulerDataStorage = schedulerDataStorage;
            if (pluginValue is IPluginToResourceTree)
                ((IPluginToResourceTree)pluginValue).resourceTree = resourceTree;
            if (pluginValue is IPluginToDateNavigator)
                ((IPluginToDateNavigator)pluginValue).dateNavigator = dateNavigator;
            if (pluginValue is IPluginToMainRibbon)
                ((IPluginToMainRibbon)pluginValue).MainRibbon = ribbonControl;
            if (pluginValue is IPluginToExternalTabControl)
                ((IPluginToExternalTabControl)pluginValue).ExternalTabControl = tabControlExternalEntities;
        }

        /// <summary>
        /// Creates the ribbon bar item using the meta data added to the plugin.
        /// </summary>
        /// <param name="pluginMetaData">meta data used to create item in the correct categories and groups</param>
        /// <param name="barItem"></param>
        void AddItemToRibbon(IPluginToMainRibbonMetadata pluginMetaData, BarItem barItem)
        {
            //now we need to determine if where the item will be added
            string categoryName = pluginMetaData.Category.Trim().Replace(" ", "");
            string catPageName = pluginMetaData.CategoryPage.Trim().Replace(" ", "");
            string groupName = pluginMetaData.PageGroup.Trim().Replace(" ", "");

            //get or create the category
            RibbonPageCategory category = null;
            if (pluginMetaData.Category == "")
            {
                category = Ribbon.DefaultPageCategory;
            }
            else
            {
                category = Ribbon.PageCategories.GetCategoryByName($"cat{categoryName}");
                if (category == null)
                {
                    category = new RibbonPageCategory(pluginMetaData.Category, Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)), true)
                    {
                        Name = $"cat{categoryName}"
                    };
                    Ribbon.PageCategories.Add(category);
                }
            }

            //get or create the page in the category
            RibbonPage page = category.Pages.GetPageByName($"catPage{catPageName}{categoryName}");
            if (page == null)
            {
                page = new RibbonPage(pluginMetaData.CategoryPage)
                {
                    Name = $"catPage{catPageName}{categoryName}"
                };
                category.Pages.Add(page);
            }

            //get or create the group in the page
            RibbonPageGroup group = page.GetGroupByName($"pGroup{groupName}{catPageName}{categoryName}");
            if (group == null)
            {
                group = new RibbonPageGroup(pluginMetaData.PageGroup)
                {
                    Name = $"pGroup{groupName}{catPageName}{categoryName}"
                };
                page.Groups.Add(group);
            }
            barItem.Tag = group.Name;
            group.ItemLinks.Add(barItem);
        }

        private void AddTabPageToTabControl(IPluginToTabControlMetadata pluginMetadata, UserControlBase pluginControl)
        {
            XtraTabPage newTabPage = new XtraTabPage
            {
                Text = pluginMetadata.PluginText
            };

            pluginControl.Dock = DockStyle.Fill;
            newTabPage.Controls.Add(pluginControl);
            tabControlExternalEntities.TabPages.Add(newTabPage);
            tabControlExternalEntities.TabPages.Move(pluginMetadata.PositionIndex, newTabPage);
        }

        void LoadResourceTreeLayout(List<string> visibleResourceIdList)
        {
            resourceTree.BeginUpdate();

            TreeListNode[] opNodes = resourceTree.FindNodes(n => n.Tag is JarsResource);
            //find all nodes where the resource id matches the id from the saved list
            IEnumerable<TreeListNode> activeNodes = opNodes.Where(n => visibleResourceIdList.Contains(((JarsResource)n.Tag).Id.ToString()));

            foreach (TreeListNode node in activeNodes)
            {
                node.Checked = true;

                DevExpress.XtraScheduler.Resource res = schedulerDataStorage.Resources.Items.Find(r => r.Id.ToString() == ((JarsResource)node.Tag).Id.ToString());
                if (res != null)
                    res.Visible = true;

                //only expand when the parent node is a group node
                if (node.ParentNode != null)
                {
                    node.ParentNode.Checked = true;
                    if (!node.ParentNode.Expanded)
                        node.ParentNode.Expand();
                }
            }
            resourceTree.EndUpdate();
            resourceTree.Refresh();
        }

        /// <summary>
        /// This method triggers when a command has been received from the ServiceStack ServiceEvent
        /// </summary>
        /// <param name="seMsg"></param>
        public override void OnCommandEvent(ServiceStack.ServerEventMessage seMsg)
        {
            if (seMsg.Selector == "cmd.onJoin")
            {
                lblSiInfo.Caption = "Connected";
                lblSiInfo.ImageIndex = 15;
            }
            base.OnCommandEvent(seMsg);
        }

        /// <summary>
        /// This action is fired when the client has connected to the server
        /// </summary>
        /// <returns></returns>
        public override Action OnReconnect()
        {
            lblSiInfo.Caption = "Connected";
            lblSiInfo.ImageIndex = 15;
            lblSiInfo.Visibility = BarItemVisibility.Always;
            return base.OnReconnect();
        }

        /// <summary>
        /// This method is raised when there has been an error communicating with the server or the ServiceStack ServiceEvent.
        /// </summary>
        /// <param name="ex">The Exception being thrown</param>
        public override void OnExceptionEvent(Exception ex)
        {
            Type t = ex.GetType();
            if (ex is IOException)
            {
                lblSiInfo.Caption = ex.Message;
                lblSiInfo.ImageIndex = 18;
                barBtnStatusInfo.Caption = "DISCONNECTED";
                barBtnStatusInfo.ImageIndex = 17;
            }

            if (ex is WebException)
            {
                lblSiInfo.Caption = ex.Message;
                lblSiInfo.ImageIndex = 18;
                //try and re-register to the main channel
                barBtnStatusInfo.Caption = Context.RegisterEventsServer();
                if (barBtnStatusInfo.Caption == "SUCCESS")
                    barBtnStatusInfo.ImageIndex = 15;
            }
            //System.TimeoutException
            base.OnExceptionEvent(ex);
        }

        /// <summary>        
        /// This method fires when a message has been received from the ServiceStack ServiceEvent
        /// This is the most commonly used method for communication between clients and services.
        /// </summary>
        /// <param name="seMsg"></param>
        public override void OnMessageEvent(ServerEventMessage seMsg)
        {
            base.OnMessageEvent(seMsg);
            try
            {
                //if (msg.Meta["from"] == Context.SSEventClient.SubscriptionId)
                if (seMsg.Selector == SelectorTypes.delete)
                {
                    //JarsSyncDeleteEvent<IEntityBase> syncEvent = seMsg.Json.FromJson<JarsSyncDeleteEvent<IEntityBase>>();
                    IProcessor processor = GetProcessorForEntity<IProcessorForEventServiceCommandReceived>(seMsg.Channel);
                    if (processor != null)
                        (processor as IProcessorForEventServiceCommandReceived).OnDeleteCommandReceived(schedulerControl, seMsg);
                }
                else if (seMsg.Selector == SelectorTypes.store)
                {
                    if (seMsg.Channel == nameof(JarsRule))
                    {
                        ServerEventMessageData sevm = seMsg.Json.FromJson<ServerEventMessageData>();
                        var eventList = sevm.jsonDataString.FromJson<List<JarsRule>>();
                        foreach (var entityCon in eventList)
                        {
                            //EntityRule entityCon = seMsg.Json.FromJson<EntityRule>();
                            IJarsRule findCond = JarsRules.FirstOrDefault(c => (c as IJarsRule).Id == entityCon.Id) as IJarsRule;
                            if (findCond != null)
                            {   //replace/update
                                JarsRules.Remove(findCond);
                                JarsRules.Add(entityCon);
                            }
                            else //add
                                JarsRules.Add(entityCon);
                        }
                    }
                    else
                    {
                        //JarsSyncStoreEvent<IEntityBase> syncEvent = seMsg.Json.FromJson<JarsSyncStoreEvent<IEntityBase>>();
                        IProcessor processor = GetProcessorForEntity<IProcessorForEventServiceCommandReceived>(seMsg.Channel);
                        if (processor != null)
                            (processor as IProcessorForEventServiceCommandReceived).OnStoreCommandReceived(schedulerControl, seMsg);
                    }
                }
                else
                {
                    IProcessor processor = GetProcessorForEntity<IProcessorForEventServiceCommandReceived>(seMsg.Channel);
                    if (processor != null)
                        (processor as IProcessorForEventServiceCommandReceived).OnAnyCommandReceived(schedulerControl, seMsg);
                }
            }
            catch (Exception oMx)
            {
                Logger.Error(oMx.Message);
#if DEBUG
                throw oMx;
#endif
            }
        }

        public override void OnUpdateEvent(ServerEventUpdate up)
        {
            base.OnUpdateEvent(up);
        }

        private void iAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
        }


        bool _isLoadOrRefreshProcessorsDataLoading;
        private async Task LoadOrRefreshProcessorsDataAsync()
        {
            try
            {
                _isLoadOrRefreshProcessorsDataLoading = true;
                //fires off all the different processors used for loading data found through MEF
                List<Task> tList = new List<Task>();
                foreach (var processor in _JarsProcessors)
                {
                    #region AttachProcessorsToControls
                    if (processor.Value is IPluginWinForms plugin)
                        LinkPluginWithControls(plugin);
                    #endregion

                    if (processor.Value is IProcessorForLoadingEntityData)
                    { //tList.Add((processor.Value as IProcessorForLoadingEntityData).LoadOrRefreshEntityDataAsync());                   
                        await (processor.Value as IProcessorForLoadingEntityData).LoadOrRefreshEntityDataAsync();
                    }
                }
                //load all the data at once.
                //await Task.WhenAll(tList.ToArray());

                //fires off all the different appointment processors used found through MEF
                //for the moment this is not async
                foreach (var processor in _JarsProcessors)
                {
                    if (processor.Value is IProcessorForLoadingEntityData)
                        (processor.Value as IProcessorForLoadingEntityData).LoadOrRefreshEntityData(schedulerControl);
                }
            }
            catch (WebServiceException wex)
            {
                MessageBox.Show($"Processor Load Data Exception{Environment.NewLine}{wex.ErrorCode}{Environment.NewLine}{wex.ResponseBody}");
                SplashScreenManager.Default.SendCommand(null, "Error Loading Processor Information!");
                //SplashScreenManager.CloseForm();
                barNotificationText.Caption = wex.Message;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Processor Information!");
                SplashScreenManager.Default.SendCommand(null, "Error Loading Processor Information!");
                //SplashScreenManager.CloseForm();
                barNotificationText.Caption = ex.Message;
            }
            finally
            {
                _isLoadOrRefreshProcessorsDataLoading = false;
            }
        }

        bool _isInitExecuteAndLoadStateForExternalPluginsLoading;
        private async Task InitExecuteAndLoadStateForExternalPlugins()
        {

            try
            {
                _isInitExecuteAndLoadStateForExternalPluginsLoading = true;
                foreach (var plugin in _ExternalEntityPlugins)
                {
                    UserControlBasePlugin plgn = plugin.Value;
                    try
                    {
                        plgn.SuspendLayout();

                        if (plgn is IPluginWithInitialize)
                            (plgn as IPluginWithInitialize).Init();

                        if (plgn is IPluginWithExecute)
                            if (((IPluginWithExecute)plgn).AutoExecute == true)
                                ((IPluginWithExecute)plgn).Execute();

                        if (plgn is IPluginWithExecuteAsync)
                            if (((IPluginWithExecuteAsync)plgn).AutoExecute == true)
                                await ((IPluginWithExecuteAsync)plgn).ExecuteAsync();

                        if (plgn is IPluginWithStateInfo)
                            LoadPluginState(plgn as IPluginWithStateInfo);

                        if (plgn is IPluginWithStateInfoAsync)
                            await LoadPluginStateAsync(plgn as IPluginWithStateInfoAsync);
                    }
                    finally
                    {
                        plgn.ResumeLayout();
                        plgn.Refresh();
                    }
                }
            }
            finally
            {
                _isInitExecuteAndLoadStateForExternalPluginsLoading = false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //jarsEngine.SaveControlLayoutsAndSettings();
            //the main layout
            SplashScreenManager.ShowForm(this, typeof(WaitForm), false, false);
            barWorkingProgress.Visibility = BarItemVisibility.Always;
            //SplashScreenManager.Default.SendCommand(null, "Saving Settings");
            SplashScreenManager.ShowDefaultWaitForm("Please Wait..", "Saving Layout and Settings.");
            JarsSetting layoutSet = LoggedInUser.Settings.FirstOrDefault(s => s.Platform == Context.PlatformCode && s.PartName == Name);
            if (layoutSet == null)
            {
                layoutSet = new JarsSetting { PartName = Name, Platform = Context.PlatformCode };
                LoggedInUser.Settings.Add(layoutSet);
            }

            using (MemoryStream msCompressed = new MemoryStream())
            {
                using (MemoryStream msToCompress = new MemoryStream())
                using (GZipStream gZipStream = new GZipStream(msCompressed, CompressionMode.Compress))
                {
                    workspaceManagerMain.CaptureWorkspace(layoutSet.PartName);
                    workspaceManagerMain.SaveWorkspace(layoutSet.PartName, msToCompress, true);
                    msToCompress.Position = 0;
                    msToCompress.CopyTo(gZipStream);
                }
                layoutSet.SettingData = msCompressed.ToArray();
            }

            JarsSetting skinSet = LoggedInUser.Settings.FirstOrDefault(s => s.Platform == Context.PlatformCode && s.PartName == $"{Name}_Skin");
            if (skinSet == null)
            {
                skinSet = new JarsSetting { PartName = $"{Name}_Skin", Platform = Context.PlatformCode };
                LoggedInUser.Settings.Add(skinSet);
            }
            skinSet.SettingData = UserLookAndFeel.Default.SkinName.Select(Convert.ToByte).ToArray();

            JarsSetting resGrpSet = LoggedInUser.Settings.FirstOrDefault(s => s.Platform == Context.PlatformCode && s.PartName == ckBtnGroupResources.Name);
            if (resGrpSet == null)
            {
                resGrpSet = new JarsSetting { PartName = ckBtnGroupResources.Name, Platform = Context.PlatformCode };
                LoggedInUser.Settings.Add(resGrpSet);
            }
            resGrpSet.SettingData = new byte[] { Convert.ToByte(ckBtnGroupResources.Checked) };

            JarsSetting resTreeSet = LoggedInUser.Settings.FirstOrDefault(s => s.Platform == Context.PlatformCode && s.PartName == resourceTree.Name);
            if (resTreeSet == null)
            {
                resTreeSet = new JarsSetting { PartName = resourceTree.Name, Platform = Context.PlatformCode };
                LoggedInUser.Settings.Add(resTreeSet);
            }


            TreeListNode[] checkedNodes = resourceTree.FindNodes(n => n.Checked && n.Tag is JarsResource);
            List<string> idList = new List<string>();
            foreach (TreeListNode node in checkedNodes)
            {
                idList.Add((node.Tag as JarsResource).Id.ToString());
            }

            using (MemoryStream msCompressed = new MemoryStream())//what gzip writes to
            {
                using (GZipStream gZipStream = new GZipStream(msCompressed, CompressionMode.Compress))//setting up gzip
                using (MemoryStream msToCompress = new MemoryStream())//what the object will serialize to
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(msToCompress, idList);
                    msToCompress.Position = 0;
                    msToCompress.CopyTo(gZipStream);
                    //gZipStream.Close();
                }
                resTreeSet.SettingData = msCompressed.ToArray();//zipped array to save data
            }

            //save the plugin statuses
            foreach (var plgn in _RibbonPlugins)
            {
                IPluginBarItemToRibbon plugin = plgn.Value;
                if (plugin is IPluginWithStateInfo)
                    GetPluginStateInformation(plugin as IPluginWithStateInfo);
                if (plugin is IPluginWithStateInfoAsync)
                    Task.Run(async () => { await GetPluginStateInformationAsync(plugin as IPluginWithStateInfoAsync); }).RunSynchronously();
            }

            foreach (var plgn in _ViewOptionPlugins)
            {
                IPluginAsViewOption plugin = plgn.Value;
                if (plugin is IPluginWithStateInfo)
                    GetPluginStateInformation(plugin as IPluginWithStateInfo);
                if (plugin is IPluginWithStateInfoAsync)
                    Task.Run(async () => { await GetPluginStateInformationAsync(plugin as IPluginWithStateInfoAsync); }).RunSynchronously();
            }

            //save user control plugin data
            foreach (var plgn in _ExternalEntityPlugins)
            {
                UserControlBasePlugin plugin = plgn.Value;
                if (plugin is IPluginWithStateInfo)
                    GetPluginStateInformation(plugin as IPluginWithStateInfo);
                if (plugin is IPluginWithStateInfoAsync)
                    Task.Run(async () => { await GetPluginStateInformationAsync(plugin as IPluginWithStateInfoAsync); }).RunSynchronously();
            }

            foreach (var plgn in _BehaviourPlugins)
            {
                IPluginAsBehaviour plugin = plgn.Value;

                if (plugin is IPluginWithStateInfo)
                    GetPluginStateInformation(plugin as IPluginWithStateInfo);

                if (plugin is IPluginWithStateInfoAsync)
                    Task.Run(async () => { await GetPluginStateInformationAsync(plugin as IPluginWithStateInfoAsync); }).RunSynchronously();
            }

            try
            {
                //save settings by saving the user
                Context.ServiceClient.Post(new StoreJarsUser { UserAccount = LoggedInUser.ConvertTo<JarsUserDto>() });
            }
            catch (WebException ex)
            {
                if (MessageBox.Show($"Connection was lost, settings can not be saved. You can try and wait for the connection by pressing the 'Retry' button.{Environment.NewLine}To exit without saving the current settings press 'Cancel'.", "Connection Lost - Unable to save settings", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Retry)
                {
                    throw ex;
                }
            }
            finally
            {
                barWorkingProgress.Visibility = BarItemVisibility.Never;
                SplashScreenManager.CloseForm();
            }
        }

        /// <summary>
        /// Load the default appointment labels and statuses and set them to the scheduler list
        /// </summary>
        void SetDefaultAppointmentStatusesAndLabels()
        {
            SchedulerDataStorage _sDataStorage = schedulerControl.DataStorage as SchedulerDataStorage;
            List<ApptLabel> defLabel = Context.ServiceClient.Get(new FindApptLabels() { ViewType = "DEFAULT", InterfaceTypeName = typeof(IEntityWithStatusLabels).AssemblyQualifiedName }).Labels.ConvertAllTo<ApptLabel>().ToList();
            List<ApptStatus> defStatus = Context.ServiceClient.Get(new FindApptStatuses() { ViewType = "DEFAULT", InterfaceTypeName = typeof(IEntityWithStatusLabels).AssemblyQualifiedName }).Statuses.ConvertAllTo<ApptStatus>().ToList();

            try
            {
                schedulerControl.BeginUpdate();
                AppointmentLabelDataStorage lStore = _sDataStorage.Labels;

                lStore.Clear();
                foreach (ApptLabel label in defLabel)
                {
                    //string id = label.AutoUId;//$"{label.ViewType}_{label.SortIndex}_{label.ID}";
                    if (lStore.Items.Find(l => l.Id.ToString() == label.Id.ToString()) == null)
                    {
                        string displayName = $"{label.LabelName}({label.LabelCriteria})";
                        AppointmentLabel lbl = lStore.Items.CreateNewLabel(label.Id, displayName);
                        lbl.Color = Color.FromArgb(label.ColourRGB);
                        lStore.Add(lbl);
                    }
                    //else
                    //    lbl.Dispose();
                }

                AppointmentStatusDataStorage sStore = _sDataStorage.Statuses;//.Appointments.Statuses;
                sStore.Clear();
                foreach (ApptStatus status in defStatus)
                {
                    if (sStore.Items.Find(s => s.Id.ToString() == status.Id.ToString()) == null)
                    {
                        string displayName = $"{status.StatusName}({status.StatusCriteria})";
                        AppointmentStatus sts = sStore.Items.CreateNewStatus(status.Id, displayName);
                        sts.SetBrush(new SolidBrush(Color.FromArgb(status.ColourRGB)));
                        sts.Type = AppointmentStatusType.Custom;
                        sStore.Add(sts);
                    }
                    //else
                    //    sts.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
#if DEBUG
                throw ex;
#endif
            }
            finally
            {
                schedulerControl.EndUpdate();
            }
        }

        private void LoadPluginState(IPluginWithStateInfo plgn)
        {
            JarsSetting plgSetting = LoggedInUser.Settings.FirstOrDefault(s => s.Platform == Context.PlatformCode && s.PartName == ((IPluginBase)plgn).GetPluginTextAsNameValue());
            if (plgSetting != null)
                plgn.LoadStateInformation(plgSetting.SettingData);
        }

        private async Task LoadPluginStateAsync(IPluginWithStateInfoAsync plgn)
        {
            JarsSetting plgSetting = LoggedInUser.Settings.FirstOrDefault(s => s.Platform == Context.PlatformCode && s.PartName == plgn.GetPluginTextAsNameValue());
            if (plgSetting != null)
                await plgn.LoadStateInformationAsync(plgSetting.SettingData);
        }

        private async Task GetPluginStateInformationAsync(IPluginWithStateInfoAsync plugin)
        {
            JarsSetting plgSetting = LoggedInUser.Settings.FirstOrDefault(s => s.Platform == Context.PlatformCode && s.PartName == plugin.GetPluginTextAsNameValue());
            if (plgSetting == null)
            {
                plgSetting = new JarsSetting { PartName = plugin.GetPluginTextAsNameValue(), Platform = Context.PlatformCode };
                LoggedInUser.Settings.Add(plgSetting);
            }
            plgSetting.SettingData = await plugin.GetStateInformationAsync();
        }

        private void GetPluginStateInformation(IPluginWithStateInfo plugin)
        {
            JarsSetting plgSetting = LoggedInUser.Settings.FirstOrDefault(s => s.Platform == Context.PlatformCode && s.PartName == plugin.GetPluginTextAsNameValue());
            if (plgSetting == null)
            {
                plgSetting = new JarsSetting { PartName = plugin.GetPluginTextAsNameValue(), Platform = Context.PlatformCode };
                LoggedInUser.Settings.Add(plgSetting);
            }
            plgSetting.SettingData = plugin.GetStateInformation();
        }

        /// <summary>
        /// Get the jars processor that implements a specific interface.
        /// </summary>
        /// <typeparam name="TProcessorType">The interface implemented by the IJarsProcessor</typeparam>
        /// <param name="entity">The entity that the processor is linked to.</param>
        /// <returns>null if no match found, or the IJarsProcessor linked to the entity</returns>
        IProcessor GetProcessorForEntity<TProcessorType>(IEntityBase entity)
        {
            //if (entity is StandardAppointmentException || entity is StandardAppointmentException)
            //    return GetProcessorForEntity<IProcessorForAppointmentEvents>(nameof(StandardAppointment));
            //else
            return GetProcessorForEntity<TProcessorType>(entity.GetType().Name);
        }

        /// <summary>
        /// Get the jars processor that implements a specific interface.
        /// </summary>
        /// <typeparam name="TProcessorType">The interface implemented by the IJarsProcessor</typeparam>
        /// <param name="entityTypeName">The name of the entity that the processor is linked to.</param>
        /// <returns>null if no match found, or the IJarsProcessor linked to the entity</returns>
        IProcessor GetProcessorForEntity<TProcessorType>(string entityTypeName)
        {
            Lazy<IProcessor, IProcessorMetadata> lazyProcessor = _JarsProcessors.SingleOrDefault(jp => jp.Metadata.LinkedEntityType.Name == entityTypeName && jp.Value is TProcessorType);
            if (lazyProcessor != null)
            {
                if (lazyProcessor.Value is TProcessorType)
                {
                    return lazyProcessor.Value;
                }
                else
                    return null;
            }
            else
                return null;
        }

        bool _isEntityRulesLoading;
        /// <summary>
        /// Load entity rules and subscribe to entityRule channel
        /// </summary>
        /// <returns></returns>
        async Task LoadEntityRules()
        {
            try
            {
                _isEntityRulesLoading = true;
                JarsRulesResponse entCondResp = await Context.ServiceClient.GetAsync(new FindJarsRules());
                JarsRules = entCondResp.Rules.ToList<IJarsRule>();

                if (!this.DesignMode)
                    if (SSEventClient != null)
                    {
                        if (SSEventClient.Status == "Started")
                        {
                            SSEventClient.SubscribeToChannels(nameof(JarsRule));
                        }
                    }
            }
            finally
            {
                _isEntityRulesLoading = false;
            }
        }

        bool _isResourcesLoading;
        List<JarsResourceGroup> _opGroupList;
        public async Task<bool> LoadResourcesAsync(bool groupByGroup, bool isFirstLoad)
        {
            try
            {
                _isResourcesLoading = true;
                Task<ResourcesResponse> opsT = Context.ServiceClient.PostAsync(new FindResources() { FetchEagerly = true, IsActive = true });
                Task<ResourceGroupsResponse> grpT = Task.Run(() =>
                {
                    if (groupByGroup)
                    {
                        return Context.ServiceClient.GetAsync(new FindResourceGroups() { FetchEagerly = true, IsActive = true });
                    }
                    else
                        return Task.Run(() => { return new ResourceGroupsResponse(); });
                });
                await Task.WhenAll(new Task[] { opsT, grpT });

                ResourcesResponse resp = opsT.Result;
                ResourceGroupsResponse grpResp = grpT.Result;
                _opGroupList = grpResp.Groups.ConvertAllTo<JarsResourceGroup>().ToList();
                if (isFirstLoad)
                {
                    operativeBindingSource.DataSource = resp.Resources.ConvertAllTo<JarsResource>();
                    operativeBindingSource.ResetBindings(false);
                    //schedulerControl.Refresh();
                }
                else
                {
                    EntityTypeAndIdComparer typeIdComparer = new EntityTypeAndIdComparer();
                    List<JarsResource> currResList = operativeBindingSource.List as List<JarsResource>;
                    //find the missing resources and add them in.
                    List<JarsResource> missingResList = resp.Resources.ConvertAllTo<JarsResource>()
                        //.Where(newOp => !currResList.Contains(newOp, EqualityComparer<JarsResource>.Default))
                        .Where(newOp => !currResList.Contains(newOp, typeIdComparer))
                        .ToList();
                    if (missingResList.Any())
                    {
                        currResList.AddRange(missingResList);
                        operativeBindingSource.DataSource = currResList;                       
                        //schedulerControl.Refresh();
                    }

                    //might have some property changes                    
                    foreach (var curRes in currResList)
                    {
                        var matchedRes = resp.Resources.Find(r => r.Id == curRes.Id);
                        if (matchedRes != null)
                            curRes.PopulateWith(matchedRes);
                    }
                    operativeBindingSource.ResetBindings(false);
                }
                //return BuildResourceTree(groupByGroup, isFirstLoad);
            }
            finally
            {
                _isResourcesLoading = false;
            }

            //return a value from the async method so that the calling action can await this result.
            return true;
        }

        public bool BuildResourceTree(bool groupByGroup, bool isFirstLoad)
        {
            bool ret = false;
            try
            {
                resourceTree.BeginUpdate();
                ResourceBaseCollection resourceCollection = schedulerControl.ActiveView.GetFilteredResources();
                //clear all the nodes..
                resourceTree.BeforeCheckNode -= TreeList_BeforeCheckNode_Setup;
                resourceTree.ClearNodes();
                if (!groupByGroup)
                {
                    resourceTree.CustomDrawNodeCheckBox -= TreeList_CustomDrawNodeCheckBox_Setup;

                    foreach (JarsResource op in (List<JarsResource>)operativeBindingSource.List)
                    {
                        TreeListNode opNode = resourceTree.AppendNode(op.DisplayName, null);
                        opNode.SetValue("Name", op.DisplayName);
                        opNode.Tag = op;
                        opNode.Expanded = false;
                        if (resourceCollection != null && resourceCollection.Count > 0)
                        {
                            DevExpress.XtraScheduler.Resource resource = resourceCollection.FirstOrDefault(r => $"{r.Id}" == $"{op.Id}");
                            if (resource != null)
                                if (isFirstLoad)
                                {
                                    DevExpress.XtraScheduler.Resource res = schedulerControl.DataStorage.Resources.Items.Find(r => $"{r.Id}" == $"{resource.Id}");
                                    opNode.Checked = false;
                                    resource.Visible = false;
                                    res.Visible = resource.Visible;
                                }
                                else
                                    opNode.Checked = true;
                            else
                            {
                                DevExpress.XtraScheduler.Resource res = schedulerControl.DataStorage.Resources.Items.Find(r => $"{r.Id}" == $"{op.Id}");
                                opNode.Checked = false;
                                res.Visible = opNode.Checked;
                            }
                        }

                        if (op.Groups.Count > 0)
                        {
                            foreach (JarsResourceGroup grp in op.Groups)
                            {
                                TreeListNode gNode = resourceTree.AppendNode(grp.Name, opNode);
                                gNode.SetValue("Name", grp.Name);
                                gNode.Tag = grp;
                            }
                        }
                    }
                    resourceTree.CustomDrawNodeCheckBox += TreeList_CustomDrawNodeCheckBox_Setup;
                }
                else
                {
                    //draw the ree as if it was by group and not operative
                    foreach (JarsResourceGroup grp in _opGroupList)
                    {
                        TreeListNode grpNode = resourceTree.AppendNode(grp.Name, null);
                        grpNode.SetValue("Name", grp.Name);
                        grpNode.Tag = grp;
                        grpNode.Checked = false;
                        grpNode.Expanded = true;
                        if (grp.Resources.Count > 0)
                        {
                            foreach (JarsResource op in grp.Resources)
                            {
                                TreeListNode opNode = resourceTree.AppendNode(op.DisplayName, grpNode);
                                opNode.SetValue("Name", op.DisplayName);
                                opNode.Tag = op;

                                if (resourceCollection != null && resourceCollection.Count > 0)
                                {
                                    DevExpress.XtraScheduler.Resource resource = resourceCollection.FirstOrDefault(r => $"{r.Id}" == $"{op.Id}");
                                    if (resource != null)
                                        if (isFirstLoad)
                                        {
                                            DevExpress.XtraScheduler.Resource res = schedulerControl.DataStorage.Resources.Items.Find(r => $"{r.Id}" == $"{resource.Id}");

                                            opNode.Checked = false;
                                            grpNode.Checked = false;
                                            resource.Visible = false;
                                            res.Visible = resource.Visible;
                                        }
                                        else
                                        {
                                            if (resource.Visible)
                                            {
                                                opNode.Checked = true;
                                                grpNode.Checked = true;
                                            }
                                            else
                                                opNode.Checked = false;
                                        }
                                    else
                                    {
                                        DevExpress.XtraScheduler.Resource res = schedulerControl.DataStorage.Resources.Items.Find(r => $"{r.Id}" == $"{op.Id}");
                                        opNode.Checked = false;
                                        res.Visible = opNode.Checked;
                                    }
                                }
                            }
                        }
                    }
                }
                resourceTree.BeforeCheckNode += TreeList_BeforeCheckNode_Setup;

                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
#if DEBUG
                throw ex;
#endif
            }
            finally
            {
                resourceTree.EndUpdate();
            }
            //also set the in place editor option if there are no resources selected
            return ret;
        }

        /// <summary>
        /// This method sets whether appointments can be created or not, it looks at the number of selected resources in the scheduler controls active view
        /// and disables appointment creation if none are selected
        /// </summary>
        private void SetAppointmentCreationOption()
        {
            if (schedulerControl.ActiveView.GetResources().Count > 0)
                schedulerControl.OptionsCustomization.AllowInplaceEditor = UsedAppointmentType.Custom;
            else
                schedulerControl.OptionsCustomization.AllowInplaceEditor = UsedAppointmentType.None;
        }

        private void TreeList_BeforeCheckNode_Setup(object sender, NodeEventArgs e)
        {

            if (!(e is CheckNodeEventArgs))
                return;

            bool isChecked = ((e as CheckNodeEventArgs).State == CheckState.Checked) ? true : false;
            schedulerControl.BeginUpdate();
            if (e.Node.Tag is JarsResourceGroup)
            {
                //hide all teh resources in the group
                foreach (JarsResource op in ((JarsResourceGroup)e.Node.Tag).Resources)
                {
                    Resource resource = schedulerControl.DataStorage.GetResourceById(op.Id);
                    resource.Visible = isChecked;

                    TreeListNode[] nodes = resourceTree.FindNodes(n => n.Tag is JarsResource && ((JarsResource)n.Tag).Id == op.Id);
                    if (nodes.Length > 0)
                        foreach (TreeListNode n in nodes)
                        {
                            n.Checked = isChecked;
                            if (n.ParentNode != null && n.ParentNode != e.Node)
                                if (n.ParentNode.Nodes.FirstOrDefault(cn => cn.Checked == true) != null)
                                    n.ParentNode.Checked = true;
                                else
                                    n.ParentNode.Checked = false;
                        }
                }
            }
            if (e.Node.Tag is JarsResource)
            {
                //hide only the operative
                Resource resource = schedulerControl.DataStorage.GetResourceById(((JarsResource)e.Node.Tag).Id);
                resource.Visible = isChecked;
                //also make sure that all other nodes that represent this resource is set.
                TreeListNode[] nodes = resourceTree.FindNodes(n => n.Tag is JarsResource && ((JarsResource)n.Tag).Id.ToString() == resource.Id.ToString());
                if (nodes.Length > 0)
                    foreach (TreeListNode n in nodes)
                    {
                        n.Checked = isChecked;
                        if (n.ParentNode != null)
                            if (n.ParentNode.Nodes.FirstOrDefault(cn => cn.Checked == true) != null)
                                n.ParentNode.Checked = true;
                            else
                                n.ParentNode.Checked = false;
                    }
            }
            schedulerControl.EndUpdate();
            SetAppointmentCreationOption();
        }

        private void TreeList_CustomDrawNodeCheckBox_Setup(object sender, CustomDrawNodeCheckBoxEventArgs e)
        {
            if (e.Node.Level > 0 && e.Node.Tag is JarsResourceGroup)
                e.Handled = true;
        }

        private async void resourceTreeBtnRefresh_Click(object sender, EventArgs e)
        {
            if (!_isResourcesLoading)
            {
                await LoadResourcesAsync(ckBtnGroupResources.Checked, _IsFirstLoad);
                BuildResourceTree(ckBtnGroupResources.Checked, _IsFirstLoad);
                resourceTree.Refresh();
                SetAppointmentCreationOption();
            }
        }

        private async void ckBtnGroupResources_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsFirstLoad)
                return;

            ckBtnGroupResources.ImageOptions.Image = ckBtnGroupResources.Checked ? ckBtnGroupResources.AppearancePressed.Image : ckBtnGroupResources.Appearance.Image;

            await LoadResourcesAsync(ckBtnGroupResources.Checked, _IsFirstLoad);
            BuildResourceTree(ckBtnGroupResources.Checked, _IsFirstLoad);
            resourceTree.Refresh();
            SetAppointmentCreationOption();
        }

        private void bBtnCheckAllResources_ItemClick(object sender, ItemClickEventArgs e)
        {
            resourceTree.CheckAll();
            schedulerControl.BeginUpdate();
            foreach (Resource res in ((ResourceDataStorage)schedulerControl.DataStorage.Resources).Items)
                res.Visible = true;
            schedulerControl.EndUpdate();
            SetAppointmentCreationOption();
        }

        private void bBtnUnCheckAllResources_ItemClick(object sender, ItemClickEventArgs e)
        {
            resourceTree.UncheckAll();
            schedulerControl.BeginUpdate();
            foreach (Resource res in schedulerControl.ActiveView.GetResources())
                res.Visible = false;
            schedulerControl.EndUpdate();
            SetAppointmentCreationOption();
        }

        private void barBtnToggleResourceExpandedNodes_ItemClick(object sender, ItemClickEventArgs e)
        {
            resourceTree.ExpandAll();
        }

        private void barBtnresTreeCollapseAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            resourceTree.CollapseAll();
        }

        private void schedulerControl_CustomDrawResourceHeader(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e)
        {

            ResourceHeader header = (ResourceHeader)e.ObjectInfo;
            header.Caption = $"{header.Resource.Caption}{Environment.NewLine}({((JarsResource)header.Resource.RowHandle).ExtRef})";
            e.DrawDefault();
            e.Handled = true;
            Rectangle rect = new Rectangle(e.Bounds.X + 2, e.Bounds.Bottom - 25, 24, 24);
            e.Graphics.DrawImage(resourceInfoImage, rect);

        }

        private void schedulerControl_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (MouseButtons == e.Button)
                {
                    if (schedulerControl.IsUpdateLocked)
                    {
                        schedulerControl.Services.AppointmentSelection.ClearSelection();
                        schedulerControl.Services.MouseHandler.OnMouseUp(e);
                    }
                    else
                    {
                        if (schedulerControl.SelectedResource != null)
                        {
                            Point pos = new Point(e.X, e.Y);
                            SchedulerViewInfoBase viewInfo = schedulerControl.ActiveView.ViewInfo;
                            SchedulerHitInfo hitInfo = viewInfo.CalcHitInfo(pos, false);
                            if (hitInfo.HitTest == SchedulerHitTest.ResourceHeader)
                            {
                                ResourceHeader resHeader = hitInfo.ViewInfo as ResourceHeader;
                                Rectangle rect = new Rectangle(resHeader.Bounds.X + 2, resHeader.Bounds.Bottom - 25, 24, 24);
                                if (rect.Contains(e.Location))
                                {
                                    schedulerControl.Services.MouseHandler.OnMouseUp(e);
                                    if (!(resHeader.Resource.RowHandle is JarsResource cuResourcer))
                                        return;
                                    var x = _JarsProcessors.Where(p => p.Metadata.LinkedEntityType == typeof(JarsResource) && p.Metadata.AdditionalProcessorType == typeof(IPluginToResourceHeader));
                                    ResourcePluginsForm.Show(cuResourcer, x.ToList(), schedulerControl);
                                    return;
                                }
                            }
                        }
                        //could add mouse down processor call here..
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("schedulerControl_MouseDown", ex);
            }
        }

        private void schedulerControl_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (MouseButtons == e.Button)
                {
                    //if the scheduler is locked release the mouse button
                    if (schedulerControl.IsUpdateLocked)
                    {
                        schedulerControl.Services.AppointmentSelection.ClearSelection();
                        schedulerControl.Services.MouseHandler.OnMouseUp(e);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("schedulerControl_MouseMove", ex);
            }
        }

        private void schedulerControl_AllowAppointmentEdit(object sender, AppointmentOperationEventArgs e)
        {
            e.Allow = RolesAndOrPermissions.CheckMatchAny(roles: DefaultRoles, permissions: DefaultPermissions);
            if (e.Allow)
            {
                try
                {
                    if (e.Appointment != null && e.Appointment.CustomFields != null)// && !(e.Appointment.ResourceId is EmptyResourceId))
                        e.Allow = true;
                    else
                        e.Allow = false;
                }
                catch (Exception ex)
                {
                    throw new Exception("schedulerControl_AllowAppointmentEdit", ex);
                }
            }
        }

        private void schedulerControl_AllowAppointmentResize(object sender, AppointmentOperationEventArgs e)
        {
            e.Allow = false;
            RolesAndOrPermissions.ExecuteActionUIOnException(
                roles: DefaultRoles,
                permissions: DefaultPermissions,
                action: () =>
                {
                    e.Allow = true;
                    try
                    {
                        if (schedulerControl.IsUpdateLocked)
                        {
                            e.Allow = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("schedulerControl_AllowAppointmentResize", ex);
                    }
                });
        }

        private void schedulerControl_AllowAppointmentCreate(object sender, AppointmentOperationEventArgs e)
        {
            e.Allow = false;
            RolesAndOrPermissions.ExecuteActionUIOnException(
                roles: DefaultRoles,
                permissions: DefaultPermissions,
                action: () =>
                {
                    e.Allow = true;
                    if (e.Appointment != null)
                        if (e.Appointment.ResourceId is EmptyResourceId)
                        {
                            e.Allow = false;
                            MessageBox.Show("Please make sure a resource is selected");
                        }
                });
        }

        private void schedulerControl_AllowInplaceEditor(object sender, AppointmentOperationEventArgs e)
        {
            e.Allow = false;
            RolesAndOrPermissions.ExecuteActionUIOnException(
                roles: DefaultRoles,
                permissions: DefaultPermissions,
                action: () =>
                {
                    try
                    {
                        if (schedulerControl.IsUpdateLocked)
                            e.Allow = false;
                        else
                        {
                            e.Allow = true;
                            if (e.Appointment != null && e.Appointment.CustomFields != null && e.Appointment.CustomFields["ENTITY"] is IEntityBase)
                            {
                                if (e.Appointment.CustomFields["ENTITY"] is IEntityWithAppointing)
                                {
                                    IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentEvents>(e.Appointment.CustomFields["ENTITY"] as IEntityBase);
                                    if (processor != null)
                                        ((IProcessorForAppointmentEvents)processor).OnAllowInplaceEditor(sender, e);
                                    else
                                    {
                                        Logger.Info($"No AllowInPlaceEditor processor found for {(e.Appointment.CustomFields["ENTITY"] as IEntityBase).GetType().Name}.");
                                        e.Allow = false;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        e.Allow = false;
#if DEBUG
                        throw new Exception("schedulerControl_AllowInplaceEditor", ex);
#endif
                    }
                });
        }

        private void schedulerControl_InplaceEditorShowing(object sender, InplaceEditorEventArgs e)
        {
            try
            {
                //if the custom fields are empty or null then we will create a new appointment
                //that will move into the standard appointment processor
                if (e.Appointment.CustomFields["ENTITY"] == null)
                {
                    e.Appointment.CustomFields["ENTITY"] = new StandardAppointment().PopulateWith(e.Appointment)
                        .ThenDo(sa => sa.ApptTypeCode = AppointmentType.Normal.ToString())
                        .ThenDo(sa => sa.StartDate = e.Appointment.Start)
                        .ThenDo(sa => sa.EndDate = e.Appointment.End);
                }

                if (e.Appointment.CustomFields != null && e.Appointment.CustomFields["ENTITY"] is IEntityBase)
                {
                    if (e.Appointment.CustomFields["ENTITY"] is IEntityWithAppointing)
                    {
                        IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentEvents>(e.Appointment.CustomFields["ENTITY"] as IEntityBase);
                        if (processor != null)
                            ((IProcessorForAppointmentEvents)processor).OnInplaceEditorShowing(sender, e);
                        else
                        {
                            Logger.Error($"No InplaceEditorShowing processor found for {(e.Appointment.CustomFields["ENTITY"] as IEntityBase).GetType().Name}.");
                        }
                    }
                }
                else
                    Logger.Info("Custom field null in InplaceEditorShowing event.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new Exception("schedulerControl_InplaceEditorShowing", ex);
            }
        }

        private void schedulerControl_AllowAppointmentDrag(object sender, AppointmentOperationEventArgs e)
        {
            e.Allow = false;
            RolesAndOrPermissions.ExecuteActionUIOnException(
                roles: DefaultRoles,
                permissions: DefaultPermissions,
                action: () =>
                {
                    e.Allow = true;
                });
        }

        public void schedulerControl_AppointmentDrag(object sender, AppointmentDragEventArgs e)
        {
            //set the e.Allow to false because this extension method does not throw an exception, it just doesn't execute the action.
            //any values like e.Allow that is true by default does not get affected.
            e.Allow = false;
            RolesAndOrPermissions.ExecuteActionUIOnException(
                roles: DefaultRoles,
                permissions: DefaultPermissions,
                action: () =>
                {
                    //rest the e.Allow to true as the user permission and roles has passed.
                    e.Allow = true;

                    if (e.EditedAppointment.CustomFields != null)
                    {
                        if (e.EditedAppointment.CustomFields["ENTITY"] is IEntityWithAppointing dragEntity)
                        {
                            dragEntity.StartDate = e.EditedAppointment.Start;
                            dragEntity.EndDate = e.EditedAppointment.End;
                            if (!EvaluateRules(e.EditedAppointment.CustomFields["ENTITY"], e.HitResource.RowHandle, RuleRunsOn.OnDragDrop))
                            {
                                e.Allow = false;
                                dragEntity.StartDate = e.SourceAppointment.Start;
                                dragEntity.EndDate = e.SourceAppointment.End;
                                return;
                            }

                            if (dragEntity is IEntityBase)
                            {
                                //check that the appointment is not double added to the resource
                                if (dragEntity is IEntityWithExternalReference extRefEnt)
                                {
                                    Appointment conflictAppt = schedulerDataStorage.Appointments.Items.Find(a =>
                                        (a.CustomFields["ENTITY"] is IEntityWithAppointing apptbleEnt)
                                        && (apptbleEnt is IEntityWithExternalReference apptExtRef)
                                        && $"{apptbleEnt.ResourceId}" == $"{e.EditedAppointment.ResourceId}"
                                        && apptExtRef.ExtRefId == $"{extRefEnt.ExtRefId}"
                                        && apptbleEnt.StartDate.Date == e.EditedAppointment.Start.Date
                                        && $"{dragEntity.Id}" == "0");
                                    if (conflictAppt != null)
                                    {
                                        e.Allow = false;
                                        tooltipEntityRules.ShowHint("This entity has already been added to this resource today!", "Same entity can not be added on same day to same resource.", MousePosition);
                                        return;
                                    }
                                }

                                tooltipEntityRules.HideHint();
                                IEntityBase entity = dragEntity as IEntityBase;
                                IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentEvents>(entity);
                                if (processor != null)
                                    ((IProcessorForAppointmentEvents)processor).OnAppointmentDrag(sender, e);
                                else
                                {
                                    tooltipEntityRules.ShowHint($"No Processor found for {entity.GetType().Name}");
                                    Logger.Warn($"No AppointmentDrag processor found for {entity.GetType().Name}.");
                                    e.Allow = false;
                                }

                            }
                        }
                        else
                        {
                            tooltipEntityRules.ShowHint("THE ENTITY REPRESENTING THIS APPOINTMENT DOES NOT HAVE THE CORRECT IMPLEMENTATION!", "NOT IMPLEMENTING IAppointable", MousePosition);
                            e.Allow = false;
                        }
                    }
                    else
                    {
                        Logger.Warn("Custom field null in AppointmentDrag event.");
                        e.Allow = false;
                    }
                });
        }

        //private async void schedulerControl_AppointmentDrop(object sender, AppointmentDragEventArgs e)
        private void schedulerControl_AppointmentDrop(object sender, AppointmentDragEventArgs e)
        {
            DateTime oldStart = e.SourceAppointment.Start;
            DateTime newStart = e.EditedAppointment.Start;
            //in case nothing changed, just cancel the process
            if ((oldStart == newStart) && (e.SourceAppointment.ResourceId == e.EditedAppointment.ResourceId))
            {
                e.Allow = false;
                return;
            }

            if (!EvaluateRules(e.EditedAppointment.CustomFields["ENTITY"], e.HitResource.RowHandle, RuleRunsOn.OnDragDrop))
            {
                e.Allow = false;
                return;
            }

            if (e.EditedAppointment.CustomFields["ENTITY"] is IEntityBase)
            {
                IEntityBase entity = e.EditedAppointment.CustomFields["ENTITY"] as IEntityBase;
                IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentEvents>(entity);

                if (processor != null)
                {
                    ////await ((IProcessorForAppointmentEvents)processor).OnAppointmentDropAsync(e);
                    ((IProcessorForAppointmentEvents)processor).OnAppointmentDrop(sender, e);
                }
                else
                {
                    Logger.Warn($"No AppointmentDrop processor found for {entity.GetType().Name}.");
                    e.Allow = false;
                }
            }
            //refresh the control
            schedulerControl.BeginInvokeIfRequired(sc =>
            {
                sc.Refresh();
                sc.SelectedAppointments.Clear();
                sc.SelectedAppointments.Add(e.EditedAppointment);
            });
        }

        private bool EvaluateRules(object sourceObject, object targetObject, RuleRunsOn ruleApplicator)
        {
            bool allow = true;
            if (sourceObject is IEntityBase && targetObject is IEntityBase)
            {
                IRuleProcessor ruleProcessor = GetProcessorForEntity<IRuleProcessor>(typeof(IEntityBase).Name) as IRuleProcessor;
                if (ruleProcessor != null)
                {
                    IJarsRule failedRule = ruleProcessor.EvaluateRules(JarsRules, (IEntityBase)sourceObject, (IEntityBase)targetObject, ruleApplicator);
                    if (failedRule != null)
                    {
                        allow = false;
                        string toolTipText = "";
                        switch (failedRule.RuleEvaluation)
                        {
                            case RuleEvaluation.SourceOnly:
                                toolTipText = $"Pass when condition - <i>{failedRule.RulePassesWhen.ToString().SpaceOnCapitalLetters()}.</i></br>" +
                                 $"[{failedRule.SourceTypeName}] :<color=255,0,0><b><i> {failedRule.SourceCriteriaString}</i></b></color></br>" +
                                 $"<i>Restrictions on </i>{failedRule.SourceTypeName}</color>";
                                break;

                            case RuleEvaluation.TargetOnly:
                                toolTipText = $"Pass when condition - <i>{failedRule.RulePassesWhen.ToString().SpaceOnCapitalLetters()}.</i></color></br>" +
                                 $"[{failedRule.TargetTypeName}] :<color=255,0,0><b><i> {failedRule.TargetCriteriaString}</i></b></color></br>" +
                                 $"<i>Restrictions on </i>{failedRule.TargetTypeName}</color>";
                                break;

                            case RuleEvaluation.Both:
                                toolTipText = $"Pass when condition - <i>{failedRule.RulePassesWhen.ToString().SpaceOnCapitalLetters()}.</i></br>" +
                                    $"[{failedRule.SourceTypeName}] :<color=255,0,0><b><i> {failedRule.SourceCriteriaString}</i></b></color></br>" +
                                    $"[{failedRule.TargetTypeName}] :<color=255,0,0><b><i> {failedRule.TargetCriteriaString}</i></b></color></br>" +
                                    $"<i>Restrictions on </i>{failedRule.SourceTypeName} <i>and</i> { failedRule.TargetTypeName}";
                                break;

                            default:
                                break;
                        }

                        tooltipEntityRules.ShowHint(toolTipText, MousePosition);
                    }
                }
                else { allow = true; }
            }

            //if (sourceObject is IEntityBase && targetObject is IEntityBase)
            //{
            //    if (entityRulesEvaluator != null && ((IEntityBase)sourceObject).GetType().HasAttribute<AllowRuleProcessingAttribute>())
            //    {
            //        IJarsRule failedRule = entityRulesEvaluator.EvaluateEntityRules(JarsRules, (IEntityBase)sourceObject, (IEntityBase)targetObject, ruleApplicator);
            //        if (failedRule != null)
            //        {
            //            allow = false;
            //            tooltipEntityRules.ShowHint($"Conditions - <i>{failedRule.RulePassesWhen.ToString().SpaceOnCapitalLetters()}.</i></br>" +
            //                $"[{failedRule.SourceTypeName}] : {failedRule.SourceCriteriaString}</br>" +
            //                $"[{failedRule.TargetTypeName}] : {failedRule.TargetCriteriaString}</br>",
            //                $"<color=255,0,0><i>Restrictions on </i><b>{failedRule.SourceTypeName}</b> <i>and</i> <b>{ failedRule.TargetTypeName}</b></color>", MousePosition);
            //        }
            //    }
            //    else { allow = true; }
            //}
            return allow;
        }

        private void schedulerControl_AppointmentResized(object sender, AppointmentResizeEventArgs e)
        {
            e.Allow = false;
            e.Handled = true;
            RolesAndOrPermissions.ExecuteActionUIOnException(
                roles: DefaultRoles,
                permissions: DefaultPermissions,
                action: () =>
                {
                    e.Allow = true;
                    e.Handled = false;
                    if (!EvaluateRules(e.EditedAppointment.CustomFields["ENTITY"], e.HitResource.RowHandle, RuleRunsOn.OnChange))
                    {
                        e.Allow = false;
                        return;
                    }

                    if (e.EditedAppointment.CustomFields["ENTITY"] is IEntityWithAppointing)
                    {
                        IEntityBase entity = e.EditedAppointment.CustomFields["ENTITY"] as IEntityBase;
                        IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentEvents>(entity);
                        if (processor != null)
                        {
                            ((IProcessorForAppointmentEvents)processor).OnAppointmentResized(sender, e);
                        }
                        else
                        {
                            Logger.Warn($"AppointmentResized - No SendCrudNotification processor found for {entity.GetType().Name}.");
                            e.Allow = false;
                        }
                    }
                });
        }

        private void schedulerControl_AppointmentResizing(object sender, AppointmentResizeEventArgs e)
        {
            e.Allow = false;
            e.Handled = true;
            RolesAndOrPermissions.ExecuteActionUIOnException(
                roles: DefaultRoles,
                permissions: DefaultPermissions,
                action: () =>
                 {
                     e.Allow = true;
                     e.Handled = false;
                     if (!EvaluateRules(e.EditedAppointment.CustomFields["ENTITY"], e.HitResource.RowHandle, RuleRunsOn.OnChange))
                     {
                         e.Allow = false;
                         return;
                     }

                     if (e.EditedAppointment.CustomFields["ENTITY"] is IEntityWithAppointing)
                     {
                         IEntityBase entity = e.EditedAppointment.CustomFields["ENTITY"] as IEntityBase;
                         IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentEvents>(entity);
                         if (processor != null)
                             ((IProcessorForAppointmentEvents)processor).OnAppointmentResizing(sender, e);
                         else
                         {
                             Logger.Warn($"AppointmentResizing - No SendCrudNotification processor found for {entity.GetType().Name}.");
                             e.Allow = false;
                         }
                     }
                 });
        }

        private void tabControlExternalEntities_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.PrevPage != null)
                if (e.PrevPage.Controls[0] is IPluginWithActivate)
                    (e.PrevPage.Controls[0] as IPluginWithActivate).Deactivate();

            if (e.Page.Controls[0] is IPluginWithActivate)
                (e.Page.Controls[0] as IPluginWithActivate).Activate();
        }

        private void schedulerControl_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            if (e.Appointment.CustomFields["ENTITY"] == null)
            {
                e.Appointment.CustomFields["ENTITY"] = new StandardAppointment();
            }

            if (e.Appointment.CustomFields["ENTITY"] is IEntityBase)
            {
                IEntityBase entity = e.Appointment.CustomFields["ENTITY"] as IEntityBase;
                IProcessor processor = GetProcessorForEntity<IProcessorForShowAppointmentForm>(entity);
                if (processor != null)
                {
                    ((IProcessorForShowAppointmentForm)processor).ShowAppointmentForm((SchedulerControl)sender, e.Appointment);
                    e.Handled = true;
                }
                else
                    Logger.Error($"No EditAppointmentFormShowing processor found for {entity.GetType().Name}.");
            }
        }

        private void schedulerControl_AllowAppointmentDelete(object sender, AppointmentOperationEventArgs e)
        {
            e.Allow = false;
            RolesAndOrPermissions.ExecuteActionUIOnException(
                roles: DefaultRoles,
                permissions: DefaultPermissions,
                action: () =>
                {
                    e.Allow = true;
                });
        }

        private void schedulerDataStorage_AppointmentDeleting(object sender, PersistentObjectCancelEventArgs e)
        {

        }

        private void schedulerDataStorage_AppointmentsDeleted(object sender, PersistentObjectsEventArgs e)
        {
            foreach (var delItem in e.Objects)
            {
                if (delItem is Appointment)
                {
                    Appointment delAppt = delItem as Appointment;
                    if ((delAppt.CustomFields["STATE"] as string) == "DELETED")
                    {
                        continue;
                    }
                    if (!delAppt.IsBase)
                        continue;

                    if (delAppt.CustomFields["ENTITY"] is IEntityBase)
                    {
                        IEntityBase entity = delAppt.CustomFields["ENTITY"] as IEntityBase;
                        IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentEvents>(entity);
                        if (processor != null)
                            (processor as IProcessorForAppointmentEvents).OnAppointmentsDeleted(sender, delAppt);
                        else
                            Logger.Error($"No AppointmentsDeleted processor found for {entity.GetType().Name}.");
                    }
                }
            }
            //refresh the control
            schedulerControl.BeginInvokeIfRequired(sc =>
            {
                sc.Refresh();
            });
        }

        private void schedulerControl_DeleteRecurrentAppointmentFormShowing(object sender, DeleteRecurrentAppointmentFormEventArgs e)
        {
            if (e.Appointment.CustomFields["ENTITY"] is IEntityBase)
            {
                IEntityBase entity = e.Appointment.CustomFields["ENTITY"] as IEntityBase;
                IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentEvents>(entity);
                if (processor != null)
                {
                    RecurrentAppointmentDeleteForm form = new RecurrentAppointmentDeleteForm(e.Appointment);
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        if (form.QueryResult == RecurrentAppointmentAction.Occurrence)
                        {
                            (processor as IProcessorForAppointmentEvents).OnAppointmentsDeleted(sender, e.Appointment);
                            e.Handled = true;
                        }
                        if (form.QueryResult == RecurrentAppointmentAction.Series)
                        {
                            (processor as IProcessorForAppointmentEvents).OnAppointmentsDeleted(sender, e.Appointment.RecurrencePattern);
                            e.Handled = true;
                        }
                    }
                }
                else
                    Logger.Error($"No AppointmentsDeleted processor found for {entity.GetType().Name}.");
            }
        }

        private void schedulerControl_InitNewAppointment(object sender, AppointmentEventArgs e)
        {
            if (e.Appointment.CustomFields["ENTITY"] == null)
            {
                var stdAppt = new StandardAppointment();
                stdAppt.PopulateWith(e.Appointment)
                    .ThenDo(a => a.StartDate = e.Appointment.Start)
                    .ThenDo(a => a.EndDate = e.Appointment.End);
                e.Appointment.CustomFields["ENTITY"] = new StandardAppointment();
            }
        }

        private void schedulerDataStorage_AppointmentInserting(object sender, PersistentObjectCancelEventArgs e)
        {
            if (e.Object != null && e.Object is Appointment appt)
            {
                //get the processor and execute.
                if (appt.CustomFields["ENTITY"] is IEntityBase)
                {
                    IEntityBase entity = appt.CustomFields["ENTITY"] as IEntityBase;
                    IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentEvents>(entity);

                    if (processor != null)
                        ((IProcessorForAppointmentEvents)processor).OnAppointmentInserting(sender, e);
                }
            }

        }

        private void schedulerDataStorage_AppointmentsInserted(object sender, PersistentObjectsEventArgs e)
        {
            if (e.Objects != null && e.Objects.Count > 0)
                if (e.Objects[0] is Appointment appt)
                {
                    //do the normal appointment update here?
                    if (appt.CustomFields["ENTITY"] is IEntityBase)
                    {
                        IEntityBase entity = appt.CustomFields["ENTITY"] as IEntityBase;
                        IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentEvents>(entity);

                        ////send the notification
                        if (processor != null)
                            ((IProcessorForAppointmentEvents)processor).OnAppointmentsInserted(sender, e);
                    }
                }
        }
        private void schedulerDataStorage_AppointmentChanging(object sender, PersistentObjectCancelEventArgs e)
        {
            if (e.Object != null && e.Object is Appointment appt)
            {
                //get the processor and execute.
                if (appt.CustomFields["ENTITY"] is IEntityBase)
                {
                    IEntityBase entity = appt.CustomFields["ENTITY"] as IEntityBase;
                    IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentEvents>(entity);

                    if (processor != null)
                        ((IProcessorForAppointmentEvents)processor).OnAppointmentChanging(sender, e);
                }
            }
        }

        private void schedulerDataStorage_AppointmentsChanged(object sender, PersistentObjectsEventArgs e)
        {
            if (e.Objects != null && e.Objects.Count > 0)
                if (e.Objects[0] is Appointment appt)
                {
                    if ((appt.CustomFields["STATE"] is string deleteFlag) && !deleteFlag.IsNullOrEmpty() && deleteFlag == "DELETED")
                        return;
                    //get the processor and execute.
                    if (appt.CustomFields["ENTITY"] is IEntityBase)
                    {
                        IEntityBase entity = appt.CustomFields["ENTITY"] as IEntityBase;
                        IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentEvents>(entity);

                        if (processor != null)
                            ((IProcessorForAppointmentEvents)processor).OnAppointmentsChanged(sender, e);
                    }
                }
        }

        private void schedulerControl_AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e)
        {
            //!Use this event to customize the appointment's appearance by modifying the style elements when it is painted
            //!This event enables you to change how the appointment is visualized. Do not modify appointment properties, and 
            //!do not add or remove appointments within this event handler. An attempt to do so may result in an unhandled exception.

            try
            {
                if (ActiveViewOption != null)
                    ActiveViewOption.AppointmentViewInfoCustomizing(sender, e, schedulerDataStorage.Statuses);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void schedulerControl_InitAppointmentImages(object sender, AppointmentImagesEventArgs e)
        {
            try
            {
                if (ActiveViewOption != null)
                    ActiveViewOption.InitAppointmentImages(sender, e, schedulerDataStorage.Statuses);
                
                if (e.Appointment.CustomFields["ENTITY"] is IEntityBase entityBase)
                {                    
                    IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentCustomization>(entityBase);

                    if (processor != null)
                        ((IProcessorForAppointmentCustomization)processor).InitAppointmentImages(sender, e);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
#if DEBUG
                throw ex;
#endif
            }
        }

        private void schedulerControl_InitAppointmentDisplayText(object sender, AppointmentDisplayTextEventArgs e)
        {

        }

        private void schedulerControl_PopupMenuShowing(object sender, DevExpress.XtraScheduler.PopupMenuShowingEventArgs e)
        {
            try
            {
                //see if there is a selected appointment
                if (schedulerControl.SelectedAppointments.Count > 0)
                { }
                if (e.Menu.Id == SchedulerMenuItemId.DefaultMenu)
                {

                }
                if (e.Menu.Id == SchedulerMenuItemId.Custom)
                {

                }
                if (e.Menu.Id == SchedulerMenuItemId.AppointmentMenu)
                {
                    bool isDefault = false;
                    //disable the default items
                    foreach (DXMenuItem popItem in e.Menu.Items)
                    {

                        if (popItem.Caption == "&Label As")
                            popItem.Visible = false;
                        if (popItem.Caption == "&Show Time As")
                            popItem.Visible = false;
                        if (popItem.Caption == "&Restore Default State")
                            popItem.Visible = false;
                    }
                    isDefault = (ActiveViewOption.LinkedToInterfaceType == typeof(IEntityWithStatusLabels));//"Appointment");
                    //make sure the active view option keys are available
                    SchedulerPopupMenu labelMenuItem = new SchedulerPopupMenu
                    {
                        Caption = $"{ActiveViewOption.PluginText} Labels"
                    };
                    e.Menu.Items.Add(labelMenuItem);
                    foreach (var item in ActiveViewOption.SchedulerLabels)
                    {
                        DXMenuItem xMenuItem = isDefault ? new DXMenuItem($"{item.LabelName} {item.LabelCriteria}", DxMenuItem_Click) : new DXMenuItem($"{item.LabelName} {item.LabelCriteria}");
                        xMenuItem.Tag = item;
                        xMenuItem.ImageOptions.Image = GetStatusOrLabelImage(Color.FromArgb(item.ColourRGB));
                        labelMenuItem.Items.Add(xMenuItem);
                    }
                    SchedulerPopupMenu statusMenuItem = new SchedulerPopupMenu
                    {
                        Caption = $"{ActiveViewOption.PluginText} Statuses"
                    };

                    e.Menu.Items.Add(statusMenuItem);
                    foreach (var item in ActiveViewOption.SchedulerStatuses)
                    {
                        DXMenuItem xMenuItem = isDefault ? new DXMenuItem($"{item.StatusName} {item.StatusCriteria}", DxMenuItem_Click) : new DXMenuItem($"{item.StatusName} {item.StatusCriteria}");
                        xMenuItem.ImageOptions.Image = GetStatusOrLabelImage(Color.FromArgb(item.ColourRGB));
                        xMenuItem.Tag = item;
                        statusMenuItem.Items.Add(xMenuItem);
                    }
                }
            }
            catch (Exception popEx)
            {
                Logger.Error(popEx.Message);
#if DEBUG
                throw popEx;
#endif
            }
        }

        private void DxMenuItem_Click(object sender, EventArgs e)
        {
            if (schedulerControl.SelectedAppointments.Count > 0)
                if (schedulerControl.SelectedAppointments[0].CustomFields["ENTITY"] is IEntityWithStatusLabels)
                {
                    IEntityWithStatusLabels slEntity = schedulerControl.SelectedAppointments[0].CustomFields["ENTITY"] as IEntityWithStatusLabels;
                    if (sender is DXMenuItem)
                    {
                        DXMenuItem itm = sender as DXMenuItem;
                        if (itm.Tag is ApptLabel)
                        {
                            slEntity.LabelKey = (itm.Tag as ApptLabel).Id.ToString();
                            schedulerControl.SelectedAppointments[0].LabelKey = slEntity.LabelKey;
                        }
                        if (itm.Tag is ApptStatus)
                        {
                            slEntity.StatusKey = (itm.Tag as ApptStatus).Id.ToString();
                            schedulerControl.SelectedAppointments[0].LabelKey = slEntity.StatusKey;
                        }
                    }
                    IEntityBase entity = schedulerControl.SelectedAppointments[0].CustomFields["ENTITY"] as IEntityBase;
                    IProcessor processor = GetProcessorForEntity<IProcessorForAppointmentEvents>(entity);
                    if (processor != null)
                        ((IProcessorForAppointmentEvents)processor).SaveAppointmentEntity(schedulerControl.SelectedAppointments[0]);
                    schedulerControl.Refresh();
                }
        }

        private Image GetStatusOrLabelImage(Color colr)
        {
            try
            {
                int width = 16, height = 16;
                Rectangle imageRect = new Rectangle(0, 0, width, height);
                Rectangle borderRect = new Rectangle(0, 0, width - 1, height - 1);
                Bitmap slBitmap = new Bitmap(width, height);
                using (Graphics slGraphic = Graphics.FromImage(slBitmap))
                {
                    Pen borderPen = new Pen(Color.Black, 1)
                    {
                        Alignment = PenAlignment.Inset
                    };
                    SolidBrush mainBrush = new SolidBrush(colr);
                    slGraphic.FillRectangle(mainBrush, imageRect);
                    slGraphic.DrawRectangle(borderPen, borderRect);
                }
                return slBitmap;
            }
            catch (Exception ex)
            {
                throw new Exception("GetStatusLabelImage", ex);
            }
        }

        private void iExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void iExitLogout_ItemClick(object sender, ItemClickEventArgs e)
        {
            Authenticator authenticator = new Authenticator(Context.AuthClient);
            authenticator.ForgetUser();
            Close();
        }

        private void iManageAccount_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageBox.Show("ToDo: create user manage account form");
        }

        private async void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!_isInitExecuteAndLoadStateForExternalPluginsLoading)
                await InitExecuteAndLoadStateForExternalPlugins();

            if (_isLoadOrRefreshProcessorsDataLoading)
                await LoadOrRefreshProcessorsDataAsync();
        }

       
    }
}