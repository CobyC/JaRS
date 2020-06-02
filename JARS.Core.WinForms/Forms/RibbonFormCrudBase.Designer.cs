namespace JARS.Core.WinForms.Forms
{
    partial class RibbonFormCrudBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RibbonFormCrudBase));
            this.baseRibbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barbtnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSave = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnFirst = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPrevious = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnNext = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnLast = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barCkShowGroupPanel = new DevExpress.XtraBars.BarCheckItem();
            this.barCkFilterRow = new DevExpress.XtraBars.BarCheckItem();
            this.barCkPreviewRow = new DevExpress.XtraBars.BarCheckItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgRecords = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgNavigate = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgListOptions = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageList = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPageDetails = new DevExpress.XtraTab.XtraTabPage();
            this.defaultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // baseRibbonControl
            // 
            this.baseRibbonControl.ExpandCollapseItem.Id = 0;
            this.baseRibbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.baseRibbonControl.ExpandCollapseItem,
            this.barbtnAdd,
            this.barBtnEdit,
            this.barBtnSave,
            this.barBtnDelete,
            this.barBtnFirst,
            this.barBtnPrevious,
            this.barBtnNext,
            this.barBtnLast,
            this.barBtnRefresh,
            this.barBtnCancel,
            this.barCkShowGroupPanel,
            this.barCkFilterRow,
            this.barCkPreviewRow});
            this.baseRibbonControl.Location = new System.Drawing.Point(0, 0);
            this.baseRibbonControl.MaxItemId = 14;
            this.baseRibbonControl.Name = "baseRibbonControl";
            this.baseRibbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.baseRibbonControl.Size = new System.Drawing.Size(894, 143);
            // 
            // barbtnAdd
            // 
            this.barbtnAdd.Caption = "&Add";
            this.barbtnAdd.Id = 1;
            this.barbtnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barbtnAdd.ImageOptions.Image")));
            this.barbtnAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barbtnAdd.ImageOptions.LargeImage")));
            this.barbtnAdd.Name = "barbtnAdd";
            this.barbtnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barbtnAdd_ItemClick);
            // 
            // barBtnEdit
            // 
            this.barBtnEdit.Caption = "&Edit";
            this.barBtnEdit.Id = 2;
            this.barBtnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnEdit.ImageOptions.Image")));
            this.barBtnEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnEdit.ImageOptions.LargeImage")));
            this.barBtnEdit.Name = "barBtnEdit";
            this.barBtnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEdit_ItemClick);
            // 
            // barBtnSave
            // 
            this.barBtnSave.Caption = "&Save";
            this.barBtnSave.Id = 3;
            this.barBtnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnSave.ImageOptions.Image")));
            this.barBtnSave.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnSave.ImageOptions.LargeImage")));
            this.barBtnSave.Name = "barBtnSave";
            this.barBtnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSave_ItemClick);
            // 
            // barBtnDelete
            // 
            this.barBtnDelete.Caption = "&Delete";
            this.barBtnDelete.Id = 4;
            this.barBtnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnDelete.ImageOptions.Image")));
            this.barBtnDelete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnDelete.ImageOptions.LargeImage")));
            this.barBtnDelete.Name = "barBtnDelete";
            this.barBtnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDelete_ItemClick);
            // 
            // barBtnFirst
            // 
            this.barBtnFirst.Caption = "&First";
            this.barBtnFirst.Id = 5;
            this.barBtnFirst.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnFirst.ImageOptions.Image")));
            this.barBtnFirst.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnFirst.ImageOptions.LargeImage")));
            this.barBtnFirst.Name = "barBtnFirst";
            this.barBtnFirst.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnFirst_ItemClick);
            // 
            // barBtnPrevious
            // 
            this.barBtnPrevious.Caption = "&Previous";
            this.barBtnPrevious.Id = 6;
            this.barBtnPrevious.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnPrevious.ImageOptions.Image")));
            this.barBtnPrevious.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnPrevious.ImageOptions.LargeImage")));
            this.barBtnPrevious.Name = "barBtnPrevious";
            this.barBtnPrevious.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnPrevious_ItemClick);
            // 
            // barBtnNext
            // 
            this.barBtnNext.Caption = "&Next";
            this.barBtnNext.Id = 7;
            this.barBtnNext.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnNext.ImageOptions.Image")));
            this.barBtnNext.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnNext.ImageOptions.LargeImage")));
            this.barBtnNext.Name = "barBtnNext";
            this.barBtnNext.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnNext_ItemClick);
            // 
            // barBtnLast
            // 
            this.barBtnLast.Caption = "&Last";
            this.barBtnLast.Id = 8;
            this.barBtnLast.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnLast.ImageOptions.Image")));
            this.barBtnLast.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnLast.ImageOptions.LargeImage")));
            this.barBtnLast.Name = "barBtnLast";
            this.barBtnLast.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnLast_ItemClick);
            // 
            // barBtnRefresh
            // 
            this.barBtnRefresh.Caption = "&Reload";
            this.barBtnRefresh.Id = 9;
            this.barBtnRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnRefresh.ImageOptions.Image")));
            this.barBtnRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnRefresh.ImageOptions.LargeImage")));
            this.barBtnRefresh.Name = "barBtnRefresh";
            this.barBtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnRefresh_ItemClick);
            // 
            // barBtnCancel
            // 
            this.barBtnCancel.Caption = "Cancel";
            this.barBtnCancel.Id = 10;
            this.barBtnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnCancel.ImageOptions.Image")));
            this.barBtnCancel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnCancel.ImageOptions.LargeImage")));
            this.barBtnCancel.Name = "barBtnCancel";
            this.barBtnCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnCancel_ItemClick);
            // 
            // barCkShowGroupPanel
            // 
            this.barCkShowGroupPanel.Caption = "Group Panel";
            this.barCkShowGroupPanel.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.barCkShowGroupPanel.Id = 11;
            this.barCkShowGroupPanel.Name = "barCkShowGroupPanel";
            this.barCkShowGroupPanel.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCkShowGroupPanel_CheckedChanged);
            // 
            // barCkFilterRow
            // 
            this.barCkFilterRow.Caption = "Filter Row";
            this.barCkFilterRow.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.barCkFilterRow.Id = 12;
            this.barCkFilterRow.Name = "barCkFilterRow";
            this.barCkFilterRow.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCkFilterRow_CheckedChanged);
            // 
            // barCkPreviewRow
            // 
            this.barCkPreviewRow.Caption = "Preview Row";
            this.barCkPreviewRow.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.barCkPreviewRow.Id = 13;
            this.barCkPreviewRow.Name = "barCkPreviewRow";
            this.barCkPreviewRow.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCkPreviewRow_CheckedChanged);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgRecords,
            this.rpgNavigate,
            this.rpgListOptions});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Home";
            // 
            // rpgRecords
            // 
            this.rpgRecords.ItemLinks.Add(this.barBtnRefresh);
            this.rpgRecords.ItemLinks.Add(this.barbtnAdd);
            this.rpgRecords.ItemLinks.Add(this.barBtnEdit);
            this.rpgRecords.ItemLinks.Add(this.barBtnSave);
            this.rpgRecords.ItemLinks.Add(this.barBtnDelete);
            this.rpgRecords.ItemLinks.Add(this.barBtnCancel);
            this.rpgRecords.Name = "rpgRecords";
            this.rpgRecords.Text = "Manage";
            // 
            // rpgNavigate
            // 
            this.rpgNavigate.ItemLinks.Add(this.barBtnFirst);
            this.rpgNavigate.ItemLinks.Add(this.barBtnPrevious);
            this.rpgNavigate.ItemLinks.Add(this.barBtnNext);
            this.rpgNavigate.ItemLinks.Add(this.barBtnLast);
            this.rpgNavigate.Name = "rpgNavigate";
            this.rpgNavigate.Text = "Navigate";
            // 
            // rpgListOptions
            // 
            this.rpgListOptions.ItemLinks.Add(this.barCkShowGroupPanel);
            this.rpgListOptions.ItemLinks.Add(this.barCkFilterRow);
            this.rpgListOptions.ItemLinks.Add(this.barCkPreviewRow);
            this.rpgListOptions.Name = "rpgListOptions";
            this.rpgListOptions.Text = "Grid Options";
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl.Location = new System.Drawing.Point(0, 143);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.xtraTabPageList;
            this.xtraTabControl.Size = new System.Drawing.Size(894, 426);
            this.xtraTabControl.TabIndex = 2;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageList,
            this.xtraTabPageDetails});
            // 
            // xtraTabPageList
            // 
            this.xtraTabPageList.Name = "xtraTabPageList";
            this.xtraTabPageList.Size = new System.Drawing.Size(888, 398);
            this.xtraTabPageList.Text = "List";
            // 
            // xtraTabPageDetails
            // 
            this.xtraTabPageDetails.Name = "xtraTabPageDetails";
            this.xtraTabPageDetails.Size = new System.Drawing.Size(888, 400);
            this.xtraTabPageDetails.Text = "Details";
            // 
            // defaultBindingSource
            // 
            this.defaultBindingSource.PositionChanged += new System.EventHandler(this.defaultBindingSource_PositionChanged);
            // 
            // RibbonCRUDFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 569);
            this.Controls.Add(this.xtraTabControl);
            this.Controls.Add(this.baseRibbonControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RibbonCRUDFormBase";
            this.Ribbon = this.baseRibbonControl;
            this.ShowIcon = false;
            this.Text = "RibbonCRUDFormBase";
            this.Load += new System.EventHandler(this.RibbonCRUDFormBase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseRibbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defaultBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraBars.Ribbon.RibbonControl baseRibbonControl;
        public DevExpress.XtraBars.BarButtonItem barbtnAdd;
        public DevExpress.XtraBars.BarButtonItem barBtnEdit;
        public DevExpress.XtraBars.BarButtonItem barBtnSave;
        public DevExpress.XtraBars.BarButtonItem barBtnDelete;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        public DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgRecords;
        public DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgNavigate;
        public DevExpress.XtraBars.BarButtonItem barBtnFirst;
        public DevExpress.XtraBars.BarButtonItem barBtnPrevious;
        public DevExpress.XtraBars.BarButtonItem barBtnNext;
        public DevExpress.XtraBars.BarButtonItem barBtnLast;
        public DevExpress.XtraTab.XtraTabControl xtraTabControl;
        public DevExpress.XtraTab.XtraTabPage xtraTabPageList;
        public DevExpress.XtraTab.XtraTabPage xtraTabPageDetails;
        public DevExpress.XtraBars.BarButtonItem barBtnRefresh;
        private DevExpress.XtraBars.BarButtonItem barBtnCancel;
        public System.Windows.Forms.BindingSource defaultBindingSource;
        public DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgListOptions;
        public DevExpress.XtraBars.BarCheckItem barCkShowGroupPanel;
        public DevExpress.XtraBars.BarCheckItem barCkFilterRow;
        public DevExpress.XtraBars.BarCheckItem barCkPreviewRow;
    }
}