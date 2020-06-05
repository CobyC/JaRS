namespace JARS.Core.WinForms.Interfaces.Plugins
{
    /// <summary>
    /// This plugin enables external another plugin to keep track of the currently active view option.
    /// 
    /// </summary>
    public interface IPluginToViewOption
    {
        IPluginAsViewOption ActiveViewOption { get; }
        void SetViewOptionPlugin(IPluginAsViewOption activeViewOption);
    }
}
