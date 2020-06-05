namespace JARS.Core.Interfaces.Processors
{
    public interface IProcessorFactory
    {
        /// <summary>
        /// Use this method to get business side processors.
        /// </summary>
        /// <typeparam name="T">The Business processor type to get.</typeparam>
        /// <returns>returns an instance of the a business processor if it was found in the MEF catalog, otherwise null</returns>
        //T GetJarsBusinessProcessor<T>() where T : IJarsBusinessProcessor;

        /// <summary>
        /// Use this method to get client side processors.
        /// </summary>
        /// <typeparam name="T">The client processor type to get.</typeparam>
        /// <returns>returns an instance of the a client processor if it was found in the MEF catalog, otherwise null</returns>

        T GetJarsProcessor<T>(string contractName) where T : IProcessor;
       
    }
}
