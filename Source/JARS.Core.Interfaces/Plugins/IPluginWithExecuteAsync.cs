using System.Threading.Tasks;

namespace JARS.Core.Interfaces.Plugins
{
    public interface IPluginWithExecuteAsync : IPluginBase
    {
        /// <summary>
        /// If true, the plugin will call the execute method as soon as it has loaded.
        /// This property must be set from a backing store property value.
        /// </summary>
        bool AutoExecute { get; }

        /// <summary>
        /// The asynchronous code to be executed from this plugin        
        /// </summary>
        Task ExecuteAsync();

        ///// <summary>
        ///// The asynchronous code to be executed from this plugin        
        ///// </summary>
        //Task<T> ExecuteAsync<T>();
    }
}
