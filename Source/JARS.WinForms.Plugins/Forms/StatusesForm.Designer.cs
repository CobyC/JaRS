
using JARS.Core.Security;
using System.Security.Permissions;

namespace JARS.Win.Plugins
{   
    partial class StatusesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusesForm));
            this.gridControlStatuses = new DevExpress.XtraGrid.GridControl();
            this.gridViewStatuses = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colViewType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLabelName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLabelValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colColourRGB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repItmLabelColorPickEdit = new DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit();
            this.colSortIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ctrl_FilterControl = new DevExpress.XtraEditors.FilterControl();
            this.filterBindingSource = new System.Windows.Forms.BindingSource();
            this.ctrl_StatusName = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_StatusColour = new DevExpress.XtraEditors.ColorPickEdit();
            this._statusViewOption = new DevExpress.XtraEditors.TextEdit();
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.disabled_ID = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.loGroupStatus = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblInfo = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.xtraTabPageList.SuspendLayout();
            this.xtraTabPageDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStatuses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStatuses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItmLabelColorPickEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_StatusName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_StatusColour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._statusViewOption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.disabled_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loGroupStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // baseRibbonControl
            // 
            this.baseRibbonControl.ExpandCollapseItem.Id = 0;
            this.baseRibbonControl.Margin = new System.Windows.Forms.Padding(6);
            this.baseRibbonControl.MaxItemId = 20;
            this.baseRibbonControl.Size = new System.Drawing.Size(731, 143);
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.xtraTabControl.SelectedTabPage = this.xtraTabPageList;
            this.xtraTabControl.Size = new System.Drawing.Size(731, 415);
            // 
            // xtraTabPageList
            // 
            this.xtraTabPageList.Controls.Add(this.layoutControl2);
            this.xtraTabPageList.Margin = new System.Windows.Forms.Padding(4);
            this.xtraTabPageList.Size = new System.Drawing.Size(725, 387);
            // 
            // xtraTabPageDetails
            // 
            this.xtraTabPageDetails.Controls.Add(this.layoutControl1);
            this.xtraTabPageDetails.Margin = new System.Windows.Forms.Padding(4);
            this.xtraTabPageDetails.Size = new System.Drawing.Size(725, 387);
            // 
            // defaultBindingSource
            // 
            this.defaultBindingSource.DataSource = typeof(JARS.Entities.ApptStatus);
            this.defaultBindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.defaultBindingSource_AddingNew);
            this.defaultBindingSource.PositionChanged += new System.EventHandler(this.defaultBindingSource_PositionChanged);
            // 
            // gridControlStatuses
            // 
            this.gridControlStatuses.DataSource = this.defaultBindingSource;
            this.gridControlStatuses.Location = new System.Drawing.Point(3, 3);
            this.gridControlStatuses.MainView = this.gridViewStatuses;
            this.gridControlStatuses.MenuManager = this.baseRibbonControl;
            this.gridControlStatuses.Name = "gridControlStatuses";
            this.gridControlStatuses.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repItmLabelColorPickEdit});
            this.gridControlStatuses.Size = new System.Drawing.Size(719, 364);
            this.gridControlStatuses.TabIndex = 0;
            this.gridControlStatuses.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewStatuses});
            // 
            // gridViewStatuses
            // 
            this.gridViewStatuses.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colViewType,
            this.colLabelName,
            this.colLabelValue,
            this.colColourRGB,
            this.colSortIndex});
            this.gridViewStatuses.GridControl = this.gridControlStatuses;
            this.gridViewStatuses.Name = "gridViewStatuses";
            this.gridViewStatuses.PreviewFieldName = "Email";
            // 
            // colViewType
            // 
            this.colViewType.FieldName = "ViewName";
            this.colViewType.Name = "colViewType";
            this.colViewType.OptionsColumn.AllowEdit = false;
            this.colViewType.OptionsColumn.ReadOnly = true;
            this.colViewType.Visible = true;
            this.colViewType.VisibleIndex = 0;
            this.colViewType.Width = 146;
            // 
            // colLabelName
            // 
            this.colLabelName.FieldName = "StatusName";
            this.colLabelName.Name = "colLabelName";
            this.colLabelName.OptionsColumn.AllowEdit = false;
            this.colLabelName.OptionsColumn.ReadOnly = true;
            this.colLabelName.Visible = true;
            this.colLabelName.VisibleIndex = 1;
            this.colLabelName.Width = 287;
            // 
            // colLabelValue
            // 
            this.colLabelValue.FieldName = "StatusCriteria";
            this.colLabelValue.Name = "colLabelValue";
            this.colLabelValue.OptionsColumn.AllowEdit = false;
            this.colLabelValue.OptionsColumn.ReadOnly = true;
            this.colLabelValue.Visible = true;
            this.colLabelValue.VisibleIndex = 2;
            this.colLabelValue.Width = 257;
            // 
            // colColourRGB
            // 
            this.colColourRGB.ColumnEdit = this.repItmLabelColorPickEdit;
            this.colColourRGB.FieldName = "ColourRGB";
            this.colColourRGB.Name = "colColourRGB";
            this.colColourRGB.OptionsColumn.AllowEdit = false;
            this.colColourRGB.OptionsColumn.ReadOnly = true;
            this.colColourRGB.Visible = true;
            this.colColourRGB.VisibleIndex = 3;
            this.colColourRGB.Width = 166;
            // 
            // repItmLabelColorPickEdit
            // 
            this.repItmLabelColorPickEdit.AutoHeight = false;
            this.repItmLabelColorPickEdit.AutomaticColor = System.Drawing.Color.Black;
            this.repItmLabelColorPickEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repItmLabelColorPickEdit.Name = "repItmLabelColorPickEdit";
            // 
            // colSortIndex
            // 
            this.colSortIndex.FieldName = "SortIndex";
            this.colSortIndex.Name = "colSortIndex";
            this.colSortIndex.OptionsColumn.AllowEdit = false;
            this.colSortIndex.OptionsColumn.ReadOnly = true;
            this.colSortIndex.Visible = true;
            this.colSortIndex.VisibleIndex = 4;
            this.colSortIndex.Width = 166;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ctrl_FilterControl);
            this.layoutControl1.Controls.Add(this.ctrl_StatusName);
            this.layoutControl1.Controls.Add(this.ctrl_StatusColour);
            this.layoutControl1.Controls.Add(this._statusViewOption);
            this.layoutControl1.Controls.Add(this.pictureEdit);
            this.layoutControl1.Controls.Add(this.disabled_ID);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1123, 275, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(725, 387);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ctrl_FilterControl
            // 
            this.ctrl_FilterControl.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ctrl_FilterControl.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "StatusCriteria", true));
            this.ctrl_FilterControl.Location = new System.Drawing.Point(15, 148);
            this.ctrl_FilterControl.Name = "ctrl_FilterControl";
            this.ctrl_FilterControl.Size = new System.Drawing.Size(695, 79);
            this.ctrl_FilterControl.SourceControl = this.filterBindingSource;
            this.ctrl_FilterControl.TabIndex = 12;
            this.ctrl_FilterControl.Text = "filterControl";
            // 
            // ctrl_StatusName
            // 
            this.ctrl_StatusName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "StatusName", true));
            this.ctrl_StatusName.Location = new System.Drawing.Point(15, 51);
            this.ctrl_StatusName.Name = "ctrl_StatusName";
            this.ctrl_StatusName.Size = new System.Drawing.Size(325, 20);
            this.ctrl_StatusName.StyleController = this.layoutControl1;
            this.ctrl_StatusName.TabIndex = 6;
            // 
            // ctrl_StatusColour
            // 
            this.ctrl_StatusColour.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "ColourRGB", true));
            this.ctrl_StatusColour.EditValue = System.Drawing.Color.Empty;
            this.ctrl_StatusColour.Location = new System.Drawing.Point(15, 91);
            this.ctrl_StatusColour.Name = "ctrl_StatusColour";
            this.ctrl_StatusColour.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.ctrl_StatusColour.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ctrl_StatusColour.Properties.StoreColorAsInteger = true;
            this.ctrl_StatusColour.Size = new System.Drawing.Size(695, 20);
            this.ctrl_StatusColour.StyleController = this.layoutControl1;
            this.ctrl_StatusColour.TabIndex = 8;
            // 
            // _statusViewOption
            // 
            this._statusViewOption.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "ViewName", true));
            this._statusViewOption.Enabled = false;
            this._statusViewOption.Location = new System.Drawing.Point(344, 51);
            this._statusViewOption.Name = "_statusViewOption";
            this._statusViewOption.Size = new System.Drawing.Size(296, 20);
            this._statusViewOption.StyleController = this.layoutControl1;
            this._statusViewOption.TabIndex = 9;
            // 
            // pictureEdit
            // 
            this.pictureEdit.EditValue = ((object)(resources.GetObject("pictureEdit.EditValue")));
            this.pictureEdit.Location = new System.Drawing.Point(15, 231);
            this.pictureEdit.Margin = new System.Windows.Forms.Padding(2);
            this.pictureEdit.MenuManager = this.baseRibbonControl;
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit.Properties.ReadOnly = true;
            this.pictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit.Size = new System.Drawing.Size(695, 141);
            this.pictureEdit.StyleController = this.layoutControl1;
            this.pictureEdit.TabIndex = 11;
            // 
            // disabled_ID
            // 
            this.disabled_ID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "Id", true));
            this.disabled_ID.Enabled = false;
            this.disabled_ID.Location = new System.Drawing.Point(644, 51);
            this.disabled_ID.MenuManager = this.baseRibbonControl;
            this.disabled_ID.Name = "disabled_ID";
            this.disabled_ID.Size = new System.Drawing.Size(66, 20);
            this.disabled_ID.StyleController = this.layoutControl1;
            this.disabled_ID.TabIndex = 13;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleSeparator1,
            this.loGroupStatus});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(725, 387);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 0);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(723, 2);
            // 
            // loGroupStatus
            // 
            this.loGroupStatus.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem10,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.lblInfo});
            this.loGroupStatus.Location = new System.Drawing.Point(0, 2);
            this.loGroupStatus.Name = "loGroupStatus";
            this.loGroupStatus.Size = new System.Drawing.Size(723, 383);
            this.loGroupStatus.Text = "Status Properties";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ctrl_StatusName;
            this.layoutControlItem1.CustomizationFormText = "Label Display Name:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(329, 40);
            this.layoutControlItem1.Text = "Status Display Name:";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(256, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this._statusViewOption;
            this.layoutControlItem4.CustomizationFormText = "Linked To View Option:";
            this.layoutControlItem4.Location = new System.Drawing.Point(329, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(300, 40);
            this.layoutControlItem4.Text = "Linked To View Option:";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(256, 13);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.pictureEdit;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 196);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(699, 145);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ctrl_StatusColour;
            this.layoutControlItem2.CustomizationFormText = "Colour:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(699, 40);
            this.layoutControlItem2.Text = "Colour:";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(256, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ctrl_FilterControl;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 97);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(699, 99);
            this.layoutControlItem3.Text = "Status Value (Criteria to match for status to activate)";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(256, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.disabled_ID;
            this.layoutControlItem5.Location = new System.Drawing.Point(629, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 40);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(70, 40);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(70, 40);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "ID:";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(256, 13);
            // 
            // lblInfo
            // 
            this.lblInfo.AllowHotTrack = false;
            this.lblInfo.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
            this.lblInfo.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblInfo.CustomizationFormText = "When adding or editing default statuses the criteria can not be changed, it will " +
    "be automatically generated.";
            this.lblInfo.Location = new System.Drawing.Point(0, 80);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.OptionsPrint.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkRed;
            this.lblInfo.OptionsPrint.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblInfo.Size = new System.Drawing.Size(699, 17);
            this.lblInfo.Text = "When adding or editing default statuses the criteria can not be changed, it will " +
    "be automatically generated.";
            this.lblInfo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblInfo.TextSize = new System.Drawing.Size(513, 13);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gridControlStatuses);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.Root;
            this.layoutControl2.Size = new System.Drawing.Size(725, 387);
            this.layoutControl2.TabIndex = 1;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9,
            this.simpleLabelItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.Root.Size = new System.Drawing.Size(725, 387);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.gridControlStatuses;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(723, 368);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 368);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(723, 17);
            this.simpleLabelItem1.Text = "Statuses";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(42, 13);
            // 
            // StatusesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 558);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StatusesForm";
            this.Text = "Appointment Statuses Form";
            this.Load += new System.EventHandler(this.JarsUserAccountsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.xtraTabPageList.ResumeLayout(false);
            this.xtraTabPageDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStatuses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStatuses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItmLabelColorPickEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.filterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_StatusName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_StatusColour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._statusViewOption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.disabled_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loGroupStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlStatuses;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewStatuses;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colViewType;
        private DevExpress.XtraGrid.Columns.GridColumn colLabelName;
        private DevExpress.XtraGrid.Columns.GridColumn colLabelValue;
        private DevExpress.XtraGrid.Columns.GridColumn colColourRGB;
        private DevExpress.XtraGrid.Columns.GridColumn colSortIndex;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit repItmLabelColorPickEdit;
        private DevExpress.XtraEditors.TextEdit ctrl_StatusName;
        private DevExpress.XtraEditors.ColorPickEdit ctrl_StatusColour;
        private DevExpress.XtraEditors.TextEdit _statusViewOption;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.LayoutControlGroup loGroupStatus;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.FilterControl ctrl_FilterControl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.BindingSource filterBindingSource;
        private DevExpress.XtraEditors.TextEdit disabled_ID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.SimpleLabelItem lblInfo;
    }
}