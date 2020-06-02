using DevExpress.Data.Filtering;
using JARS.Entities;
using JARS.Core.Extensions;
using JARS.Core.Interfaces.Entities;
using JARS.Core.WinForms.Extensions;
using JARS.Core.WinForms.Forms;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Drawing;

namespace JARS.Win.Plugins
{
    //[PrincipalPermission(SecurityAction.Demand, Role = JarsCoreSecurity.Administrators)]
    //[PrincipalPermission(SecurityAction.Demand, Role = JarsCoreSecurity.JarsAdministrators)]
    //[PrincipalPermission(SecurityAction.Demand, Role = JarsCoreSecurity.JarsPowerUsers)]
    public partial class LabelsForm : RibbonFormCrudBase
    {
        public LabelsForm()
        {
            InitializeComponent();
        }

        private void JarsUserAccountsForm_Load(object sender, EventArgs e)
        {
            SetGridControl(gridControlLabels);
            OnRefreshDataAsync();

        }

        public override async void OnRefreshDataAsync()
        {
            base.OnRefreshDataAsync();
            ApptLabelsResponse apptLabelsResponse = await ServiceClient.GetAsync(new FindApptLabels());
            defaultBindingSource.DataSource = apptLabelsResponse.Labels.ConvertAllTo<ApptLabel>();
            FormEditState = FormEditStates.Browsing;
        }

        public override void OnAddData()
        {
            base.OnAddData();
            ApptLabel newLabel = defaultBindingSource.AddNew() as ApptLabel;

            Type newLabelInterfaceType = Type.GetType(newLabel.UseInterfaceType);
            if (newLabelInterfaceType == typeof(IEntityWithStatusLabels))
            {
                newLabel.LabelCriteria = "([LabelKey] = '0')";
                lblInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                ctrl_FilterControl.Enabled = false;
            }
            else
            { lblInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; }

            defaultBindingSource.Position = defaultBindingSource.IndexOf(newLabel);
            defaultBindingSource.ResetCurrentItem();

        }

        public override void OnEditData()
        {
            base.OnEditData();
            //start code here
            Type newLabelInterfaceType = Type.GetType((defaultBindingSource.Current as ApptLabel).UseInterfaceType);
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
            ApptLabel saveObject = defaultBindingSource.Current as ApptLabel;
            saveObject.LabelCriteria = CriteriaToWhereClauseHelper.GetDataSetWhere(ctrl_FilterControl.FilterCriteria);
            var store = new StoreApptLabel()
            {
                Label = saveObject.ConvertTo<ApptLabelDto>()
            };
            var resp = ServiceClient.Post(store);
            
            //if the response was good, then notify the others.
            if (resp.ResponseStatus == null)
            {                
                saveObject = resp.Label.ConvertTo<ApptLabel>();
                Context.ServiceClient.Post(new ApptLabelsNotification()
                {
                    FromUserName = Context.LoggedInUser.UserName,
                    Selector = SelectorTypes.store,
                    Ids = new List<int>() { resp.Label.Id }
                });
            }
            base.OnSaveData();
            lblInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

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
                ApptLabel delLabel = defaultBindingSource.Current as ApptLabel;
                var delReq = new DeleteApptLabel { Id = delLabel.Id };
                ServiceClient.Delete(delReq);

                defaultBindingSource.RemoveCurrent();
                defaultBindingSource.ResetBindings(false);
                lblInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            return base.OnDeleteData();
        }

        public override void OnMessageEvent(ServiceStack.ServerEventMessage msg)
        {
            if (msg.Channel != typeof(ApptLabel).Name)
                return;

            if (msg.Selector == SelectorTypes.store)
            {
                var msgData = msg.Json.FromJson<ServerEventMessageData>();
                var lbls = msgData.jsonDataString.FromJson<List<ApptLabel>>();
                foreach (var lbl in lbls)
                {
                    var fnd = ((List<ApptLabel>)defaultBindingSource.List).Find(l => l.Id == lbl.Id);
                    if (fnd == null)
                        defaultBindingSource.Add(fnd);
                }
                this.BeginInvokeIfRequired(c => { c.defaultBindingSource.ResetBindings(false); });
            }
            if (msg.Selector == SelectorTypes.delete)
            {
                var msgData = msg.Json.FromJson<ServerEventMessageData>();
                var ids = msgData.jsonDataString.FromJson<List<int>>();
                foreach (var id in ids)
                {
                    var fnd = ((List<ApptLabel>)defaultBindingSource.List).Find(l => l.Id == id);
                    if (fnd != null)
                        defaultBindingSource.Remove(fnd);
                    this.BeginInvokeIfRequired(c => { c.defaultBindingSource.ResetBindings(false); });
                }
            }
            base.OnMessageEvent(msg);
        }

        private void defaultBindingSource_PositionChanged(object sender, EventArgs e)
        {
            UpdateFilterBindingsource();
            if (defaultBindingSource.Current != null)
                ctrl_FilterControl.FilterString = (defaultBindingSource.Current as ApptLabel).LabelCriteria;
            picEdit.Invalidate();
        }

        void UpdateFilterBindingsource()
        {
            //create an entity out of thin code.... and assign it to the datasource of the filter bs
            if (defaultBindingSource.Current != null)
            {
                string interfaceTypename = (defaultBindingSource.Current as ApptLabel).UseInterfaceType;
                Type t = Type.GetType(interfaceTypename);
                //filterBindingSource.DataSource = TempDataTableCreator.CreateDataTableFromType(t);
                filterBindingSource.DataSource = t.CreatePropertiesDataTableFromType();
            }
        }

        private void defaultBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            string currlbl = (defaultBindingSource.Current as ApptLabel).ViewName;
            string currIntfT = (defaultBindingSource.Current as ApptLabel).UseInterfaceType;
            ApptLabel newLabel = new ApptLabel();
            newLabel.ViewName = currlbl;
            newLabel.LabelName = "";
            newLabel.UseInterfaceType = currIntfT;
            e.NewObject = newLabel;
        }

        private void picEdit_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            SolidBrush backBrush = new SolidBrush(ctrl_txtLabelColour.Color);
            Region region = new Region(picEdit.ClientRectangle);
            e.Graphics.FillRegion(backBrush, region);
            e.Graphics.DrawString($"This is the big area of the appointment.{Environment.NewLine}Where there might be multiple lines of information",
                new Font(this.Font, FontStyle.Bold), new SolidBrush(ctrl_txtLabelForeColour.Color), 10, (picEdit.ClientRectangle.Bottom / 2));
        }
    }
}
