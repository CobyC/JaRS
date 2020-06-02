namespace JARS.Core.WinForms.Controls
{
    partial class UserControlBase
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
            this.ucRolesAndPermsToolTip = new DevExpress.Utils.ToolTipController();
            this.SuspendLayout();
            // 
            // ucRolesAndPermsToolTip
            // 
            this.ucRolesAndPermsToolTip.AllowHtmlText = true;
            this.ucRolesAndPermsToolTip.AutoPopDelay = 2000;
            this.ucRolesAndPermsToolTip.InitialDelay = 200;
            this.ucRolesAndPermsToolTip.KeepWhileHovered = true;
            // 
            // UserControlBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UserControlBase";
            this.Size = new System.Drawing.Size(209, 219);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ToolTipController ucRolesAndPermsToolTip;
    }
}
