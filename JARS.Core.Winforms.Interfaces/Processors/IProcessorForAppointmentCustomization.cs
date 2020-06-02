using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.WinForms.Interfaces.Processors
{

    /// <summary>
    /// This interface enables the customization of the appearance of the appointment.
    /// </summary>
    public interface IProcessorForAppointmentCustomization
    {
        /// <summary>
        /// Use this event to customize the appointment's appearance by modifying the style elements when it is painted
        ///!This event enables you to change how the appointment is visualized. Do not modify appointment properties, and 
        ///!do not add or remove appointments within this event handler. An attempt to do so may result in an unhandled exception.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e);

        /// <summary>
        /// Change the way the text is displayed on the appointment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InitAppointmentDisplayText(object sender, AppointmentDisplayTextEventArgs e);

        /// <summary>
        /// show any images on teh appointment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InitAppointmentImages(object sender, AppointmentImagesEventArgs e);


        /// <summary>
        /// Customize the way the flyout appear.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CustomizeAppointmentFlyout(object sender, CustomizeAppointmentFlyoutEventArgs e);

        /// <summary>
        /// This can be used to show a custom flyout if needed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AppointmentFlyoutShowing(object sender, AppointmentFlyoutShowingEventArgs e);

    }
}
