using JARS.Core.Interfaces.Attributes.UI;
using System;
using System.ComponentModel.Composition;

namespace JARS.Core.Attributes
{
    /// <summary>
    /// Its purpose is to help with the export of external data panel plugins. (using MEF)
    /// This attribute is used to identify plugins with additional metadata using/inheriting from Export attribute (MEF attribute) 
    /// it implements the <see cref="IPluginToTabControlMetadata"/> interface which is helpful in identifying the plugins for further use.
    /// (Use this attribute to export multiple items under the same contract using the Lazy&lt;&gt; function).    
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false), MetadataAttribute]
    public class ExportPluginToTabControlAttribute : ExportPluginAttribute, IPluginToTabControlMetadata
    {
        /// <summary>
        /// This will create a plugin and use the contract type as the text for the plugin name.
        /// The default Index position will be 0
        /// Use the overloads to specify custom data
        /// </summary>
        /// <param name="contractType"></param>
        public ExportPluginToTabControlAttribute(Type contractType) : base(contractType)
        {
            PositionIndex = 0;
        }

        /// <summary>
        /// Supply the values for the plugin by supplying the contract type and the text values for the plugin.
        /// The PositionIndex value will be set to 0.
        /// </summary>
        /// <param name="contractType">The type of contract that tells MEF what to look for, this is required</param>
        /// <param name="pluginText">The name of the plugin, this text will appear as the item text, it will default to the plugin type name if no value supplied.</param>
        public ExportPluginToTabControlAttribute(Type contractType, string pluginText = "") : base(contractType, pluginText)
        {
            PositionIndex = 0;
        }

        /// <summary>
        /// Supply the values for the plugin by supplying the contract type and the text values for the plugin.
        /// The PositionIndex value will be set to 0.
        /// </summary>
        /// <param name="contractType">The type of contract that tells MEF what to look for, this is required</param>
        /// <param name="pluginText">The name of the plugin, this text will appear as the item text, it will default to the plugin type name if no value supplied.</param>
        /// <param name="autoExecute">Default value is false, if set to true, the execute method will be called as soon as the plugin has loaded.</param>
        public ExportPluginToTabControlAttribute(Type contractType, string pluginText = "", int positionIndex = 0) : base(contractType, pluginText)
        {
            PositionIndex = positionIndex;
        }
        
        /// <summary>
        /// The position the plugin will appear at in the main panel control
        /// </summary>
        public int PositionIndex { get; private set; }
    }
}
