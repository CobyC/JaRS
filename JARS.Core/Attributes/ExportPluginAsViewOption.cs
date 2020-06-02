using JARS.Core.Interfaces.Attributes;
using System;
using System.ComponentModel.Composition;

namespace JARS.Core.Attributes
{
    /// <summary>
    /// Its purpose is to help with the export of view option plugins. (using MEF)
    /// This attribute is used to identify plugins with additional metadata using Export attribute (MEF attribute) 
    /// it implements the <see cref="IPluginAsViewOptionMetadata"/> interface which is helpful in identifying the plugins for further use.
    /// (Use this attribute to export multiple items under the same contract using the Lazy&lt;&gt; function).    
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false), MetadataAttribute]
    public class ExportPluginAsViewOption : ExportPluginToMainRibbonAttribute, IPluginAsViewOptionMetadata
    {

        /// <summary>
        /// This will export (MEF) the plugins of the View Option type.
        /// The ViewOptionFilter is the value of the ViewType on for appointments statuses and appointment labels.
        /// </summary>
        /// <param name="contractType">The MEF contract type (MEF)</param>
        /// <param name="applyToEntityInterfaceType">The type of interface that the entity bound to a view option must implement to use this view option</param>
        /// <param name="pluginText">The text that will appear as the plugin text</param>
        /// <param name="viewOptionCategory">The filter that will be used to select the correct information from the datastore.</param>
        public ExportPluginAsViewOption(Type contractType, Type applyToEntityInterfaceType, string pluginText = "", string viewOptionCategory = "DEFAULT") : base(contractType, pluginText, "View Options", "Home", "")
        {
            _ViewOptionCategory = "DEFAULT";
            _ApplyToEntityInterfaceType = applyToEntityInterfaceType;
        }

        string _ViewOptionCategory;
        public string ViewOptionCategory
        {
            get
            {
                return _ViewOptionCategory;
            }
        }

        Type _ApplyToEntityInterfaceType;
        /// <summary>
        /// Gets the interface type that this view is bound to.
        /// ie if an entity implements the IStatusLabeled interface, then this view option can be applied. 
        /// </summary>
        public Type ApplyToEntityInterfaceType
        {
            get
            {
                return _ApplyToEntityInterfaceType;
            }
        }

    }
}
