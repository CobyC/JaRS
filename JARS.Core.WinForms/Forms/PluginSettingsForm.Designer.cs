namespace JARS.Core.WinForms.Forms
{
    partial class PluginSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginSettingsForm));
            this.SelectUsersFormlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.gcSettings = new DevExpress.XtraGrid.GridControl();
            this.gvSettings = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPropertyKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPropertyValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.btnOKitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnCancelitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.SelectUsersFormlayoutControl1ConvertedLayout)).BeginInit();
            this.SelectUsersFormlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOKitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectUsersFormlayoutControl1ConvertedLayout
            // 
            this.SelectUsersFormlayoutControl1ConvertedLayout.Controls.Add(this.gcSettings);
            this.SelectUsersFormlayoutControl1ConvertedLayout.Controls.Add(this.btnOK);
            this.SelectUsersFormlayoutControl1ConvertedLayout.Controls.Add(this.btnCancel);
            this.SelectUsersFormlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectUsersFormlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.SelectUsersFormlayoutControl1ConvertedLayout.Name = "SelectUsersFormlayoutControl1ConvertedLayout";
            this.SelectUsersFormlayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.SelectUsersFormlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(377, 378);
            this.SelectUsersFormlayoutControl1ConvertedLayout.TabIndex = 9;
            // 
            // gcSettings
            // 
            this.gcSettings.Location = new System.Drawing.Point(3, 3);
            this.gcSettings.MainView = this.gvSettings;
            this.gcSettings.Name = "gcSettings";
            this.gcSettings.Size = new System.Drawing.Size(371, 330);
            this.gcSettings.TabIndex = 10;
            this.gcSettings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSettings});
            // 
            // gvSettings
            // 
            this.gvSettings.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPropertyKey,
            this.colPropertyValue});
            this.gvSettings.GridControl = this.gcSettings;
            this.gvSettings.Name = "gvSettings";
            this.gvSettings.OptionsView.ShowAutoFilterRow = true;
            this.gvSettings.OptionsView.ShowGroupPanel = false;
            this.gvSettings.CustomRowCellEditForEditing += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvSettings_CustomRowCellEditForEditing);
            // 
            // colPropertyKey
            // 
            this.colPropertyKey.Caption = "Key";
            this.colPropertyKey.FieldName = "Key";
            this.colPropertyKey.Name = "colPropertyKey";
            this.colPropertyKey.OptionsColumn.AllowEdit = false;
            this.colPropertyKey.OptionsColumn.ReadOnly = true;
            this.colPropertyKey.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colPropertyKey.Visible = true;
            this.colPropertyKey.VisibleIndex = 0;
            // 
            // colPropertyValue
            // 
            this.colPropertyValue.Caption = "Value";
            this.colPropertyValue.FieldName = "Value";
            this.colPropertyValue.Name = "colPropertyValue";
            this.colPropertyValue.Visible = true;
            this.colPropertyValue.VisibleIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.ImageOptions.Image")));
            this.btnOK.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnOK.Location = new System.Drawing.Point(3, 337);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(181, 38);
            this.btnOK.StyleController = this.SelectUsersFormlayoutControl1ConvertedLayout;
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCancel.Location = new System.Drawing.Point(188, 337);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(186, 38);
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
            this.layoutControlItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(377, 378);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // btnOKitem
            // 
            this.btnOKitem.Control = this.btnOK;
            this.btnOKitem.Location = new System.Drawing.Point(0, 334);
            this.btnOKitem.Name = "btnOKitem";
            this.btnOKitem.Size = new System.Drawing.Size(185, 42);
            this.btnOKitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnOKitem.TextVisible = false;
            // 
            // btnCancelitem
            // 
            this.btnCancelitem.Control = this.btnCancel;
            this.btnCancelitem.Location = new System.Drawing.Point(185, 334);
            this.btnCancelitem.Name = "btnCancelitem";
            this.btnCancelitem.Size = new System.Drawing.Size(190, 42);
            this.btnCancelitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnCancelitem.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcSettings;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(375, 334);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // PluginSettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(377, 378);
            this.Controls.Add(this.SelectUsersFormlayoutControl1ConvertedLayout);
            this.Name = "PluginSettingsForm";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.SelectUsersFormlayoutControl1ConvertedLayout)).EndInit();
            this.SelectUsersFormlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOKitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl SelectUsersFormlayoutControl1ConvertedLayout;
        private DevExpress.XtraGrid.GridControl gcSettings;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSettings;
        private DevExpress.XtraGrid.Columns.GridColumn colPropertyKey;
        private DevExpress.XtraGrid.Columns.GridColumn colPropertyValue;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem btnOKitem;
        private DevExpress.XtraLayout.LayoutControlItem btnCancelitem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}