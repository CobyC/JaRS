using JARS.Core.Interfaces.Entities;

namespace JARS.Core.Interfaces.Plugins
{
    /// <summary>
    /// This interface identifies that the plugin is linked to a specific entity.
    /// It has a property for the entity that will be affected by the plugin.
    /// </summary>
    public interface IPluginForEntity<T> : IPluginBase
        where T : IEntityBase
    {
        /// <summary>
        /// The Entity that will be affected by the plugin
        /// </summary>
        T Entity { get; set; }
    }
}
