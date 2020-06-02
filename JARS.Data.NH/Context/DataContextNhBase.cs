using JARS.Core;
using JARS.Data.NH.Interfaces;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;

namespace JARS.Data.NH.Context
{
    public abstract class DataContextBaseNh : IDataContextBaseNh
    {
        private static object lockObject = new object(); //<-- the session factory needs to be set up only once so we use this to apply a lock around the creation.
        private ISessionFactory _SessionFactory;
        public ISessionFactory SessionFactory
        {
            get
            {
                lock (lockObject)//<-- the lock lets the first thread come in and create the static session factory. The second thread needs to wait until the first is leaving the lock scope
                {
                    try
                    {
                        if (_SessionFactory == null)
                        {
                            _SessionFactory = CurrentConfig.BuildSessionFactory();
                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Logger.Error(ex.Message, ex);
#if DEBUG
                        throw ex;
#endif
                    }
                    return _SessionFactory;
                }
            }
        }
        /// <summary>
        /// This property needs to be over written by the implementing class, otherwise it will use the default JaRS connection properties.
        /// This connection string is set in the the BuildConfiguration(...) method.
        /// </summary>
        public virtual string ConnectionString { get; set; }

        internal NHibernate.Cfg.Configuration _CurrentConfig;
        /// <summary>
        /// The configuration gets set in the InitializeConnections() implementation.
        /// The session factory uses this configuration.
        /// </summary>
        public NHibernate.Cfg.Configuration CurrentConfig
        {
            get
            {
                if (_CurrentConfig == null)
                    _CurrentConfig = InitializeConnections() as NHibernate.Cfg.Configuration;
                //BuildConfiguration();
                return _CurrentConfig;
            }
            set
            {
                _CurrentConfig = value;
            }
        }


        /// <summary>
        /// Builds (sets up) the configuration and session factory (the object that specifies what classes are mapped, and can be persisted to the database)
        /// This information is only set up once, or if the factory was destroyed (web app closed or web session timed out)
        /// </summary>        
        public virtual object InitializeConnections()
        {
            throw new NotImplementedException("This Method must be over written with the override keyword in the implementing class.");
        }

        /// <summary>
        /// Closes the session factory and destroys all connections
        /// </summary>
        public void DestroyConnections()
        {
            //Close the connections via the session factory
            SessionFactory.Close();
            _SessionFactory = null;
        }

        /// <summary>
        /// WARNING!!! This will drop the database and all the information in it.
        /// </summary>
        public void DropDatabaseTables()
        {
            try
            {
                SchemaExport export = new SchemaExport(CurrentConfig);
                export.Drop(true, true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
#if DEBUG
                throw ex;
#endif
            }
        }

        /// <summary>
        /// This will attempt to update the database tables with corresponding mappings that might have changed.
        /// It will only update tables that can be altered! (so changes are not guaranteed)
        /// </summary>
        public void UpdateDatabaseTableSchemas()
        {
            try
            {
                SchemaUpdate update = new SchemaUpdate(CurrentConfig);
                update.Execute(true, true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
#if DEBUG
                throw ex;
#endif
            }
        }

        /// <summary>
        /// This will build all the database tables from the available mappings.
        /// This function should only be used when the database is new and does not contain any tables yet.
        /// </summary>
        public void CreateDatabaseTableSchemas()
        {
            try
            {
                SchemaExport export = new SchemaExport(CurrentConfig);
                export.Create(true, true);

            }
            catch (Exception ex)
            {

                Logger.Error(ex.Message, ex);
#if DEBUG
                throw ex;
#endif
            }
        }

        /// <summary>
        /// This method is used to populate the database with default information.
        /// It needs to be overwritten in the implementing class.
        /// </summary>
        public virtual void PopulateDefaultData()
        {
            throw new NotImplementedException("This Method must be over written with the override keyword in the implementing class.");
        }
    }
}
