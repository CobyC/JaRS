using DevExpress.XtraBars;
using DevExpress.XtraScheduler;
using JARS.Core.Interfaces.Plugins;
using JARS.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace JARS.Core.WinForms.Interfaces.Plugins
{
    /// <summary>
    /// This interface is used to enable the customization of 
    /// </summary>
    public interface IPluginAsViewOption :
        IPluginWinForms,
        IPluginWithStateInfo
    {
        Type LinkedToInterfaceType { get; }

        IList<ApptLabel> SchedulerLabels { get; set; }        

        IList<ApptStatus> SchedulerStatuses { get; set; }        

        BarCheckItem BarCheckItem { get; }

        event EventHandler Selected;
        event EventHandler Unselected;

        ApptStatus GetApptStatus(DataTable table);
        ApptLabel GetApptLabel(DataTable table);

        /// <summary>
        /// Apply the view options for the appointment being drawn
        /// </summary>
        /// <param name="e">the appointment information currently being drawn.</param>
        void AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e, AppointmentStatusDataStorage statuses);

        /// <summary>
        /// Custom draw the appointment, this method is called after the AppointmentViewInfoCustomizing event
        /// </summary>
        /// <param name="sender">where the event was triggered from</param>
        /// <param name="e">the event handler containing the parameters of the event</param>
        void InitAppointmentImages(object sender, AppointmentImagesEventArgs e, AppointmentStatusDataStorage statuses);
    }
}
