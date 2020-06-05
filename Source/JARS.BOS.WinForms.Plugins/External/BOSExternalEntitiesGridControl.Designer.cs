namespace JARS.BOS.WinForms.Plugins.External
{
    partial class BOSExternalEntitiesGridControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BosEntityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblTimerIndicator = new DevExpress.XtraEditors.LabelControl();
            this.gcBOSExternalEntity = new DevExpress.XtraGrid.GridControl();
            this.gvBOSExternalEntity = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colJobRefId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLineOfWork = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDuration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.BosEntityBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBOSExternalEntity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBOSExternalEntity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // BosEntityBindingSource
            // 
            this.BosEntityBindingSource.DataSource = typeof(JARS.BOS.Entities.BOSEntity);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblTimerIndicator);
            this.layoutControl1.Controls.Add(this.gcBOSExternalEntity);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(279, 520);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblTimerIndicator
            // 
            this.lblTimerIndicator.Location = new System.Drawing.Point(82, 12);
            this.lblTimerIndicator.Name = "lblTimerIndicator";
            this.lblTimerIndicator.Size = new System.Drawing.Size(56, 13);
            this.lblTimerIndicator.StyleController = this.layoutControl1;
            this.lblTimerIndicator.TabIndex = 5;
            this.lblTimerIndicator.Text = "5 - Seconds";
            // 
            // gcBOSExternalEntity
            // 
            this.gcBOSExternalEntity.DataSource = this.BosEntityBindingSource;
            this.gcBOSExternalEntity.Location = new System.Drawing.Point(12, 29);
            this.gcBOSExternalEntity.MainView = this.gvBOSExternalEntity;
            this.gcBOSExternalEntity.Name = "gcBOSExternalEntity";
            this.gcBOSExternalEntity.Size = new System.Drawing.Size(255, 479);
            this.gcBOSExternalEntity.TabIndex = 4;
            this.gcBOSExternalEntity.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBOSExternalEntity});
            this.gcBOSExternalEntity.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gcBOSExternalEntity_MouseDown);
            this.gcBOSExternalEntity.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gcBOSExternalEntity_MouseMove);
            // 
            // gvBOSExternalEntity
            // 
            this.gvBOSExternalEntity.Appearance.Preview.Options.UseTextOptions = true;
            this.gvBOSExternalEntity.Appearance.Preview.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.gvBOSExternalEntity.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colJobRefId,
            this.colLocation,
            this.colLocationCode,
            this.colLineOfWork,
            this.colDuration});
            this.gvBOSExternalEntity.GridControl = this.gcBOSExternalEntity;
            this.gvBOSExternalEntity.Name = "gvBOSExternalEntity";
            this.gvBOSExternalEntity.OptionsView.ShowPreview = true;
            this.gvBOSExternalEntity.PreviewFieldName = "Description";
            this.gvBOSExternalEntity.PreviewLineCount = 2;
            this.gvBOSExternalEntity.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvBOSExternalEntity_CustomDrawRowIndicator);
            this.gvBOSExternalEntity.CustomDrawRowPreview += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.gvBOSExternalEntity_CustomDrawRowPreview);
            this.gvBOSExternalEntity.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvBOSExternalEntity_RowStyle);
            this.gvBOSExternalEntity.CalcPreviewText += new DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventHandler(this.gvBOSExternalEntity_CalcPreviewText);
            // 
            // colJobRefId
            // 
            this.colJobRefId.FieldName = "ExtRefId";
            this.colJobRefId.Name = "colJobRefId";
            this.colJobRefId.OptionsColumn.AllowEdit = false;
            this.colJobRefId.OptionsColumn.ReadOnly = true;
            this.colJobRefId.Visible = true;
            this.colJobRefId.VisibleIndex = 0;
            // 
            // colLocation
            // 
            this.colLocation.FieldName = "Location";
            this.colLocation.Name = "colLocation";
            this.colLocation.OptionsColumn.AllowEdit = false;
            this.colLocation.OptionsColumn.ReadOnly = true;
            // 
            // colLocationCode
            // 
            this.colLocationCode.FieldName = "LocationCode";
            this.colLocationCode.Name = "colLocationCode";
            this.colLocationCode.OptionsColumn.AllowEdit = false;
            this.colLocationCode.OptionsColumn.ReadOnly = true;
            // 
            // colLineOfWork
            // 
            this.colLineOfWork.FieldName = "LineOfWork";
            this.colLineOfWork.Name = "colLineOfWork";
            this.colLineOfWork.OptionsColumn.AllowEdit = false;
            this.colLineOfWork.OptionsColumn.ReadOnly = true;
            this.colLineOfWork.Visible = true;
            this.colLineOfWork.VisibleIndex = 1;
            // 
            // colDuration
            // 
            this.colDuration.FieldName = "Duration";
            this.colDuration.Name = "colDuration";
            this.colDuration.OptionsColumn.AllowEdit = false;
            this.colDuration.OptionsColumn.ReadOnly = true;
            this.colDuration.Visible = true;
            this.colDuration.VisibleIndex = 2;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(279, 520);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcBOSExternalEntity;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 17);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(259, 483);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lblTimerIndicator;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(259, 17);
            this.layoutControlItem2.Text = "Refreshing in:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(67, 13);
            // 
            // BOSExternalEntitiesGridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "BOSExternalEntitiesGridControl";
            this.Size = new System.Drawing.Size(279, 520);
            ((System.ComponentModel.ISupportInitialize)(this.BosEntityBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBOSExternalEntity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBOSExternalEntity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.BindingSource BosEntityBindingSource;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LabelControl lblTimerIndicator;
        private DevExpress.XtraGrid.GridControl gcBOSExternalEntity;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBOSExternalEntity;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colJobRefId;
        private DevExpress.XtraGrid.Columns.GridColumn colLocation;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationCode;
        private DevExpress.XtraGrid.Columns.GridColumn colLineOfWork;
        private DevExpress.XtraGrid.Columns.GridColumn colDuration;
    }
}
