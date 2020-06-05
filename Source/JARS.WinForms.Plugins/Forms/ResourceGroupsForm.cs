using JARS.Entities;
using JARS.Core.Utils;
using JARS.Core.WinForms.Extensions;
using JARS.Core.WinForms.Forms;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Base;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//Note !!
//Resource Groups works in a strange way when using service stack.
//Nhibernate is set to save the resource first, and then the group, so when working with groups and service stack,
//the resource must contain the group, but the sub group must not contain the resource again, otherwise service stack will go into a circular reference 
//when serializing the object for saving..


namespace JARS.Win.Plugins
{
    public partial class ResourceGroupsForm : RibbonFormCrudBase
    {
        public ResourceGroupsForm()
        {
            InitializeComponent();
        }

        private void OperativeGroups_Load(object sender, EventArgs e)
        {
            SetGridControl(gridControlGroups);
            OnRefreshDataAsync();
        }

        public override async void OnRefreshDataAsync()
        {
            try
            {
                base.OnRefreshDataAsync();
                ResourceGroupsResponse resourceResponse = await Context.ServiceClient.GetAsync(new FindResourceGroups { FetchEagerly = true });
                this.InvokeIfRequired(frm =>
                {
                    defaultBindingSource.DataSource = resourceResponse.Groups.ConvertAll(g => g.ConvertTo<JarsResourceGroup>());
                    UpdateLinkedBindingSources();
                    FormEditState = FormEditStates.Browsing;
                });
            }
            catch (Exception exR)
            {
                OnExceptionEvent(exR);
            }
        }

        public override void OnAddData()
        {
            base.OnAddData();
            JarsResourceGroup newOp = defaultBindingSource.AddNew() as JarsResourceGroup;
            defaultBindingSource.Position = defaultBindingSource.IndexOf(newOp);
        }

        public override void OnEditData()
        {
            base.OnEditData();
            //start code here
        }

