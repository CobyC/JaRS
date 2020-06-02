namespace JARS.Core.Interfaces.Plugins
{
    public interface IPluginWithInitialize: IPluginBase
    {
        /// <summary>
        /// Initialize the plugin ie. load settings, bind to events, use this to set up and activate the plugin.
        /// </summary>
        void Init();
    }
}
