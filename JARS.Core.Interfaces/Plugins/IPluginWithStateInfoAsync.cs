using System.Threading.Tasks;

namespace JARS.Core.Interfaces.Plugins
{
    public interface IPluginWithStateInfoAsync : IPluginBase
    {
        /// <summary>
        /// Get a byte array of information that can be stored for reloading when the plugin is loaded again.
        /// </summary>
        /// <returns>a byte array of information</returns>
        Task<byte[]> GetStateInformationAsync();

        /// <summary>
        /// Supply this method with the byte array of information that represents the state the plugin needs to be restored to.
        /// </summary>
        /// <param name="stateInfo">a byte array of information</param>
        Task LoadStateInformationAsync(byte[] stateInfo);
    }
}
