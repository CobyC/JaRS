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
    [ExportPluginToMainRibbon(typeof(IPluginBarItemToRibbon), "Default Appointments", "Properties", "Configure", "Settings")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JarsDefaultAppointmentsFormPlugin : IPluginBarItemToRibbon, IPluginRequiresPermission
    {
        public string[] RequiredRoles => JarsRoles.Internal;
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
                        Glyph = WinForms.Plugins.Properties.Resources.Appointment_16x16,
                        LargeGlyph = WinForms.Plugins.Properties.Resources.Appointment_32x32,
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
            JarsDefaultAppointmentForm frm = new JarsDefaultAppointmentForm();
            frm.Show();
        }
    }
}
