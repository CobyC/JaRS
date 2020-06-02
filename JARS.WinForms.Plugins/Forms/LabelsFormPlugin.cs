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

    [ExportPluginToMainRibbon(typeof(IPluginBarItemToRibbon), "Appointment Labels", "Properties", "Application", "Configure")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LabelsFormPlugin : IPluginBarItemToRibbon, IPluginRequiresPermission
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
                        Glyph = WinForms.Plugins.Properties.Resources.Label_Settings_16x16,
                        LargeGlyph = WinForms.Plugins.Properties.Resources.Label_Settings_32x32,
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
            LabelsForm frm = new LabelsForm();
            frm.Show();
        }

    }
}
