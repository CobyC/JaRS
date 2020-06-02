using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraScheduler;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using JARS.Core.Attributes;
using JARS.Core.Extensions;
using JARS.Core.Interfaces.Entities;
using JARS.Core.WinForms.Behaviours;
using JARS.Core.WinForms.Forms;
using JARS.Core.WinForms.Interfaces.Plugins;
using JARS.Core.WinForms.Plugins;
using JARS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace JARS.WinForms.Plugins.Behaviours
{
    [ExportPluginToMainRibbon(typeof(IPluginAsBehaviour), "Auto Filter by Line of Work", "Behaviours", "Home", "")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FilterByWorkCodeBehaviourPlugin : BehaviourPluginBase, IPluginAsBehaviour, IPluginToSchedulerStorage, IPluginToResourceTree, IPluginToExternalTabControl
    {
        public string PluginText => this.GetPluginTextFromAttributeValue();

        PopupMenu _popupMenu;
        public PopupMenu PopupMenu
        {
            get
            {
                if (_popupMenu == null)
                {
                    _popupMenu = new PopupMenu();
                    var _menuItem = new BarButtonItem
                    {
                        Caption = "Settings",
                        Name = "baritmSettings",
                        Glyph = Properties.Resources.Filter_LineOfWork_16x16
                    };
                    _menuItem.ItemClick += _menuItem_ItemClick;
                    _popupMenu.AddItem(_menuItem);

                }
                return _popupMenu;
            }
        }

        private void _menuItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            PluginSettings = PluginSettingsForm.ShowSettings(PluginSettings);
        }

        BarButtonItem _BarBtnItem;
        public BarItem BarItem
        {
            get
            {
                if (_BarBtnItem == null)
                {
                    _BarBtnItem = new BarButtonItem()
                    {
                        Caption = PluginText,
                        Glyph = Properties.Resources.Filter_LineOfWork_16x16,
                        LargeGlyph = Properties.Resources.Filter_LineOfWork_32x32,
                        DropDownControl = PopupMenu,
                        ButtonStyle = BarButtonStyle.CheckDropDown,
                        Id = 706
                    };
                    _BarBtnItem.DownChanged += _BarItem_DownChanged;
                }
                return _BarBtnItem;
            }
        }

        public XtraTabControl ExternalTabControl { get; set; }
        public ISchedulerStorage schedulerDataStorage { get; set; }
        public TreeList resourceTree { get; set; }


        private void _BarItem_DownChanged(object sender, ItemClickEventArgs e)
        {
            BarButtonItem item = BarItem as BarButtonItem;
            if (item.Down)
            {
                //set things here that needs to happen
                //rase the select event if not null
                OnActivated(sender, e);
                Activate();
            }
            else
            {
                //unload the things (disable things)
                OnDeactivated(sender, e);
                Deactivate();
            }
        }

        const string EXPAND_COLLAPS_NODES = "EXPAND_COLLAPS_NODES";


        Dictionary<string, object> _PluginSettings;
        public Dictionary<string, object> PluginSettings
        {
            get
            {
                if (_PluginSettings == null)
                {
                    _PluginSettings = new Dictionary<string, object>();
                    _PluginSettings.Add(EXPAND_COLLAPS_NODES, false);

                }
                return _PluginSettings;
            }
            set
            {
                _PluginSettings = value;
            }
        }

        public byte[] GetStateInformation()
        {
            Dictionary<string, object> settings = new Dictionary<string, object>
            {
                { "Checked", ((BarButtonItem)BarItem).Down },
                { "BehaviourSettings", PluginSettings}
            };
            return this.SerializeAndCompressStateInformation(settings);
        }

        public void LoadStateInformation(byte[] stateInfo)
        {
            Dictionary<string, object> settings = this.DeserializeAndDecompressStateInformation(stateInfo);
            ((BarButtonItem)BarItem).Down = (settings.ContainsKey("Checked") && settings["Checked"] != null) ? (bool)settings["Checked"] : ((BarButtonItem)BarItem).Down;
            PluginSettings = (settings.ContainsKey("BehaviourSettings") && settings["BehaviourSettings"] != null) ? (Dictionary<string, object>)settings["BehaviourSettings"] : PluginSettings;
            //_BarItem_CheckedChanged(null, new ItemClickEventArgs(BarCheckItem, null));
        }

        public void Activate()
        {
            if (ExternalTabControl != null)
                foreach (XtraTabPage parentTab in ExternalTabControl.Controls)
                {
                    var basePlugin = FindUserControlBasePlugin(parentTab);
                    foreach (Control control in basePlugin.Controls)
                    {
                        GridControl mainEventControl = FindMainEventControl<GridControl>(control);
                        ActivateControlEvents(mainEventControl);
                    }
                }
        }

        /// <summary>
        /// This method activates the events on a specific control 
        /// </summary>
        /// <param name="mainGrid">The control that will get its events initialized</param>
        private void ActivateControlEvents(GridControl mainGrid)
        {
            if (mainGrid != null)
                if (mainGrid.MainView is GridView mainView)
                    mainView.FocusedRowChanged += FilterByWorkCodeBehaviourPlugin_FocusedRowChanged;
        }

        private void FilterByWorkCodeBehaviourPlugin_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //MessageBox.Show("Grid View!!");
            if (sender != null && sender is GridView)
            {
                GridView view = sender as GridView;

                if (view.GetRow(view.FocusedRowHandle) is IEntityWithLineOfWork workEntity)
                {                    
                    try
                    {
                        bool isGroupSorted = false;
                        //determine what sorting has been applied to the tree.
                        if (resourceTree.Nodes[0].Tag is JarsResourceGroup)
                            isGroupSorted = true;

                        schedulerDataStorage.BeginUpdate();
                        resourceTree.BeginUpdate();

                        List<TreeListNode> visibleResourcesNodes = (!isGroupSorted) ? resourceTree.NodesIterator.All.Where(n => n.Tag is JarsResource && (((JarsResource)n.Tag).Groups.FirstOrDefault(g => g.Code == workEntity.LineOfWork)) != null).ToList()
                            : resourceTree.NodesIterator.All.Where(n => n.Tag is JarsResourceGroup && ((JarsResourceGroup)n.Tag).Code == workEntity.LineOfWork).ToList();

                        List<TreeListNode> allResourcesNodes = resourceTree.NodesIterator.All.Where(n => n.Tag is JarsResource).ToList();
                        //hide all resources by default
                        foreach (TreeListNode treeNode in allResourcesNodes)
                        {
                            treeNode.CheckState = CheckState.Unchecked;
                            var res = schedulerDataStorage.GetResourceById((treeNode.Tag as JarsResource).Id);
                            res.Visible = false;
                            if (treeNode.ParentNode != null)
                                if (treeNode.ParentNode.Nodes.Where(n => n.CheckState == CheckState.Checked).Count() == 0)
                                {
                                    treeNode.ParentNode.CheckState = CheckState.Unchecked;
                                    if ((bool)PluginSettings[EXPAND_COLLAPS_NODES] == true)
                                        treeNode.ParentNode.Collapse();

                                }
                                else
                                {
                                    if ((bool)PluginSettings[EXPAND_COLLAPS_NODES] == true)
                                        treeNode.ParentNode.Expand();
                                }
                        }

                        //now iterate through the nodes that should be made visible
                        foreach (TreeListNode treeNode in visibleResourcesNodes)
                        {
                            treeNode.CheckState = CheckState.Checked;
                            if (isGroupSorted)//the parent nodes are groups and not resources
                            {
                                foreach (TreeListNode childNode in treeNode.Nodes)
                                {
                                    childNode.CheckState = CheckState.Checked;
                                    var res = schedulerDataStorage.GetResourceById((childNode.Tag as JarsResource).Id);
                                    res.Visible = true;
                                    if ((bool)PluginSettings[EXPAND_COLLAPS_NODES] == true)
                                        childNode.ParentNode.Expand();
                                }
                            }
                            else
                            {
                                var res = schedulerDataStorage.GetResourceById((treeNode.Tag as JarsResource).Id);
                                res.Visible = true;
                                if (treeNode.ParentNode != null)
                                    treeNode.ParentNode.CheckState = CheckState.Checked;
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    finally
                    {
                        schedulerDataStorage.EndUpdate();
                        resourceTree.EndUpdate();
                    }

                }
            }
        }

        public void Deactivate()
        {
            if (ExternalTabControl != null)
                foreach (XtraTabPage parentTab in ExternalTabControl.Controls)
                {
                    var basePlugin = FindUserControlBasePlugin(parentTab);
                    foreach (Control control in basePlugin.Controls)
                    {
                        GridControl mainEventControl = FindMainEventControl<GridControl>(control);
                        DeactivateControlEvents(mainEventControl);
                    }
                }
        }

        void DeactivateControlEvents(GridControl mainGrid)
        {
            if (mainGrid != null)
            {
                if (mainGrid.MainView is GridView mainView)
                {
                    mainView.FocusedRowChanged -= FilterByWorkCodeBehaviourPlugin_FocusedRowChanged;
                }
            }
        }
    }
}
