using JARS.Core.Data.Interfaces.DataContext;
using NHibernate;
using NHibernate.Cfg;

namespace JARS.Data.NH.Interfaces
{
    /// <summary>
    /// This interface should be implemented by any Data Context interfaces that will use NHibernate.
    /// </summary>
    public interface IDataContextBaseNh : IDataContextBase, IManageDatabase
    {
       
        /// <summary>
        /// The session factory is what is used by NHibernate to communicate with the database.
        /// The session factory is read only and will attempt to call the BuildConfiguration() method.
        /// </summary>
        ISessionFactory SessionFactory { get; }

        /// <summary>
        /// The configuration gets set in the InitializeConnections() implementation.
        /// The session factory uses this configuration.
        /// </summary>
        Configuration CurrentConfig { get; }
    }
}
