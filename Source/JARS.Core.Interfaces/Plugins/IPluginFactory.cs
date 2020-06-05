namespace JARS.Core.Interfaces.Plugins
{
    public interface IPluginFactory
    {
        /// <summary>
        /// Get a plugin represented by T, any plugin that inherits from IJarsPluginBase."/>.
        /// </summary>
        /// <typeparam name="T">Any plugin that implements the IJarsPluginBase interface</typeparam>
        /// <returns></returns>
        T GetJarsPlugin<T>() where T : IPluginBase;

        T GetJarsPlugin<T>(string contractName) where T : IPluginBase;
    }
}
