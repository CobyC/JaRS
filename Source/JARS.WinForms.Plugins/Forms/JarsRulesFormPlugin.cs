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

    [ExportPluginToMainRibbon(typeof(IPluginBarItemToRibbon), "Entity Rules", "Properties", "Application", "Configure")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JarsRulesFormPlugin : IPluginBarItemToRibbon, IPluginRequiresPermission
    {
        public string[] RequiredRoles => JarsRoles.AppConfig;
        public string[] RequiredPermissions => null;

        //the base implementation will extract the name from the attribute so we can just get the base value
        //unless you want to display something else for the plugin text

        public string PluginText => this.GetPluginTextFromAttributeValue();      

        private BarItem barControl;
        public BarItem BarItem
        {
            get
            {
                if (barControl == null)
                    Init();
                return barControl;
            }           
        }

        public void Init()
        {
            {
                barControl = new BarButtonItem();
                barControl.Caption = PluginText;
                barControl.Glyph = DevExpress.Images.ImageResourceCache.Default.GetImage("images/xaf/bo_rules_16x16.png");
                barControl.LargeGlyph = DevExpress.Images.ImageResourceCache.Default.GetImage("images/xaf/bo_rules_32x32.png");
                barControl.Name = $"barItm{GetType().Name}";
                barControl.ItemClick += BarControl_ItemClick;
                barControl.Id = 903;
            }
        }

        public void BarControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            JarsRulesForm form = new JarsRulesForm();
            form.ShowDialog();
        }
    }
}
