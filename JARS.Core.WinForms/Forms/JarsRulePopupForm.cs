using JARS.Core.Extensions;
using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Rules;
using JARS.Core.Rules;
using JARS.Core.Rules.Utils;
using JARS.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Forms
{
    /// <summary>
    /// This for is for setting rules on a specific target entity.
    /// The target entity needs to always be the entity where this form is triggered from.
    /// The source entity can be changed as required.
    /// </summary>
    public partial class JarsRulePopupForm : DevExpress.XtraEditors.XtraForm
    {
        public JarsRulePopupForm()
        {
            InitializeComponent();
            btnOK.Enabled = false;
        }

        IJarsRule CurrentRule;
        private IList<Type> _ruleTypes;

        public IList<Type> RuleTypes
        {
            get
            {
                if (_ruleTypes == null)
                {
                    _ruleTypes = Task.Run(async () => { return await AssemblyLoaderUtil.FindAllEntityTypesThatAllowRules(); }).Result;
                }
                return _ruleTypes;
            }
            set => _ruleTypes = value;
        }

        public IList<CheckInfoItem> RuleRunOns
        {
            get
            {
                IList<CheckInfoItem> checkItems = new List<CheckInfoItem>();
                foreach (var name in Enum.GetNames(typeof(RuleRunsOn)))
                {
                    checkItems.Add(new CheckInfoItem() { IsChecked = false, Name = name });
                }
                return checkItems;
            }
        }

        void InitForm(IEntityBase targetType, Type sourceType = null, IJarsRule currentRule = null)
        {
            string targetDefaultCriteria = $"[{nameof(targetType.Id)}] = {targetType.Id}";
            if (currentRule == null)
                currentRule = new JarsRule()
                {
                    SourceCriteriaString = "",
                    TargetCriteriaString = targetDefaultCriteria,
                    TargetTypeName = targetType.GetType().Name,
                    RuleEvaluation = RuleEvaluation.Both
                };

            CurrentRule = currentRule;

            if (sourceType == null)
                sourceType = targetType.GetType();

            CboRuleApplicators.Properties.AddEnum(typeof(RuleRunsOn));

            CurrentRule.SourceTypeName = sourceType.Name;

            foreach (Type t in RuleTypes)
            {
                cboSourceEntity.Properties.Items.Add(t.ToTypeNameItem());
            }

            conditionBindingSource.DataSource = CurrentRule;
            conditionBindingSource.ResetBindings(false);

            //find the type in the dropdown and set the position
            TypeNameItem findItem = null;
            foreach (TypeNameItem item in cboSourceEntity.Properties.Items)
            {
                if (item.Name == CurrentRule.SourceTypeName)
                {
                    findItem = item;
                    break;
                }
            }

            if (findItem != null)
                cboSourceEntity.SelectedIndex = cboSourceEntity.Properties.Items.IndexOf(findItem);

            if (!CurrentRule.TargetCriteriaString.Contains(targetDefaultCriteria))
                CurrentRule.TargetCriteriaString += targetDefaultCriteria;

            cboConditionTest.Properties.Items.AddRange(new[] { RulePassesWhen.IsTrue.ToString(), RulePassesWhen.IsFalse.ToString() });

            filterBindingSource.DataSource = sourceType.CreatePropertiesDataTableFromType();
            filterEditorControl1.FilterString = CurrentRule.SourceCriteriaString;

            this.Text = $"Rule on {targetType.GetType().Name}";
            //UpdateRuleInfo();
        }

        private void UpdateRuleInfo()
        {
            //CurrentRule.SourceCriteriaString = filterEditorControl1.FilterString;
            CurrentRule = conditionBindingSource.Current as IJarsRule;
            string source = $"Check [{CurrentRule.SourceTypeName}] for ({CurrentRule.SourceCriteriaString})";
            string patchIn = string.Empty;

            if (!string.IsNullOrEmpty(CurrentRule.TargetCriteriaString))
                patchIn = $" where ({CurrentRule.TargetTypeName} {CurrentRule.TargetCriteriaString})";

            txtRuleInfo.Text = $"{source}.{Environment.NewLine}When targeting [{CurrentRule.TargetTypeName}]{patchIn}.{Environment.NewLine}The rule passes when the criteria [{CurrentRule.RulePassesWhen}].";
        }

        /// <summary>
        /// Set a rule for an entity, the entity Id is used to link the rule to the specific entity.
        /// The <paramref name="sourceType"/> is the entity whose properties are used to set up, and check the rule.
        /// ie, the properties from the <paramref name="sourceType"/> needs to pass the criteria in order to pass the rule.
        /// The <paramref name="targetEntity"/> is the entity that is affected by the rules.
        /// i.e. a [JobType](source) needs to be booked against a [Resource](target), if the rules set against the target ([Resource]) pass, the [Job] can be booked for the specified Resource.
        /// </summary>
        /// <param name="sourceType">The entity whose properties will be used for setting up the rules</param>
        /// <param name="targetEntity">The single entity that will be impacted by the source.</param>
        /// <param name="currentRule">The rule that needs to be edited, null if a new rule needs to be created</param>
        /// <returns>Returns the new or edited rule</returns>
        public static IJarsRule AddRuleOnEntity(IEntityBase targetEntity, Type sourceType = null)
        {
            //now create a rule using the properties from the trigger entity, ie the trigger entity needs to meet conditions to be allowed for processing with the target entity           
            IJarsRule newRule = null;
            JarsRulePopupForm frm = new JarsRulePopupForm();
            frm.InitForm(targetEntity, sourceType);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                newRule = frm.CurrentRule;
            }
            return newRule;
        }

        public static IJarsRule EditRuleOnEntity(IEntityBase targetEntity, IJarsRule currentRule)
        {
            JarsRulePopupForm frm = new JarsRulePopupForm();
            Type source = frm.RuleTypes.FirstOrDefault(t => t.Name == currentRule.SourceTypeName);
            frm.InitForm(targetEntity, source, currentRule);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                currentRule = frm.CurrentRule;
            }
            return currentRule;
        }

        private void FilterEditorControl1_FilterTextChanged(object sender, DevExpress.XtraEditors.FilterTextChangedEventArgs e)
        {
            if (CriteriaHelper.ValidateStringAsFilterCriteria(filterEditorControl1.FilterString) && dxValidationProvider.Validate())
                btnOK.Enabled = true;
            else
                btnOK.Enabled = false;

            CurrentRule.SourceCriteriaString = e.Text;
            UpdateRuleInfo();
        }

        private void TxtRuleDescription_Properties_Validating(object sender, CancelEventArgs e)
        {
            dxValidationProvider.Validate();
        }
        private void CboRuleApplicators_Validating(object sender, CancelEventArgs e)
        {
            dxValidationProvider.Validate();
        }

        private void BtnOK_MouseEnter(object sender, EventArgs e)
        {
            btnOK.Enabled = (CriteriaHelper.ValidateStringAsFilterCriteria(filterEditorControl1.FilterString) && dxValidationProvider.Validate()) ? true : false;
        }

        private void DxValidationProvider_ValidationFailed(object sender, DevExpress.XtraEditors.DXErrorProvider.ValidationFailedEventArgs e)
        {
            btnOK.Enabled = false;
        }

        private void DxValidationProvider_ValidationSucceeded(object sender, DevExpress.XtraEditors.DXErrorProvider.ValidationSucceededEventArgs e)
        {
            btnOK.Enabled = true;
        }

        private void CboSourceEntity_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboSourceEntity.SelectedItem != null && cboSourceEntity.SelectedItem is TypeNameItem)
            {
                TypeNameItem sourceItem = (TypeNameItem)cboSourceEntity.SelectedItem;
                CurrentRule.SourceTypeName = sourceItem.Name;
                filterEditorControl1.FilterString = string.Empty;
                filterBindingSource.DataSource = sourceItem.Type.CreatePropertiesDataTableFromType();
                filterBindingSource.ResetBindings(true);
                filterEditorControl1.Refresh();
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            CurrentRule = conditionBindingSource.Current as IJarsRule;
        }

        private void ConditionBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            UpdateRuleInfo();
        }

        private void CboConditionTest_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboConditionTest.EditValue == null)
                return;

            CurrentRule.RulePassesWhen = (RulePassesWhen)Enum.Parse(typeof(RulePassesWhen), $"{cboConditionTest.EditValue}");
        }


    }
}
