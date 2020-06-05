namespace JARS.Core.Interfaces.Attributes
{
    /// <summary>
    /// This Interface describes the basic metadata that is required for a Jars plugin
    /// this is usually part of an inheritance chain.
    /// This is useful with MEF (Lazy&lt;gt&; method)
    /// </summary>
    public interface IPluginMetadata
    {
        /// <summary>
        /// The text that will be shown for the plugin (this could be form title, ribbon text, button text, rule name etc..)
        /// </summary>
        string PluginText { get; }        
    }
}
