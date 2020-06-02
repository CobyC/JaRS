using DevExpress.XtraBars;
using JARS.Core.Attributes;
using JARS.Core.Security;
using JARS.Core.Extensions;
using JARS.Core.WinForms.Interfaces.Plugins;
using System;
using System.ComponentModel.Composition;
using System.Security.Permissions;
using System.Threading.Tasks;
using JARS.Core.Interfaces.Plugins;

namespace JARS.Win.Plugins
{

    [ExportPluginToMainRibbon(typeof(IPluginBarItemToRibbon), "Send Chat", "Chat", "Messages", "Communication")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MessagingFormPlugin : IPluginBarItemToRibbon, IPluginRequiresPermission
    {
        public string[] RequiredRoles => new[] { JarsRoles.Admin, JarsRoles.PowerUser, JarsRoles.Manager, JarsRoles.User };
        public string[] RequiredPermissions => null;

        private BarButtonItem barItem;

        public BarItem BarItem
        {
            get
            {
                if (barItem == null)
                {
                    barItem = new BarButtonItem
                    {
                        Caption = PluginText,
                        Glyph = DevExpress.Images.ImageResourceCache.Default.GetImage("images/miscellaneous/comment_16x16.png"),
                        LargeGlyph = DevExpress.Images.ImageResourceCache.Default.GetImage("images/miscellaneous/comment_32x32.png"),
                        Id = 501
                    };
                    barItem.ItemClick += BarItem_ItemClick_plg;
                }
                return barItem;
            }
        }
        public string PluginText => this.GetPluginTextFromAttributeValue();

        private void BarItem_ItemClick_plg(object sender, ItemClickEventArgs e)
        {
            MessagingForm frm = new MessagingForm();
            frm.Show();
        }
    }
}
