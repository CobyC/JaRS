namespace JARS.Win.Plugins
{
    partial class MessagingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessagingForm));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.rpHome = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgHome = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.tabCtrlMessages = new DevExpress.XtraTab.XtraTabControl();
            this.tabOnlineUsers = new DevExpress.XtraTab.XtraTabPage();
            this.tabHistory = new DevExpress.XtraTab.XtraTabPage();
            this.barBtnNewMessage = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.gridControlActiveUsers = new DevExpress.XtraGrid.GridControl();
            this.gridViewActiveUser = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barBtnGlobalMsg = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabCtrlMessages)).BeginInit();
            this.tabCtrlMessages.SuspendLayout();
            this.tabOnlineUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlActiveUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewActiveUser)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.barBtnNewMessage,
            this.barButtonItem1,
            this.barBtnGlobalMsg});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 4;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpHome});
            this.ribbonControl1.Size = new System.Drawing.Size(890, 143);
            // 
            // rpHome
            // 
            this.rpHome.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgHome});
            this.rpHome.Name = "rpHome";
            this.rpHome.Text = "Home";
            // 
            // rpgHome
            // 
            this.rpgHome.ItemLinks.Add(this.barBtnNewMessage);
            this.rpgHome.ItemLinks.Add(this.barBtnGlobalMsg);
            this.rpgHome.ItemLinks.Add(this.barButtonItem1);
            this.rpgHome.Name = "rpgHome";
            this.rpgHome.Text = "Home";
            // 
            // tabCtrlMessages
            // 
            this.tabCtrlMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlMessages.Location = new System.Drawing.Point(0, 143);
            this.tabCtrlMessages.Name = "tabCtrlMessages";
            this.tabCtrlMessages.SelectedTabPage = this.tabOnlineUsers;
            this.tabCtrlMessages.Size = new System.Drawing.Size(890, 379);
            this.tabCtrlMessages.TabIndex = 1;
            this.tabCtrlMessages.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabOnlineUsers,
            this.tabHistory});
            // 
            // tabOnlineUsers
            // 
            this.tabOnlineUsers.Controls.Add(this.gridControlActiveUsers);
            this.tabOnlineUsers.Name = "tabOnlineUsers";
            this.tabOnlineUsers.Size = new System.Drawing.Size(884, 351);
            this.tabOnlineUsers.Text = "Active Users";
            // 
            // tabHistory
            // 
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Size = new System.Drawing.Size(884, 351);
            this.tabHistory.Text = "History";
            // 
            // barBtnNewMessage
            // 
            this.barBtnNewMessage.Caption = "New Message";
            this.barBtnNewMessage.Id = 1;
            this.barBtnNewMessage.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnNewMessage.ImageOptions.Image")));
            this.barBtnNewMessage.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnNewMessage.ImageOptions.LargeImage")));
            this.barBtnNewMessage.Name = "barBtnNewMessage";
            toolTipTitleItem1.Text = "Send message.";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "This will open a box where text can be entered.\r\nText will be sent to all the act" +
    "ive users selected in the Active Users tab.\r\n";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.barBtnNewMessage.SuperTip = superToolTip1;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "History";
            this.barButtonItem1.Id = 2;
            this.barButtonItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.barButtonItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // gridControlActiveUsers
            // 
            this.gridControlActiveUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlActiveUsers.Location = new System.Drawing.Point(0, 0);
            this.gridControlActiveUsers.MainView = this.gridViewActiveUser;
            this.gridControlActiveUsers.MenuManager = this.ribbonControl1;
            this.gridControlActiveUsers.Name = "gridControlActiveUsers";
            this.gridControlActiveUsers.Size = new System.Drawing.Size(884, 351);
            this.gridControlActiveUsers.TabIndex = 0;
            this.gridControlActiveUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewActiveUser});
            // 
            // gridViewActiveUser
            // 
            this.gridViewActiveUser.GridControl = this.gridControlActiveUsers;
            this.gridViewActiveUser.Name = "gridViewActiveUser";
            this.gridViewActiveUser.OptionsSelection.MultiSelect = true;
            this.gridViewActiveUser.OptionsView.ShowGroupPanel = false;
            // 
            // barBtnGlobalMsg
            // 
            this.barBtnGlobalMsg.Caption = "Global Message";
            this.barBtnGlobalMsg.Id = 3;
            this.barBtnGlobalMsg.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnGlobalMsg.ImageOptions.Image")));
            this.barBtnGlobalMsg.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnGlobalMsg.ImageOptions.LargeImage")));
            this.barBtnGlobalMsg.Name = "barBtnGlobalMsg";
            toolTipTitleItem2.Text = "Send Global Message";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "This will send a global message to all active users.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.barBtnGlobalMsg.SuperTip = superToolTip2;
            this.barBtnGlobalMsg.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnGlobalMsg_ItemClick);
            // 
            // MessagingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 522);
            this.Controls.Add(this.tabCtrlMessages);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "MessagingForm";
            this.Ribbon = this.ribbonControl1;
            this.Text = "MessagingForm";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabCtrlMessages)).EndInit();
            this.tabCtrlMessages.ResumeLayout(false);
            this.tabOnlineUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlActiveUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewActiveUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem barBtnNewMessage;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barBtnGlobalMsg;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpHome;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgHome;
        private DevExpress.XtraTab.XtraTabControl tabCtrlMessages;
        private DevExpress.XtraTab.XtraTabPage tabOnlineUsers;
        private DevExpress.XtraGrid.GridControl gridControlActiveUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewActiveUser;
        private DevExpress.XtraTab.XtraTabPage tabHistory;
    }
}