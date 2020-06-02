using DevExpress.XtraBars;
using JARS.Core.Attributes;
using JARS.Core.Extensions;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.Security;
using JARS.Core.WinForms.Interfaces.Plugins;
using System.ComponentModel.Composition;
using System.Linq;

namespace JARS.Win.Plugins
{

    [ExportPluginToMainRibbon(typeof(IPluginBarItemToRibbon), "Resource Groups", "Mobile Resources", "Application", "Configure")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ResourceGroupsFormPlugin : IPluginBarItemToRibbon, IPluginRequiresPermission
    {
        public string[] RequiredRoles => JarsRoles.AppConfig.Concat(new[] { JarsRoles.PowerUser }).ToArray();
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
                        Glyph = DevExpress.Images.ImageResourceCache.Default.GetImage("images/people/team_16x16.png"),
                        LargeGlyph = DevExpress.Images.ImageResourceCache.Default.GetImage("images/people/team_32x32.png"),
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
            ResourceGroupsForm frm = new ResourceGroupsForm();
            frm.Show();
        }
    }
}
