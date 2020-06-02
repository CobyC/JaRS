using DevExpress.XtraScheduler;
using JARS.Core.Attributes;
using JARS.Core.Client.Processors;
using JARS.Core.Rules;
using JARS.Core.WinForms.Interfaces.Plugins;
using JARS.Core.WinForms.Interfaces.Processors;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JARS.WinForms.Plugins
{
    /// <summary>
    /// This example shows how to link to the AppointmentEdit form
    /// The Plugin takes the type of IJarsPluginAppointmentForm and the contract name is the name of 
    /// the entity the appointment is directly or indirectly linked to.
    /// </summary>
    [ExportProcessor(typeof(InterfaceRules))]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    public class InterfaceRulesProcessor : ProcessorBase,
    IJarsProcessorForLoadingEntityData<InterfaceRules>,
    IProcessorForEventServiceCommandReceived,
    IProcessorForAppointmentEvents,
    IProcessorForAppointmentCustomization
    {
        public bool AllowServerPost { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<InterfaceRules> DataList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool OnDeleteCommandReceived(Control control, ServerEventMessage seMsg)
        {
            return true;
        }

        public bool OnStoreCommandReceived(Control control, ServerEventMessage seMsg)//JarsSyncStoreEvent<IEntityBase> syncEvent)
        {
            return true;
        }

        public bool OnAnyCommandReceived(Control control, ServerEventMessage msg)
        {
            return true;
        }

        public void OnAppointmentInserting(object sender, PersistentObjectCancelEventArgs e)
        {

        }

        public void OnAppointmentsInserted(object sender, PersistentObjectsEventArgs e)
        {

        }

        public void OnAppointmentChanging(object sender, PersistentObjectCancelEventArgs e)
        {

        }

        public void OnAppointmentsChanged(object sender, PersistentObjectsEventArgs e)
        {

        }

        public void OnAppointmentsDeleted(object sender, Appointment deleteAppointment)
        {

        }

        public void SaveAppointmentEntity(Appointment editedAppointment)
        {

        }

        public void OnAppointmentResizing(object sender, AppointmentResizeEventArgs e)
        {

        }

        public void OnAppointmentResized(object sender, AppointmentResizeEventArgs e)
        {

        }

        public void OnAppointmentDrop(object sender, AppointmentDragEventArgs e)
        {

        }

        public void OnAppointmentDrag(object sender, AppointmentDragEventArgs e)
        {

        }

        public void OnAllowInplaceEditor(object sender, AppointmentOperationEventArgs e)
        {

        }

        public void OnInplaceEditorShowing(object sender, InplaceEditorEventArgs e)
        {

        }

        public Task LoadOrRefreshEntityDataAsync(bool isRefresh = false)
        {
            return Task.Run(() => { return true; });
        }

        public void LoadOrRefreshEntityData(SchedulerControl schedulerControl)
        {

        }

        public void AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e)
        {

        }

        public void InitAppointmentDisplayText(object sender, AppointmentDisplayTextEventArgs e)
        {

        }

        public void InitAppointmentImages(object sender, AppointmentImagesEventArgs e)
        {
            //if (e.Appointment.CustomFields["ENTITY"] is IEntityWithNotes)
            //{
            //    AppointmentImageInfo apptImgInfo = e.ImageInfoList.Find(im => (im.Image.Tag as string) == "note");
            //    if (apptImgInfo == null)
            //    {
            //        var newImg = Properties.Resources.ActivityLogs_16x16;
            //        newImg.Tag = "note";
            //        apptImgInfo = new AppointmentImageInfo() { Image = newImg, Visible = false };
            //        e.ImageInfoList.Add(apptImgInfo);
            //    }
            //    apptImgInfo.Visible = true;
            //}
            //else
            //{
            //    AppointmentImageInfo apptImgInfo = e.ImageInfoList.Find(im => (im.Image.Tag as string) == "note");
            //    if (apptImgInfo != null)
            //        apptImgInfo.Visible = false;
            //}
        }

        public void CustomizeAppointmentFlyout(object sender, CustomizeAppointmentFlyoutEventArgs e)
        {

        }

        public void AppointmentFlyoutShowing(object sender, AppointmentFlyoutShowingEventArgs e)
        {

        }
    }
}
