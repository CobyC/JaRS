namespace JARS.Win.Plugins

{
    partial class JarsDefaultAppointmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JarsDefaultAppointmentForm));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ctrl_ckIsAllDay = new DevExpress.XtraEditors.CheckEdit();
            this.ctrl_ckIsMobile = new DevExpress.XtraEditors.CheckEdit();
            this.ctrl_txtDisplayName = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_txtUID = new DevExpress.XtraEditors.TextEdit();
            this.ctrl_dtStartTime = new DevExpress.XtraEditors.SpinEdit();
            this.ctrl_memDescription = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.DisplayNameLO = new DevExpress.XtraLayout.LayoutControlItem();
            this.UID_LO = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.StartTimeLO = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.gridControlDefAppts = new DevExpress.XtraGrid.GridControl();
            this.gridViewDefAppts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDescription1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDuration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsAllDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShowOnMobile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.xtraTabPageList.SuspendLayout();
            this.xtraTabPageDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_ckIsAllDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_ckIsMobile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtDisplayName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtUID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_dtStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_memDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayNameLO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UID_LO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartTimeLO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDefAppts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDefAppts)).BeginInit();
            this.SuspendLayout();
            // 
            // baseRibbonControl
            // 
            this.baseRibbonControl.ExpandCollapseItem.Id = 0;
            this.baseRibbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1});
            this.baseRibbonControl.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.baseRibbonControl.Size = new System.Drawing.Size(721, 143);
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xtraTabControl.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.None;
            this.xtraTabControl.SelectedTabPage = this.xtraTabPageList;
            this.xtraTabControl.Size = new System.Drawing.Size(721, 284);
            // 
            // xtraTabPageList
            // 
            this.xtraTabPageList.Controls.Add(this.gridControlDefAppts);
            this.xtraTabPageList.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPageList.ImageOptions.Image")));
            this.xtraTabPageList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xtraTabPageList.Size = new System.Drawing.Size(715, 256);
            // 
            // xtraTabPageDetails
            // 
            this.xtraTabPageDetails.Controls.Add(this.layoutControl1);
            this.xtraTabPageDetails.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPageDetails.ImageOptions.Image")));
            this.xtraTabPageDetails.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xtraTabPageDetails.Size = new System.Drawing.Size(715, 256);
            // 
            // defaultBindingSource
            // 
            this.defaultBindingSource.DataSource = typeof(JARS.Entities.JarsDefaultAppointment);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 10;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ctrl_ckIsAllDay);
            this.layoutControl1.Controls.Add(this.ctrl_ckIsMobile);
            this.layoutControl1.Controls.Add(this.ctrl_txtDisplayName);
            this.layoutControl1.Controls.Add(this.ctrl_txtUID);
            this.layoutControl1.Controls.Add(this.ctrl_dtStartTime);
            this.layoutControl1.Controls.Add(this.ctrl_memDescription);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2837, 290, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(715, 256);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ctrl_ckIsAllDay
            // 
            this.ctrl_ckIsAllDay.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "IsAllDay", true));
            this.ctrl_ckIsAllDay.EditValue = true;
            this.ctrl_ckIsAllDay.Location = new System.Drawing.Point(453, 39);
            this.ctrl_ckIsAllDay.MenuManager = this.baseRibbonControl;
            this.ctrl_ckIsAllDay.Name = "ctrl_ckIsAllDay";
            this.ctrl_ckIsAllDay.Properties.Caption = "Is All Day:";
            this.ctrl_ckIsAllDay.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ctrl_ckIsAllDay.Size = new System.Drawing.Size(119, 19);
            this.ctrl_ckIsAllDay.StyleController = this.layoutControl1;
            toolTipTitleItem1.Text = "Is All Day:";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Indicates that the appointment will be an all day appointment.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ctrl_ckIsAllDay.SuperTip = superToolTip1;
            this.ctrl_ckIsAllDay.TabIndex = 19;
            // 
            // ctrl_ckIsMobile
            // 
            this.ctrl_ckIsMobile.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "ShowOnMobile", true));
            this.ctrl_ckIsMobile.EditValue = true;
            this.ctrl_ckIsMobile.Location = new System.Drawing.Point(453, 60);
            this.ctrl_ckIsMobile.MenuManager = this.baseRibbonControl;
            this.ctrl_ckIsMobile.Name = "ctrl_ckIsMobile";
            this.ctrl_ckIsMobile.Properties.Caption = "Show On Mobile";
            this.ctrl_ckIsMobile.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ctrl_ckIsMobile.Size = new System.Drawing.Size(119, 19);
            this.ctrl_ckIsMobile.StyleController = this.layoutControl1;
            toolTipTitleItem2.Text = "Show On Mobile:";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Indicatesthat the appointment will be sent to the mobile device.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.ctrl_ckIsMobile.SuperTip = superToolTip2;
            this.ctrl_ckIsMobile.TabIndex = 18;
            // 
            // ctrl_txtDisplayName
            // 
            this.ctrl_txtDisplayName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "Subject", true));
            this.ctrl_txtDisplayName.Location = new System.Drawing.Point(2, 17);
            this.ctrl_txtDisplayName.MenuManager = this.baseRibbonControl;
            this.ctrl_txtDisplayName.Name = "ctrl_txtDisplayName";
            this.ctrl_txtDisplayName.Size = new System.Drawing.Size(570, 20);
            this.ctrl_txtDisplayName.StyleController = this.layoutControl1;
            this.ctrl_txtDisplayName.TabIndex = 4;
            // 
            // ctrl_txtUID
            // 
            this.ctrl_txtUID.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "ID", true));
            this.ctrl_txtUID.Location = new System.Drawing.Point(615, 17);
            this.ctrl_txtUID.MenuManager = this.baseRibbonControl;
            this.ctrl_txtUID.Name = "ctrl_txtUID";
            this.ctrl_txtUID.Properties.ReadOnly = true;
            this.ctrl_txtUID.Size = new System.Drawing.Size(98, 20);
            this.ctrl_txtUID.StyleController = this.layoutControl1;
            this.ctrl_txtUID.TabIndex = 6;
            // 
            // ctrl_dtStartTime
            // 
            this.ctrl_dtStartTime.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.defaultBindingSource, "DefaultDuration", true));
            this.ctrl_dtStartTime.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ctrl_dtStartTime.Location = new System.Drawing.Point(2, 54);
            this.ctrl_dtStartTime.MenuManager = this.baseRibbonControl;
            this.ctrl_dtStartTime.Name = "ctrl_dtStartTime";
            this.ctrl_dtStartTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ctrl_dtStartTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ctrl_dtStartTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ctrl_dtStartTime.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.ctrl_dtStartTime.Properties.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.ctrl_dtStartTime.Properties.Mask.EditMask = "n2";
            this.ctrl_dtStartTime.Properties.NullText = "1";
            this.ctrl_dtStartTime.Size = new System.Drawing.Size(153, 20);
            this.ctrl_dtStartTime.StyleController = this.layoutControl1;
            this.ctrl_dtStartTime.TabIndex = 10;
            // 
            // ctrl_memDescription
            // 
            this.ctrl_memDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.defaultBindingSource, "Description", true));
            this.ctrl_memDescription.Location = new System.Drawing.Point(2, 96);
            this.ctrl_memDescription.MenuManager = this.baseRibbonControl;
            this.ctrl_memDescription.Name = "ctrl_memDescription";
            this.ctrl_memDescription.Size = new System.Drawing.Size(711, 158);
            this.ctrl_memDescription.StyleController = this.layoutControl1;
            this.ctrl_memDescription.TabIndex = 20;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.DisplayNameLO,
            this.UID_LO,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.StartTimeLO,
            this.layoutControlItem1,
            this.layoutControlItem8,
            this.emptySpaceItem3,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(715, 256);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // DisplayNameLO
            // 
            this.DisplayNameLO.Control = this.ctrl_txtDisplayName;
            this.DisplayNameLO.Location = new System.Drawing.Point(0, 0);
            this.DisplayNameLO.Name = "DisplayNameLO";
            this.DisplayNameLO.Size = new System.Drawing.Size(572, 37);
            this.DisplayNameLO.Text = "Subject:";
            this.DisplayNameLO.TextLocation = DevExpress.Utils.Locations.Top;
            this.DisplayNameLO.TextSize = new System.Drawing.Size(53, 13);
            // 
            // UID_LO
            // 
            this.UID_LO.BestFitWeight = 200;
            this.UID_LO.Control = this.ctrl_txtUID;
            this.UID_LO.Location = new System.Drawing.Point(613, 0);
            this.UID_LO.Name = "UID_LO";
            this.UID_LO.Size = new System.Drawing.Size(100, 37);
            this.UID_LO.Text = "UID:";
            this.UID_LO.TextLocation = DevExpress.Utils.Locations.Top;
            this.UID_LO.TextSize = new System.Drawing.Size(53, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(572, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(41, 37);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ctrl_memDescription;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 79);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(713, 175);
            this.layoutControlItem2.Text = "Description";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(53, 13);
            // 
            // StartTimeLO
            // 
            this.StartTimeLO.Control = this.ctrl_dtStartTime;
            this.StartTimeLO.Location = new System.Drawing.Point(0, 37);
            this.StartTimeLO.Name = "StartTimeLO";
            this.StartTimeLO.Size = new System.Drawing.Size(155, 42);
            this.StartTimeLO.Text = "Duration:";
            this.StartTimeLO.TextLocation = DevExpress.Utils.Locations.Top;
            this.StartTimeLO.TextSize = new System.Drawing.Size(53, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ctrl_ckIsAllDay;
            this.layoutControlItem1.Location = new System.Drawing.Point(451, 37);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(121, 21);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.ctrl_ckIsMobile;
            this.layoutControlItem8.Location = new System.Drawing.Point(451, 58);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(121, 21);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(155, 37);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(296, 42);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(572, 37);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(141, 42);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // gridControlDefAppts
            // 
            this.gridControlDefAppts.DataSource = this.defaultBindingSource;
            this.gridControlDefAppts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDefAppts.Location = new System.Drawing.Point(0, 0);
            this.gridControlDefAppts.MainView = this.gridViewDefAppts;
            this.gridControlDefAppts.MenuManager = this.baseRibbonControl;
            this.gridControlDefAppts.Name = "gridControlDefAppts";
            this.gridControlDefAppts.Size = new System.Drawing.Size(715, 256);
            this.gridControlDefAppts.TabIndex = 0;
            this.gridControlDefAppts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDefAppts});
            // 
            // gridViewDefAppts
            // 
            this.gridViewDefAppts.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDescription1,
            this.colDuration,
            this.colIsAllDay,
            this.colShowOnMobile,
            this.colID});
            this.gridViewDefAppts.GridControl = this.gridControlDefAppts;
            this.gridViewDefAppts.Name = "gridViewDefAppts";
            this.gridViewDefAppts.OptionsView.ShowPreview = true;
            this.gridViewDefAppts.PreviewFieldName = "Description";
            // 
            // colDescription1
            // 
            this.colDescription1.FieldName = "Subject";
            this.colDescription1.Name = "colDescription1";
            this.colDescription1.OptionsColumn.AllowEdit = false;
            this.colDescription1.OptionsColumn.ReadOnly = true;
            this.colDescription1.Visible = true;
            this.colDescription1.VisibleIndex = 0;
            // 
            // colDuration
            // 
            this.colDuration.FieldName = "DefaultDuration";
            this.colDuration.Name = "colDuration";
            this.colDuration.OptionsColumn.AllowEdit = false;
            this.colDuration.OptionsColumn.ReadOnly = true;
            this.colDuration.Visible = true;
            this.colDuration.VisibleIndex = 1;
            // 
            // colIsAllDay
            // 
            this.colIsAllDay.FieldName = "IsAllDay";
            this.colIsAllDay.Name = "colIsAllDay";
            this.colIsAllDay.OptionsColumn.AllowEdit = false;
            this.colIsAllDay.OptionsColumn.ReadOnly = true;
            this.colIsAllDay.Visible = true;
            this.colIsAllDay.VisibleIndex = 2;
            // 
            // colShowOnMobile
            // 
            this.colShowOnMobile.FieldName = "ShowOnMobile";
            this.colShowOnMobile.Name = "colShowOnMobile";
            this.colShowOnMobile.OptionsColumn.AllowEdit = false;
            this.colShowOnMobile.OptionsColumn.ReadOnly = true;
            this.colShowOnMobile.Visible = true;
            this.colShowOnMobile.VisibleIndex = 3;
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.OptionsColumn.ReadOnly = true;
            // 
            // JarsDefaultAppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 427);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "JarsDefaultAppointmentForm";
            this.Text = "Default Appointment Types";
            this.Load += new System.EventHandler(this.JarsDefaultAppointmentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.xtraTabPageList.ResumeLayout(false);
            this.xtraTabPageDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_ckIsAllDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_ckIsMobile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtDisplayName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_txtUID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_dtStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrl_memDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayNameLO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UID_LO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartTimeLO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDefAppts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDefAppts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit ctrl_txtDisplayName;
        private DevExpress.XtraEditors.TextEdit ctrl_txtUID;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem DisplayNameLO;
        private DevExpress.XtraLayout.LayoutControlItem UID_LO;
        private DevExpress.XtraLayout.LayoutControlItem StartTimeLO;
        private DevExpress.XtraGrid.GridControl gridControlDefAppts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDefAppts;
        private DevExpress.XtraEditors.CheckEdit ctrl_ckIsMobile;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription1;
        private DevExpress.XtraGrid.Columns.GridColumn colDuration;
        private DevExpress.XtraGrid.Columns.GridColumn colIsAllDay;
        private DevExpress.XtraGrid.Columns.GridColumn colShowOnMobile;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraEditors.CheckEdit ctrl_ckIsAllDay;
        private DevExpress.XtraEditors.SpinEdit ctrl_dtStartTime;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.MemoEdit ctrl_memDescription;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}