namespace JARS.Win.Plugins
{
    partial class ResourceGroupsForm
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
            this.gridControlGroups = new DevExpress.XtraGrid.GridControl();
            this.gridViewGroups = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSortIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRecordCreatedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRecordCreatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRecordModifiedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRecordModifiedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcOperatives = new DevExpress.XtraGrid.GridControl();
            this.resourceBindingSource = new System.Windows.Forms.BindingSource();
            this.gvOperatives = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDisplayName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsMobileOperative = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMobileNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ctrl_txtName = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_txtCode = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_txtSortIdx = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_cbIsActive = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.xtraTabPageList.SuspendLayout();
            this.xtraTabPageDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOperatives)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOperatives)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtSortIdx.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cbIsActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // baseRibbonControl
            // 
            this.baseRibbonControl.ExpandCollapseItem.Id = 0;
            this.baseRibbonControl.Size = new System.Drawing.Size(840, 143);
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.SelectedTabPage = this.xtraTabPageList;
            this.xtraTabControl.Size = new System.Drawing.Size(840, 409);
            // 
            // xtraTabPageList
            // 
            this.xtraTabPageList.Controls.Add(this.gridControlGroups);
            this.xtraTabPageList.Size = new System.Drawing.Size(834, 381);
            // 
            // xtraTabPageDetails
            // 
            this.xtraTabPageDetails.Controls.Add(this.layoutControl1);
            this.xtraTabPageDetails.Size = new System.Drawing.Size(834, 381);
            // 
            // defaultBindingSource
            // 
            this.defaultBindingSource.DataSource = typeof(JARS.Entities.JarsResourceGroup);
            this.defaultBindingSource.PositionChanged += new System.EventHandler(this.defaultBindingSource_PositionChanged);
            // 
            // gridControlGroups
            // 
            this.gridControlGroups.DataSource = this.defaultBindingSource;
            this.gridControlGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlGroups.Location = new System.Drawing.Point(0, 0);
            this.gridControlGroups.MainView = this.gridViewGroups;
            this.gridControlGroups.MenuManager = this.baseRibbonControl;
            this.gridControlGroups.Name = "gridControlGroups";
            this.gridControlGroups.Size = new System.Drawing.Size(834, 381);
            this.gridControlGroups.TabIndex = 0;
            this.gridControlGroups.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewGroups});
            // 
            // gridViewGroups
            // 
            this.gridViewGroups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.colCode,
            this.colSortIndex,
            this.colIsActive,
            this.colRecordCreatedOn,
            this.colRecordCreatedBy,
            this.colRecordModifiedOn,
            this.colRecordModifiedBy,
            this.colID});
            this.gridViewGroups.GridControl = this.gridControlGroups;
            this.gridViewGroups.Name = "gridViewGroups";
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 359;
            // 
            // colCode
            // 
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 2;
            this.colCode.Width = 83;
            // 
            // colSortIndex
            // 
            this.colSortIndex.FieldName = "SortIndex";
            this.colSortIndex.Name = "colSortIndex";
            this.colSortIndex.OptionsColumn.AllowEdit = false;
            this.colSortIndex.Visible = true;
            this.colSortIndex.VisibleIndex = 3;
            this.colSortIndex.Width = 112;
            // 
            // colIsActive
            // 
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.OptionsColumn.AllowEdit = false;
            this.colIsActive.Visible = true;
            this.colIsActive.VisibleIndex = 4;
            this.colIsActive.Width = 92;
            // 
            // colRecordCreatedOn
            // 
            this.colRecordCreatedOn.Caption = "Created On";
            this.colRecordCreatedOn.FieldName = "RecordCreatedOn";
            this.colRecordCreatedOn.Name = "colRecordCreatedOn";
            this.colRecordCreatedOn.OptionsColumn.AllowEdit = false;
            this.colRecordCreatedOn.Width = 62;
            // 
            // colRecordCreatedBy
            // 
            this.colRecordCreatedBy.Caption = "Created By";
            this.colRecordCreatedBy.FieldName = "RecordCreatedBy";
            this.colRecordCreatedBy.Name = "colRecordCreatedBy";
            this.colRecordCreatedBy.OptionsColumn.AllowEdit = false;
            this.colRecordCreatedBy.Width = 55;
            // 
            // colRecordModifiedOn
            // 
            this.colRecordModifiedOn.Caption = "Modified On";
            this.colRecordModifiedOn.FieldName = "RecordModifiedOn";
            this.colRecordModifiedOn.Name = "colRecordModifiedOn";
            this.colRecordModifiedOn.OptionsColumn.AllowEdit = false;
            this.colRecordModifiedOn.Width = 55;
            // 
            // colRecordModifiedBy
            // 
            this.colRecordModifiedBy.Caption = "Modified By";
            this.colRecordModifiedBy.FieldName = "RecordModifiedBy";
            this.colRecordModifiedBy.Name = "colRecordModifiedBy";
            this.colRecordModifiedBy.OptionsColumn.AllowEdit = false;
            this.colRecordModifiedBy.Width = 55;
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 46;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcOperatives);
            this.layoutControl1.Controls.Add(this.ctrl_txtName);
            this.layoutControl1.Controls.Add(this.ctrl_txtCode);
            this.layoutControl1.Controls.Add(this.ctrl_txtSortIdx);
            this.layoutControl1.Controls.Add(this.ctrl_cbIsActive);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(834, 381);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcOperatives
            // 
            this.gcOperatives.DataSource = this.resourceBindingSource;
            this.gcOperatives.Location = new System.Drawing.Point(3, 43);
            this.gcOperatives.MainView = this.gvOperatives;
            this.gcOperatives.MenuManager = this.baseRibbonControl;
            this.gcOperatives.Name = "gcOperatives";
            this.gcOperatives.Size = new System.Drawing.Size(828, 335);
            this.gcOperatives.TabIndex = 8;
            this.gcOperatives.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOperatives});
            // 
            // resourceBindingSource
            // 
            this.resourceBindingSource.DataSource = typeof(JARS.Entities.JarsResource);
            // 
            // gvOperatives
            // 
            this.gvOperatives.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID1,
            this.colDisplayName,
            this.colIsMobileOperative,
            this.colMobileNo});
            this.gvOperatives.GridControl = this.gcOperatives;
            this.gvOperatives.Name = "gvOperatives";
            this.gvOperatives.OptionsView.ShowGroupPanel = false;
            // 
            // colID1
            // 
            this.colID1.FieldName = "ID";
            this.colID1.Name = "colID1";
            this.colID1.OptionsColumn.AllowEdit = false;
            this.colID1.OptionsColumn.ReadOnly = true;
            this.colID1.Visible = true;
            this.colID1.VisibleIndex = 0;
            this.colID1.Width = 58;
            // 
            // colDisplayName
            // 
            this.colDisplayName.FieldName = "DisplayName";
            this.colDisplayName.Name = "colDisplayName";
            this.colDisplayName.OptionsColumn.AllowEdit = false;
            this.colDisplayName.Visible = true;
            this.colDisplayName.VisibleIndex = 1;
            this.colDisplayName.Width = 356;
            // 
            // colIsMobileOperative
            // 
            this.colIsMobileOperative.Caption = "Mobile Op ?";
            this.colIsMobileOperative.FieldName = "IsMobileResource";
            this.colIsMobileOperative.Name = "colIsMobileOperative";
            this.colIsMobileOperative.OptionsColumn.AllowEdit = false;
            this.colIsMobileOperative.Visible = true;
            this.colIsMobileOperative.VisibleIndex = 3;
            this.colIsMobileOperative.Width = 117;
            // 
            // colMobileNo
            // 
            this.colMobileNo.Caption = "Mobile No";
            this.colMobileNo.FieldName = "MobileNo";
            this.colMobileNo.Name = "colMobileNo";
            this.colMobileNo.OptionsColumn.AllowEdit = false;
            this.colMobileNo.Visible = true;
            this.colMobileNo.VisibleIndex = 2;
            this.colMobileNo.Width = 161;
            // 
            // ctrl_txtName
            // 
            this.ctrl_txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "Name", true));
            this.ctrl_txtName.Location = new System.Drawing.Point(3, 19);
            this.ctrl_txtName.MenuManager = this.baseRibbonControl;
            this.ctrl_txtName.Name = "ctrl_txtName";
            this.ctrl_txtName.Size = new System.Drawing.Size(500, 20);
            this.ctrl_txtName.StyleController = this.layoutControl1;
            this.ctrl_txtName.TabIndex = 4;
            // 
            // ctrl_txtCode
            // 
            this.ctrl_txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "Code", true));
            this.ctrl_txtCode.Location = new System.Drawing.Point(507, 19);
            this.ctrl_txtCode.MenuManager = this.baseRibbonControl;
            this.ctrl_txtCode.Name = "ctrl_txtCode";
            this.ctrl_txtCode.Size = new System.Drawing.Size(142, 20);
            this.ctrl_txtCode.StyleController = this.layoutControl1;
            this.ctrl_txtCode.TabIndex = 5;
            this.ctrl_txtCode.Validating += new System.ComponentModel.CancelEventHandler(this.ctrl_txtCode_Validating);
            // 
            // ctrl_txtSortIdx
            // 
            this.ctrl_txtSortIdx.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "SortIndex", true));
            this.ctrl_txtSortIdx.Location = new System.Drawing.Point(653, 19);
            this.ctrl_txtSortIdx.MenuManager = this.baseRibbonControl;
            this.ctrl_txtSortIdx.Name = "ctrl_txtSortIdx";
            this.ctrl_txtSortIdx.Size = new System.Drawing.Size(104, 20);
            this.ctrl_txtSortIdx.StyleController = this.layoutControl1;
            this.ctrl_txtSortIdx.TabIndex = 6;
            // 
            // ctrl_cbIsActive
            // 
            this.ctrl_cbIsActive.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "IsActive", true));
            this.ctrl_cbIsActive.Location = new System.Drawing.Point(761, 20);
            this.ctrl_cbIsActive.MenuManager = this.baseRibbonControl;
            this.ctrl_cbIsActive.Name = "ctrl_cbIsActive";
            this.ctrl_cbIsActive.Properties.Caption = "Is Active:";
            this.ctrl_cbIsActive.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ctrl_cbIsActive.Size = new System.Drawing.Size(70, 19);
            this.ctrl_cbIsActive.StyleController = this.layoutControl1;
            this.ctrl_cbIsActive.TabIndex = 7;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(834, 381);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ctrl_txtName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(504, 40);
            this.layoutControlItem1.Text = "Name:";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(61, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ctrl_txtCode;
            this.layoutControlItem2.Location = new System.Drawing.Point(504, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(146, 40);
            this.layoutControlItem2.Text = "Group Code:";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(61, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ctrl_cbIsActive;
            this.layoutControlItem4.Location = new System.Drawing.Point(758, 17);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(74, 23);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ctrl_txtSortIdx;
            this.layoutControlItem3.Location = new System.Drawing.Point(650, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(108, 40);
            this.layoutControlItem3.Text = "Sort Index:";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(61, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gcOperatives;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(832, 339);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(758, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(74, 17);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ResourceGroupsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 552);
            this.Name = "ResourceGroupsForm";
            this.Text = "Resource Groups";
            this.Load += new System.EventHandler(this.OperativeGroups_Load);
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.xtraTabPageList.ResumeLayout(false);
            this.xtraTabPageDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcOperatives)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOperatives)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtSortIdx.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_cbIsActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlGroups;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewGroups;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSortIndex;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
        private DevExpress.XtraGrid.Columns.GridColumn colRecordCreatedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colRecordCreatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colRecordModifiedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colRecordModifiedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit ctrl_txtName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit ctrl_txtCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gcOperatives;
        private System.Windows.Forms.BindingSource resourceBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOperatives;
        private DevExpress.XtraGrid.Columns.GridColumn colID1;
        private DevExpress.XtraGrid.Columns.GridColumn colDisplayName;        
        private DevExpress.XtraGrid.Columns.GridColumn colMobileNo;
        private DevExpress.XtraEditors.TextEdit ctrl_txtSortIdx;
        private DevExpress.XtraEditors.CheckEdit ctrl_cbIsActive;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colIsMobileOperative;
    }
}