namespace JARS.Core.Interfaces.Attributes.UI
{
    /// <summary>
    /// This interface describes the metadata for a plugin that plugs into the Main ribbon control.
    /// It specifies what category, page and group the plugin will appear in.    
    /// This is useful with MEF (Lazy&lt;gt&; method)
    /// </summary>
    public interface IPluginToMainRibbonMetadata : IPluginMetadata
    {
        /// <summary>
        /// The Top level of the ribbon category (default value will be Plugins if no value supplied)
        /// </summary>
        string Category { get; }

        /// <summary>
        /// The page within the category (the second level or the ribbon page, will default to Plugin Page if no value supplied)
        /// </summary>
        string CategoryPage { get; }

        /// <summary>
        /// The 3rd level down, the group within the page. (will default to Plugin Group if no value supplied)
        /// </summary>
        string PageGroup { get; }

    }
}
