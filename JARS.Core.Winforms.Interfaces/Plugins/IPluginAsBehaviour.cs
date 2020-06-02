using JARS.Core.Interfaces.Plugins;

namespace JARS.Core.WinForms.Interfaces.Plugins
{
    /// <summary>
    /// This interface is used to filter out plugins that will be used as behaviour plugins.
    /// It implements access to the main ribbon via the <see cref="IPluginBarItemToRibbon"/>, and requires an activate and deactivate methods from the <see cref="IPluginWithActivate"/>.
    /// It also implements the <see cref="IPluginWithStateInfo"/> so that the state and settings (<see cref="IPluginWithSettings"/>) of the plugin can be saved.
    /// </summary>
    public interface IPluginAsBehaviour : 
        IPluginBarItemToRibbon,
        IPluginWithActivate,
        IPluginWithStateInfo,
        IPluginWithSettings
    {      
    }
}
