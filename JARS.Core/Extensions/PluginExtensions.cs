using JARS.Core.Attributes;
using JARS.Core.Interfaces.Attributes;
using JARS.Core.Interfaces.Plugins;
using System.Linq;
using System.Reflection;

namespace JARS.Core.Extensions
{
    /// <summary>
    /// This class holds any extension methods that extends the functionality of the JaRS plugins
    /// </summary>
    public static class PluginExtensions
    {
        /// <summary>
        /// If the class implements the ExportJarsPlugin (or inherited from) attribute, this function will extract the name of the plugin given in the Attribute.
        /// </summary>
        /// <returns></returns>
        public static string GetPluginTextFromAttributeValue(this IPluginBase plugin)
        {
            IPluginMetadata attribute = plugin.GetType().GetCustomAttributes<ExportPluginAttribute>(false).FirstOrDefault();
            return (attribute != null) ? attribute.PluginText : plugin.GetType().Name;
        }

        /// <summary>
        /// Remove all the whitespace characters so the PluginText property so it can be used as a name on a control or key in a dictionary.
        /// </summary>
        /// <param name="plugin"></param>
        /// <returns></returns>
        public static string GetPluginTextAsNameValue(this IPluginBase plugin)
        {
            return plugin.PluginText.Replace(" ", "");
        }

        //public static Type GetLinkedInterfaceFromAttributeValue(this IJarsPlugin plugin)
        //{

        //}
    }
}
