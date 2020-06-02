using JARS.Entities;
using JARS.Core.Extensions;
using JARS.Core.Interfaces.Entities;
using JARS.Core.WinForms.Forms;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;

namespace JARS.Win.Plugins
{
    //[PrincipalPermission(SecurityAction.Demand, Role = JarsCoreSecurity.Administrators)]
    //[PrincipalPermission(SecurityAction.Demand, Role = JarsCoreSecurity.JarsAdministrators)]
    //[PrincipalPermission(SecurityAction.Demand, Role = JarsCoreSecurity.JarsPowerUsers)]
    public partial class StatusesForm : RibbonFormCrudBase
    {
        public StatusesForm()
        {
            InitializeComponent();
        }

        private void JarsUserAccountsForm_Load(object sender, EventArgs e)
        {
            SetGridControl(gridControlStatuses);
            OnRefreshDataAsync();
            //List<DefaultEntityBase> list = new List<DefaultEntityBase>();
            //list.Add(new DefaultEntityBase());
            //filterBindingSource.DataSource = list;
        }

        public override async void OnRefreshDataAsync()
        {
            base.OnRefreshDataAsync();
            ApptStatusesResponse apptStatusResponse = await SSEventClient.ServiceClient.GetAsync(new FindApptStatuses());
            defaultBindingSource.DataSource = apptStatusResponse.Statuses.ConvertAllTo<ApptStatus>();
            FormEditState = FormEditStates.Browsing;
        }

        public override void OnAddData()
        {
            base.OnAddData();
            //Create a new label record
            ApptStatus newStatus = defaultBindingSource.AddNew() as ApptStatus;

            Type newLabelInterfaceType = Type.GetType(newStatus.UseInterfaceType);
            if (newLabelInterfaceType == typeof(IEntityWithStatusLabels))
            {
                newStatus.StatusCriteria = "([StatusKey] = '0')";
                lblInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                ctrl_FilterControl.Enabled = false;
            }
            else
            { lblInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; }

            defaultBindingSource.Position = defaultBindingSource.IndexOf(newStatus);
            defaultBindingSource.ResetCurrentItem();
        }

        public override void OnEditData()
        {
            base.OnEditData();
            //start code here
            Type newLabelInterfaceType = Type.GetType((defaultBindingSource.Current as ApptStatus).UseInterfaceType);
            if (newLabelInterfaceType == typeof(IEntityWithStatusLabels))
            {
                lblInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                ctrl_FilterControl.Enabled = false;
            }
            else
            { lblInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; }
        }

        public override void OnSaveData()
        {
            try
            {
                ApptStatus saveObj = defaultBindingSource.Current as ApptStatus;
                saveObj.StatusCriteria = ctrl_FilterControl.FilterString;
                var store = new StoreApptStatus()
                {
                    Status = saveObj.ConvertTo<ApptStatusDto>()
                };
                var resp = ServiceClient.Post(store);

                //if the response was good, then notify the others.
                if (resp.ResponseStatus == null)
                {
                    saveObj = resp.Status.ConvertTo<ApptStatus>();
                    Context.ServiceClient.Post(new ApptStatusesNotification()
                    {
                        FromUserName = Context.LoggedInUser.UserName,
                        Selector = SelectorTypes.store,
                        Ids = new List<int>() { resp.Status.Id }
                    });
                }
                base.OnSaveData();
                lblInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            catch (Exception exS)
            {
                OnExceptionEvent(exS);
            }
        }


        public override void OnCancelData()
        {
            base.OnCancelData();
            lblInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            OnRefreshDataAsync();
        }

        public override bool OnDeleteData()
        {
            if (base.OnDeleteData(true))
            {
                ApptStatus delObj = defaultBindingSource.Current as ApptStatus;
                var delReq = new DeleteApptStatus { Id = delObj.Id };
                ServiceClient.Delete(delReq);
                defaultBindingSource.RemoveCurrent();
                defaultBindingSource.ResetBindings(false);
            }
            lblInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            return base.OnDeleteData();
        }

        public override void OnMessageEvent(ServiceStack.ServerEventMessage msg)
        {
            if (msg.Channel != typeof(ApptStatus).Name)
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

        private void defaultBindingSource_PositionChanged(object sender, EventArgs e)
        {
            UpdateFilterBindingsource();
            if (defaultBindingSource.Current != null)
                ctrl_FilterControl.FilterString = (defaultBindingSource.Current as ApptStatus).StatusCriteria;
        }
        void UpdateFilterBindingsource()
        {
            //create an entity out of thin code.... and assign it to the datasource of the filter bs
            if (defaultBindingSource.Current != null)
            {
                string interfaceTypename = (defaultBindingSource.Current as ApptStatus).UseInterfaceType;
                Type t = Type.GetType(interfaceTypename);
                //filterBindingSource.DataSource = TempDataTableCreator.CreateDataTableFromType(t);
                filterBindingSource.DataSource = t.CreatePropertiesDataTableFromType();
            }
        }

        private void defaultBindingSource_AddingNew(object sender, System.ComponentModel.AddingNewEventArgs e)
        {
            string currlbl = (defaultBindingSource.Current as ApptStatus).ViewName;
            string currIntfT = (defaultBindingSource.Current as ApptStatus).UseInterfaceType;
            ApptStatus newStatus = new ApptStatus();
            newStatus.ViewName = currlbl;
            newStatus.StatusName = "";
            newStatus.UseInterfaceType = currIntfT;
            e.NewObject = newStatus;
        }
    }


}
