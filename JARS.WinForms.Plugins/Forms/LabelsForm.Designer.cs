
using JARS.Core.Security;
using System.Security.Permissions;

namespace JARS.Win.Plugins
{   
    partial class LabelsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabelsForm));
            this.gridControlLabels = new DevExpress.XtraGrid.GridControl();
            this.gridViewLabels = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colViewType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLabelName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLabelValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colColourRGB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repItmLabelColorPickEdit = new DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit();
            this.colSortIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ctrl_FilterControl = new DevExpress.XtraEditors.FilterControl();
            this.filterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.picEdit = new DevExpress.XtraEditors.PictureEdit();
            this.ctrl_txtLabelName = new DevExpress.XtraEditors.TextEdit();
            this._txtLinkedToViewOption = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_txtLabelColour = new DevExpress.XtraEditors.ColorPickEdit();
            this.disabled_id = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_txtLabelForeColour = new DevExpress.XtraEditors.ColorPickEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.loGroupLabels = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblInfo = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.colForeColourRGB = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.xtraTabPageList.SuspendLayout();
            this.xtraTabPageDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLabels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLabels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItmLabelColorPickEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtLabelName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtLinkedToViewOption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtLabelColour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.disabled_id.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtLabelForeColour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loGroupLabels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
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
            // 
            // 
            // 
            this.baseRibbonControl.SearchEditItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.baseRibbonControl.SearchEditItem.EditWidth = 150;
            this.baseRibbonControl.SearchEditItem.Id = -5000;
            this.baseRibbonControl.SearchEditItem.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
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
            this.xtraTabPageDetails.Size = new System.Drawing.Size(888, 398);
            // 
            // defaultBindingSource
            // 
            this.defaultBindingSource.DataSource = typeof(JARS.Entities.ApptLabel);
            this.defaultBindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.defaultBindingSource_AddingNew);
            this.defaultBindingSource.PositionChanged += new System.EventHandler(this.defaultBindingSource_PositionChanged);
            // 
            // gridControlLabels
            // 
            this.gridControlLabels.DataSource = this.defaultBindingSource;
            this.gridControlLabels.Location = new System.Drawing.Point(3, 3);
            this.gridControlLabels.MainView = this.gridViewLabels;
            this.gridControlLabels.MenuManager = this.baseRibbonControl;
            this.gridControlLabels.Name = "gridControlLabels";
            this.gridControlLabels.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repItmLabelColorPickEdit});
            this.gridControlLabels.Size = new System.Drawing.Size(719, 364);
            this.gridControlLabels.TabIndex = 0;
            this.gridControlLabels.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLabels});
            // 
            // gridViewLabels
            // 
            this.gridViewLabels.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colViewType,
            this.colLabelName,
            this.colLabelValue,
            this.colForeColourRGB,
            this.colColourRGB,
            this.colSortIndex});
            this.gridViewLabels.GridControl = this.gridControlLabels;
            this.gridViewLabels.Name = "gridViewLabels";
            this.gridViewLabels.PreviewFieldName = "Email";
            // 
            // colViewType
            // 
            this.colViewType.FieldName = "ViewName";
            this.colViewType.Name = "colViewType";
            this.colViewType.OptionsColumn.AllowEdit = false;
            this.colViewType.OptionsColumn.ReadOnly = true;
            this.colViewType.Visible = true;
            this.colViewType.VisibleIndex = 0;
            this.colViewType.Width = 93;
            // 
            // colLabelName
            // 
            this.colLabelName.FieldName = "LabelName";
            this.colLabelName.Name = "colLabelName";
            this.colLabelName.OptionsColumn.AllowEdit = false;
            this.colLabelName.OptionsColumn.ReadOnly = true;
            this.colLabelName.Visible = true;
            this.colLabelName.VisibleIndex = 1;
            this.colLabelName.Width = 130;
            // 
            // colLabelValue
            // 
            this.colLabelValue.FieldName = "LabelCriteria";
            this.colLabelValue.Name = "colLabelValue";
            this.colLabelValue.OptionsColumn.AllowEdit = false;
            this.colLabelValue.OptionsColumn.ReadOnly = true;
            this.colLabelValue.Visible = true;
            this.colLabelValue.VisibleIndex = 2;
            this.colLabelValue.Width = 209;
            // 
            // colColourRGB
            // 
            this.colColourRGB.Caption = "Back Colour";
            this.colColourRGB.ColumnEdit = this.repItmLabelColorPickEdit;
            this.colColourRGB.FieldName = "ColourRGB";
            this.colColourRGB.Name = "colColourRGB";
            this.colColourRGB.OptionsColumn.AllowEdit = false;
            this.colColourRGB.OptionsColumn.ReadOnly = true;
            this.colColourRGB.Visible = true;
            this.colColourRGB.VisibleIndex = 4;
            this.colColourRGB.Width = 109;
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
            this.colSortIndex.VisibleIndex = 5;
            this.colSortIndex.Width = 57;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ctrl_FilterControl);
            this.layoutControl1.Controls.Add(this.picEdit);
            this.layoutControl1.Controls.Add(this.ctrl_txtLabelName);
            this.layoutControl1.Controls.Add(this._txtLinkedToViewOption);
            this.layoutControl1.Controls.Add(this.ctrl_txtLabelColour);
            this.layoutControl1.Controls.Add(this.disabled_id);
            this.layoutControl1.Controls.Add(this.ctrl_txtLabelForeColour);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1123, 275, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(888, 398);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ctrl_FilterControl
            // 
            this.ctrl_FilterControl.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ctrl_FilterControl.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "LabelCriteria", true));
            this.ctrl_FilterControl.Location = new System.Drawing.Point(15, 148);
            this.ctrl_FilterControl.Name = "ctrl_FilterControl";
            this.ctrl_FilterControl.Size = new System.Drawing.Size(426, 235);
            this.ctrl_FilterControl.SourceControl = this.filterBindingSource;
            this.ctrl_FilterControl.TabIndex = 13;
            this.ctrl_FilterControl.Text = "filterControl";
            // 
            // picEdit
            // 
            this.picEdit.EditValue = ((object)(resources.GetObject("picEdit.EditValue")));
            this.picEdit.Location = new System.Drawing.Point(445, 132);
            this.picEdit.Margin = new System.Windows.Forms.Padding(2);
            this.picEdit.MenuManager = this.baseRibbonControl;
            this.picEdit.Name = "picEdit";
            this.picEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picEdit.Properties.Appearance.Options.UseBackColor = true;
            this.picEdit.Properties.ReadOnly = true;
            this.picEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picEdit.Size = new System.Drawing.Size(428, 251);
            this.picEdit.StyleController = this.layoutControl1;
            this.picEdit.TabIndex = 12;
            this.picEdit.Paint += new System.Windows.Forms.PaintEventHandler(this.picEdit_Paint);
            // 
            // ctrl_txtLabelName
            // 
            this.ctrl_txtLabelName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "LabelName", true));
            this.ctrl_txtLabelName.Location = new System.Drawing.Point(15, 51);
            this.ctrl_txtLabelName.MenuManager = this.baseRibbonControl;
            this.ctrl_txtLabelName.Name = "ctrl_txtLabelName";
            this.ctrl_txtLabelName.Size = new System.Drawing.Size(426, 20);
            this.ctrl_txtLabelName.StyleController = this.layoutControl1;
            this.ctrl_txtLabelName.TabIndex = 6;
            // 
            // _txtLinkedToViewOption
            // 
            this._txtLinkedToViewOption.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "ViewName", true));
            this._txtLinkedToViewOption.Enabled = false;
            this._txtLinkedToViewOption.Location = new System.Drawing.Point(445, 51);
            this._txtLinkedToViewOption.MenuManager = this.baseRibbonControl;
            this._txtLinkedToViewOption.Name = "_txtLinkedToViewOption";
            this._txtLinkedToViewOption.Size = new System.Drawing.Size(342, 20);
            this._txtLinkedToViewOption.StyleController = this.layoutControl1;
            this._txtLinkedToViewOption.TabIndex = 9;
            // 
            // ctrl_txtLabelColour
            // 
            this.ctrl_txtLabelColour.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "ColourRGB", true));
            this.ctrl_txtLabelColour.EditValue = System.Drawing.Color.Empty;
            this.ctrl_txtLabelColour.Location = new System.Drawing.Point(15, 91);
            this.ctrl_txtLabelColour.MenuManager = this.baseRibbonControl;
            this.ctrl_txtLabelColour.Name = "ctrl_txtLabelColour";
            this.ctrl_txtLabelColour.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.ctrl_txtLabelColour.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ctrl_txtLabelColour.Properties.StoreColorAsInteger = true;
            this.ctrl_txtLabelColour.Size = new System.Drawing.Size(426, 20);
            this.ctrl_txtLabelColour.StyleController = this.layoutControl1;
            this.ctrl_txtLabelColour.TabIndex = 8;
            // 
            // disabled_id
            // 
            this.disabled_id.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "ID", true));
            this.disabled_id.Enabled = false;
            this.disabled_id.Location = new System.Drawing.Point(791, 51);
            this.disabled_id.MenuManager = this.baseRibbonControl;
            this.disabled_id.Name = "disabled_id";
            this.disabled_id.Size = new System.Drawing.Size(82, 20);
            this.disabled_id.StyleController = this.layoutControl1;
            this.disabled_id.TabIndex = 14;
            // 
            // ctrl_txtLabelForeColour
            // 
            this.ctrl_txtLabelForeColour.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "ForeColourRGB", true));
            this.ctrl_txtLabelForeColour.EditValue = System.Drawing.Color.Empty;
            this.ctrl_txtLabelForeColour.Location = new System.Drawing.Point(445, 91);
            this.ctrl_txtLabelForeColour.MenuManager = this.baseRibbonControl;
            this.ctrl_txtLabelForeColour.Name = "ctrl_txtLabelForeColour";
            this.ctrl_txtLabelForeColour.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.ctrl_txtLabelForeColour.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ctrl_txtLabelForeColour.Properties.StoreColorAsInteger = true;
            this.ctrl_txtLabelForeColour.Size = new System.Drawing.Size(428, 20);
            this.ctrl_txtLabelForeColour.StyleController = this.layoutControl1;
            this.ctrl_txtLabelForeColour.TabIndex = 15;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleSeparator1,
            this.loGroupLabels});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(888, 398);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 0);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(886, 2);
            // 
            // loGroupLabels
            // 
            this.loGroupLabels.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.lblInfo,
            this.layoutControlItem11,
            this.layoutControlItem4});
            this.loGroupLabels.Location = new System.Drawing.Point(0, 2);
            this.loGroupLabels.Name = "loGroupLabels";
            this.loGroupLabels.Size = new System.Drawing.Size(886, 394);
            this.loGroupLabels.Text = "Label Properties";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ctrl_txtLabelName;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(430, 40);
            this.layoutControlItem3.Text = "Label Display Name:";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(187, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ctrl_txtLabelColour;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(430, 40);
            this.layoutControlItem5.Text = "Back Colour:";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(187, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this._txtLinkedToViewOption;
            this.layoutControlItem6.Location = new System.Drawing.Point(430, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(346, 40);
            this.layoutControlItem6.Text = "Linked To View Option:";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(187, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ctrl_FilterControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 97);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(430, 255);
            this.layoutControlItem1.Text = "Label Value (Criteria to apply this label)";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(187, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.disabled_id;
            this.layoutControlItem2.Location = new System.Drawing.Point(776, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 40);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(70, 40);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(86, 40);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "ID:";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(187, 13);
            // 
            // lblInfo
            // 
            this.lblInfo.AllowHotTrack = false;
            this.lblInfo.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
            this.lblInfo.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblInfo.Location = new System.Drawing.Point(0, 80);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(862, 17);
            this.lblInfo.Text = "When adding or editing default labels the criteria can not be changed, it will be" +
    " automatically generated.";
            this.lblInfo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblInfo.TextSize = new System.Drawing.Size(499, 13);
            this.lblInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.picEdit;
            this.layoutControlItem11.Location = new System.Drawing.Point(430, 97);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(432, 255);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ctrl_txtLabelForeColour;
            this.layoutControlItem4.Location = new System.Drawing.Point(430, 40);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(432, 40);
            this.layoutControlItem4.Text = "Fore Colour:";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(187, 13);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gridControlLabels);
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
            this.layoutControlItem9.Control = this.gridControlLabels;
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
            // colForeColourRGB
            // 
            this.colForeColourRGB.Caption = "Fore Colour";
            this.colForeColourRGB.ColumnEdit = this.repItmLabelColorPickEdit;
            this.colForeColourRGB.FieldName = "ForeColourRGB";
            this.colForeColourRGB.Name = "colForeColourRGB";
            this.colForeColourRGB.OptionsColumn.AllowEdit = false;
            this.colForeColourRGB.OptionsColumn.ReadOnly = true;
            this.colForeColourRGB.Visible = true;
            this.colForeColourRGB.VisibleIndex = 3;
            this.colForeColourRGB.Width = 103;
            // 
            // LabelsForm
            // 
            this.defaultToolTipController.SetAllowHtmlText(this, DevExpress.Utils.DefaultBoolean.Default);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 558);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LabelsForm";
            this.Text = "Appointment Labels Form";
            this.Load += new System.EventHandler(this.JarsUserAccountsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.xtraTabPageList.ResumeLayout(false);
            this.xtraTabPageDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLabels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLabels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItmLabelColorPickEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.filterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtLabelName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._txtLinkedToViewOption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtLabelColour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.disabled_id.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtLabelForeColour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loGroupLabels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlLabels;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLabels;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit ctrl_txtLabelName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit _txtLinkedToViewOption;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraEditors.ColorPickEdit ctrl_txtLabelColour;
        private DevExpress.XtraGrid.Columns.GridColumn colViewType;
        private DevExpress.XtraGrid.Columns.GridColumn colLabelName;
        private DevExpress.XtraGrid.Columns.GridColumn colLabelValue;
        private DevExpress.XtraGrid.Columns.GridColumn colColourRGB;
        private DevExpress.XtraGrid.Columns.GridColumn colSortIndex;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit repItmLabelColorPickEdit;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.LayoutControlGroup loGroupLabels;
        private DevExpress.XtraEditors.PictureEdit picEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.FilterControl ctrl_FilterControl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource filterBindingSource;
        private DevExpress.XtraEditors.TextEdit disabled_id;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SimpleLabelItem lblInfo;
        private DevExpress.XtraEditors.ColorPickEdit ctrl_txtLabelForeColour;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colForeColourRGB;
    }
}