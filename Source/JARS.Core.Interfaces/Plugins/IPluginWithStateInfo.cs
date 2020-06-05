namespace JARS.Core.Interfaces.Plugins
{
    public interface IPluginWithStateInfo : IPluginBase
    {
        /// <summary>
        /// Get a byte array of information that can be stored for reloading when the plugin is loaded again.
        /// </summary>
        /// <returns>a byte array of information</returns>
        byte[] GetStateInformation();

        /// <summary>
        /// Supply this method with the byte array of information that represents the state the plugin needs to be restored to.
        /// </summary>
        /// <param name="stateInfo">a byte array of information</param>
        void LoadStateInformation(byte[] stateInfo);
    }
}
