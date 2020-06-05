using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using JARS.Core;
using JARS.Core.Attributes;
using JARS.Core.Client.Processors;
using JARS.Core.Security;
using JARS.Core.WinForms.Extensions;
using JARS.Core.WinForms.Interfaces.Plugins;
using JARS.Core.WinForms.Interfaces.Processors;
using JARS.Entities;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using JARS.WinForms.Plugins.CustomForms;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JARS.WinForms.Plugins.Processors
{
    /// <summary>
    /// This example shows how to link to the AppointmentEdit form
    /// The Plugin takes the type of IJarsPluginAppointmentForm and the contract name is the name of 
    /// the entity the appointment is directly or indirectly linked to.
    /// </summary>
    [ExportProcessor(typeof(StandardAppointment))]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    public class StandardAppointmentProcessor : ProcessorBase,
        IJarsProcessorForLoadingEntityData<StandardAppointment>,
        IProcessorForEventServiceCommandReceived,
        IProcessorForShowAppointmentForm,
        IProcessorForAppointmentEvents,
        IPluginToDateNavigator
    {
        /// <summary>
        /// This can be used as a switch when the OnAppointmentChanged method is reached.
        /// Set it true or false according to what is allowed or not.
        /// by default this doesn't do anything, it's only a hint on how to prevent multiple calls to the Server.
        /// </summary>
        public bool AllowServerPost { get; set; } = true;

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
                    IList<Appointment> appts = schedulerControl.DataStorage.Appointments.Items
                        .Where(a => a.CustomFields["ENTITY"] is StandardAppointment
                        && ids.Contains((a.CustomFields["ENTITY"] as StandardAppointment).Id)).ToList();//, idComparer));

                    if (appts.Any())
                    {
                        schedulerControl.BeginInvokeIfRequired(sc =>
                        {
                            AllowServerPost = false;
                            foreach (Appointment appt in appts)
                            {
                                appt.CustomFields["STATE"] = "DELETED";
                                sc.DataStorage.Appointments.Remove(appt);
                            }
                            sc.Refresh();
                            AllowServerPost = true;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
#if DEBUG
                throw ex;
#endif 
            }
            return true;
        }

        public bool OnStoreCommandReceived(Control control, ServerEventMessage seMsg)
        {
            if (control is SchedulerControl schedulerControl)
            {
                try
                {
                    ServerEventMessageData msgJson = seMsg.Json.FromJson<ServerEventMessageData>();
                    // if (msgJson.FromClientGuid == Context.ClientInstanceGuid)
                    if (msgJson.From == Context.LoggedInUser.UserName)
                        return true;

                    IList<StandardAppointment> sAppts = msgJson.jsonDataString.FromJson<List<StandardAppointment>>();
                    schedulerControl.BeginUpdate();
                    if (sAppts.Any())
                    {
                        foreach (StandardAppointment sAppt in sAppts)
                        {
                            Appointment appt = null;

                            AppointmentType apptType = (AppointmentType)Enum.Parse(typeof(AppointmentType), sAppt.ApptTypeCode);
                            if (new[] { AppointmentType.ChangedOccurrence, AppointmentType.DeletedOccurrence, AppointmentType.Occurrence }.Contains(apptType))
                            {
                                //get the main pattern appt
                                appt = schedulerControl.DataStorage.Appointments.Items
                                    .FirstOrDefault(a => a.CustomFields["ENTITY"] is StandardAppointment
                                    && a.Type == AppointmentType.Pattern
                                    && a.RecurrenceInfo.Id.ToString() == sAppt.RecurrenceId.ToString());
                            }
                            else
                            {
                                appt = schedulerControl.DataStorage.Appointments.Items
                                    .FirstOrDefault(a => a.CustomFields["ENTITY"] is StandardAppointment
                                    && (a.CustomFields["ENTITY"] as StandardAppointment).Id == sAppt.Id);
                            }
                            //this appointment is most likely received from a server event so no need to post it back and forth.
                            AllowServerPost = false;
                            schedulerControl.InvokeIfRequired(sc =>
                            {
                                appt = CreateOrUpdateAppointmentFromEntity(schedulerControl, sAppt, appt);
                            });
                            AllowServerPost = true;//enable server posting again.
                        }
                    }
                }
                catch (Exception ex)
                {
                    schedulerControl.InvokeIfRequired(sc =>
                    {
                        sc.EndUpdate();
                        sc.Refresh();
                    });

                    Logger.Error(ex.Message, ex);
#if DEBUG
                    throw ex;
#endif
                }
                finally
                {
                    AllowServerPost = true;//enable server posting again.
                    //schedulerControl.EndUpdate();
                    schedulerControl.InvokeIfRequired(sc =>
                    {
                        sc.EndUpdate();
                        sc.Refresh();
                    });
                }
            }
            return true;
        }

        /// <summary>
        /// Update the Entity representing the appointment with the information from the Appointment
        /// </summary>
        /// <param name="appointment">the DevExpress Appointment</param>
        private StandardAppointment UpdateEntityFromAppointment(Appointment appointment)
        {
            StandardAppointment stdAppointment = appointment.CustomFields["ENTITY"] as StandardAppointment;
            if (appointment.IsBase)
            {
                //its just a standard appointment
                stdAppointment.ApptTypeCode = appointment.Type.ToString();

                stdAppointment.PopulateWith(appointment)
                    .ThenDo(a => a.StartDate = appointment.Start)
                    .ThenDo(a => a.EndDate = appointment.End)
                    .ThenDo(a => a.IsAllDay = appointment.AllDay)
                    .ThenDo(a => a.ResourceId.PopulateWith(appointment.ResourceId));

                if (appointment.RecurrenceInfo != null)
                {
                    stdAppointment.RecurrenceId = Guid.Parse(appointment.RecurrenceInfo.Id.ToString());
                    stdAppointment.RecurrenceInfo = appointment.RecurrenceInfo.ToXml();
                }

                //appointment.CustomFields["ENTITY"] = stdAppointment;//triggers change event
            }
            else
            {
                //the standard appointment is still the ENTITY field, so we need to create one that is not the standard one, that represents the changed appt.
                if (stdAppointment.ApptTypeCode == AppointmentType.Pattern.ToString())
                    stdAppointment = new StandardAppointment().PopulateWith(stdAppointment)
                        .ThenDo(a => a.Id = 0)
                        .ThenDo(a => a.GuidValue = null)
                        .ThenDo(a => a.RecurrenceInfo = "");

                stdAppointment.ApptTypeCode = appointment.Type.ToString() == AppointmentType.Occurrence.ToString() ? AppointmentType.ChangedOccurrence.ToString() : appointment.Type.ToString();

                stdAppointment.PopulateWith(appointment)
                    .ThenDo(a => a.StartDate = appointment.Start)
                    .ThenDo(a => a.EndDate = appointment.End)
                    .ThenDo(a => a.IsAllDay = appointment.AllDay)
                    .ThenDo(a => a.ResourceId.PopulateWith(appointment.ResourceId));

                if (appointment.RecurrenceInfo != null)
                    stdAppointment.RecurrenceInfo = $"<RecurrenceInfo Id=\"{appointment.RecurrenceInfo.Id}\" Index=\"{appointment.RecurrenceIndex}\" />";

            }
            return stdAppointment;
        }

        /// <summary>
        /// Use the entity (StandardAppointment) to update the Scheduler Appointment information.
        /// </summary>
        /// <param name="schedulerControl">The scheduler control that contains the appointments</param>
        /// <returns></returns>
        protected Appointment CreateOrUpdateAppointmentFromEntity(SchedulerControl schedulerControl, StandardAppointment stdAppointment, Appointment appt)
        {
            try
            {
                if (appt == null)
                {
                    //new
                    AppointmentType apptType = (AppointmentType)Enum.Parse(typeof(AppointmentType), stdAppointment.ApptTypeCode);
                    appt = schedulerControl.DataStorage.CreateAppointment(apptType, stdAppointment.StartDate, stdAppointment.EndDate, stdAppointment.Subject);

                    //if (RolesAndOrPermissions.CheckMatchAny(JarsRoles.Internal, JarsPermissions.Viewers))
                    appt.PopulateWith(stdAppointment)
                     .ThenDo(a => a.AllDay = stdAppointment.IsAllDay)
                     .ThenDo(a => a.SetId(stdAppointment.GuidValue));
                    //else
                    //{
                    //    appt.Subject = "-- redacted for guest users --";
                    //    appt.AllDay = stdAppointment.IsAllDay;
                    //    appt.SetId(stdAppointment.GuidValue);
                    //    appt.Description = "-- redacted for guest users --";
                    //}
                    appt.ResourceId = stdAppointment.ResourceId;

                    if (apptType == AppointmentType.Pattern)
                        appt.RecurrenceInfo.FromXml(stdAppointment.RecurrenceInfo);

                    appt.CustomFields["ENTITY"] = stdAppointment;
                    schedulerControl.DataStorage.Appointments.Add(appt);
                }
                else
                {
                    //UpdateAppointmentFromEntity(stdAppointment, appointment);
                    if (appt.IsBase)
                    {
                        //its just a standard appointment
                        AppointmentType apptType = (AppointmentType)Enum.Parse(typeof(AppointmentType), stdAppointment.ApptTypeCode);
                        if (apptType == appt.Type)//update an existing appointment
                        {
                            appt.BeginUpdate();
                            appt.PopulateWith(stdAppointment)
                                .ThenDo(a => a.Start = stdAppointment.StartDate)
                                .ThenDo(a => a.End = stdAppointment.EndDate)
                                .ThenDo(a => a.AllDay = stdAppointment.IsAllDay);
                            appt.ResourceId = stdAppointment.ResourceId;

                            if (apptType == AppointmentType.Pattern)
                                appt.RecurrenceInfo.FromXml(stdAppointment.RecurrenceInfo);

                            appt.CustomFields["ENTITY"] = stdAppointment;
                            appt.EndUpdate();
                        }
                        else if (apptType == AppointmentType.Pattern && appt.Type == AppointmentType.Normal)
                        {  //working with a change of type.
                           //we need to remove the current appointment and create a new one
                            appt.CustomFields["STATE"] = "DELETED";
                            schedulerControl.DataStorage.Appointments.Remove(appt);
                            Appointment exAppt = schedulerControl.DataStorage.CreateAppointment(apptType, stdAppointment.StartDate, stdAppointment.EndDate, stdAppointment.Subject);
                            exAppt.PopulateWith(stdAppointment);
                            exAppt.SetId(stdAppointment.GuidValue);
                            exAppt.ResourceId = stdAppointment.ResourceId;
                            if (apptType == AppointmentType.Pattern)
                                exAppt.RecurrenceInfo.FromXml(stdAppointment.RecurrenceInfo);

                            exAppt.CustomFields["ENTITY"] = stdAppointment;
                            schedulerControl.DataStorage.Appointments.Add(exAppt);
                        }
                        else
                        {
                            Appointment exAppt = appt.HasExceptions ? appt.GetExceptions().Find(a => a.RecurrenceIndex == stdAppointment.RecurrenceIndex) : null;
                            if (exAppt == null)
                            {
                                AppointmentType exApptType = (AppointmentType)Enum.Parse(typeof(AppointmentType), stdAppointment.ApptTypeCode);
                                exAppt = appt.CreateException(exApptType, stdAppointment.RecurrenceIndex);
                                exAppt.Description = stdAppointment.Description;//.PopulateWith(ex)//this breaks it?
                                exAppt.Subject = stdAppointment.Subject;
                                exAppt.Start = stdAppointment.StartDate;
                                exAppt.End = stdAppointment.EndDate;
                                exAppt.CustomFields["ENTITY"] = stdAppointment;
                            }
                            else
                            {
                                exAppt.Description = stdAppointment.Description;//.PopulateWith(ex)//this breaks it?
                                exAppt.Subject = stdAppointment.Subject;
                                exAppt.Start = stdAppointment.StartDate;
                                exAppt.End = stdAppointment.EndDate;
                                exAppt.CustomFields["ENTITY"] = stdAppointment;
                            }
                        }
                    }
                    else
                    {
                        if (appt.IsOccurrence)
                        {
                            if (appt.CustomFields["ENTITY"] is StandardAppointment occAppt)
                            {
                                appt.BeginUpdate();
                                //find the appointment with the same recurring info signature
                                Appointment baseAppt = schedulerControl.DataStorage.Appointments.Items.FirstOrDefault(a => a.Type == AppointmentType.Pattern
                                && a.RecurrenceInfo != null
                                && a.RecurrenceInfo.Id.ToString() == occAppt.RecurrenceId.ToString());
                                //apply the exception

                                Appointment exAppt = baseAppt.HasExceptions ? baseAppt.GetExceptions().Find(a => a.RecurrenceIndex == occAppt.RecurrenceIndex) : null;
                                if (exAppt == null)
                                {
                                    AppointmentType exApptType = (AppointmentType)Enum.Parse(typeof(AppointmentType), occAppt.ApptTypeCode);
                                    exAppt = baseAppt.CreateException(exApptType, occAppt.RecurrenceIndex);
                                    exAppt.Description = occAppt.Description;//.PopulateWith(ex)//this breaks it?
                                    exAppt.Subject = occAppt.Subject;
                                    exAppt.Start = occAppt.StartDate;
                                    exAppt.End = occAppt.EndDate;
                                    exAppt.CustomFields["ENTITY"] = occAppt;
                                }
                                appt.EndUpdate();
                            }
                        }

                    }
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
            return appt;
        }

        private void CreateOrUpdateExceptionAppointments(StandardAppointment stdAppointment, Appointment appointment)
        {
            //foreach (StandardAppointmentException exception in stdAppointment.StandardAppointmentExceptions)
            //{
            //    AppointmentBaseCollection exColl = appointment.GetExceptions();
            //    Appointment exAppt = exColl.Find(x => x.Id != null && x.Id.ToString() == exception.GuidValue);
            //    if (exAppt == null)
            //    {
            //        if (exception.ApptTypeCode == AppointmentType.ChangedOccurrence.ToString())
            //        {
            //            //check if there is an exception of the same index
            //            exAppt = exColl.Find(x => x.Id == null && x.RecurrenceIndex == exception.IndexPosition);
            //            if (exAppt == null)
            //                exAppt = appointment.CreateException(AppointmentType.ChangedOccurrence, exception.IndexPosition);
            //        }

            //        if (exception.ApptTypeCode == AppointmentType.DeletedOccurrence.ToString())
            //            exAppt = appointment.CreateException(AppointmentType.DeletedOccurrence, exception.IndexPosition);
            //    }
            //    else
            //    {
            //        if (exAppt.IsException && exAppt.Type.ToString() != exception.ApptTypeCode)
            //        {
            //            exAppt.RestoreOccurrence();
            //            //remove exappt and create it again..
            //            if (exception.ApptTypeCode == AppointmentType.DeletedOccurrence.ToString())
            //                exAppt = appointment.CreateException(AppointmentType.DeletedOccurrence, exception.IndexPosition);
            //        }
            //    }

            //    exAppt.SetId(exception.GuidValue);
            //    exAppt.Start = exception.StartDate;
            //    exAppt.End = exception.EndDate;
            //    exAppt.Subject = exception.Subject;
            //    exAppt.Description = exception.Description;
            //    exAppt.CustomFields["ENTITY"] = exception;

            //}
        }

        /// <summary>
        /// Set the Scheduler appointment recurrence info using the info from the entity (standard appointment)
        /// </summary>
        /// <param name="appointment">The appointment that will be updated with the entity information</param>
        /// <param name="stdAppointment">the entity that will be used to update the scheduler appointment with.</param>
        void UpdateRecurrenceInfo(Appointment appointment, StandardAppointment stdAppointment)
        {
            try
            {
                //appointment.RecurrenceInfo.OccurrenceCount = stdAppointment.RecurrenceCount.Value;
                //appointment.RecurrenceInfo.Periodicity = stdAppointment.RecurrenceIntervalPeriod;
                //appointment.RecurrenceInfo.Range = (RecurrenceRange)stdAppointment.RecurrenceIntervalRange;
                //appointment.RecurrenceInfo.Type = (RecurrenceType)stdAppointment.RecurrenceIntervalType;
                //appointment.RecurrenceInfo.WeekDays = (WeekDays)stdAppointment.DaysOfWeek;
                //appointment.RecurrenceInfo.WeekOfMonth = (WeekOfMonth)stdAppointment.WeekOfMonth;
                //appointment.RecurrenceInfo.Month = stdAppointment.MonthOfYear;
                if (appointment.IsBase)
                {
                    appointment.RecurrenceInfo.Start = stdAppointment.StartDate;
                    appointment.RecurrenceInfo.End = stdAppointment.EndDate;
                }

                if (appointment.RecurrenceInfo.Range == RecurrenceRange.EndByDate)
                    appointment.RecurrenceInfo.End = stdAppointment.EndDate;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
#if DEBUG
                throw ex;
#endif
            }

        }

        public bool OnAnyCommandReceived(Control control, ServerEventMessage msg)
        {
            ServerEventMessage m = msg as ServerEventMessage;
            if (control is SchedulerControl schedulerControl)
            {

            }
            return true;
        }

        public void OnAppointmentDrag(object sender, AppointmentDragEventArgs eventArgs)
        {
            //at this point the roles and permissions have already been checked, but additional checks can be added.
        }

        //public async Task OnAppointmentDropAsync(AppointmentDragEventArgs e)
        public void OnAppointmentDrop(object sender, AppointmentDragEventArgs e)
        {
            SaveAppointmentEntity(e.EditedAppointment);
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
            if (AllowServerPost)
            {
                foreach (var apptObj in e.Objects)
                {
                    if (apptObj is Appointment appt)
                        SaveAppointmentEntity(appt);
                }
            }
        }

        public void SaveAppointmentEntity(Appointment appointment)
        {
            if (!AllowServerPost)
                return;

            try
            {
                //because we set the custom field on response (triggering a call to change)
                //this will prevent another change being fired.
                AllowServerPost = false;

                StandardAppointment stdAppointment = UpdateEntityFromAppointment(appointment);
                StoreStandardAppointment store = new StoreStandardAppointment()
                {
                    Appointment = stdAppointment.ConvertTo<StandardAppointmentDto>()
                };

                StandardAppointmentResponse response = Context.ServiceClient.Post(store);
                appointment.CustomFields["ENTITY"] = response.Appointment.ConvertTo<StandardAppointment>();

                if (response.ResponseStatus == null)
                    Context.ServiceClient.Post(new StandardAppointmentsNotification()
                    {
                        ClientGuid = Context.ClientInstanceGuid,
                        FromUserName = Context.LoggedInUser.UserName,
                        Selector = SelectorTypes.store,
                        Ids = new List<int>() { response.Appointment.Id }
                    });
            }
            catch (Exception)
            {
                AllowServerPost = true;
                throw;
            }
            finally
            {
                AllowServerPost = true;
            }
        }

        /// <summary>
        /// This method is called from the internal SchedulerControl_EditAppointmentFormShowing() method.
        /// The appointment form needs to link to the scheduler control and the appointment that is being edited (as per the DevExpress documentation)
        /// The form does not have to inherit from the DevExpress forms, it can be a completely new implementation of a form.
        /// </summary>
        /// <param name="schedulerControl">the scheduler control linked to the form</param>
        /// <param name="appointment">the appointment being edited/viewed</param>
        public void ShowAppointmentForm(SchedulerControl schedulerControl, Appointment appointment)
        {
            if (appointment.IsOccurrence)
            {
                CustomStandardEditAppointmentForm frm = new CustomStandardEditAppointmentForm(schedulerControl, appointment);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK || result == DialogResult.Abort)
                {
                    SaveAppointmentEntity(appointment);
                }
            }
            if (!appointment.IsOccurrence)
            {
                CustomStandardEditAppointmentForm frm = new CustomStandardEditAppointmentForm(schedulerControl, appointment);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    SaveAppointmentEntity(appointment);
                }
            }
        }

        public void OnAppointmentsDeleted(object sender, Appointment appointment)
        {
            if (appointment.IsBase)
            {
                StandardAppointment stdAppointment = appointment.CustomFields["ENTITY"] as StandardAppointment;

                DeleteStandardAppointment delete = new DeleteStandardAppointment()
                {
                    IsAppointment = true,
                    Id = stdAppointment.Id
                };
                Context.ServiceClient.Delete(delete);
            }
            else
            {
                if (new[] { AppointmentType.DeletedOccurrence, AppointmentType.Occurrence }.Contains(appointment.Type))
                {
                    //need to create an exception here.
                    appointment.CreateException(AppointmentType.DeletedOccurrence, appointment.RecurrenceIndex);
                }
            }
        }

        IList<StandardAppointment> dataList;
        public IList<StandardAppointment> DataList
        {
            get
            {
                if (dataList == null)
                    dataList = new List<StandardAppointment>();
                return dataList;
            }
            set
            {
                dataList = value;
            }
        }

        public DateNavigator dateNavigator { get; set; }

        public string PluginText => "StandardAppointmentProcessor";//not really needed, but comes with the plugin..

        public async Task LoadOrRefreshEntityDataAsync(bool isRefresh = false)
        {
            //StandardAppointmentsResponse saResT = await Context.ServiceClient.GetAsync(new FindStandardAppointments() { FromStartDate = DateTime.Now.AddDays(-3) });            
            StandardAppointmentsResponse saResT = await Context.ServiceClient.GetAsync(new FindStandardAppointments() { FromStartDate = dateNavigator.SelectionStart });
            if (!saResT.IsErrorResponse())
                DataList = saResT.Appointments.ConvertAllTo<StandardAppointment>().ToList();// (a => a.ConvertTo<StandardAppointment>());
        }

        public void LoadOrRefreshEntityData(SchedulerControl schedulerControl)
        {
            try
            {
                schedulerControl.BeginUpdate();
                foreach (StandardAppointment stdAppointment in DataList)
                {
                    //check if the appointment already exists, if it does then no need to load it again.
                    if (schedulerControl.DataStorage.Appointments.Items.Any(a => a.CustomFields["ENTITY"] != null && a.CustomFields["ENTITY"] is StandardAppointment && (a.CustomFields["ENTITY"] as StandardAppointment).Id == stdAppointment.Id))
                        continue;

                    AppointmentType apptType = (AppointmentType)Enum.Parse(typeof(AppointmentType), stdAppointment.ApptTypeCode);

                    if (new[] { AppointmentType.Normal, AppointmentType.Pattern }.Contains(apptType))
                    {
                        Appointment appointment = schedulerControl.DataStorage.CreateAppointment(apptType, stdAppointment.StartDate, stdAppointment.EndDate, stdAppointment.Subject);
                        appointment.PopulateWith(stdAppointment);
                        appointment.SetId(stdAppointment.GuidValue);
                        appointment.ResourceId = stdAppointment.ResourceId;
                        if (apptType == AppointmentType.Pattern)
                            appointment.RecurrenceInfo.FromXml(stdAppointment.RecurrenceInfo);

                        appointment.CustomFields["ENTITY"] = stdAppointment;
                        if (apptType == AppointmentType.Pattern)
                        {
                            //now look for exceptions in the list
                            var exList = DataList.Where(a =>
                                a.Id != stdAppointment.Id &&
                                a.RecurrenceId == stdAppointment.RecurrenceId &&
                                a.ApptTypeCode != "Normal");

                            if (exList.Any())
                            {
                                foreach (var ex in exList)
                                {
                                    if (ex.ApptTypeCode != stdAppointment.ApptTypeCode)
                                    {
                                        AppointmentType exType = (AppointmentType)Enum.Parse(typeof(AppointmentType), ex.ApptTypeCode);
                                        Appointment exAppt = appointment.CreateException(exType, ex.RecurrenceIndex);
                                        exAppt.Description = ex.Description;//.PopulateWith(ex)
                                        exAppt.Subject = ex.Subject;
                                        exAppt.Start = ex.StartDate;
                                        exAppt.End = ex.EndDate;
                                        exAppt.CustomFields["ENTITY"] = ex;
                                    }
                                }
                            }
                        }

                        schedulerControl.DataStorage.Appointments.Add(appointment);
                    }
                    else
                    {
                        continue;
                        //because we already added the exceptions.
                    }
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
                schedulerControl.EndUpdate();
            }
        }

        public void OnAppointmentResizing(object sender, AppointmentResizeEventArgs e)
        {
            if (!RolesAndOrPermissions.CheckMatchAny(new[] { JarsRoles.Admin, JarsRoles.User, JarsRoles.Manager, JarsRoles.PowerUser }))
                e.Allow = false;
        }

        public void OnAppointmentResized(object sender, AppointmentResizeEventArgs e)
        {
            SaveAppointmentEntity(e.EditedAppointment);
        }

        public void OnAllowInplaceEditor(object sender, AppointmentOperationEventArgs e)
        {
            e.Allow = true;
        }

        public void OnInplaceEditorShowing(object sender, InplaceEditorEventArgs e)
        {
            if (!(e.Appointment.ResourceId is EmptyResourceId))
            {
                e.InplaceEditorEx = new StandardAppointmentInplaceEditorControl(e.SchedulerInplaceEditorEventArgs);//new CustomStandardAppointmentInPlaceEditorControl(e.SchedulerInplaceEditorEventArgs);
                e.InplaceEditorEx.CommitChanges += InplaceEditorEx_CommitChanges;
                e.SchedulerInplaceEditorEventArgs.Handled = true;
            }
        }

        private void InplaceEditorEx_CommitChanges(object sender, EventArgs e)
        {
            if (sender is StandardAppointmentInplaceEditorControl edEx)
            {
                AllowServerPost = false;
                if (edEx.EditEditor is TextBox)
                    edEx.EditAppointment.Subject = (edEx.EditEditor as TextBox).Text;
                if (edEx.EditEditor is MemoEdit)
                    edEx.EditAppointment.Subject = (edEx.EditEditor as MemoEdit).Text;

                AllowServerPost = true;
                if (edEx.EditAppointment.CustomFields["ENTITY"] is StandardAppointment stdAppointment)
                {
                    stdAppointment = UpdateEntityFromAppointment(edEx.EditAppointment);
                    if (stdAppointment.Id == 0)
                        edEx.EditAppointment.SetId(stdAppointment.GuidValue);

                    edEx.EditAppointment.CustomFields["ENTITY"] = stdAppointment;

                    SaveAppointmentEntity(edEx.EditAppointment);
                }
            }
        }
    }
}
