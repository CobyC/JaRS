namespace JARS.Core.Data.Interfaces.DataContext
{
    /// <summary>
    /// The most basic requirements for a data context to be used.
    /// </summary>
    public interface IDataContextBase
    {
        /// <summary>
        /// The connection string is required for the DataContext to work.
        /// over write the connection string in the implementing class to avoid exceptions 
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// The default database connection setup function.
        /// Call this to establish the connection to the database/s
        /// </summary>
        object InitializeConnections();

        /// <summary>
        /// The default database destruction function
        /// Use this to destroy or close down any connections to the database.
        /// </summary>
        void DestroyConnections();
    }
}