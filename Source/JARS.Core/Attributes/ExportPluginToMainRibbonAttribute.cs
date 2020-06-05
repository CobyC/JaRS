using JARS.Core.Interfaces.Attributes.UI;
using System;
using System.ComponentModel.Composition;

namespace JARS.Core.Attributes
{

    /// <summary>
    /// Its purpose is to help with the export of main ribbon plugins. (using MEF)
    /// This attribute is used to identify plugins with additional metadata using/inheriting from Export attribute (MEF attribute) 
    /// it implements the <see cref="IPluginToMainRibbonMetadata"/> interface which is helpful in identifying the plugins for further use.
    /// (Use this attribute to export multiple items under the same contract using the Lazy&lt;&gt; function).    
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false), MetadataAttribute]
    public class ExportPluginToMainRibbonAttribute : ExportPluginAttribute, IPluginToMainRibbonMetadata
    {
        /// <summary>
        /// This will create a plugin and place it in the Plugins Group, In the Plugins Page, on the Plugins Category with the text as the plugin type name'
        /// Use the overloads to specify custom data
        /// </summary>
        /// <param name="contractType"></param>
        public ExportPluginToMainRibbonAttribute(Type contractType) : base(contractType)
        {
            PageGroup = "Plugins";
            CategoryPage = "Plugins";
            Category = "Plugins";
        }

        /// <summary>
        /// Supply the values for the plugin by supplying the contract type and the text values for where the plugin will appear in the ribbon.
        /// </summary>
        /// <param name="contractType">The type of contract that tells MEF what to look for, this is required</param>
        /// <param name="pluginText">The name of the plugin, this text will appear as the item text, it will default to the plugin type name if no value supplied.</param>
        public ExportPluginToMainRibbonAttribute(Type contractType, string pluginText = "") : base(contractType, pluginText)
        {
            PageGroup = "Plugins";
            CategoryPage = "Plugins";
            Category = "Plugins";
        }

        /// <summary>
        /// Supply the values for the plugin by supplying the contract type and the text values for where the plugin will appear in the ribbon.
        /// </summary>
        /// <param name="contractType">The type of contract that tells MEF what to look for, this is required</param>
        /// <param name="pluginText">The name of the plugin, this text will appear as the item text, it will default to the plugin type name if no value supplied.</param>
        /// <param name="category">The category the item will fall under, will default to "Plugins Category" if not supplied </param>
        public ExportPluginToMainRibbonAttribute(Type contractType, string pluginText = "", string pageGroup = "") : this(contractType, pluginText)
        {
            if (pageGroup != null && pageGroup != "")
                PageGroup = pageGroup;


        }

        /// <summary>
        /// Supply the values for the plugin by supplying the contract type and the text values for where the plugin will appear in the ribbon.
        /// </summary>
        /// <param name="contractType">The type of contract that tells MEF what to look for, this is required</param>
        /// <param name="pluginText">The name of the plugin, this text will appear as the item text, it will default to the plugin type name if no value supplied.</param>
        /// <param name="category">The category the item will fall under, will default to "Plugins Category" if not supplied </param>
        /// <param name="categoryPage">The category page the item will fall under, will default to "Plugins Page" if not supplied </param>
        public ExportPluginToMainRibbonAttribute(Type contractType, string pluginText = "", string pageGroup = "", string categoryPage = "") : this(contractType, pluginText, pageGroup)
        {
            if (categoryPage != null && categoryPage != "")
                CategoryPage = categoryPage;
        }

        /// <summary>
        /// Supply the values for the plugin by supplying the contract type and the text values for where the plugin will appear in the ribbon.
        /// </summary>
        /// <param name="contractType">The type of contract that tells MEF what to look for, this is required</param>
        /// <param name="pluginText">The name of the plugin, this text will appear as the item text, it will default to the plugin type name if no value supplied.</param>
        /// <param name="category">The category the item will fall under, will default to "Plugins Category" if not supplied </param>
        /// <param name="categoryPage">The category page the item will fall under, will default to "Plugins Page" if not supplied </param>
        /// <param name="pageGroup">The group the item will fall under, will default to "Plugins Group" if not supplied </param>
        public ExportPluginToMainRibbonAttribute(Type contractType, string pluginText = "", string pageGroup = "", string categoryPage = "", string category = "Plugins") : this(contractType, pluginText, pageGroup, categoryPage)
        {
            if (category != null)
                Category = category;
        }

        /// <summary>
        /// The category the plugin will appear in on the ribbon control
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// The page in the category the plugin will appear in.
        /// </summary>
        public string CategoryPage { get; private set; }

        /// <summary>
        /// The group in the category page, the plugin will appear in.
        /// </summary>
        public string PageGroup { get; private set; }
        
    }
}
