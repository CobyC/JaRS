using JARS.Core.Interfaces.Attributes;
using System;
using System.ComponentModel.Composition;

namespace JARS.Core.Attributes
{
    /// <summary>
    /// Its purpose is to help with the export of behaviour plugins. (using MEF)
    /// This attribute is used to identify plugins with additional metadata using Export attribute (MEF attribute) 
    /// it implements the <see cref="IPluginAsBehaviourMetadata"/> interface which is helpful in identifying the plugins for further use.
    /// (Use this attribute to export multiple items under the same contract using the Lazy&lt;&gt; function).    
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false), MetadataAttribute]
    public class ExportPluginAsBehaviour : ExportPluginToMainRibbonAttribute, IPluginAsBehaviourMetadata
    {
        /// <summary>
        /// This will export (MEF) the plugins of the behaviour.        
        /// </summary>
        /// <param name="contractType">The MEF contract type (MEF)</param>        
        /// <param name="pluginText">The text that will appear as the plugin text</param>
        /// <param name="viewOptionCategory">The filter that will be used to select the correct information from the datastore.</param>
        public ExportPluginAsBehaviour(Type contractType, string pluginText = "") : base(contractType, pluginText, "Behaviours", "Home", "")
        {
        }
    }
}
