using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using JARS.BOS.Entities;
using JARS.BOS.SS.DTOs;
using JARS.BOS.WinForms.Plugins.CustomForms;
using JARS.Core;
using JARS.Core.Attributes;
using JARS.Core.Client.Processors;
using JARS.Core.Interfaces.Entities;
using JARS.Core.Security;
using JARS.Core.WinForms.Extensions;
using JARS.Core.WinForms.Interfaces.Plugins;
using JARS.Core.WinForms.Interfaces.Processors;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JARS.BOS.WinForms.Plugins.Processors
{
    /// <summary>
    /// This plugin shows how to implement a processor for the linked IEntityBase inherited entity
    /// The processor gets called and executed from the main application, the interfaces helps with 
    /// linking the processes within this processor to the various parts of the main system.
    /// </summary>
    [ExportProcessor(typeof(BOSEntity))]
    //[PartCreationPolicy(CreationPolicy.NonShared)] //<-- Only want one instance so not creating a new instance for every call
    public class BOSEntityAppointmentProcessor : ProcessorBase,
        IJarsProcessorForLoadingEntityData<BOSEntity>,
        IProcessorForEventServiceCommandReceived,
        IProcessorForShowAppointmentForm,
        IProcessorForAppointmentEvents,
        IProcessorForAppointmentCustomization,
        IProcessorWithSettings
    {

        /// <summary>
        /// This can be used as a switch when the OnAppointmentChanged method is reached.
        /// Set it true or false according to what is allowed or not.
        /// by default this doesn't do anything, it's only a hint on how to prevent multiple calls to the Server.
        /// </summary>
        public bool AllowServerPost { get; set; } = true;

        Dictionary<string, object> _ProcessorSettings;
        public Dictionary<string, object> ProcessorSettings
        {
            get
            {
                if (_ProcessorSettings == null)
                    _ProcessorSettings = new Dictionary<string, object>();
                return _ProcessorSettings;
            }

            set => _ProcessorSettings = value;
        }

        public bool OnDeleteCommandReceived(Control control, ServerEventMessage seMsg)
        {
            try
            {
                if (control is SchedulerControl schedulerControl)
                {
                    //EntityIdComparer idComparer = new EntityIdComparer();
                    ServerEventMessageData msgData = seMsg.Json.FromJson<ServerEventMessageData>();
                    // if (msgJson.FromClientGuid == Context.ClientInstanceGuid)
                    if (msgData.From == Context.LoggedInUser.UserName)
                        return true;

                    List<int> ids = msgData.jsonDataString.FromJson<List<int>>();
                    schedulerControl.BeginInvokeIfRequired(sc =>
                    {
                        foreach (int bosId in ids)
                        {
                            Appointment appt = schedulerControl.DataStorage.Appointments.Items
                            .FirstOrDefault(a => (a.CustomFields["ENTITY"] is BOSEntity bosEnt) && bosEnt.Id == bosId);
                            if (appt != null)
                            {
                                appt.BeginUpdate();
                                appt.CustomFields["STATE"] = "DELETED";
                                appt.EndUpdate();
                                sc.DataStorage.Appointments.Remove(appt);
                            }
                        }
                        sc.Refresh();
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
#if DEBUG
                throw ex;
#endif 
            }
            finally
            {

            }
            return true;
        }

        public bool OnStoreCommandReceived(Control control, ServerEventMessage seMsg)//JarsSyncStoreEvent<IEntityBase> syncEvent)
        {
            if (control is SchedulerControl schedulerControl)
            {
                try
                {
                    ServerEventMessageData msgJson = seMsg.Json.FromJson<ServerEventMessageData>();
                    //if (msgJson.FromClientGuid == Context.ClientInstanceGuid)
                    if (msgJson.From == Context.LoggedInUser.UserName)
                        return true;

                    IList<BOSEntity> eRecs = msgJson.jsonDataString.FromJson<List<BOSEntity>>();//  .Entities;

                    AllowServerPost = false;
                    schedulerControl.InvokeIfRequired(sc =>
                         {
                             sc.BeginUpdate();
                             foreach (BOSEntity eRec in eRecs)
                             {
                                 Appointment appt = sc.DataStorage.Appointments.Items
                                         .Find(a => a.CustomFields["ENTITY"] is BOSEntity
                                         && (a.CustomFields["ENTITY"] as BOSEntity).GuidValue == eRec.GuidValue);

                                 appt = CreateOrUpdateAppointmentFromEntity(sc.DataStorage, eRec, appt);
                                 appt.CustomFields["ENTITY"] = eRec;
                             }
                         });
                    AllowServerPost = true;//enable server posting again.
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message, ex);
#if DEBUG
                    throw ex;
#endif
                }
                finally
                {
                    AllowServerPost = true;//enable server posting again.


                    schedulerControl.InvokeIfRequired(sc =>
                    {
                        sc.EndUpdate();
                        sc.Refresh();
                    });
                }
            }
            return true;
        }

        public bool OnAnyCommandReceived(Control control, ServerEventMessage msg)
        {
            ServerEventMessage m = msg as ServerEventMessage;
            if (control is SchedulerControl schedulerControl)
            {

            }
            return true;
        }

        private Appointment CreateOrUpdateAppointmentFromEntity(ISchedulerStorage schedulerStorage, BOSEntity eRec, Appointment appt)
        {
            try
            {
                bool isNew = false;
                if (appt == null)
                {
                    isNew = true;
                    appt = schedulerStorage.CreateAppointment(AppointmentType.Normal, eRec.StartDate, eRec.EndDate, eRec.ExtRefId);
                    appt.SetId(eRec.GuidValue);
                }
                // appt.BeginUpdate();
                //if (appt.CustomFields["ENTITY"] is BOSEntity cusRec)
                //    cusRec = eRec;
                //else
                appt.CustomFields["ENTITY"] = eRec;

                appt.Subject = eRec.ExtRefId;
                appt.ResourceId = eRec.ResourceId;
                appt.Description = eRec.Description;

                //if (eRec.ActualStartDate.HasValue)
                //    appt.Start = eRec.ActualStartDate.Value;
                //else
                appt.Start = eRec.StartDate;

                //if (eRec.ActualEndDate.HasValue)
                //    appt.End = eRec.ActualEndDate.Value;
                //else
                appt.End = eRec.EndDate;

                appt.Location = eRec.Location;
                appt.LabelKey = eRec.LabelKey;
                appt.StatusKey = eRec.StatusKey;
                if (isNew)
                    schedulerStorage.Appointments.Add(appt);
                // appt.EndUpdate();

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
#if DEBUG
                throw ex;
#endif
            }
            return appt;
        }

        public void OnAppointmentDrag(object sender, AppointmentDragEventArgs de)
        {
            //at this point the roles and permissions have already been checked, but additional checks can be added.
        }

        //public async Task<IEntityBase> OnAppointmentDropAsync(AppointmentDragEventArgs e)
        public void OnAppointmentDrop(object sender, AppointmentDragEventArgs e)
        {
            if (e.EditedAppointment.CustomFields["ENTITY"] is BOSEntity ent)
            {
                if (ent.Id == 0)
                    SaveAppointmentEntity(e.EditedAppointment);
                //else
                //update handled by OnAppointmentChanged event
            }
        }

        public void OnAppointmentInserting(object sender, PersistentObjectCancelEventArgs e)
        {
            //this is raised from the storage component additional checks can be made here if needed.
        }

        public void OnAppointmentsInserted(object sender, PersistentObjectsEventArgs e)
        {
            //this is raised from the storage component additional checks can be made here if needed.
        }

        public void OnAppointmentChanging(object sender, PersistentObjectCancelEventArgs e)
        {
            //this is raised from the storage component additional checks can be made here if needed.            
        }

        public void OnAppointmentsChanged(object sender, PersistentObjectsEventArgs e)
        {
            foreach (var apptObj in e.Objects)
            {
                if (apptObj is Appointment appt)
                    SaveAppointmentEntity(appt);
            }
        }

        /// <summary>
        /// Update the Entity representing the appointment with the information from the Appointment
        /// </summary>
        /// <param name="appointment">the DevExpress Appointment</param>
        public void SaveAppointmentEntity(Appointment appointment)
        {
            if (!AllowServerPost)
                return;
            try
            {
                //because we set the custom field on response (triggering a call to change)
                //this will prevent another change being fired.
                AllowServerPost = false;

                BOSEntity saveEntity = UpdateEntityFromAppointment(appointment);
                StoreBOSEntity store = new StoreBOSEntity()
                {
                    BOSEntity = saveEntity.ConvertTo<BOSEntityDto>(),
                    IsAppointment = true
                };

                BOSEntityResponse response = Context.ServiceClient.Post(store);
                appointment.CustomFields["ENTITY"] = response.BOSEntity.ConvertTo<BOSEntity>();

                if (response.ResponseStatus == null)
                    Context.ServiceClient.Post(new BOSEntitiesNotification()
                    {
                        ClientGuid = Context.ClientInstanceGuid,
                        FromUserName = Context.LoggedInUser.UserName,
                        Selector = SelectorTypes.store,
                        Ids = new List<int>() { response.BOSEntity.Id }
                    });
            }
            catch (Exception ex)
            {
                AllowServerPost = true;
                throw ex;
            }
            finally
            {
                AllowServerPost = true;
            }
        }

        private BOSEntity UpdateEntityFromAppointment(Appointment appointment)
        {
            BOSEntity updEntity = appointment.CustomFields["ENTITY"] as BOSEntity;

            updEntity.StartDate = appointment.Start;
            updEntity.EndDate = appointment.End;
            updEntity.ResourceId = (int)appointment.ResourceId;
            updEntity.Description = appointment.Description;
            updEntity.StatusKey = appointment.StatusKey.ToString();
            updEntity.LabelKey = appointment.LabelKey.ToString();
            updEntity.Location = appointment.Location;

            return updEntity;
        }

        /// <summary>
        /// This method is called from the internal SchedulerControl_EditAppointmentFormShowing() method.
        /// The appointment form needs to link to the scheduler control and the appointment that is being edited (as per the DevExpress documentation)
        /// The form does not have to inherit from the DevExpress forms, it can be a completely new implementation of a form.
        /// </summary>
        /// <param name="schedulerCon">the scheduler control linked to the form</param>
        /// <param name="appointment">the appointment being edited/viewed</param>
        public void ShowAppointmentForm(SchedulerControl schedulerCon, Appointment appointment)
        {
            BOSEntityAppointmentForm frm = new BOSEntityAppointmentForm(schedulerCon, appointment);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                SaveAppointmentEntity(appointment);
            }
        }

        public void OnAppointmentsDeleted(object sender, Appointment appointment)
        {
            BOSEntity ent = appointment.CustomFields["ENTITY"] as BOSEntity;
            Context.ServiceClient.Delete(new DeleteBOSEntity()
            {
                Id = ent.Id,
                IsAppointment = true
            });
        }

        IList<BOSEntity> dataList;
        public IList<BOSEntity> DataList
        {
            get
            {
                if (dataList == null)
                    dataList = new List<BOSEntity>();
                return dataList;
            }
            set
            {
                dataList = value;
            }
        }



        public async Task LoadOrRefreshEntityDataAsync(bool isRefresh = false)
        {
            BOSEntitiesResponse elResult = await Context.ServiceClient.GetAsync(new FindBOSEntities() { });
            if (!elResult.IsErrorResponse())
                DataList = elResult.BOSEntities.ConvertAllTo<BOSEntity>().ToList();
        }

        public void LoadOrRefreshEntityData(SchedulerControl schedulerCon)
        {
            schedulerCon.BeginUpdate();
            try
            {
                foreach (BOSEntity eRec in DataList)
                {
                    Appointment appt = schedulerCon.DataStorage.Appointments.Items.Find(a => (a.CustomFields["ENTITY"] as IEntityBase<int>).Id == eRec.Id);
                    CreateOrUpdateAppointmentFromEntity(schedulerCon.DataStorage, eRec, appt);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { schedulerCon.EndUpdate(); }
        }

        public void OnAppointmentResizing(object sender, AppointmentResizeEventArgs e)
        {
            if (!RolesAndOrPermissions.CheckMatchAny(new[] { JarsRoles.Admin, JarsRoles.User, JarsRoles.Manager, JarsRoles.PowerUser }))
                e.Allow = false;
        }

        public void OnAppointmentResized(object sender, AppointmentResizeEventArgs e)
        {
            //this event will be followed by the appointment changed event, where the appointment is saved, so no need to call saveAppointment
            // SaveAppointmentEntity(e.EditedAppointment);
        }

        public void OnAllowInplaceEditor(object sender, AppointmentOperationEventArgs e)
        {
            e.Allow = false;
        }

        public void OnInplaceEditorShowing(object sender, InplaceEditorEventArgs e)
        {
            if (!(e.Appointment.ResourceId is EmptyResourceId))
            { }
        }

        public void AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e)
        {

        }

        public void InitAppointmentDisplayText(object sender, AppointmentDisplayTextEventArgs e)
        {

        }

        public void InitAppointmentImages(object sender, AppointmentImagesEventArgs e)
        {
            if (e.Appointment.CustomFields["ENTITY"] is IEntityWithNotes noteEnt)
            {
                AppointmentImageInfo apptImgInfo = e.ImageInfoList.Find(im => (im.Image.Tag as string) == "note");
                if (apptImgInfo == null)
                {
                    var newImg = Properties.Resources.Notes_16x16;
                    newImg.Tag = "note";
                    apptImgInfo = new AppointmentImageInfo() { Image = newImg, Visible = false };
                    e.ImageInfoList.Add(apptImgInfo);
                }
                if (!noteEnt.AddedNotes.IsNullOrEmpty())
                    apptImgInfo.Visible = true;
                else
                    apptImgInfo.Visible = false;
            }
            else
            {
                AppointmentImageInfo apptImgInfo = e.ImageInfoList.Find(im => (im.Image.Tag as string) == "note");
                if (apptImgInfo != null)
                    apptImgInfo.Visible = false;
            }
        }

        public void CustomizeAppointmentFlyout(object sender, CustomizeAppointmentFlyoutEventArgs e)
        {

        }

        public void AppointmentFlyoutShowing(object sender, AppointmentFlyoutShowingEventArgs e)
        {

        }
    }
}
