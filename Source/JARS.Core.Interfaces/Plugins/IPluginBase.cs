namespace JARS.Core.Interfaces.Plugins
{
    /// <summary>
    /// This is the interface used for identifying a plugin in JaRS.
    /// </summary>
    public interface IPluginBase
    {
        /// <summary>
        /// The text that will be shown for the plugin (this could be form title, ribbon text, button text).
        /// </summary>
        string PluginText { get; }      
        
    }
}
