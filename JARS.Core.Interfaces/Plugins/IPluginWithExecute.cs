namespace JARS.Core.Interfaces.Plugins
{
    public interface IPluginWithExecute : IPluginBase
    {
        /// <summary>
        /// If true, the plugin will call the execute method as soon as it has loaded.
        /// </summary>
        bool AutoExecute { get; }

        /// <summary>
        /// The synchronous code to be executed from this plugin, this is where the plugin gets executed.
        /// </summary>
        void Execute();

    }
}
