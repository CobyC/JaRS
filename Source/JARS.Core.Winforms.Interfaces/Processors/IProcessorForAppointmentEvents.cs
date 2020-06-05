using DevExpress.XtraScheduler;
using JARS.Core.Interfaces.Processors;

namespace JARS.Core.WinForms.Interfaces.Processors
{
    public interface IProcessorForAppointmentEvents : IProcessor
    {
        /// <summary>
        /// This can be used as a switch when the OnAppointmentChanged method is reached.
        /// Set it true or false according to what is allowed or not.
        /// by default this doesn't do anything, it's only a hint on how to prevent multiple calls to the Server.
        /// </summary>
        bool AllowServerPost { get; set; }

        /// <summary>
        /// This is raised by the scheduler data storage when an appointment is being inserted.
        /// This is mainly to determine if an appointment can/ should be inserted.
        /// See the DevExpress documentation https://documentation.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerDataStorage.AppointmentInserting.event
        /// </summary>
        /// <param name="sender">the calling sender</param>
        /// <param name="e">the persistent events that can be used to cancel the insertion</param>
        void OnAppointmentInserting(object sender, PersistentObjectCancelEventArgs e);

        /// <summary>
        /// This is hit after an appointment has been inserted.
        /// NB! Appointment data should not be modified here!!
        /// See the DevExpress notes for more details https://documentation.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerDataStorage.AppointmentsInserted.event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAppointmentsInserted(object sender, PersistentObjectsEventArgs e);

        /// <summary>
        /// This is hit just before an appointment property is changed.
        /// NB! Appointment data should not be modified here.
        /// see DevExpress details https://documentation.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerDataStorage.AppointmentChanging.event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAppointmentChanging(object sender, PersistentObjectCancelEventArgs e);

        /// <summary>
        /// This is hit after the properties of an appointment has changed.
        /// NB! Do not set appointment properties in this event
        /// See DevExpress documentation https://documentation.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerDataStorage.AppointmentsChanged.event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAppointmentsChanged(object sender, PersistentObjectsEventArgs e);

        /// <summary>
        /// This is hit after the appointment has been deleted.
        /// NB Do not set Appointment properties in this method.
        /// See DevExpress documentation https://documentation.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerDataStorage.AppointmentsDeleted.event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="deleteAppointment"></param>
        void OnAppointmentsDeleted(object sender, Appointment deleteAppointment);

        /// <summary>
        /// This is where the implementation for saving the event linked to the appointment is made.
        /// The <see cref="AllowServerPost"/> can be used here to prevent posting to the server, as long as it has been set elsewhere.
        /// </summary>
        /// <param name="editedAppointment"></param>
        void SaveAppointmentEntity(Appointment editedAppointment);

        /// <summary>
        /// This gets hit when an appointment is being resized. the caller will be the SchedulerControl.
        /// DevExpress documentation https://documentation.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.AppointmentResizing.event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAppointmentResizing(object sender, AppointmentResizeEventArgs e);

        /// <summary>
        /// This is hit after the appointment has been resized, the caller will be the scheduler control.
        /// DevExpress Documentation https://documentation.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.AppointmentResized.event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAppointmentResized(object sender, AppointmentResizeEventArgs e);

        /// <summary>
        /// This is hit after an appointment has been dropped.
        /// DevExpress documentation https://documentation.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.AppointmentDrop.event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAppointmentDrop(object sender, AppointmentDragEventArgs e);

        /// <summary>
        /// This is hit as the appointment gets dragged.
        /// DevExpress documentation https://documentation.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.AppointmentDrag.event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAppointmentDrag(object sender, AppointmentDragEventArgs e);

        /// <summary>
        /// Can determine if the in place editor should be shown or not.
        /// DevExpress documentation https://documentation.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.AllowInplaceEditor.event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnAllowInplaceEditor(object sender, AppointmentOperationEventArgs e);

        /// <summary>
        /// This gets hit if the inplace editor was allowed to show.
        /// DevExpress documentation https://documentation.devexpress.com/WindowsForms/DevExpress.XtraScheduler.SchedulerControl.InplaceEditorShowing.event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnInplaceEditorShowing(object sender, InplaceEditorEventArgs e);
    }
}
