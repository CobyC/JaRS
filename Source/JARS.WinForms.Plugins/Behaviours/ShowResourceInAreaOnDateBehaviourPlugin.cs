using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraScheduler;
using DevExpress.XtraTab;
using JARS.Core.Attributes;
using JARS.Core.Entities;
using JARS.Core.Extensions;
using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.WinForms.Behaviours;
using JARS.Core.WinForms.Extensions;
using JARS.Core.WinForms.Forms;
using JARS.Core.WinForms.Interfaces.Plugins;
using JARS.Core.WinForms.Plugins;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JARS.WinForms.Plugins.Behaviours
{
    /// <summary>
    /// This plugin highlights the dates where the resource is in a particular area.
    /// It uses the active selected record from the external control, looks at the area code and looks for appointments made in the same area as the selected records area code.
    /// This plugin also has rules linked to it that can be managed in the configuration section
    /// </summary>
    [ExportPluginToMainRibbon(typeof(IPluginAsBehaviour), "Show Resource Location On Dates", "Behaviours", "Home", "")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ShowResourceInAreaOnDateBehaviourPlugin : BehaviourPluginBase, IPluginAsBehaviour, IPluginToDateNavigator, IPluginToSchedulerStorage, IPluginToExternalTabControl, IPluginWithSettings
    {

        PopupMenu _popupMenu;
        public PopupMenu popupMenu
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
                        Glyph = Properties.Resources.Resource_in_Area_32x32
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

        BarButtonItem _BarCheckItem;
        public BarItem BarItem
        {
            get
            {
                if (_BarCheckItem == null)
                {
                    _BarCheckItem = new BarButtonItem()
                    {
                        ButtonStyle = BarButtonStyle.CheckDropDown,
                        DropDownControl = popupMenu,
                        Caption = PluginText,
                        Glyph = Properties.Resources.Resource_in_Area_16x16,
                        LargeGlyph = Properties.Resources.Resource_in_Area_32x32,
                        Id = 705,

                    };
                    _BarCheckItem.DownChanged += _BarCheckItem_DownChangedChanged;
                }
                return _BarCheckItem;
            }
        }

        private void _BarCheckItem_DownChangedChanged(object sender, ItemClickEventArgs e)
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


        public string PluginText => this.GetPluginTextFromAttributeValue();

        const string LEVEL1_MATCH_LENGTH = "LEVEL1_MATCH_LENGTH";
        const string LEVEL1_MATCH_COLOUR = "LEVEL1_MATCH_COLOUR";
        const string LEVEL1_MATCH_ENABLED = "LEVEL1_MATCH_ENABLED";
        const string LEVEL2_MATCH_LENGTH = "LEVEL2_MATCH_LENGTH";
        const string LEVEL2_MATCH_COLOUR = "LEVEL2_MATCH_COLOUR";
        const string LEVEL2_MATCH_ENABLED = "LEVEL2_MATCH_ENABLED";
        const string LEVEL3_MATCH_LENGTH = "LEVEL3_MATCH_LENGTH";
        const string LEVEL3_MATCH_COLOUR = "LEVEL3_MATCH_COLOUR";
        const string LEVEL3_MATCH_ENABLED = "LEVEL3_MATCH_ENABLED";
        const string LEVEL4_MATCH_LENGTH = "LEVEL4_MATCH_LENGTH";
        const string LEVEL4_MATCH_COLOUR = "LEVEL4_MATCH_COLOUR";
        const string LEVEL4_MATCH_ENABLED = "LEVEL4_MATCH_ENABLED";

        Dictionary<string, object> _PluginSettings;
        public Dictionary<string, object> PluginSettings
        {
            get
            {
                if (_PluginSettings == null)
                {
                    _PluginSettings = new Dictionary<string, object>();
                    _PluginSettings.Add(LEVEL1_MATCH_LENGTH, 2);
                    _PluginSettings.Add(LEVEL1_MATCH_COLOUR, Color.Green);
                    _PluginSettings.Add(LEVEL1_MATCH_ENABLED, true);
                    _PluginSettings.Add(LEVEL2_MATCH_LENGTH, 3);
                    _PluginSettings.Add(LEVEL2_MATCH_COLOUR, Color.GreenYellow);
                    _PluginSettings.Add(LEVEL2_MATCH_ENABLED, true);
                    _PluginSettings.Add(LEVEL3_MATCH_LENGTH, 4);
                    _PluginSettings.Add(LEVEL3_MATCH_COLOUR, Color.LimeGreen);
                    _PluginSettings.Add(LEVEL3_MATCH_ENABLED, true);
                    _PluginSettings.Add(LEVEL4_MATCH_LENGTH, 5);
                    _PluginSettings.Add(LEVEL4_MATCH_COLOUR, Color.MediumSeaGreen);
                    _PluginSettings.Add(LEVEL4_MATCH_ENABLED, true);
                }
                return _PluginSettings;
            }
            set
            {
                _PluginSettings = value;
            }
        }

        public DateNavigator dateNavigator { get; set; }

        public ISchedulerStorage schedulerDataStorage { get; set; }

        public XtraTabControl ExternalTabControl { get; set; }

        IEntityBase selectedExternalEntity;

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

            dateNavigator.CustomDrawDayNumberCell += DateNavigator_CustomDrawDayNumberCell;
            dateNavigator.InvokeIfRequired(dtN =>
            {
                dtN.Invalidate();
            });
        }

        private void ActivateControlEvents(GridControl mainGrid)
        {
            if (mainGrid != null)
                if (mainGrid.MainView is GridView mainView)
                {
                    if (mainView.GetRow(mainView.FocusedRowHandle) is IEntityBase extEntity)
                    {
                        selectedExternalEntity = extEntity;
                        mainView.FocusedRowChanged += ShowResourceInAreaOnDateBehaviourPlugin_FocusedRowChanged;
                    }

                }
        }

        private void ShowResourceInAreaOnDateBehaviourPlugin_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (sender is GridView view)
            {
                if (view.GetRow(view.FocusedRowHandle) is IEntityBase focusedEnt)
                {
                    selectedExternalEntity = focusedEnt;
                    dateNavigator.Invalidate();
                }
            }
        }

        private void DateNavigator_CustomDrawDayNumberCell(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            //draw a rectangle around the date where a match is found
            if (selectedExternalEntity is IEntityWithLocation entityWithLocation)
            {
                var visibleResources = schedulerDataStorage.Resources.Items.Where(r => r.Visible == true);
                var ApptList = schedulerDataStorage.Appointments.Items.Where(a => (a.CustomFields["ENTITY"] is IEntityWithLocation apptEntity)
                && apptEntity.Location != null
                && apptEntity.LocationCode != null
                && visibleResources.FirstOrDefault(res => $"{res.Id}" == $"{a.ResourceId}"
                && a.Start.Date == e.Date) != null);

                if (ApptList.Count() > 0)
                {
                    int lvl1Cut = (int)PluginSettings[LEVEL1_MATCH_LENGTH];
                    Color lvl1Col = (Color)PluginSettings[LEVEL1_MATCH_COLOUR];
                    bool lvl1Enbl = (bool)PluginSettings[LEVEL1_MATCH_ENABLED];
                    int lvl2Cut = (int)PluginSettings[LEVEL2_MATCH_LENGTH];
                    Color lvl2Col = (Color)PluginSettings[LEVEL2_MATCH_COLOUR];
                    bool lvl2Enbl = (bool)PluginSettings[LEVEL2_MATCH_ENABLED];
                    int lvl3Cut = (int)PluginSettings[LEVEL3_MATCH_LENGTH];
                    Color lvl3Col = (Color)PluginSettings[LEVEL3_MATCH_COLOUR];
                    bool lvl3Enbl = (bool)PluginSettings[LEVEL3_MATCH_ENABLED];
                    int lvl4Cut = (int)PluginSettings[LEVEL4_MATCH_LENGTH];
                    Color lvl4Col = (Color)PluginSettings[LEVEL4_MATCH_COLOUR];
                    bool lvl4Enbl = (bool)PluginSettings[LEVEL4_MATCH_ENABLED];
                    //match on level 1

                    if (entityWithLocation.LocationCode == null || entityWithLocation.LocationCode == string.Empty)
                    {
                        e.Handled = true;
                        return;
                    }

                    if (lvl1Enbl)
                        if (ApptList.FirstOrDefault(a => (a.CustomFields["ENTITY"] as IEntityWithLocation).LocationCode.StartsWith(entityWithLocation.LocationCode.Substring(0, lvl1Cut))) != null)
                        {
                            RectangleF rect = new RectangleF(e.Bounds.Location, e.Bounds.Size);
                            e.Style.ForeColor = Color.DarkSlateGray;
                            Color backColor = lvl1Col;
                            rect.Inflate(-4, -4);
                            e.Graphics.FillRectangle(new SolidBrush(backColor), rect);
                            e.Graphics.DrawString(e.Date.Day.ToString(), new Font(e.Style.Font, FontStyle.Bold), new SolidBrush(e.Style.ForeColor), rect, e.Style.GetStringFormat());
                            e.Handled = true;
                        }

                    if (lvl2Enbl)
                        if (ApptList.FirstOrDefault(a => (a.CustomFields["ENTITY"] as IEntityWithLocation).LocationCode.StartsWith(entityWithLocation.LocationCode.Substring(0, lvl2Cut))) != null)
                        {
                            RectangleF rect = new RectangleF(e.Bounds.Location, e.Bounds.Size);
                            e.Style.ForeColor = Color.DarkSlateGray;
                            Color backColor = lvl2Col;
                            rect.Inflate(-4, -4);
                            e.Graphics.FillRectangle(new SolidBrush(backColor), rect);
                            e.Graphics.DrawString(e.Date.Day.ToString(), new Font(e.Style.Font, FontStyle.Bold), new SolidBrush(e.Style.ForeColor), rect, e.Style.GetStringFormat());
                            e.Handled = true;
                        }

                    if (lvl3Enbl)
                        if (ApptList.FirstOrDefault(a => (a.CustomFields["ENTITY"] as IEntityWithLocation).LocationCode.StartsWith(entityWithLocation.LocationCode.Substring(0, lvl3Cut))) != null)
                        {
                            RectangleF rect = new RectangleF(e.Bounds.Location, e.Bounds.Size);
                            e.Style.ForeColor = Color.DarkSlateGray;
                            Color backColor = lvl3Col;
                            rect.Inflate(-4, -4);
                            e.Graphics.FillRectangle(new SolidBrush(backColor), rect);
                            e.Graphics.DrawString(e.Date.Day.ToString(), new Font(e.Style.Font, FontStyle.Bold), new SolidBrush(e.Style.ForeColor), rect, e.Style.GetStringFormat());
                            e.Handled = true;
                        }
                    if (lvl4Enbl)
                        if (ApptList.FirstOrDefault(a => (a.CustomFields["ENTITY"] as IEntityWithLocation).LocationCode.StartsWith(entityWithLocation.LocationCode.Substring(0, lvl4Cut))) != null)
                        {
                            RectangleF rect = new RectangleF(e.Bounds.Location, e.Bounds.Size);
                            e.Style.ForeColor = Color.DarkSlateGray;
                            Color backColor = lvl4Col;
                            rect.Inflate(-4, -4);
                            e.Graphics.FillRectangle(new SolidBrush(backColor), rect);
                            e.Graphics.DrawString(e.Date.Day.ToString(), new Font(e.Style.Font, FontStyle.Bold), new SolidBrush(e.Style.ForeColor), rect, e.Style.GetStringFormat());
                            e.Handled = true;
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

            dateNavigator.CustomDrawDayNumberCell -= DateNavigator_CustomDrawDayNumberCell;
            dateNavigator.InvokeIfRequired(dtN =>
            {
                dtN.Invalidate();
            });
        }

        void DeactivateControlEvents(GridControl mainGrid)
        {
            if (mainGrid != null)
                if (mainGrid.MainView is GridView mainView)
                    mainView.FocusedRowChanged -= ShowResourceInAreaOnDateBehaviourPlugin_FocusedRowChanged;

        }

        public byte[] GetStateInformation()
        {
            Dictionary<string, object> settings = new Dictionary<string, object>
            {
                { "Checked", ((BarButtonItem)BarItem).Down },
                {"BehaviourSettings",PluginSettings }
            };
            return this.SerializeAndCompressStateInformation(settings);
        }

        public void LoadStateInformation(byte[] stateInfo)
        {
            Dictionary<string, object> settings = this.DeserializeAndDecompressStateInformation(stateInfo);
            ((BarButtonItem)BarItem).Down = settings["Checked"] != null ? (bool)settings["Checked"] : ((BarButtonItem)BarItem).Down;
            PluginSettings = settings["BehaviourSettings"] != null ? (Dictionary<string, object>)settings["BehaviourSettings"] : PluginSettings;
            //_BarItem_CheckedChanged(null, new ItemClickEventArgs(BarCheckItem, null));
        }
    }
}
