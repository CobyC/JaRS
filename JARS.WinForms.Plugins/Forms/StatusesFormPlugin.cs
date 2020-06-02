using DevExpress.XtraBars;
using JARS.Core.Attributes;
using JARS.Core.Extensions;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.Security;
using JARS.Core.WinForms.Interfaces.Plugins;
using System.ComponentModel.Composition;

namespace JARS.Win.Plugins
{
    [ExportPluginToMainRibbon(typeof(IPluginBarItemToRibbon), "Appointment Statuses", "Properties", "Application", "Configure")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StatusesFormPlugin : IPluginBarItemToRibbon, IPluginRequiresPermission
    {
        public string[] RequiredRoles => JarsRoles.AppConfig;
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
                        Glyph = WinForms.Plugins.Properties.Resources.Status_Settings_16x16,
                        LargeGlyph = WinForms.Plugins.Properties.Resources.Status_Settings_32x32,
                        Id = 501
                    };
                    barItem.ItemClick += BarItem_ItemClick_plg;
                }
                return barItem;
            }
        }

        public string PluginText => this.GetPluginTextFromAttributeValue();

        void BarItem_ItemClick_plg(object sender, ItemClickEventArgs e)
        {
            StatusesForm frm = new StatusesForm();
            frm.Show();
        }

    }
}
