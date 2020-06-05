using JARS.Core.Attributes;
using JARS.Core.Interfaces.Attributes;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.Security;
using JARS.Core.WinForms.Controls;
using JARS.Core.WinForms.Interfaces.Plugins;
using System.Linq;
using System.Reflection;

namespace JARS.Core.WinForms.Plugins
{
    /// <summary>
    /// This control can be used for setting up controls that will be detected as plugins.
    /// It already implements the UserControlBase and the IPluginWinForms, IPluginRequiresPermission interfaces.
    /// </summary>
    public partial class UserControlBasePlugin : UserControlBase, IPluginWinForms, IPluginRequiresPermission
    {

        public UserControlBasePlugin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Over write this property if the control needs specific roles, the default value is Admin
        /// </summary>
        public virtual string[] RequiredRoles => new string[] { JarsRoles.Admin};

        /// <summary>
        /// Over write this property if the control needs specific permissions, the default value is Full
        /// </summary>
        public virtual string[] RequiredPermissions => new string[] { JarsPermissions.Full };

        /// <summary>
        /// The text that names the plugin.
        /// By default it will look for the name of the plugin by inspecting the plugin attributes
        /// </summary>
        public virtual string PluginText
        {
            get
            {
                IPluginMetadata attribute = this.GetType().GetCustomAttributes<ExportPluginAttribute>(false).FirstOrDefault();
                return (attribute != null) ? attribute.PluginText : this.GetType().Name;
            }
        }


    }
}