        public override void OnSaveData()
        {
            //see notes above!!
            try
            {
                JarsResourceGroup saveObj = defaultBindingSource.Current as JarsResourceGroup;
                //var upList = new List<OperativeGroup>() { saveOp };
                var store = new StoreResourceGroup()
                {
                    Group = saveObj.ConvertTo<ResourceGroupDto>()
                };
                var resp = ServiceClient.Post(store);

                //if the response was good, then notify the others.
                if (resp.ResponseStatus == null)
                {
                    saveObj = resp.Group.ConvertTo<JarsResourceGroup>();
                    Context.ServiceClient.Post(new ResourceGroupsNotification()
                    {
                        FromUserName = Context.LoggedInUser.UserName,
                        Selector = SelectorTypes.store,
                        Ids = new List<int>() { resp.Group.Id }
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
                JarsResourceGroup delObj = defaultBindingSource.Current as JarsResourceGroup;
                if (delObj.Resources.Count > 0)
                {
                    MessageBox.Show($"Unable to delete the group!{Environment.NewLine}There are still resources assigned to it.", "Unable to Delete Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (base.OnDeleteData(true))
                {
                    var delReq = new DeleteResourceGroup { Id = delObj.Id };
                    ServiceClient.Delete(delReq);
                    defaultBindingSource.RemoveCurrent();
                    defaultBindingSource.ResetBindings(false);
                }
            }
            catch (Exception exD)
            {
                OnExceptionEvent(exD);
            }
            return base.OnDeleteData();
        }

        public override void OnMessageEvent(ServiceStack.ServerEventMessage msg)
        {
            if (msg.Channel != typeof(JarsResourceGroup).Name)
                return;

            alertControl.FormDisplaySpeed = DevExpress.XtraBars.Alerter.AlertFormDisplaySpeed.Fast;
            alertControl.FormShowingEffect = DevExpress.XtraBars.Alerter.AlertFormShowingEffect.SlideHorizontal;
            alertControl.FormLocation = DevExpress.XtraBars.Alerter.AlertFormLocation.BottomRight;
            var alertInfo = new StringBuilder();
            switch (msg.Selector)
            {
                case SelectorTypes.delete:
                    List<string> ids = msg.Data.ConvertTo<List<string>>();
                    if (ids.Any())
                    {
                        foreach (var id in ids)
                        {
                            var rec = ((IEnumerable<JarsResourceGroup>)defaultBindingSource.List).FirstOrDefault(r => $"{r.Id}" == $"{id}");
                            if (rec != null)
                            {
                                this.InvokeIfRequired(frm => frm.defaultBindingSource.Remove(rec));
                            }
                            alertInfo.AppendLine($"Id: {id} (Removed) ");
                        }
                    }
                    this.InvokeIfRequired(frm => alertControl.Show(this, "Delete Group", alertInfo.ToString()));
                    break;
                case SelectorTypes.store:
                    List<JarsResourceGroup> storeDto = msg.Json.FromJson<List<JarsResourceGroup>>();

                    if (storeDto != null)
                    {
                        foreach (var entity in storeDto)
                        {
                            var rec = ((IEnumerable<JarsResourceGroup>)defaultBindingSource.List).FirstOrDefault(r => $"{r.Id}" == $"{entity.Id}");
                            if (rec != null)
                            {
                                rec = entity.PopulateWith(entity);
                            }
                            else
                            {
                                defaultBindingSource.List.Add(entity);
                            }
                            alertInfo.AppendLine($"{entity.Name} ({entity.Code}) ");
                        }
                    }
                    this.InvokeIfRequired(frm => alertControl.Show(this, "Group Stored", alertInfo.ToString()));
                    OnRefreshDataAsync();
                    break;
            }
            base.OnMessageEvent(msg);
        }

        private void defaultBindingSource_PositionChanged(object sender, EventArgs e)
        {
            UpdateLinkedBindingSources();
        }

        private void UpdateLinkedBindingSources()
        {
            if (defaultBindingSource.Current is JarsResourceGroup)
            {
                resourceBindingSource.DataSource = (defaultBindingSource.Current as JarsResourceGroup).Resources;
                resourceBindingSource.ResetBindings(false);
                gcOperatives.Refresh();
            }
        }

        private void ctrl_btnManUsers_Click(object sender, EventArgs e)
        {
            IList<SearchEntity<int>> existingUsers = new List<SearchEntity<int>>();
            IList<SearchEntity<int>> AllUsers = new List<SearchEntity<int>>();

            foreach (JarsResource user in resourceBindingSource.List)
            {
                existingUsers.Add(new SearchEntity<int> { DisplayText = user.DisplayName, ValueId = user.Id, IsSelected = true });
            }

            List<JarsResource> AllOpps = ServiceClient.Get(new FindResources() { IsActive = true, FetchEagerly = false }).Resources.ConvertAll(r => r.ConvertTo<JarsResource>());
            foreach (JarsResource user in AllOpps)
            {
                AllUsers.Add(new SearchEntity<int> { DisplayText = user.DisplayName, ValueId = user.Id, IsSelected = false });
            }

            IList<SearchEntity<int>> returnList = SelectEntitiesForm.ShowForm(existingUsers, AllUsers, "Select Users");
            //convert the return list to the entities.
            IList<JarsResource> newList = new List<JarsResource>();
            foreach (var sEnt in returnList)
            {
                JarsResource resource = AllOpps.FirstOrDefault(op => op.Id.ToString() == sEnt.ValueId.ToString());
                if (resource != null)
                    newList.Add(resource);
            }

            //see the notes at the top of the class!!
            //we need to make sure we don't end up with a circular reference, so we need to clean the resource group for serialization.

            (defaultBindingSource.Current as ResourceGroupDto).Resources.Clear();

            foreach (var nOp in newList)
            {
                JarsResource op = (defaultBindingSource.Current as JarsResourceGroup).Resources.FirstOrDefault(g => g.Id == nOp.Id);
                if (op == null)
                {
                    if (!nOp.Groups.Contains(defaultBindingSource.Current as JarsResourceGroup))
                        nOp.Groups.Add(defaultBindingSource.Current as JarsResourceGroup);

                    (defaultBindingSource.Current as JarsResourceGroup).Resources.Add(nOp);
                }
                else
                {
                    if (!op.Groups.Contains(defaultBindingSource.Current as JarsResourceGroup))
                        op.Groups.Add(defaultBindingSource.Current as JarsResourceGroup);
                }



            }
            foreach (var opP in (defaultBindingSource.Current as JarsResourceGroup).Resources)
            {
                foreach (var opPg in opP.Groups)
                {
                    opPg.Resources = null;
                }
            }
            // operativeBindingSource.ResetBindings(false);
            UpdateLinkedBindingSources();


        }

        private void ctrl_btnRemUser_Click(object sender, EventArgs e)
        {

        }

        private void ctrl_txtCode_Validating(object sender, CancelEventArgs e)
        {
            //check that the value is unique
            if (((IList<JarsResourceGroup>)defaultBindingSource.DataSource).FirstOrDefault(g => g.Code == ctrl_txtCode.Text) != null)
            {
                e.Cancel = true;
            }
        }
    }
}
