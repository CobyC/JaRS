using DevExpress.XtraGrid.Views.Grid;
using JARS.Core.Interfaces.Rules;
using JARS.Core.Rules;
using JARS.Core.Utils;
using JARS.Core.WinForms.Extensions;
using JARS.Core.WinForms.Forms;
using JARS.Entities;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JARS.Win.Plugins
{
    public partial class ResourcesForm : RibbonFormCrudBase
    {

        IList<IJarsRule> _ResourceEntityRulesList;
        /// <summary>
        /// Holds the list of conditions for this type of entity. 
        /// </summary>
        public IList<IJarsRule> ResourceEntityRulesList
        {
            get
            {
                if (_ResourceEntityRulesList == null)
                {
                    _ResourceEntityRulesList = new List<IJarsRule>();
                }
                return _ResourceEntityRulesList;
            }
            set
            {
                _ResourceEntityRulesList = value;
            }
        }

        public ResourcesForm()
        {
            InitializeComponent();
        }


        private void OperativesForm_Load(object sender, EventArgs e)
        {

            if (!this.DesignMode)
                if (SSEventClient != null)
                {
                    if (SSEventClient.Status == "Started")
                    {
                        SSEventClient.SubscribeToChannels(nameof(JarsRule));
                    }
                }

            SetGridControl(gridControlOperatives);
            OnRefreshDataAsync();
        }

        public override async void OnRefreshDataAsync()
        {
            try
            {
                base.OnRefreshDataAsync();
                ResourcesResponse operativeResponse = await Context.ServiceClient.GetAsync(new FindResources() { FetchEagerly = true });
                JarsRulesResponse conditionsResponse = await Context.ServiceClient.GetAsync(new FindJarsRules { TargetEntityTypeName = typeof(JarsResource).Name });
                defaultBindingSource.DataSource = operativeResponse.Resources.ConvertAll(r => r.ConvertTo<JarsResource>());
                ResourceEntityRulesList = conditionsResponse.Rules.ToList<IJarsRule>().Where(r => r.TargetCriteriaString.Contains("[Id] = ")).ToList();
                UpdateLinkedBindingSources();
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
            //Create a new resource record
            JarsResource newOp = defaultBindingSource.AddNew() as JarsResource;
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
                JarsResource saveObj = defaultBindingSource.Current as JarsResource;
                var store = new StoreResource()
                {
                    Resource = saveObj.ConvertTo<ResourceDto>()
                };
                var resp = ServiceClient.Post(store);

                //if the response was good, then notify the others.
                if (resp.ResponseStatus == null)
                {
                    saveObj = resp.Resource.ConvertTo<JarsResource>();
                    Context.ServiceClient.Post(new ResourcesNotification()
                    {
                        FromUserName = Context.LoggedInUser.UserName,
                        Selector = SelectorTypes.store,
                        Ids = new List<int>() { resp.Resource.Id }
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

                    JarsResource delObj = defaultBindingSource.Current as JarsResource;                   
                    var delReq = new DeleteResource { Id = delObj.Id };
                    ServiceClient.Delete(delReq);
                    defaultBindingSource.RemoveCurrent();
                    defaultBindingSource.ResetBindings(false);           

                }
                //call this after the record removal was successful.
            }
            catch (Exception exD)
            {
                OnExceptionEvent(exD);
            }
            return base.OnDeleteData();
        }

        public override void OnMessageEvent(ServiceStack.ServerEventMessage msg)
        {
            if (msg.Channel != typeof(JarsResource).Name)
                return;

            switch (msg.Selector)
            {
                case SelectorTypes.delete:
                    MessageBox.Show($"DELETE - Op:{msg.Op} Selector:{msg.Selector} Target:{msg.Target} Channel:{msg.Channel} EventId:{msg.EventId}");
                    break;
                case SelectorTypes.store:
                    if (msg.Channel == nameof(JarsRule))
                    {
                        JarsRule entityCon = msg.Json.FromJson<JarsRule>();
                        IJarsRule findCond = ResourceEntityRulesList.FirstOrDefault(c => c.Id == entityCon.Id);
                        if (findCond != null)
                        {
                            //replace
                            ResourceEntityRulesList.Remove(findCond);
                            ResourceEntityRulesList.Add(entityCon);
                        }
                        else
                        {
                            //add
                            ResourceEntityRulesList.Add(entityCon);
                        }
                        //update bindings
                        UpdateLinkedBindingSources();
                    }
                    //MessageBox.Show($"SAVE - Op:{msg.Op} Selector:{msg.Selector} Target:{msg.Target} Channel:{msg.Channel} EventId:{msg.EventId}");
                    break;
            }
            base.OnMessageEvent(msg);
        }

        private void ctrl_AddToGroup_Click(object sender, EventArgs e)
        {
            List<JarsResourceGroup> AllGrps = Context.ServiceClient.Get(new FindResourceGroups() { IsActive = true }).Groups.ConvertAll(gcConditions => gcConditions.ConvertTo<JarsResourceGroup>());
            IList<SearchEntity<int>> existingGroups = ((IList<JarsResourceGroup>)groupsBindingSource.List).GenerateSearchEntities(x => x.Name, x => x.Id, true);
            IList<SearchEntity<int>> allSearchGroups = AllGrps.GenerateSearchEntities(x => x.Name, x => x.Id, false);

            IList<SearchEntity<int>> returnList = SelectEntitiesForm.ShowForm(existingGroups, allSearchGroups, "Select Groups");
            //convert the return list to the entities.
            IList<JarsResourceGroup> newList = new List<JarsResourceGroup>();
            foreach (var sEnt in returnList)
            {
                JarsResourceGroup opG = AllGrps.FirstOrDefault(op => op.Id.ToString() == sEnt.ValueId.ToString());
                if (opG != null)
                    newList.Add(opG);
            }
            (defaultBindingSource.Current as JarsResource).Groups.Clear();

            //add the new group, if there were any
            foreach (var grp in newList)
            {
                if ((defaultBindingSource.Current as JarsResource).Groups.FirstOrDefault(g => g.Id == grp.Id) == null)
                    (defaultBindingSource.Current as JarsResource).Groups.Add(grp);
            }
            groupsBindingSource.ResetBindings(false);
        }

        private void ctrl_RemFromGroup_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove the group from this user?", "Remove Group?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //(defaultBindingSource.Current as Operative).Groups.Remove(groupsBindingSource.Current as OperativeGroup);
                groupsBindingSource.RemoveCurrent();
                defaultBindingSource.ResetBindings(false);
                //.ResetBindings(false);
            }
        }

        private void defaultBindingSource_PositionChanged(object sender, EventArgs e)
        {
            UpdateLinkedBindingSources();
        }

        private void UpdateLinkedBindingSources()
        {
            if (defaultBindingSource.Current is JarsResource)
            {
                groupsBindingSource.DataSource = ((JarsResource)defaultBindingSource.Current).Groups;
                skillsBindingSource.DataSource = ((JarsResource)defaultBindingSource.Current).Skills;
                JarsResource res = defaultBindingSource.Current as JarsResource;
                IList<IJarsRule> filterCondLst = ResourceEntityRulesList.Where(c => c.TargetTypeName == res.GetType().Name && c.TargetCriteriaString.Contains($"[Id] = {res.Id}")).OrderBy(c => c.Id).ToList();
                gcConditions.InvokeIfRequired(c => c.DataSource = filterCondLst);
                entityRuleBindingSource.DataSource = gcConditions.DataSource;
                gridControlGroups.InvokeIfRequired(c => c.Refresh());
                gridControlSkills.InvokeIfRequired(c => c.Refresh());
                gcConditions.InvokeIfRequired(c => c.Refresh());

            }

        }

        private void btnAddCondition_Click(object sender, EventArgs e)
        {
            if (defaultBindingSource.Current is JarsResource)
            {
                IJarsRule ec = JarsRulePopupForm.AddRuleOnEntity(defaultBindingSource.Current as JarsResource);
                if (ec != null)
                    OnSaveEntityRule(ec as JarsRule);
            }
        }

        private void btnEditCondition_Click(object sender, EventArgs e)
        {
            if (defaultBindingSource.Current is JarsResource)
            {
                IJarsRule ec = entityRuleBindingSource.Current as JarsRule;
                ec = JarsRulePopupForm.EditRuleOnEntity(defaultBindingSource.Current as JarsResource, ec);
                OnSaveEntityRule(ec as JarsRule);
            }
        }

        void OnSaveEntityRule(JarsRule rule)
        {
            StoreJarsRules storeRules = new StoreJarsRules()
            {
                Rules = new List<JarsRule>() { rule }
            };
            var response = ServiceClient.Post(storeRules);
            if (response.ResponseStatus == null)
            {
                rule = response.Rules.FirstNonDefault();
                ServiceClient.Post(new JarsRulesNotification()
                {
                    Selector = SelectorTypes.store,
                    FromUserName = Context.LoggedInUser.UserName,
                    Ids = response.Rules.Select(r => r.Id).ToList()
                });
            }
        }

        private void btnRemoveCondition_Click(object sender, EventArgs e)
        {
            if (defaultBindingSource.Current is JarsResource)
            {

                IJarsRule ec = entityRuleBindingSource.Current as JarsRule;
                IJarsRule findCond = ResourceEntityRulesList.FirstOrDefault(c => c.Id == ec.Id);
                if (findCond != null)
                {
                    //remove
                    ResourceEntityRulesList.Remove(findCond);
                }
                UpdateLinkedBindingSources();
            }
        }

        private void gvConditions_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {            
            if (((GridView)sender).GetFocusedRow() is IJarsRule ec )
            {
                entityRuleBindingSource.Position = ((GridView)sender).FocusedRowHandle;
            }
        }

        private void gvConditions_CalcPreviewText(object sender, CalcPreviewTextEventArgs e)
        {
            if (e.Row is IJarsRule er)
            {                
                e.PreviewText = $"{er.SourceTypeName} ({er.SourceCriteriaString}){Environment.NewLine}Were({er.TargetTypeName}{er.TargetCriteriaString})";
            }
        }
    }
}
