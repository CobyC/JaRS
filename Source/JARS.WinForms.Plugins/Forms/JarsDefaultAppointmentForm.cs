using JARS.Entities;
using JARS.Core.WinForms.Forms;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace JARS.Win.Plugins
{
    public partial class JarsDefaultAppointmentForm : RibbonFormCrudBase
    {
        public JarsDefaultAppointmentForm()
        {
            InitializeComponent();
        }

        private void JarsDefaultAppointmentForm_Load(object sender, EventArgs e)
        {
            SetGridControl(gridControlDefAppts);
            OnRefreshDataAsync();
        }

        public override async void OnRefreshDataAsync()
        {
            try
            {
                base.OnRefreshDataAsync();
                JarsDefaultAppointmentsResponse defApptResponse = await ServiceClient.GetAsync(new FindJarsDefaultAppointments());
                defaultBindingSource.DataSource = defApptResponse.Appointments.ConvertAll(a => a.ConvertTo<JarsDefaultAppointment>());
                FormEditState = FormEditStates.Browsing;
            }
            catch (Exception exR)
            {
                OnExceptionEvent(exR);
            }
        }

        public override void OnAddData()
        {
            base.OnAddData();
            //Create a new record
            JarsDefaultAppointment newOp = defaultBindingSource.AddNew() as JarsDefaultAppointment;
            defaultBindingSource.Position = defaultBindingSource.IndexOf(newOp);
        }

        public override void OnEditData()
        {
            base.OnEditData();
            //start code here
        }

        public override void OnSaveData()
        {
            try
            {
                JarsDefaultAppointment storeAppt = defaultBindingSource.Current as JarsDefaultAppointment;

                StoreJarsDefaultAppointment storeReq = new StoreJarsDefaultAppointment();
                storeReq.Appointment = storeAppt.ConvertTo<JarsDefaultAppointmentDto>();
                var response = ServiceClient.Post(storeReq);

                //if the response was good, then notify the others.
                if (response.ResponseStatus == null)
                {
                    storeAppt = response.Appointment.ConvertTo<JarsDefaultAppointment>();
                    Context.ServiceClient.PostAsync(new JarsDefaultAppointmentNotification()
                    {
                        FromUserName = Context.LoggedInUser.UserName,
                        Selector = SelectorTypes.store,
                        Ids = new List<int>() { response.Appointment.Id }
                    });
                }

                base.OnSaveData();
            }
            catch (Exception exS)
            {
                OnExceptionEvent(exS);
            }
        }

        public override void OnCancelData()
        {
            base.OnCancelData();
            OnRefreshDataAsync();
        }

        public override bool OnDeleteData()
        {
            try
            {
                if (base.OnDeleteData(true))
                {
                    JarsDefaultAppointment delJobDefAppt = defaultBindingSource.Current as JarsDefaultAppointment;
                    DeleteJarsDefaultAppointment delReq = new DeleteJarsDefaultAppointment
                    {
                        Id = delJobDefAppt.Id
                    };
                    ServiceClient.Delete(delReq);
                    defaultBindingSource.RemoveCurrent();
                    defaultBindingSource.ResetBindings(false);
                }
            }
            catch (Exception exD)
            {
                OnExceptionEvent(exD);
            }
            //call this after the record removal was successful.
            return base.OnDeleteData();
        }

        public override void OnMessageEvent(ServiceStack.ServerEventMessage msg)
        {
            if (msg.Channel != typeof(JarsDefaultAppointment).Name)
                return;

            //var thread = new Thread(() =>
            //{
            //    switch (msg.Selector)
            //    {
            //        case SelectorTypes.delete:
            //            MessageBox.Show($"DELETE - Op:{msg.Op} Selector:{msg.Selector} Target:{msg.Target} Channel:{msg.Channel} EventId:{msg.EventId}");
            //            break;
            //        case SelectorTypes.store:
            //            MessageBox.Show($"SAVE - Op:{msg.Op} Selector:{msg.Selector} Target:{msg.Target} Channel:{msg.Channel} EventId:{msg.EventId}");
            //            break;
            //    }
            //});
            //thread.Start();
            base.OnMessageEvent(msg);
        }
    }
}
