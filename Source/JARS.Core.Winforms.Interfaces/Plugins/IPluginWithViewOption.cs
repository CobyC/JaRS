namespace JARS.Core.WinForms.Interfaces.Plugins
{
    /// <summary>
    /// Use this interface when creating a plugin that needs to have access to the active view options (usually status and label properties
    /// </summary>
    public interface IPluginWithViewOption
    {
        IPluginAsViewOption ActiveViewOption { get; set; }
    }
}
