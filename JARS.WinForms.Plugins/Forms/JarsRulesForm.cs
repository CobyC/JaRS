using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout.Utils;
using JARS.Core.Extensions;
using JARS.Core.Interfaces;
using JARS.Core.Interfaces.Rules;
using JARS.Core.Rules;
using JARS.Core.Rules.Utils;
using JARS.Core.Utils;
using JARS.Core.WinForms.Forms;
using JARS.Core.WinForms.Utils;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JARS.Win.Plugins
{

    public partial class JarsRulesForm : RibbonFormCrudBase
    {
        public JarsRulesForm()
        {
            InitializeComponent();

            //discover all the entities that are marked with the AllowRules attribute.
            IList<Type> types = Task.Run(async () => { return await AssemblyLoaderUtil.FindAllEntityTypesThatAllowRules(); }).Result;

            ctrl_cboSourceEntityType.SuspendLayout();
            ctrl_cboTargetEntityType.SuspendLayout();
            foreach (Type t in types)
            {

                ctrl_cboSourceEntityType.Properties.Items.Add(t.ToTypeNameItem());
                ctrl_cboTargetEntityType.Properties.Items.Add(t.ToTypeNameItem());
            }
            ctrl_cboSourceEntityType.ResumeLayout(true);
            ctrl_cboTargetEntityType.ResumeLayout(true);

            ctrl_cboRuleApplicators.Properties.AddEnum(typeof(RuleRunsOn));
            ctrl_cboRuleValidWhen.Properties.Items.AddRange(Enum.GetNames(typeof(RulePassesWhen)));
            ctrl_cbRuleEvaluation.Properties.Items.AddRange(Enum.GetNames(typeof(RuleEvaluation)));

        }

        private void GridViewEntityRules_CalcPreviewText(object sender, DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventArgs e)
        {
            JarsRule current = e.Row as JarsRule;
            if (current != null)
            {
                e.PreviewText = $"{current.SourceCriteriaString}{Environment.NewLine}{current.TargetCriteriaString}";
            }

        }

        public IList<JarsRule> _EntityRules { get; set; }

        private void EntityRulesForm_Load(object sender, System.EventArgs e)
        {
            if (!this.DesignMode)
                if (SSEventClient != null)
                {
                    if (SSEventClient.Status == "Started")
                    {
                        SSEventClient.SubscribeToChannels(nameof(JarsRule));
                    }
                }

            SetGridControl(gridControlEntityRules);
            OnRefreshDataAsync();
        }



        public override async void OnRefreshDataAsync()
        {
            try
            {
                base.OnRefreshDataAsync();

                JarsRulesResponse entityRulesResponse = await ServiceClient.GetAsync(new FindJarsRules());
                ////    EntityRulesResponse conditionsResponse = await SSEventClient.ServiceClient.PostAsync(new FindEntityRules { EntityTypeNameLike = typeof(Resource).AssemblyQualifiedName });
                defaultBindingSource.DataSource = entityRulesResponse.Rules;
                defaultBindingSource.ResetBindings(false);
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
            //Create a new entityRule record
            ctrl_frEdSourceEntityRule.FilterString = string.Empty;
            ctrl_frEdTargetEntityRule.FilterString = string.Empty;
            JarsRule newOp = defaultBindingSource.AddNew() as JarsRule;
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
                this.Validate();
                if (CriteriaHelper.ValidateStringAsFilterCriteria(ctrl_frEdSourceEntityRule.FilterString)
                    && CriteriaHelper.ValidateStringAsFilterCriteria(ctrl_frEdTargetEntityRule.FilterString, true)
                    && dxValidator.Validate())
                {
                    JarsRule saveRule = defaultBindingSource.Current as JarsRule;
                    saveRule.SourceCriteriaString = ctrl_frEdSourceEntityRule.FilterString;
                    saveRule.TargetCriteriaString = ctrl_frEdTargetEntityRule.FilterString;

                    StoreJarsRule store = new StoreJarsRule()
                    { Rule = saveRule };

                    JarsRuleResponse response = ServiceClient.Post(store);

                    if (response.ResponseStatus == null)
                    {
                        saveRule = response.Rule;

                        ServiceClient.Post(new JarsRulesNotification
                        {
                            Selector = SelectorTypes.store,
                            FromUserName = LoggedInUser.UserName,
                            Ids = new List<int> { response.Rule.Id }
                        });
                    }

                    base.OnSaveData();
                }
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
                    JarsRule delOp = defaultBindingSource.Current as JarsRule;

                    DeleteJarsRules delete = new DeleteJarsRules()
                    {
                        Id = delOp.Id
                    };
                    ServiceClient.Delete(delete);
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
            if (msg.Channel != typeof(JarsRule).Name)
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
                        //IEntityRule findCond = ResourceEntityConditionsList.FirstOrDefault(c => c.Id == entityCon.Id);
                        //if (findCond != null)
                        //{
                        //    //replace
                        //    ResourceEntityConditionsList.Remove(findCond);
                        //    ResourceEntityConditionsList.Add(entityCon);
                        //}
                        //else
                        //{
                        //    //add
                        //    ResourceEntityConditionsList.Add(entityCon);
                        //}
                        //update bindings
                        UpdateLinkedBindingSources();
                    }
                    //MessageBox.Show($"SAVE - Op:{msg.Op} Selector:{msg.Selector} Target:{msg.Target} Channel:{msg.Channel} EventId:{msg.EventId}");
                    break;
            }
            base.OnMessageEvent(msg);
        }


        private void UpdateLinkedBindingSources()
        {
            JarsRule currentRule = defaultBindingSource.Current as JarsRule;
            if (currentRule != null)
            {
                ctrl_frEdSourceEntityRule.FilterString = currentRule.SourceCriteriaString;
                ctrl_frEdTargetEntityRule.FilterString = currentRule.TargetCriteriaString;
                ctrl_frEdSourceEntityRule.Invalidate();
                ctrl_frEdTargetEntityRule.Invalidate();
                UpdateRuleSummary();
            }
        }

        private void UpdateRuleSummary()
        {
            //$"Check [{currentRule.SourceTypeName}] for [{patchCriteria}] when targeting [{currentRule.TargetTypeName}]{patchIn}. The criteria [{currentRule.RuleEvaluation}]"
            if (defaultBindingSource.Current is JarsRule currentRule)
            {
                string source = string.Empty;
                string target = string.Empty;
                string summary = string.Empty;
                switch (currentRule.RuleEvaluation)
                {
                    case RuleEvaluation.SourceOnly:
                        summary = $"Check [{currentRule.SourceTypeName}] for ({currentRule.SourceCriteriaString})";
                        break;
                    case RuleEvaluation.TargetOnly:
                        summary = $"Check [{currentRule.TargetTypeName}] for ({currentRule.TargetCriteriaString})";
                        break;
                    case RuleEvaluation.Both:
                        source = $"Check [{currentRule.SourceTypeName}] for ({currentRule.SourceCriteriaString})";
                        target = $" where ({currentRule.TargetTypeName} {currentRule.TargetCriteriaString})";
                        summary = $"{source}. {Environment.NewLine}When targeting [{currentRule.TargetTypeName}]{target}.";

                        break;
                    default:
                        break;
                }
                txtRuleSummary.Text = $"{summary}. {Environment.NewLine}The rule passes when the criteria [{currentRule.RulePassesWhen}].";
            }
        }

        private void ctrl_cboSourceEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeNameItem sourceItem = ctrl_cboSourceEntityType.SelectedItem as TypeNameItem;
            if (sourceItem != null)
            {
                filterSourceBindingSource.DataSource = sourceItem.Type.CreatePropertiesDataTableFromType();
                filterSourceBindingSource.ResetBindings(true);
                ctrl_frEdSourceEntityRule.Refresh();
            }
        }
        private void ctrl_cboTargetEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeNameItem targetItem = ctrl_cboTargetEntityType.SelectedItem as TypeNameItem;
            if (targetItem != null)
            {
                filterTargetBindingSource.DataSource = targetItem.Type.CreatePropertiesDataTableFromType();
                filterTargetBindingSource.ResetBindings(true);
                ctrl_frEdTargetEntityRule.Refresh();
            }
        }

        private void defaultBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            UpdateLinkedBindingSources();
        }

        private void ctrl_cboSourceEntityType_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            if (e.Value is string)
            {
                foreach (TypeNameItem item in ctrl_cboSourceEntityType.Properties.Items)
                {
                    if (item.Name == (string)e.Value)
                    {
                        e.Value = item;
                        e.Handled = true;
                        break;
                    }
                }
            }
        }

        private void ctrl_cboTargetEntityType_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            if (e.Value is string)
            {
                foreach (TypeNameItem item in ctrl_cboTargetEntityType.Properties.Items)
                {
                    if (item.Name == (string)e.Value)
                    {
                        e.Value = item;
                        e.Handled = true;
                        break;
                    }
                }
            }
        }

        private void ctrl_frEdSourceEntityRule_FilterTextChanged(object sender, DevExpress.XtraEditors.FilterTextChangedEventArgs e)
        {
            JarsRule currentRule = defaultBindingSource.Current as JarsRule;
            if (currentRule != null)
            {
                currentRule.SourceCriteriaString = ctrl_frEdSourceEntityRule.FilterString;
                UpdateRuleSummary();
            }
        }

        private void ctrl_frEdTargetEntityRule_FilterTextChanged(object sender, DevExpress.XtraEditors.FilterTextChangedEventArgs e)
        {
            JarsRule currentRule = defaultBindingSource.Current as JarsRule;
            if (currentRule != null)
            {
                currentRule.TargetCriteriaString = ctrl_frEdTargetEntityRule.FilterString;
                UpdateRuleSummary();
            }
        }


        private void ctrl_cbRuleEvaluation_SelectedValueChanged(object sender, EventArgs e)
        {
            RuleEvaluation eval = (RuleEvaluation)Enum.Parse(typeof(RuleEvaluation), ctrl_cbRuleEvaluation.EditValue.ToString());

            if (FormEditState == FormEditStates.Adding || FormEditState == FormEditStates.Editing)
            {
                ctrl_cboTargetEntityType.Visible = (eval == (RuleEvaluation.TargetOnly | RuleEvaluation.Both));
                ctrl_frEdTargetEntityRule.Visible = (eval == (RuleEvaluation.TargetOnly | RuleEvaluation.Both));

                ctrl_cboSourceEntityType.Visible = (eval == (RuleEvaluation.SourceOnly | RuleEvaluation.Both));
                ctrl_frEdSourceEntityRule.Visible = (eval == (RuleEvaluation.SourceOnly | RuleEvaluation.Both));
            }
            switch (eval)
            {
                case RuleEvaluation.SourceOnly:
                    lo_TargetEntity.Visibility = LayoutVisibility.Never;
                    lo_TargetCriteriaTree.Visibility = LayoutVisibility.Never;
                    lo_SourceEntity.Visibility = LayoutVisibility.Always;
                    lo_SourceCriteriaTree.Visibility = LayoutVisibility.Always;
                    break;
                case RuleEvaluation.TargetOnly:
                    lo_TargetEntity.Visibility = LayoutVisibility.Always;
                    lo_TargetCriteriaTree.Visibility = LayoutVisibility.Always;
                    lo_SourceEntity.Visibility = LayoutVisibility.Never;
                    lo_SourceCriteriaTree.Visibility = LayoutVisibility.Never;
                    break;
                case RuleEvaluation.Both:
                    lo_TargetEntity.Visibility = LayoutVisibility.Always;
                    lo_TargetCriteriaTree.Visibility = LayoutVisibility.Always;
                    lo_SourceEntity.Visibility = LayoutVisibility.Always;
                    lo_SourceCriteriaTree.Visibility = LayoutVisibility.Always;
                    //dxValidator.GetValidationRule(ctrl_frEdTargetEntityRule);
                    break;
                default:
                    break;
            }

        }

        private void ctrl_cbRuleEvaluation_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit comboBox)
            {
                if (comboBox.EditValue is RuleEvaluation)
                    ctrl_cbRuleEvaluation_SelectedValueChanged(sender, e);
            }
        }
    }
}
