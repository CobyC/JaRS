using System;

namespace JARS.Core.Interfaces.Plugins
{
    /// <summary>
    /// This interface enables a plugin to be activated or deactivated.
    /// This is where times can be enabled, bindings made or removed etc..
    /// </summary>
    public interface IPluginWithActivate : IPluginBase
    {
        /// <summary>
        /// If true, the plugin will call the execute method as soon as it has loaded.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// The synchronous code to be activated for this plugin, this is where the underlying bindings can be called, times activated etc..
        /// </summary>
        void Activate();

        /// <summary>
        /// The event handler that can be triggered just before activation of the plugin
        /// </summary>
        event EventHandler OnActivate;
        
        /// <summary>
        /// This is the method where all bindings are suspended/removed and times events etc gets unbound.
        /// </summary>
        void Deactivate();
        
        /// <summary>
        /// The event handler that can be used to triggered just before deactivation of the plugin
        /// </summary>
        event EventHandler OnDeactivate;

    }
}
