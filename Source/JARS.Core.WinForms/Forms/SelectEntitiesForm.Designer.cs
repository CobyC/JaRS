namespace JARS.Core.WinForms.Forms
{
    partial class SelectEntitiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectEntitiesForm));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.SelectUsersFormlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.chkLstBoxCtrl_Entities = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.searchEntityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.btnOKitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnCancelitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lytCntrItm_Search = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.SelectUsersFormlayoutControl1ConvertedLayout)).BeginInit();
            this.SelectUsersFormlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLstBoxCtrl_Entities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchEntityBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOKitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lytCntrItm_Search)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.ImageOptions.Image")));
            this.btnOK.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnOK.Location = new System.Drawing.Point(3, 428);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(187, 38);
            this.btnOK.StyleController = this.SelectUsersFormlayoutControl1ConvertedLayout;
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            // 
            // SelectUsersFormlayoutControl1ConvertedLayout
            // 
            this.SelectUsersFormlayoutControl1ConvertedLayout.Controls.Add(this.searchControl1);
            this.SelectUsersFormlayoutControl1ConvertedLayout.Controls.Add(this.chkLstBoxCtrl_Entities);
            this.SelectUsersFormlayoutControl1ConvertedLayout.Controls.Add(this.btnOK);
            this.SelectUsersFormlayoutControl1ConvertedLayout.Controls.Add(this.btnCancel);
            this.SelectUsersFormlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectUsersFormlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.SelectUsersFormlayoutControl1ConvertedLayout.Name = "SelectUsersFormlayoutControl1ConvertedLayout";
            this.SelectUsersFormlayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.SelectUsersFormlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(389, 469);
            this.SelectUsersFormlayoutControl1ConvertedLayout.TabIndex = 8;
            // 
            // searchControl1
            // 
            this.searchControl1.Client = this.chkLstBoxCtrl_Entities;
            this.searchControl1.Location = new System.Drawing.Point(43, 3);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.Client = this.chkLstBoxCtrl_Entities;
            this.searchControl1.Size = new System.Drawing.Size(343, 20);
            this.searchControl1.StyleController = this.SelectUsersFormlayoutControl1ConvertedLayout;
            this.searchControl1.TabIndex = 9;
            // 
            // chkLstBoxCtrl_Entities
            // 
            this.chkLstBoxCtrl_Entities.CheckMember = "IsSelected";
            this.chkLstBoxCtrl_Entities.DataSource = this.searchEntityBindingSource;
            this.chkLstBoxCtrl_Entities.DisplayMember = "DisplayText";
            this.chkLstBoxCtrl_Entities.Location = new System.Drawing.Point(3, 27);
            this.chkLstBoxCtrl_Entities.Name = "chkLstBoxCtrl_Entities";
            this.chkLstBoxCtrl_Entities.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.chkLstBoxCtrl_Entities.Size = new System.Drawing.Size(383, 397);
            this.chkLstBoxCtrl_Entities.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.chkLstBoxCtrl_Entities.StyleController = this.SelectUsersFormlayoutControl1ConvertedLayout;
            this.chkLstBoxCtrl_Entities.TabIndex = 8;
            // 
            // searchEntityBindingSource
            // 
            this.searchEntityBindingSource.DataSource = typeof(JARS.Core.Utils.SearchEntity<long>);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCancel.Location = new System.Drawing.Point(194, 428);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(192, 38);
            this.btnCancel.StyleController = this.SelectUsersFormlayoutControl1ConvertedLayout;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.btnOKitem,
            this.btnCancelitem,
            this.layoutControlItem1,
            this.lytCntrItm_Search});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(389, 469);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // btnOKitem
            // 
            this.btnOKitem.Control = this.btnOK;
            this.btnOKitem.Location = new System.Drawing.Point(0, 425);
            this.btnOKitem.Name = "btnOKitem";
            this.btnOKitem.Size = new System.Drawing.Size(191, 42);
            this.btnOKitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnOKitem.TextVisible = false;
            // 
            // btnCancelitem
            // 
            this.btnCancelitem.Control = this.btnCancel;
            this.btnCancelitem.Location = new System.Drawing.Point(191, 425);
            this.btnCancelitem.Name = "btnCancelitem";
            this.btnCancelitem.Size = new System.Drawing.Size(196, 42);
            this.btnCancelitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnCancelitem.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkLstBoxCtrl_Entities;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(387, 401);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lytCntrItm_Search
            // 
            this.lytCntrItm_Search.Control = this.searchControl1;
            this.lytCntrItm_Search.Location = new System.Drawing.Point(0, 0);
            this.lytCntrItm_Search.Name = "lytCntrItm_Search";
            this.lytCntrItm_Search.Size = new System.Drawing.Size(387, 24);
            this.lytCntrItm_Search.Text = "Search:";
            this.lytCntrItm_Search.TextSize = new System.Drawing.Size(37, 13);
            // 
            // SelectEntitiesForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(389, 469);
            this.Controls.Add(this.SelectUsersFormlayoutControl1ConvertedLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SelectEntitiesForm";
            this.Text = "Select Entities";
            ((System.ComponentModel.ISupportInitialize)(this.SelectUsersFormlayoutControl1ConvertedLayout)).EndInit();
            this.SelectUsersFormlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLstBoxCtrl_Entities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchEntityBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOKitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lytCntrItm_Search)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControl SelectUsersFormlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem btnOKitem;
        private DevExpress.XtraLayout.LayoutControlItem btnCancelitem;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl chkLstBoxCtrl_Entities;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem lytCntrItm_Search;
        private System.Windows.Forms.BindingSource searchEntityBindingSource;
    }
}