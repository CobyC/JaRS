
using JARS.Core.Security;
using System.Security.Permissions;

namespace JARS.Win.Plugins
{   
    partial class JarsUsersForm
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
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule3 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule4 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.gridControlJarsUserAccounts = new DevExpress.XtraGrid.GridControl();
            this.gridViewJarsUserAccounts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDisplayName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserCode1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserCode2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRecordCreatedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRecordCreatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRecordModifiedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRecordModifiedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ctrl_cbListPermissions = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.lblSelectPermissions = new DevExpress.XtraEditors.LabelControl();
            this.lblSelectRoles = new DevExpress.XtraEditors.LabelControl();
            this.ctrl_cbListRoles = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.ctrl_txtAccName = new DevExpress.XtraEditors.TextEdit();
            this._txtUID = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_txteMail = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_cbIsActive = new DevExpress.XtraEditors.CheckEdit();
            this.ctrl_txtExtUserCode = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_txtExtUserCode2 = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_txtUserCode = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_txtFirstName = new DevExpress.XtraEditors.TextEdit();
            this.ctr_txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_txtLastname = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ctrl_txtAPIKey = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlAPIKey = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.xtraTabPageList.SuspendLayout();
            this.xtraTabPageDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlJarsUserAccounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewJarsUserAccounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cbListPermissions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cbListRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtAccName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtUID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txteMail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cbIsActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtExtUserCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtExtUserCode2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtUserCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtFirstName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctr_txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtLastname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtAPIKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAPIKey)).BeginInit();
            this.SuspendLayout();
            // 
            // baseRibbonControl
            // 
            this.baseRibbonControl.ExpandCollapseItem.Id = 0;
            // 
            // 
            // 
            this.baseRibbonControl.SearchEditItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.baseRibbonControl.SearchEditItem.EditWidth = 150;
            this.baseRibbonControl.SearchEditItem.Id = -5000;
            this.baseRibbonControl.SearchEditItem.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.baseRibbonControl.Size = new System.Drawing.Size(771, 143);
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.SelectedTabPage = this.xtraTabPageList;
            this.xtraTabControl.Size = new System.Drawing.Size(771, 441);
            // 
            // xtraTabPageList
            // 
            this.xtraTabPageList.Controls.Add(this.gridControlJarsUserAccounts);
            this.xtraTabPageList.Size = new System.Drawing.Size(765, 413);
            // 
            // xtraTabPageDetails
            // 
            this.xtraTabPageDetails.Controls.Add(this.layoutControl1);
            this.xtraTabPageDetails.Size = new System.Drawing.Size(765, 413);
            // 
            // defaultBindingSource
            // 
            this.defaultBindingSource.DataSource = typeof(JARS.Entities.JarsUser);
            this.defaultBindingSource.PositionChanged += new System.EventHandler(this.defaultBindingSource_PositionChanged);
            // 
            // gridControlJarsUserAccounts
            // 
            this.gridControlJarsUserAccounts.DataSource = this.defaultBindingSource;
            this.gridControlJarsUserAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlJarsUserAccounts.Location = new System.Drawing.Point(0, 0);
            this.gridControlJarsUserAccounts.MainView = this.gridViewJarsUserAccounts;
            this.gridControlJarsUserAccounts.MenuManager = this.baseRibbonControl;
            this.gridControlJarsUserAccounts.Name = "gridControlJarsUserAccounts";
            this.gridControlJarsUserAccounts.Size = new System.Drawing.Size(765, 413);
            this.gridControlJarsUserAccounts.TabIndex = 0;
            this.gridControlJarsUserAccounts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewJarsUserAccounts});
            // 
            // gridViewJarsUserAccounts
            // 
            this.gridViewJarsUserAccounts.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDisplayName,
            this.colUserName,
            this.colEmail,
            this.colUserCode,
            this.colUserCode1,
            this.colUserCode2,
            this.colIsActive,
            this.colRecordCreatedOn,
            this.colRecordCreatedBy,
            this.colRecordModifiedOn,
            this.colRecordModifiedBy,
            this.colId});
            this.gridViewJarsUserAccounts.GridControl = this.gridControlJarsUserAccounts;
            this.gridViewJarsUserAccounts.Name = "gridViewJarsUserAccounts";
            this.gridViewJarsUserAccounts.PreviewFieldName = "Email";
            // 
            // colDisplayName
            // 
            this.colDisplayName.FieldName = "DisplayName";
            this.colDisplayName.Name = "colDisplayName";
            this.colDisplayName.Visible = true;
            this.colDisplayName.VisibleIndex = 0;
            this.colDisplayName.Width = 192;
            // 
            // colUserName
            // 
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 1;
            this.colUserName.Width = 114;
            // 
            // colEmail
            // 
            this.colEmail.FieldName = "Email";
            this.colEmail.Name = "colEmail";
            // 
            // colUserCode
            // 
            this.colUserCode.FieldName = "UserCode";
            this.colUserCode.Name = "colUserCode";
            this.colUserCode.Visible = true;
            this.colUserCode.VisibleIndex = 2;
            this.colUserCode.Width = 92;
            // 
            // colUserCode1
            // 
            this.colUserCode1.FieldName = "UserCode";
            this.colUserCode1.Name = "colUserCode1";
            this.colUserCode1.Visible = true;
            this.colUserCode1.VisibleIndex = 3;
            this.colUserCode1.Width = 92;
            // 
            // colUserCode2
            // 
            this.colUserCode2.FieldName = "UserCode2";
            this.colUserCode2.Name = "colUserCode2";
            this.colUserCode2.Visible = true;
            this.colUserCode2.VisibleIndex = 4;
            this.colUserCode2.Width = 92;
            // 
            // colIsActive
            // 
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.Visible = true;
            this.colIsActive.VisibleIndex = 5;
            this.colIsActive.Width = 110;
            // 
            // colRecordCreatedOn
            // 
            this.colRecordCreatedOn.FieldName = "CreatedDate";
            this.colRecordCreatedOn.Name = "colRecordCreatedOn";
            // 
            // colRecordCreatedBy
            // 
            this.colRecordCreatedBy.FieldName = "CreatedBy";
            this.colRecordCreatedBy.Name = "colRecordCreatedBy";
            // 
            // colRecordModifiedOn
            // 
            this.colRecordModifiedOn.FieldName = "ModifiedDate";
            this.colRecordModifiedOn.Name = "colRecordModifiedOn";
            // 
            // colRecordModifiedBy
            // 
            this.colRecordModifiedBy.FieldName = "ModifiedBy";
            this.colRecordModifiedBy.Name = "colRecordModifiedBy";
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ctrl_cbListPermissions);
            this.layoutControl1.Controls.Add(this.lblSelectPermissions);
            this.layoutControl1.Controls.Add(this.lblSelectRoles);
            this.layoutControl1.Controls.Add(this.ctrl_cbListRoles);
            this.layoutControl1.Controls.Add(this.ctrl_txtAccName);
            this.layoutControl1.Controls.Add(this._txtUID);
            this.layoutControl1.Controls.Add(this.ctrl_txteMail);
            this.layoutControl1.Controls.Add(this.ctrl_cbIsActive);
            this.layoutControl1.Controls.Add(this.ctrl_txtExtUserCode);
            this.layoutControl1.Controls.Add(this.ctrl_txtExtUserCode2);
            this.layoutControl1.Controls.Add(this.ctrl_txtUserCode);
            this.layoutControl1.Controls.Add(this.ctrl_txtFirstName);
            this.layoutControl1.Controls.Add(this.ctr_txtUserName);
            this.layoutControl1.Controls.Add(this.ctrl_txtLastname);
            this.layoutControl1.Controls.Add(this.ctrl_txtAPIKey);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(765, 413);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ctrl_cbListPermissions
            // 
            this.ctrl_cbListPermissions.Location = new System.Drawing.Point(357, 220);
            this.ctrl_cbListPermissions.Name = "ctrl_cbListPermissions";
            this.ctrl_cbListPermissions.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.ctrl_cbListPermissions.Size = new System.Drawing.Size(405, 190);
            this.ctrl_cbListPermissions.StyleController = this.layoutControl1;
            this.ctrl_cbListPermissions.TabIndex = 17;
            // 
            // lblSelectPermissions
            // 
            this.lblSelectPermissions.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSelectPermissions.Location = new System.Drawing.Point(357, 163);
            this.lblSelectPermissions.Name = "lblSelectPermissions";
            this.lblSelectPermissions.Size = new System.Drawing.Size(405, 13);
            this.lblSelectPermissions.StyleController = this.layoutControl1;
            this.lblSelectPermissions.TabIndex = 16;
            this.lblSelectPermissions.Text = "Select Permissions";
            // 
            // lblSelectRoles
            // 
            this.lblSelectRoles.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSelectRoles.Location = new System.Drawing.Point(3, 163);
            this.lblSelectRoles.Name = "lblSelectRoles";
            this.lblSelectRoles.Size = new System.Drawing.Size(350, 13);
            this.lblSelectRoles.StyleController = this.layoutControl1;
            this.lblSelectRoles.TabIndex = 15;
            this.lblSelectRoles.Text = "Select Roles";
            // 
            // ctrl_cbListRoles
            // 
            this.ctrl_cbListRoles.Location = new System.Drawing.Point(3, 220);
            this.ctrl_cbListRoles.Name = "ctrl_cbListRoles";
            this.ctrl_cbListRoles.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.ctrl_cbListRoles.Size = new System.Drawing.Size(350, 190);
            this.ctrl_cbListRoles.StyleController = this.layoutControl1;
            this.ctrl_cbListRoles.TabIndex = 14;
            // 
            // ctrl_txtAccName
            // 
            this.ctrl_txtAccName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "DisplayName", true));
            this.ctrl_txtAccName.Enabled = false;
            this.ctrl_txtAccName.Location = new System.Drawing.Point(3, 59);
            this.ctrl_txtAccName.MenuManager = this.baseRibbonControl;
            this.ctrl_txtAccName.Name = "ctrl_txtAccName";
            this.ctrl_txtAccName.Size = new System.Drawing.Size(326, 20);
            this.ctrl_txtAccName.StyleController = this.layoutControl1;
            this.ctrl_txtAccName.TabIndex = 4;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            this.dxValidator.SetValidationRule(this.ctrl_txtAccName, conditionValidationRule1);
            // 
            // _txtUID
            // 
            this._txtUID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "Id", true));
            this._txtUID.Enabled = false;
            this._txtUID.Location = new System.Drawing.Point(663, 59);
            this._txtUID.MenuManager = this.baseRibbonControl;
            this._txtUID.Name = "_txtUID";
            this._txtUID.Size = new System.Drawing.Size(99, 20);
            this._txtUID.StyleController = this.layoutControl1;
            this._txtUID.TabIndex = 5;
            // 
            // ctrl_txteMail
            // 
            this.ctrl_txteMail.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "Email", true));
            this.ctrl_txteMail.Location = new System.Drawing.Point(3, 99);
            this.ctrl_txteMail.MenuManager = this.baseRibbonControl;
            this.ctrl_txteMail.Name = "ctrl_txteMail";
            this.ctrl_txteMail.Size = new System.Drawing.Size(759, 20);
            this.ctrl_txteMail.StyleController = this.layoutControl1;
            this.ctrl_txteMail.TabIndex = 6;
            // 
            // ctrl_cbIsActive
            // 
            this.ctrl_cbIsActive.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "IsActive", true));
            this.ctrl_cbIsActive.Location = new System.Drawing.Point(663, 19);
            this.ctrl_cbIsActive.MenuManager = this.baseRibbonControl;
            this.ctrl_cbIsActive.Name = "ctrl_cbIsActive";
            this.ctrl_cbIsActive.Properties.Caption = "";
            this.ctrl_cbIsActive.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ctrl_cbIsActive.Size = new System.Drawing.Size(99, 19);
            this.ctrl_cbIsActive.StyleController = this.layoutControl1;
            this.ctrl_cbIsActive.TabIndex = 7;
            // 
            // ctrl_txtExtUserCode
            // 
            this.ctrl_txtExtUserCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "UserCode1", true));
            this.ctrl_txtExtUserCode.Location = new System.Drawing.Point(274, 139);
            this.ctrl_txtExtUserCode.MenuManager = this.baseRibbonControl;
            this.ctrl_txtExtUserCode.Name = "ctrl_txtExtUserCode";
            this.ctrl_txtExtUserCode.Size = new System.Drawing.Size(233, 20);
            this.ctrl_txtExtUserCode.StyleController = this.layoutControl1;
            this.ctrl_txtExtUserCode.TabIndex = 8;
            // 
            // ctrl_txtExtUserCode2
            // 
            this.ctrl_txtExtUserCode2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "UserCode2", true));
            this.ctrl_txtExtUserCode2.Location = new System.Drawing.Point(511, 139);
            this.ctrl_txtExtUserCode2.MenuManager = this.baseRibbonControl;
            this.ctrl_txtExtUserCode2.Name = "ctrl_txtExtUserCode2";
            this.ctrl_txtExtUserCode2.Size = new System.Drawing.Size(251, 20);
            this.ctrl_txtExtUserCode2.StyleController = this.layoutControl1;
            this.ctrl_txtExtUserCode2.TabIndex = 9;
            // 
            // ctrl_txtUserCode
            // 
            this.ctrl_txtUserCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "UserCode", true));
            this.ctrl_txtUserCode.Location = new System.Drawing.Point(3, 139);
            this.ctrl_txtUserCode.MenuManager = this.baseRibbonControl;
            this.ctrl_txtUserCode.Name = "ctrl_txtUserCode";
            this.ctrl_txtUserCode.Size = new System.Drawing.Size(267, 20);
            this.ctrl_txtUserCode.StyleController = this.layoutControl1;
            this.ctrl_txtUserCode.TabIndex = 10;
            // 
            // ctrl_txtFirstName
            // 
            this.ctrl_txtFirstName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "FirstName", true));
            this.ctrl_txtFirstName.Location = new System.Drawing.Point(3, 19);
            this.ctrl_txtFirstName.MenuManager = this.baseRibbonControl;
            this.ctrl_txtFirstName.Name = "ctrl_txtFirstName";
            this.ctrl_txtFirstName.Size = new System.Drawing.Size(326, 20);
            this.ctrl_txtFirstName.StyleController = this.layoutControl1;
            this.ctrl_txtFirstName.TabIndex = 18;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            this.dxValidator.SetValidationRule(this.ctrl_txtFirstName, conditionValidationRule2);
            this.ctrl_txtFirstName.EditValueChanged += new System.EventHandler(this.ctrl_txtFirstName_EditValueChanged);
            this.ctrl_txtFirstName.TextChanged += new System.EventHandler(this.ctrl_txtFirstName_TextChanged);
            // 
            // ctr_txtUserName
            // 
            this.ctr_txtUserName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "UserName", true));
            this.ctr_txtUserName.Enabled = false;
            this.ctr_txtUserName.Location = new System.Drawing.Point(333, 59);
            this.ctr_txtUserName.MenuManager = this.baseRibbonControl;
            this.ctr_txtUserName.Name = "ctr_txtUserName";
            this.ctr_txtUserName.Size = new System.Drawing.Size(326, 20);
            this.ctr_txtUserName.StyleController = this.layoutControl1;
            this.ctr_txtUserName.TabIndex = 19;
            conditionValidationRule3.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.NotContains;
            conditionValidationRule3.ErrorText = "The value can not contain blanks";
            conditionValidationRule3.Value1 = " ";
            conditionValidationRule3.Value2 = "  ";
            this.dxValidator.SetValidationRule(this.ctr_txtUserName, conditionValidationRule3);
            // 
            // ctrl_txtLastname
            // 
            this.ctrl_txtLastname.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "LastName", true));
            this.ctrl_txtLastname.Location = new System.Drawing.Point(333, 19);
            this.ctrl_txtLastname.MenuManager = this.baseRibbonControl;
            this.ctrl_txtLastname.Name = "ctrl_txtLastname";
            this.ctrl_txtLastname.Size = new System.Drawing.Size(326, 20);
            this.ctrl_txtLastname.StyleController = this.layoutControl1;
            this.ctrl_txtLastname.TabIndex = 20;
            conditionValidationRule4.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule4.ErrorText = "This value is not valid";
            this.dxValidator.SetValidationRule(this.ctrl_txtLastname, conditionValidationRule4);
            this.ctrl_txtLastname.TextChanged += new System.EventHandler(this.ctrl_txtLastname_TextChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem7,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.layoutControlItem14,
            this.layoutControlItem1,
            this.layoutControlItem13,
            this.layoutControlItem4,
            this.layoutControlItem2,
            this.layoutControlAPIKey});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(765, 413);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ctrl_txteMail;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(763, 40);
            this.layoutControlItem3.Text = "e Mail:";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.ctrl_txtUserCode;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(271, 40);
            this.layoutControlItem7.Text = "User Code:";
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ctrl_txtExtUserCode;
            this.layoutControlItem5.Location = new System.Drawing.Point(271, 120);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(237, 40);
            this.layoutControlItem5.Text = "Extra User Code 1:";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.ctrl_txtExtUserCode2;
            this.layoutControlItem6.Location = new System.Drawing.Point(508, 120);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(255, 40);
            this.layoutControlItem6.Text = "Extra User Code 2:";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.ctrl_cbListRoles;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 217);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(354, 194);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.lblSelectRoles;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 160);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(354, 17);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.lblSelectPermissions;
            this.layoutControlItem10.Location = new System.Drawing.Point(354, 160);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(409, 17);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.ctrl_cbListPermissions;
            this.layoutControlItem11.Location = new System.Drawing.Point(354, 217);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(409, 194);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.ctrl_txtFirstName;
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(330, 40);
            this.layoutControlItem12.Text = "First Name:";
            this.layoutControlItem12.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem12.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.ctrl_txtLastname;
            this.layoutControlItem14.Location = new System.Drawing.Point(330, 0);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(330, 40);
            this.layoutControlItem14.Text = "Last Name:";
            this.layoutControlItem14.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem14.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ctrl_txtAccName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(330, 40);
            this.layoutControlItem1.Text = "Display Name:";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.ctr_txtUserName;
            this.layoutControlItem13.Location = new System.Drawing.Point(330, 40);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(330, 40);
            this.layoutControlItem13.Text = "User Name:";
            this.layoutControlItem13.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this._txtUID;
            this.layoutControlItem2.Location = new System.Drawing.Point(660, 40);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 40);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(50, 40);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(103, 40);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "UID";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(92, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ctrl_cbIsActive;
            this.layoutControlItem4.Location = new System.Drawing.Point(660, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(103, 40);
            this.layoutControlItem4.Text = "Is Active:";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(92, 13);
            // 
            // ctrl_txtAPIKey
            // 
            this.ctrl_txtAPIKey.Location = new System.Drawing.Point(3, 196);
            this.ctrl_txtAPIKey.MenuManager = this.baseRibbonControl;
            this.ctrl_txtAPIKey.Name = "ctrl_txtAPIKey";
            this.ctrl_txtAPIKey.Properties.ReadOnly = true;
            this.ctrl_txtAPIKey.Size = new System.Drawing.Size(759, 20);
            this.ctrl_txtAPIKey.StyleController = this.layoutControl1;
            this.ctrl_txtAPIKey.TabIndex = 21;
            // 
            // layoutControlAPIKey
            // 
            this.layoutControlAPIKey.Control = this.ctrl_txtAPIKey;
            this.layoutControlAPIKey.Location = new System.Drawing.Point(0, 177);
            this.layoutControlAPIKey.Name = "layoutControlAPIKey";
            this.layoutControlAPIKey.Size = new System.Drawing.Size(763, 40);
            this.layoutControlAPIKey.Text = "API Key:";
            this.layoutControlAPIKey.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlAPIKey.TextSize = new System.Drawing.Size(92, 13);
            // 
            // JarsUsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 584);
            this.Name = "JarsUsersForm";
            this.Text = "JarsUserForm";
            this.Load += new System.EventHandler(this.JarsUsersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.xtraTabPageList.ResumeLayout(false);
            this.xtraTabPageDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlJarsUserAccounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewJarsUserAccounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cbListPermissions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cbListRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtAccName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtUID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txteMail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cbIsActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtExtUserCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtExtUserCode2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtUserCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtFirstName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctr_txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtLastname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtAPIKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAPIKey)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlJarsUserAccounts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewJarsUserAccounts;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit ctrl_txtAccName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colDisplayName;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
        private DevExpress.XtraGrid.Columns.GridColumn colUserCode;
        private DevExpress.XtraGrid.Columns.GridColumn colUserCode1;
        private DevExpress.XtraGrid.Columns.GridColumn colUserCode2;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colRecordCreatedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colRecordCreatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colRecordModifiedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colRecordModifiedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraEditors.TextEdit _txtUID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit ctrl_txteMail;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.CheckEdit ctrl_cbIsActive;
        private DevExpress.XtraEditors.TextEdit ctrl_txtExtUserCode;
        private DevExpress.XtraEditors.TextEdit ctrl_txtExtUserCode2;
        private DevExpress.XtraEditors.TextEdit ctrl_txtUserCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.LabelControl lblSelectRoles;
        private DevExpress.XtraEditors.CheckedListBoxControl ctrl_cbListRoles;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.CheckedListBoxControl ctrl_cbListPermissions;
        private DevExpress.XtraEditors.LabelControl lblSelectPermissions;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.TextEdit ctrl_txtFirstName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraEditors.TextEdit ctr_txtUserName;
        private DevExpress.XtraEditors.TextEdit ctrl_txtLastname;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraEditors.TextEdit ctrl_txtAPIKey;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlAPIKey;
    }
}