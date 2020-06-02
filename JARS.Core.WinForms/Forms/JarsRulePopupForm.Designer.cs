using JARS.Core.Interfaces.Entities;

namespace JARS.Core.WinForms.Forms
{
    partial class JarsRulePopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JarsRulePopupForm));
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.filterEditorControl1 = new DevExpress.DataAccess.UI.FilterEditorControl();
            this.conditionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.filterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtRuleDescription = new DevExpress.XtraEditors.TextEdit();
            this.cboConditionTest = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtRuleInfo = new DevExpress.XtraEditors.MemoEdit();
            this.cboSourceEntity = new DevExpress.XtraEditors.ComboBoxEdit();
            this.CboRuleApplicators = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conditionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRuleDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboConditionTest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRuleInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSourceEntity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CboRuleApplicators.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.filterEditorControl1);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.txtRuleDescription);
            this.layoutControl1.Controls.Add(this.cboConditionTest);
            this.layoutControl1.Controls.Add(this.txtRuleInfo);
            this.layoutControl1.Controls.Add(this.cboSourceEntity);
            this.layoutControl1.Controls.Add(this.CboRuleApplicators);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(322, 445);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // filterEditorControl1
            // 
            this.filterEditorControl1.ActiveView = DevExpress.XtraFilterEditor.FilterEditorActiveView.Visual;
            this.filterEditorControl1.AppearanceEmptyValueColor = System.Drawing.Color.Empty;
            this.filterEditorControl1.AppearanceFieldNameColor = System.Drawing.Color.Empty;
            this.filterEditorControl1.AppearanceGroupOperatorColor = System.Drawing.Color.Empty;
            this.filterEditorControl1.AppearanceOperatorColor = System.Drawing.Color.Empty;
            this.filterEditorControl1.AppearanceValueColor = System.Drawing.Color.Empty;
            this.filterEditorControl1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.conditionBindingSource, "SourceCriteriaString", true));
            this.filterEditorControl1.Location = new System.Drawing.Point(3, 123);
            this.filterEditorControl1.Name = "filterEditorControl1";
            this.filterEditorControl1.ShowDateTimeFunctions = DevExpress.XtraEditors.DateTimeFunctionsShowMode.Advanced;
            this.filterEditorControl1.ShowOperandTypeIcon = true;
            this.filterEditorControl1.Size = new System.Drawing.Size(316, 197);
            this.filterEditorControl1.SourceControl = this.filterBindingSource;
            this.filterEditorControl1.TabIndex = 4;
            this.filterEditorControl1.UseMenuForOperandsAndOperators = false;
            this.filterEditorControl1.ViewMode = DevExpress.XtraEditors.FilterEditorViewMode.Visual;
            this.filterEditorControl1.FilterTextChanged += new DevExpress.XtraEditors.FilterTextChangedEventHandler(this.FilterEditorControl1_FilterTextChanged);
            // 
            // conditionBindingSource
            // 
            this.conditionBindingSource.DataSource = typeof(JARS.Core.Rules.JarsRule);
            this.conditionBindingSource.CurrentItemChanged += new System.EventHandler(this.ConditionBindingSource_CurrentItemChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.ImageOptions.Image")));
            this.btnOK.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnOK.Location = new System.Drawing.Point(3, 404);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(165, 38);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            this.btnOK.MouseEnter += new System.EventHandler(this.BtnOK_MouseEnter);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCancel.Location = new System.Drawing.Point(172, 404);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(147, 38);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            // 
            // txtRuleDescription
            // 
            this.txtRuleDescription.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.conditionBindingSource, "Name", true));
            this.txtRuleDescription.Location = new System.Drawing.Point(3, 19);
            this.txtRuleDescription.Name = "txtRuleDescription";
            this.txtRuleDescription.Properties.Validating += new System.ComponentModel.CancelEventHandler(this.TxtRuleDescription_Properties_Validating);
            this.txtRuleDescription.Size = new System.Drawing.Size(316, 20);
            this.txtRuleDescription.StyleController = this.layoutControl1;
            this.txtRuleDescription.TabIndex = 7;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "Please enter a description";
            this.dxValidationProvider.SetValidationRule(this.txtRuleDescription, conditionValidationRule1);
            // 
            // cboConditionTest
            // 
            this.cboConditionTest.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.conditionBindingSource, "RulePassesWhen", true));
            this.cboConditionTest.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.conditionBindingSource, "RulePassesWhen", true));
            this.cboConditionTest.Location = new System.Drawing.Point(207, 324);
            this.cboConditionTest.Name = "cboConditionTest";
            this.cboConditionTest.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboConditionTest.Properties.DropDownRows = 3;
            this.cboConditionTest.Size = new System.Drawing.Size(112, 20);
            this.cboConditionTest.StyleController = this.layoutControl1;
            this.cboConditionTest.TabIndex = 9;
            this.cboConditionTest.SelectedValueChanged += new System.EventHandler(this.CboConditionTest_SelectedValueChanged);
            // 
            // txtRuleInfo
            // 
            this.txtRuleInfo.EditValue = "[{0}] Must meet below criteria before it can be used with [{1}]";
            this.txtRuleInfo.Location = new System.Drawing.Point(3, 348);
            this.txtRuleInfo.Name = "txtRuleInfo";
            this.txtRuleInfo.Properties.LinesCount = 3;
            this.txtRuleInfo.Properties.ReadOnly = true;
            this.txtRuleInfo.Size = new System.Drawing.Size(316, 52);
            this.txtRuleInfo.StyleController = this.layoutControl1;
            this.txtRuleInfo.TabIndex = 10;
            // 
            // cboSourceEntity
            // 
            this.cboSourceEntity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.conditionBindingSource, "SourceTypeName", true));
            this.cboSourceEntity.Location = new System.Drawing.Point(3, 99);
            this.cboSourceEntity.Name = "cboSourceEntity";
            this.cboSourceEntity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSourceEntity.Size = new System.Drawing.Size(316, 20);
            this.cboSourceEntity.StyleController = this.layoutControl1;
            this.cboSourceEntity.TabIndex = 11;
            this.cboSourceEntity.SelectedValueChanged += new System.EventHandler(this.CboSourceEntity_SelectedValueChanged);
            // 
            // CboRuleApplicators
            // 
            this.CboRuleApplicators.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.conditionBindingSource, "RuleRunsOn", true));
            this.CboRuleApplicators.EditValue = "";
            this.CboRuleApplicators.Location = new System.Drawing.Point(3, 59);
            this.CboRuleApplicators.Name = "CboRuleApplicators";
            this.CboRuleApplicators.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CboRuleApplicators.Size = new System.Drawing.Size(316, 20);
            this.CboRuleApplicators.StyleController = this.layoutControl1;
            this.CboRuleApplicators.TabIndex = 12;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "Select at least one value";
            this.dxValidationProvider.SetValidationRule(this.CboRuleApplicators, conditionValidationRule2);
            this.CboRuleApplicators.Validating += new System.ComponentModel.CancelEventHandler(this.CboRuleApplicators_Validating);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem7,
            this.layoutControlItem6,
            this.layoutControlItem8});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(322, 445);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.filterEditorControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(320, 201);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(169, 401);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(151, 42);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnOK;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 401);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(169, 42);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtRuleDescription;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(320, 40);
            this.layoutControlItem4.Text = "Name:";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(64, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtRuleInfo;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 345);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(320, 56);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cboSourceEntity;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(320, 40);
            this.layoutControlItem7.Text = "Source Type:";
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(64, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cboConditionTest;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 321);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(320, 24);
            this.layoutControlItem6.Text = "The rule passes when the criteria :";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(200, 13);
            this.layoutControlItem6.TextToControlDistance = 4;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.CboRuleApplicators;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(320, 40);
            this.layoutControlItem8.Text = "Applicators:";
            this.layoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(64, 13);
            // 
            // dxValidationProvider
            // 
            this.dxValidationProvider.ValidationFailed += new DevExpress.XtraEditors.DXErrorProvider.ValidationFailedEventHandler(this.DxValidationProvider_ValidationFailed);
            this.dxValidationProvider.ValidationSucceeded += new DevExpress.XtraEditors.DXErrorProvider.ValidationSucceededEventHandler(this.DxValidationProvider_ValidationSucceeded);
            // 
            // JarsRulePopupForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(322, 445);
            this.Controls.Add(this.layoutControl1);
            this.Name = "JarsRulePopupForm";
            this.ShowInTaskbar = false;
            this.Text = "Rule Condition";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.conditionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRuleDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboConditionTest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRuleInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSourceEntity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CboRuleApplicators.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.DataAccess.UI.FilterEditorControl filterEditorControl1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        public System.Windows.Forms.BindingSource filterBindingSource;
        private DevExpress.XtraEditors.TextEdit txtRuleDescription;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.ComboBoxEdit cboConditionTest;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.MemoEdit txtRuleInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider;
        private DevExpress.XtraEditors.ComboBoxEdit cboSourceEntity;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        public System.Windows.Forms.BindingSource conditionBindingSource;
        private DevExpress.XtraEditors.CheckedComboBoxEdit CboRuleApplicators;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}