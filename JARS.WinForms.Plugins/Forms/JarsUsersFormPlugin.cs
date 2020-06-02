using DevExpress.XtraBars;
using JARS.Core.Attributes;
using JARS.Core.Extensions;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.Security;
using JARS.Core.WinForms.Interfaces.Plugins;
using System.ComponentModel.Composition;
using System.Security.Permissions;

namespace JARS.Win.Plugins
{

    [ExportPluginToMainRibbon(typeof(IPluginBarItemToRibbon), "Jars Users", "Users", "Configure", "Administrator")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JarsUsersFormPlugin : IPluginBarItemToRibbon, IPluginRequiresPermission
    {
        public string[] RequiredRoles => new[] { JarsRoles.Admin };
        public string[] RequiredPermissions => new[] { JarsPermissions.Full };


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
                        Glyph = DevExpress.Images.ImageResourceCache.Default.GetImage("images/business%20objects/bouser_16x16.png"),
                        LargeGlyph = DevExpress.Images.ImageResourceCache.Default.GetImage("images/business%20objects/bouser_32x32.png"),
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
            JarsUsersForm frm = new JarsUsersForm();
            frm.Show();
        }
    }
}
