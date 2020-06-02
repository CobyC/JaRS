using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using JARS.BOS.Entities.Maps;
using JARS.Core;
using JARS.Data.NH.Context;
using NHibernate;
using System;
using System.ComponentModel.Composition;
using System.Configuration;

namespace JARS.BOS.Data
{

    [Export(typeof(IDataContextBOS))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DataContextBOS : DataContextBaseNh, IDataContextBOS
    {

        public override string ConnectionString
        {
            get
            {
                base.ConnectionString = ConfigurationManager.ConnectionStrings["BOSDb"].ConnectionString;
                return base.ConnectionString;
            }
        }

        public override object InitializeConnections()
        {
            try
            {
                return Fluently.Configure() //<- with this you don't have to set up nHibernate in the web.config file.
                       .Database(MsSqlConfiguration.MsSql2012
                            .ConnectionString(ConnectionString)
                            .ShowSql)
                       .Mappings(m =>
                       {
                           m.FluentMappings.Add<BOSEntityMap>();//<- this could be changed to use the assembly, but as this is the only one we need and both
                                                                // internal and external mappings are in one assembly, it is specified explicitely
                       }).BuildConfiguration();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
#if DEBUG
                throw ex;
#else
                return null;
#endif
            }
        }

        public override void PopulateDefaultData()
        {
            using (ISession s = SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    //default code goes here..
                    t.Commit();
                }
            }
        }
    }

//    [Export(typeof(IDataContextExternalBOS))]
//    [PartCreationPolicy(CreationPolicy.Shared)]
//    public class DataContextExternalBOS : DataContextBaseNh, IDataContextExternalBOS
//    {

//        public override string ConnectionString
//        {
//            get
//            {
//                base.ConnectionString = Properties.Settings.Default.BOSExternalDb;
//                return base.ConnectionString;
//            }
//        }

//        public override object InitializeConnections()
//        {
//            try
//            {
//                return Fluently.Configure() //<- with this you don't have to set up nHibernate in the web.config file.
//                       .Database(MsSqlConfiguration.MsSql2012
//                            .ConnectionString(ConnectionString)
//                            .ShowSql)
//                       .Mappings(m =>
//                       {
//                           m.FluentMappings.Add<BOSExternalEntityMap>();
//                       }).BuildConfiguration();
//            }
//            catch (Exception ex)
//            {
//                Logger.Error(ex.Message, ex);
//#if DEBUG
//                throw ex;
//#else
//                return null;
//#endif
//            }
//        }

//        public override void PopulateDefaultData()
//        {
//            using (ISession s = SessionFactory.OpenSession())
//            {
//                using (ITransaction t = s.BeginTransaction())
//                {
//                    //default code goes here..
//                    t.Commit();
//                }
//            }
//        }
//    }
}

