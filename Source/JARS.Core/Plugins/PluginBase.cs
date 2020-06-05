using JARS.Core.Attributes;
using JARS.Core.Interfaces.Attributes;
using JARS.Core.Interfaces.Plugins;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JARS.Core.Plugins
{
    /// <summary>
    /// This is the base abstract class representing a JarsPlugin.
    /// It contains the minimal information for a plugin that can be used in JaRS.
    /// All properties and methods are overridable. 
    /// All methods will throw a NotImplementedException if it does not get overridden by the implementing class/plugin.
    /// </summary>
    public abstract class PluginBase : IPluginBase
    {
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
