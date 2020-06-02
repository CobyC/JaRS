using JARS.Core.Interfaces.Attributes;
using System;
using System.ComponentModel.Composition;

namespace JARS.Core.Attributes
{
    /// <summary>
    /// Its purpose is to help with the export of jars plugins. (using MEF)
    /// This attribute is used to identify plugins with additional metadata using Export attribute (MEF attribute) 
    /// it implements the IPluginMetadata interface which is helpful in identifying the plugins for further use when the Lazy&lt;&gt; function is used.
    /// (Use this attribute to export multiple items under the same contract).    
    /// This Attribute inherits from the default Export (MEF) attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false), MetadataAttribute]
    public class ExportPluginAttribute : ExportAttribute, IPluginMetadata
    {
        public ExportPluginAttribute(Type contractType) : base(contractType)
        {
            PluginText = $"{contractType.Name} No Name";           
        }

        /// <summary>
        /// Supply the values for the plugin by supplying the contract type and the text values for where the plugin will appear in the ribbon.
        /// </summary>
        /// <param name="contractType">The type of contract that tells MEF what to look for, this is required</param>
        /// <param name="pluginText">The name of the plugin, this text will appear as the item text, it will default to the plugin type name if no value supplied.</param>
        public ExportPluginAttribute(Type contractType, string pluginText = "") : this(contractType)
        {
            if (pluginText != null && pluginText != "")
                PluginText = pluginText;           
        }            

        /// <summary>
        /// The text that will appear with the plugin item
        /// </summary>
        public string PluginText { get; private set; }       
    }
}
