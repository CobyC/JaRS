using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Tools;
using DevExpress.XtraTab;
using JARS.Core.Attributes;
using JARS.Core.Extensions;
using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.WinForms.Behaviours;
using JARS.Core.WinForms.Extensions;
using JARS.Core.WinForms.Forms;
using JARS.Core.WinForms.Interfaces.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JARS.WinForms.Plugins.Behaviours
{
    [ExportPluginToMainRibbon(typeof(IPluginAsBehaviour), "Show Available Dates", "Behaviours", "Home", "")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ShowAvailableDatesBehaviourPlugin : BehaviourPluginBase, 
        IPluginAsBehaviour, 
        IPluginToDateNavigator, 
        IPluginToSchedulerStorage, 
        IPluginToSchedulerControl, 
        IPluginToExternalTabControl, 
        IPluginWithSettings
    {
        public string PluginText => this.GetPluginTextFromAttributeValue();

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
                        Glyph = Properties.Resources.Available_Dates_16x16
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
                        Caption = PluginText,
                        Glyph = Properties.Resources.Available_Dates_16x16,
                        LargeGlyph = Properties.Resources.Available_Dates_32x32,
                        DropDownControl = popupMenu,
                        ButtonStyle = BarButtonStyle.CheckDropDown,
                        Id = 705
                    };
                    _BarCheckItem.DownChanged += _BarItem_DownChanged;
                }
                return _BarCheckItem;
            }
        }

        public DateNavigator dateNavigator { get; set; }

        public XtraTabControl ExternalTabControl { get; set; }

        public ISchedulerStorage schedulerDataStorage { get; set; }

        public SchedulerControl schedulerControl { get; set; }

        const string TEXT_COLOUR = "TEXT_COLOUR";
        const string FRAME_COLOUR = "FRAME_COLOUR";
        const string DAYS_INTERVAL = "DAYS_INTERVAL";

        Dictionary<string, object> _PluginSettings;
        public Dictionary<string, object> PluginSettings
        {
            get
            {
                if (_PluginSettings == null)
                {
                    _PluginSettings = new Dictionary<string, object>();
                    _PluginSettings.Add(TEXT_COLOUR, Color.Black);
                    _PluginSettings.Add(FRAME_COLOUR, Color.OrangeRed);
                    _PluginSettings.Add(DAYS_INTERVAL, 15);
                }
                return _PluginSettings;
            }
            set
            {
                _PluginSettings = value;
            }
        }

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
            ((BarButtonItem)BarItem).Down = settings["Checked"] != null ? (bool)settings["Checked"] : ((BarButtonItem)BarItem).Down;
            PluginSettings = (settings.ContainsKey("BehaviourSettings") && settings["BehaviourSettings"] != null) ? (Dictionary<string, object>)settings["BehaviourSettings"] : PluginSettings;
            _BarItem_DownChanged(null, new ItemClickEventArgs(BarItem, null));
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

            dateNavigator.CustomDrawDayNumberCell += DateNavigator_CustomDrawDayNumberCell;
            dateNavigator.InvokeIfRequired(dtN =>
            {
                dtN.Invalidate();
            });
        }

        /// <summary>
        /// This method activates the events on a specific control 
        /// </summary>
        /// <param name="mainGrid">The control that will get its events initialized</param>
        private void ActivateControlEvents(GridControl mainGrid)
        {
            if (mainGrid != null)
                if (mainGrid.MainView is GridView mainView)
                    mainView.FocusedRowChanged += ShowAvailableDatesBehaviourPlugin_FocusedRowChanged;
        }

        IEntityBase selectedExternalEntity;
        TimeIntervalCollection _AvailableTimeIntervals;

        TimeZoneHelper _TimeZoneHelper;
        TimeZoneHelper TimeZoneHelper
        {
            get
            {
                if (_TimeZoneHelper == null)
                {
                    _TimeZoneHelper = new TimeZoneHelper(schedulerControl.OptionsBehavior.ClientTimeZoneId);
                }
                return _TimeZoneHelper;
            }
            set
            {
                _TimeZoneHelper = value;
            }
        }

        private void ShowAvailableDatesBehaviourPlugin_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (sender is GridView view)
            {
                if (view.GetRow(view.FocusedRowHandle) is IEntityWithDuration durationEnt) 
                {
                    selectedExternalEntity = durationEnt;
                    //the duration looked for from the selected entity
                    TimeSpan findDuration = TimeSpan.FromHours(durationEnt.Duration);
                    //get the date from when a slot will be looked for
                    DateTime fromStart = dateNavigator.SelectionStart;
                    //Set the wide range interval where an interval will be looked for
                    TimeInterval wideRange = new TimeInterval(fromStart, fromStart.AddDays((int)PluginSettings[DAYS_INTERVAL]));
                    _AvailableTimeIntervals = FindFreeIntervals(wideRange, findDuration);
                    dateNavigator.InvokeIfRequired(dtN =>
                    {
                        dtN.Invalidate();
                    });
                }
            }
        }

        FreeTimeCalculator _FreeTimeCalculator;
        internal FreeTimeCalculator FreeTimeCalculator
        {
            get
            {
                if (_FreeTimeCalculator is null)
                {
                    _FreeTimeCalculator = new FreeTimeCalculator(schedulerDataStorage);
                    _FreeTimeCalculator.IntervalFound += FreeTimeCalc_IntervalFound;
                }
                return _FreeTimeCalculator;
            }
            private set
            {
                _FreeTimeCalculator = value;
            }
        }

        TimeIntervalCollection FindFreeIntervals(TimeInterval wideRange, TimeSpan findDuration)
        {
            //FreeTimeCalculator freeTimeCalc = new FreeTimeCalculator(schedulerDataStorage);
            //assign the event handler that will be raised once an interval has been found.
            //FreeTimeCalculator.IntervalFound += FreeTimeCalc_IntervalFound;
            TimeInterval interval = TimeZoneHelper.FromClientTime(wideRange);

            //var firstAvailableTime = freeTimeCalc.FindFreeTimeInterval(interval,findDuration,true);
            return FreeTimeCalculator.CalculateFreeTime(interval);
        }

        private void FreeTimeCalc_IntervalFound(object sender, IntervalFoundEventArgs e)
        {
            TimeIntervalCollectionEx freeIntervals = e.FreeIntervals;
            DateTime start = freeIntervals.Start.Date.AddDays(-1);
            DateTime end = freeIntervals.End;
            while (start < end)
            {
                RemoveNonWorkingTime(freeIntervals, start);
                RemoveNonWorkingDay(freeIntervals, start);
                start += TimeSpan.FromDays(1);
            }
        }

        TimeOfDayInterval nonWorkingTime = new TimeOfDayInterval(TimeSpan.FromHours(18), TimeSpan.FromDays(1) + TimeSpan.FromHours(9));
        internal TimeOfDayInterval NonWorkingTime { get { return nonWorkingTime; } set { nonWorkingTime = value; } }

        private void RemoveNonWorkingTime(TimeIntervalCollectionEx freeIntervals, DateTime date)
        {
            DateTime clientDate = TimeZoneHelper.ToClientTime(date).Date;

            TimeInterval clientNonWorkingTime = new TimeInterval(clientDate + NonWorkingTime.Start, clientDate + NonWorkingTime.End);
            freeIntervals.Remove(TimeZoneHelper.FromClientTime(clientNonWorkingTime));
        }
        private void RemoveNonWorkingDay(TimeIntervalCollectionEx freeIntervals, DateTime date)
        {
            DateTime clientDate = TimeZoneHelper.ToClientTime(date).Date;
            bool isWorkDay = schedulerControl.WorkDays.IsWorkDay(clientDate);
            if (!isWorkDay)
            {
                TimeInterval clientInterval = new TimeInterval(clientDate, TimeSpan.FromDays(1));
                freeIntervals.Remove(TimeZoneHelper.FromClientTime(clientInterval));
            }
        }

        private void DateNavigator_CustomDrawDayNumberCell(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            try
            {
                if (_AvailableTimeIntervals != null && selectedExternalEntity is IEntityWithDuration durationEnt)
                { 
                    TimeInterval timeInt = _AvailableTimeIntervals.FirstOrDefault(d => d.Start.Date == e.Date && d.Duration >= TimeSpan.FromHours(durationEnt.Duration));
                    if (timeInt != null)
                    {
                        RectangleF rect = new RectangleF(e.Bounds.Location, e.Bounds.Size);
                        e.Style.ForeColor = (Color)PluginSettings[TEXT_COLOUR];
                        Color backColor = (Color)(PluginSettings[FRAME_COLOUR]);
                        rect.Inflate(-2, -2);
                        e.Graphics.DrawRectangles(new Pen(backColor) { Width = 3 }, new[] { rect });
                        e.Graphics.DrawString(e.Date.Day.ToString(), new Font(e.Style.Font, FontStyle.Bold), new SolidBrush(e.Style.ForeColor), rect, e.Style.GetStringFormat());
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

            FreeTimeCalculator.IntervalFound -= FreeTimeCalc_IntervalFound;
            dateNavigator.CustomDrawDayNumberCell -= DateNavigator_CustomDrawDayNumberCell;
            if (_AvailableTimeIntervals != null)
                _AvailableTimeIntervals.Clear();
            _AvailableTimeIntervals = null;
            dateNavigator.InvokeIfRequired(dtN =>
            {
                dtN.Invalidate();
            });

        }


        void DeactivateControlEvents(GridControl mainGrid)
        {
            if (mainGrid != null)
                if (mainGrid.MainView is GridView mainView)
                    mainView.FocusedRowChanged -= ShowAvailableDatesBehaviourPlugin_FocusedRowChanged;

        }
    }
}
