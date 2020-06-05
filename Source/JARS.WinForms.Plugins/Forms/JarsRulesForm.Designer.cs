namespace JARS.Win.Plugins
{
    partial class JarsRulesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule3 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule4 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.gridControlEntityRules = new DevExpress.XtraGrid.GridControl();
            this.gridViewEntityRules = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSourceTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTargetTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRuleRunsOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRuleEvaluation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIncludeTarget = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ctrl_frEdTargetEntityRule = new DevExpress.DataAccess.UI.FilterEditorControl();
            this.filterTargetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ctrl_frEdSourceEntityRule = new DevExpress.DataAccess.UI.FilterEditorControl();
            this.filterSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ctrl_cboSourceEntityType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ctrl_cboTargetEntityType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ctrl_cboRuleValidWhen = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtRuleSummary = new DevExpress.XtraEditors.MemoEdit();
            this.ctrl_cboRuleApplicators = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.ctrl_RuleName = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.ctrl_cbRuleEvaluation = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lo_SourceCriteriaTree = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lo_TargetCriteriaTree = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lo_TargetEntity = new DevExpress.XtraLayout.LayoutControlItem();
            this.lo_SourceEntity = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.xtraTabPageList.SuspendLayout();
            this.xtraTabPageDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEntityRules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEntityRules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filterTargetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cboSourceEntityType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cboTargetEntityType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cboRuleValidWhen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRuleSummary.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cboRuleApplicators.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_RuleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cbRuleEvaluation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lo_SourceCriteriaTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lo_TargetCriteriaTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lo_TargetEntity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lo_SourceEntity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // baseRibbonControl
            // 
            this.baseRibbonControl.ExpandCollapseItem.Id = 0;
            this.baseRibbonControl.Margin = new System.Windows.Forms.Padding(6);
            // 
            // 
            // 
            this.baseRibbonControl.SearchEditItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.baseRibbonControl.SearchEditItem.EditWidth = 150;
            this.baseRibbonControl.SearchEditItem.Id = -5000;
            this.baseRibbonControl.SearchEditItem.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.baseRibbonControl.Size = new System.Drawing.Size(822, 143);
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.xtraTabControl.SelectedTabPage = this.xtraTabPageList;
            this.xtraTabControl.Size = new System.Drawing.Size(822, 452);
            // 
            // xtraTabPageList
            // 
            this.xtraTabPageList.Controls.Add(this.gridControlEntityRules);
            this.xtraTabPageList.Margin = new System.Windows.Forms.Padding(4);
            this.xtraTabPageList.Size = new System.Drawing.Size(816, 424);
            // 
            // xtraTabPageDetails
            // 
            this.xtraTabPageDetails.Controls.Add(this.layoutControl1);
            this.xtraTabPageDetails.Margin = new System.Windows.Forms.Padding(4);
            this.xtraTabPageDetails.Size = new System.Drawing.Size(816, 424);
            // 
            // defaultBindingSource
            // 
            this.defaultBindingSource.DataSource = typeof(JARS.Core.Rules.JarsRule);
            this.defaultBindingSource.CurrentItemChanged += new System.EventHandler(this.defaultBindingSource_CurrentItemChanged);
            // 
            // dxValidator
            // 
            this.dxValidator.ValidateHiddenControls = false;
            this.dxValidator.ValidationMode = DevExpress.XtraEditors.DXErrorProvider.ValidationMode.Auto;
            // 
            // gridControlEntityRules
            // 
            this.gridControlEntityRules.DataSource = this.defaultBindingSource;
            this.gridControlEntityRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlEntityRules.Location = new System.Drawing.Point(0, 0);
            this.gridControlEntityRules.MainView = this.gridViewEntityRules;
            this.gridControlEntityRules.MenuManager = this.baseRibbonControl;
            this.gridControlEntityRules.Name = "gridControlEntityRules";
            this.gridControlEntityRules.Size = new System.Drawing.Size(816, 424);
            this.gridControlEntityRules.TabIndex = 1;
            this.gridControlEntityRules.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewEntityRules});
            // 
            // gridViewEntityRules
            // 
            this.gridViewEntityRules.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.colSourceTypeName,
            this.colTargetTypeName,
            this.colRuleRunsOn,
            this.colRuleEvaluation,
            this.colId,
            this.colIncludeTarget});
            this.gridViewEntityRules.GridControl = this.gridControlEntityRules;
            this.gridViewEntityRules.Name = "gridViewEntityRules";
            this.gridViewEntityRules.OptionsView.ShowPreview = true;
            this.gridViewEntityRules.PreviewLineCount = 2;
            this.gridViewEntityRules.CalcPreviewText += new DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventHandler(this.GridViewEntityRules_CalcPreviewText);
            // 
            // colName
            // 
            this.colName.Caption = "Name";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 256;
            // 
            // colSourceTypeName
            // 
            this.colSourceTypeName.Caption = "Source Type";
            this.colSourceTypeName.FieldName = "SourceTypeName";
            this.colSourceTypeName.Name = "colSourceTypeName";
            this.colSourceTypeName.Visible = true;
            this.colSourceTypeName.VisibleIndex = 1;
            this.colSourceTypeName.Width = 116;
            // 
            // colTargetTypeName
            // 
            this.colTargetTypeName.Caption = "Target Type";
            this.colTargetTypeName.FieldName = "TargetTypeName";
            this.colTargetTypeName.Name = "colTargetTypeName";
            this.colTargetTypeName.Visible = true;
            this.colTargetTypeName.VisibleIndex = 2;
            this.colTargetTypeName.Width = 110;
            // 
            // colRuleRunsOn
            // 
            this.colRuleRunsOn.Caption = "Runs On";
            this.colRuleRunsOn.FieldName = "RuleRunsOn";
            this.colRuleRunsOn.Name = "colRuleRunsOn";
            this.colRuleRunsOn.Visible = true;
            this.colRuleRunsOn.VisibleIndex = 3;
            this.colRuleRunsOn.Width = 104;
            // 
            // colRuleEvaluation
            // 
            this.colRuleEvaluation.FieldName = "RulePassesWhen";
            this.colRuleEvaluation.Name = "colRuleEvaluation";
            this.colRuleEvaluation.Visible = true;
            this.colRuleEvaluation.VisibleIndex = 4;
            this.colRuleEvaluation.Width = 106;
            // 
            // colId
            // 
            this.colId.Caption = "Id";
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colIncludeTarget
            // 
            this.colIncludeTarget.Caption = "Include Target";
            this.colIncludeTarget.FieldName = "IncludeTarget";
            this.colIncludeTarget.Name = "colIncludeTarget";
            this.colIncludeTarget.Visible = true;
            this.colIncludeTarget.VisibleIndex = 5;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ctrl_frEdTargetEntityRule);
            this.layoutControl1.Controls.Add(this.ctrl_frEdSourceEntityRule);
            this.layoutControl1.Controls.Add(this.ctrl_cboSourceEntityType);
            this.layoutControl1.Controls.Add(this.ctrl_cboTargetEntityType);
            this.layoutControl1.Controls.Add(this.ctrl_cboRuleValidWhen);
            this.layoutControl1.Controls.Add(this.txtRuleSummary);
            this.layoutControl1.Controls.Add(this.ctrl_cboRuleApplicators);
            this.layoutControl1.Controls.Add(this.ctrl_RuleName);
            this.layoutControl1.Controls.Add(this.ctrl_txtDescription);
            this.layoutControl1.Controls.Add(this.ctrl_cbRuleEvaluation);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(980, 104, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(816, 424);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ctrl_frEdTargetEntityRule
            // 
            this.ctrl_frEdTargetEntityRule.ActiveView = DevExpress.XtraFilterEditor.FilterEditorActiveView.Visual;
            this.ctrl_frEdTargetEntityRule.AppearanceEmptyValueColor = System.Drawing.Color.Empty;
            this.ctrl_frEdTargetEntityRule.AppearanceFieldNameColor = System.Drawing.Color.Empty;
            this.ctrl_frEdTargetEntityRule.AppearanceGroupOperatorColor = System.Drawing.Color.Empty;
            this.ctrl_frEdTargetEntityRule.AppearanceOperatorColor = System.Drawing.Color.Empty;
            this.ctrl_frEdTargetEntityRule.AppearanceValueColor = System.Drawing.Color.Empty;
            this.ctrl_frEdTargetEntityRule.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "TargetCriteriaString", true));
            this.ctrl_frEdTargetEntityRule.Location = new System.Drawing.Point(410, 190);
            this.ctrl_frEdTargetEntityRule.Margin = new System.Windows.Forms.Padding(2);
            this.ctrl_frEdTargetEntityRule.Name = "ctrl_frEdTargetEntityRule";
            this.ctrl_frEdTargetEntityRule.ShowDateTimeFunctions = DevExpress.XtraEditors.DateTimeFunctionsShowMode.Advanced;
            this.ctrl_frEdTargetEntityRule.ShowOperandTypeIcon = true;
            this.ctrl_frEdTargetEntityRule.Size = new System.Drawing.Size(402, 160);
            this.ctrl_frEdTargetEntityRule.SourceControl = this.filterTargetBindingSource;
            this.ctrl_frEdTargetEntityRule.TabIndex = 11;
            this.ctrl_frEdTargetEntityRule.Text = "filterTargetEditorControl";
            this.ctrl_frEdTargetEntityRule.UseMenuForOperandsAndOperators = false;
            this.ctrl_frEdTargetEntityRule.ViewMode = DevExpress.XtraEditors.FilterEditorViewMode.Visual;
            this.ctrl_frEdTargetEntityRule.FilterTextChanged += new DevExpress.XtraEditors.FilterTextChangedEventHandler(this.ctrl_frEdTargetEntityRule_FilterTextChanged);
            // 
            // ctrl_frEdSourceEntityRule
            // 
            this.ctrl_frEdSourceEntityRule.ActiveView = DevExpress.XtraFilterEditor.FilterEditorActiveView.Visual;
            this.ctrl_frEdSourceEntityRule.AppearanceEmptyValueColor = System.Drawing.Color.Empty;
            this.ctrl_frEdSourceEntityRule.AppearanceFieldNameColor = System.Drawing.Color.Empty;
            this.ctrl_frEdSourceEntityRule.AppearanceGroupOperatorColor = System.Drawing.Color.Empty;
            this.ctrl_frEdSourceEntityRule.AppearanceOperatorColor = System.Drawing.Color.Empty;
            this.ctrl_frEdSourceEntityRule.AppearanceValueColor = System.Drawing.Color.Empty;
            this.ctrl_frEdSourceEntityRule.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "SourceCriteriaString", true));
            this.ctrl_frEdSourceEntityRule.Location = new System.Drawing.Point(4, 190);
            this.ctrl_frEdSourceEntityRule.Margin = new System.Windows.Forms.Padding(2);
            this.ctrl_frEdSourceEntityRule.Name = "ctrl_frEdSourceEntityRule";
            this.ctrl_frEdSourceEntityRule.ShowDateTimeFunctions = DevExpress.XtraEditors.DateTimeFunctionsShowMode.Advanced;
            this.ctrl_frEdSourceEntityRule.ShowOperandTypeIcon = true;
            this.ctrl_frEdSourceEntityRule.Size = new System.Drawing.Size(402, 160);
            this.ctrl_frEdSourceEntityRule.SourceControl = this.filterSourceBindingSource;
            this.ctrl_frEdSourceEntityRule.TabIndex = 9;
            this.ctrl_frEdSourceEntityRule.Text = "filterSourceEditorControl";
            this.ctrl_frEdSourceEntityRule.UseMenuForOperandsAndOperators = false;
            this.ctrl_frEdSourceEntityRule.ViewMode = DevExpress.XtraEditors.FilterEditorViewMode.Visual;
            this.ctrl_frEdSourceEntityRule.FilterTextChanged += new DevExpress.XtraEditors.FilterTextChangedEventHandler(this.ctrl_frEdSourceEntityRule_FilterTextChanged);
            // 
            // ctrl_cboSourceEntityType
            // 
            this.ctrl_cboSourceEntityType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "SourceTypeName", true));
            this.ctrl_cboSourceEntityType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "SourceTypeName", true));
            this.ctrl_cboSourceEntityType.Location = new System.Drawing.Point(4, 166);
            this.ctrl_cboSourceEntityType.Margin = new System.Windows.Forms.Padding(2);
            this.ctrl_cboSourceEntityType.MenuManager = this.baseRibbonControl;
            this.ctrl_cboSourceEntityType.Name = "ctrl_cboSourceEntityType";
            this.ctrl_cboSourceEntityType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ctrl_cboSourceEntityType.Properties.Sorted = true;
            this.ctrl_cboSourceEntityType.Size = new System.Drawing.Size(402, 20);
            this.ctrl_cboSourceEntityType.StyleController = this.layoutControl1;
            this.ctrl_cboSourceEntityType.TabIndex = 5;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information;
            this.dxValidator.SetValidationRule(this.ctrl_cboSourceEntityType, conditionValidationRule1);
            this.ctrl_cboSourceEntityType.SelectedIndexChanged += new System.EventHandler(this.ctrl_cboSourceEntityType_SelectedIndexChanged);
            this.ctrl_cboSourceEntityType.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(this.ctrl_cboSourceEntityType_ParseEditValue);
            // 
            // ctrl_cboTargetEntityType
            // 
            this.ctrl_cboTargetEntityType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "TargetTypeName", true));
            this.ctrl_cboTargetEntityType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "TargetTypeName", true));
            this.ctrl_cboTargetEntityType.Location = new System.Drawing.Point(410, 166);
            this.ctrl_cboTargetEntityType.Margin = new System.Windows.Forms.Padding(2);
            this.ctrl_cboTargetEntityType.MenuManager = this.baseRibbonControl;
            this.ctrl_cboTargetEntityType.Name = "ctrl_cboTargetEntityType";
            this.ctrl_cboTargetEntityType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ctrl_cboTargetEntityType.Size = new System.Drawing.Size(402, 20);
            this.ctrl_cboTargetEntityType.StyleController = this.layoutControl1;
            this.ctrl_cboTargetEntityType.TabIndex = 6;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "Please Select a Value";
            conditionValidationRule2.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information;
            this.dxValidator.SetValidationRule(this.ctrl_cboTargetEntityType, conditionValidationRule2);
            this.ctrl_cboTargetEntityType.SelectedIndexChanged += new System.EventHandler(this.ctrl_cboTargetEntityType_SelectedIndexChanged);
            this.ctrl_cboTargetEntityType.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(this.ctrl_cboTargetEntityType_ParseEditValue);
            // 
            // ctrl_cboRuleValidWhen
            // 
            this.ctrl_cboRuleValidWhen.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "RulePassesWhen", true));
            this.ctrl_cboRuleValidWhen.Location = new System.Drawing.Point(410, 126);
            this.ctrl_cboRuleValidWhen.Margin = new System.Windows.Forms.Padding(2);
            this.ctrl_cboRuleValidWhen.MenuManager = this.baseRibbonControl;
            this.ctrl_cboRuleValidWhen.Name = "ctrl_cboRuleValidWhen";
            this.ctrl_cboRuleValidWhen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ctrl_cboRuleValidWhen.Size = new System.Drawing.Size(402, 20);
            this.ctrl_cboRuleValidWhen.StyleController = this.layoutControl1;
            this.ctrl_cboRuleValidWhen.TabIndex = 8;
            conditionValidationRule3.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule3.ErrorText = "Please select a value";
            conditionValidationRule3.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information;
            this.dxValidator.SetValidationRule(this.ctrl_cboRuleValidWhen, conditionValidationRule3);
            // 
            // txtRuleSummary
            // 
            this.txtRuleSummary.EditValue = "[{0}] Must meet the cabove criteria before it can be used with [{1}] and for the " +
    "rule to pass the result [{2}]";
            this.txtRuleSummary.Location = new System.Drawing.Point(4, 370);
            this.txtRuleSummary.Margin = new System.Windows.Forms.Padding(2);
            this.txtRuleSummary.MenuManager = this.baseRibbonControl;
            this.txtRuleSummary.Name = "txtRuleSummary";
            this.txtRuleSummary.Properties.ReadOnly = true;
            this.txtRuleSummary.Size = new System.Drawing.Size(808, 50);
            this.txtRuleSummary.StyleController = this.layoutControl1;
            this.txtRuleSummary.TabIndex = 10;
            // 
            // ctrl_cboRuleApplicators
            // 
            this.ctrl_cboRuleApplicators.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "RuleRunsOn", true));
            this.ctrl_cboRuleApplicators.Location = new System.Drawing.Point(4, 126);
            this.ctrl_cboRuleApplicators.MenuManager = this.baseRibbonControl;
            this.ctrl_cboRuleApplicators.Name = "ctrl_cboRuleApplicators";
            this.ctrl_cboRuleApplicators.Properties.AllowMultiSelect = true;
            this.ctrl_cboRuleApplicators.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ctrl_cboRuleApplicators.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.ctrl_cboRuleApplicators.Size = new System.Drawing.Size(402, 20);
            this.ctrl_cboRuleApplicators.StyleController = this.layoutControl1;
            this.ctrl_cboRuleApplicators.TabIndex = 12;
            conditionValidationRule4.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule4.ErrorText = "Select at least one value";
            this.dxValidator.SetValidationRule(this.ctrl_cboRuleApplicators, conditionValidationRule4);
            // 
            // ctrl_RuleName
            // 
            this.ctrl_RuleName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "Name", true));
            this.ctrl_RuleName.Location = new System.Drawing.Point(4, 20);
            this.ctrl_RuleName.MenuManager = this.baseRibbonControl;
            this.ctrl_RuleName.Name = "ctrl_RuleName";
            this.ctrl_RuleName.Size = new System.Drawing.Size(615, 20);
            this.ctrl_RuleName.StyleController = this.layoutControl1;
            this.ctrl_RuleName.TabIndex = 13;
            // 
            // ctrl_txtDescription
            // 
            this.ctrl_txtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "Description", true));
            this.ctrl_txtDescription.Location = new System.Drawing.Point(4, 60);
            this.ctrl_txtDescription.Margin = new System.Windows.Forms.Padding(2);
            this.ctrl_txtDescription.MenuManager = this.baseRibbonControl;
            this.ctrl_txtDescription.Name = "ctrl_txtDescription";
            this.ctrl_txtDescription.Size = new System.Drawing.Size(808, 46);
            this.ctrl_txtDescription.StyleController = this.layoutControl1;
            this.ctrl_txtDescription.TabIndex = 4;
            // 
            // ctrl_cbRuleEvaluation
            // 
            this.ctrl_cbRuleEvaluation.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "RuleEvaluation", true));
            this.ctrl_cbRuleEvaluation.EditValue = false;
            this.ctrl_cbRuleEvaluation.Location = new System.Drawing.Point(623, 20);
            this.ctrl_cbRuleEvaluation.MenuManager = this.baseRibbonControl;
            this.ctrl_cbRuleEvaluation.Name = "ctrl_cbRuleEvaluation";
            this.ctrl_cbRuleEvaluation.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ctrl_cbRuleEvaluation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ctrl_cbRuleEvaluation.Size = new System.Drawing.Size(189, 18);
            this.ctrl_cbRuleEvaluation.StyleController = this.layoutControl1;
            this.ctrl_cbRuleEvaluation.TabIndex = 14;
            this.ctrl_cbRuleEvaluation.ToolTip = "Indicate what side of the rule needs to execute.\r\nSourceOnly - only source side w" +
    "ill be tested.\r\nTargetOnly - Only target side will be tested.\r\nBoth -Both sides " +
    "will be tested.\r\n\r\n";
            this.ctrl_cbRuleEvaluation.ToolTipController = this.defaultToolTipController.DefaultController;
            this.ctrl_cbRuleEvaluation.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.ctrl_cbRuleEvaluation.ToolTipTitle = "Rule has Target?";
            this.ctrl_cbRuleEvaluation.SelectedValueChanged += new System.EventHandler(this.ctrl_cbRuleEvaluation_SelectedValueChanged);
            this.ctrl_cbRuleEvaluation.EditValueChanged += new System.EventHandler(this.ctrl_cbRuleEvaluation_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lo_SourceCriteriaTree,
            this.layoutControlItem7,
            this.lo_TargetCriteriaTree,
            this.layoutControlItem4,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.lo_TargetEntity,
            this.lo_SourceEntity,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(816, 424);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ctrl_txtDescription;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(812, 66);
            this.layoutControlItem1.Text = "Description:";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(150, 13);
            // 
            // lo_SourceCriteriaTree
            // 
            this.lo_SourceCriteriaTree.Control = this.ctrl_frEdSourceEntityRule;
            this.lo_SourceCriteriaTree.Location = new System.Drawing.Point(0, 186);
            this.lo_SourceCriteriaTree.Name = "lo_SourceCriteriaTree";
            this.lo_SourceCriteriaTree.Size = new System.Drawing.Size(406, 164);
            this.lo_SourceCriteriaTree.TextSize = new System.Drawing.Size(0, 0);
            this.lo_SourceCriteriaTree.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtRuleSummary;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 350);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(812, 70);
            this.layoutControlItem7.Text = "Rule Summary:";
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(150, 13);
            // 
            // lo_TargetCriteriaTree
            // 
            this.lo_TargetCriteriaTree.Control = this.ctrl_frEdTargetEntityRule;
            this.lo_TargetCriteriaTree.Location = new System.Drawing.Point(406, 186);
            this.lo_TargetCriteriaTree.Name = "lo_TargetCriteriaTree";
            this.lo_TargetCriteriaTree.Size = new System.Drawing.Size(406, 164);
            this.lo_TargetCriteriaTree.TextSize = new System.Drawing.Size(0, 0);
            this.lo_TargetCriteriaTree.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ctrl_cboRuleApplicators;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 106);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(406, 40);
            this.layoutControlItem4.Text = "Run rule on action(s):";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(150, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.ctrl_RuleName;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(619, 40);
            this.layoutControlItem9.Text = "Name:";
            this.layoutControlItem9.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(150, 13);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.ctrl_cbRuleEvaluation;
            this.layoutControlItem10.CustomizationFormText = "Evaluate Rule Using";
            this.layoutControlItem10.Location = new System.Drawing.Point(619, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(193, 40);
            this.layoutControlItem10.Text = "Evaluate Rule Using :";
            this.layoutControlItem10.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(150, 13);
            // 
            // lo_TargetEntity
            // 
            this.lo_TargetEntity.Control = this.ctrl_cboTargetEntityType;
            this.lo_TargetEntity.Location = new System.Drawing.Point(406, 146);
            this.lo_TargetEntity.Name = "lo_TargetEntity";
            this.lo_TargetEntity.Size = new System.Drawing.Size(406, 40);
            this.lo_TargetEntity.Text = "Target Entity Type:";
            this.lo_TargetEntity.TextLocation = DevExpress.Utils.Locations.Top;
            this.lo_TargetEntity.TextSize = new System.Drawing.Size(150, 13);
            // 
            // lo_SourceEntity
            // 
            this.lo_SourceEntity.Control = this.ctrl_cboSourceEntityType;
            this.lo_SourceEntity.Location = new System.Drawing.Point(0, 146);
            this.lo_SourceEntity.Name = "lo_SourceEntity";
            this.lo_SourceEntity.Size = new System.Drawing.Size(406, 40);
            this.lo_SourceEntity.Text = "Source Entity Type:";
            this.lo_SourceEntity.TextLocation = DevExpress.Utils.Locations.Top;
            this.lo_SourceEntity.TextSize = new System.Drawing.Size(150, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ctrl_cboRuleValidWhen;
            this.layoutControlItem5.Location = new System.Drawing.Point(406, 106);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(406, 40);
            this.layoutControlItem5.Text = "The rule will pass when criteria:";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(150, 13);
            // 
            // JarsRulesForm
            // 
            this.defaultToolTipController.SetAllowHtmlText(this, DevExpress.Utils.DefaultBoolean.Default);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 595);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "JarsRulesForm";
            this.Text = "Entity Rule Setup Form";
            this.Load += new System.EventHandler(this.EntityRulesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.xtraTabPageList.ResumeLayout(false);
            this.xtraTabPageDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlEntityRules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEntityRules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.filterTargetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cboSourceEntityType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cboTargetEntityType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cboRuleValidWhen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRuleSummary.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cboRuleApplicators.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_RuleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cbRuleEvaluation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lo_SourceCriteriaTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lo_TargetCriteriaTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lo_TargetEntity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lo_SourceEntity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }       

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlEntityRules;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewEntityRules;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colTargetTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colRuleRunsOn;
        private DevExpress.XtraGrid.Columns.GridColumn colSourceTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colRuleEvaluation;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.ComboBoxEdit ctrl_cboSourceEntityType;
        private DevExpress.XtraEditors.ComboBoxEdit ctrl_cboTargetEntityType;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem lo_SourceEntity;
        private DevExpress.XtraLayout.LayoutControlItem lo_TargetEntity;
        private DevExpress.XtraEditors.ComboBoxEdit ctrl_cboRuleValidWhen;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.DataAccess.UI.FilterEditorControl ctrl_frEdSourceEntityRule;
        private DevExpress.XtraEditors.MemoEdit txtRuleSummary;
        private DevExpress.XtraLayout.LayoutControlItem lo_SourceCriteriaTree;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        public System.Windows.Forms.BindingSource filterSourceBindingSource;
        private DevExpress.DataAccess.UI.FilterEditorControl ctrl_frEdTargetEntityRule;
        private DevExpress.XtraLayout.LayoutControlItem lo_TargetCriteriaTree;
        public System.Windows.Forms.BindingSource filterTargetBindingSource;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ctrl_cboRuleApplicators;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraEditors.TextEdit ctrl_RuleName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.MemoEdit ctrl_txtDescription;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraGrid.Columns.GridColumn colIncludeTarget;
        private DevExpress.XtraEditors.ComboBoxEdit ctrl_cbRuleEvaluation;
    }
}