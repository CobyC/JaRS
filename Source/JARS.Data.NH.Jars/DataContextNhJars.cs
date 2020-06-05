using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using JARS.Core;
using JARS.Core.Utils;
using JARS.Data.NH.Context;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Entities.Maps;
using NHibernate;
using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Reflection;

namespace JARS.Data.NH.Jars
{
    /// <summary>
    /// This DataContext is the main JaRS data context, it uses the default database (as set up in the config file) and configuration.
    /// For MEF: this has the CreationPolicy.Shared, making it a singleton. (MEF will handle disposal)
    /// </summary>
    [Export(typeof(IDataContextNhJars))]
    [PartCreationPolicy(CreationPolicy.Shared)]//we want a single instance of the context, otherwise the context will be created every time, its quite expensive.
    public class DataContextNhJars : DataContextBaseNh, IDataContextNhJars
    {
        public override string ConnectionString
        {
            get
            {
                base.ConnectionString = ConfigurationManager.ConnectionStrings["JaRSDatabase"].ConnectionString;
                return base.ConnectionString;
            }
        }

        public override object InitializeConnections()
        {
            //to enable us to add external assemblies, we need to check if the assembly exists, if it does we load it.
            //Assembly extSysAssembly = null;
            //string executingPath = Assembly.GetExecutingAssembly().Location;
            //string executingDirectory = Path.GetDirectoryName(executingPath);
            //the additional assembly will be specified in the config file.
            //this way we don't need to add the reference to this DataContext project.
            //string assemblyPath = Path.Combine(executingDirectory, ConfigurationManager.AppSettings["ExtSysAssembly"]);

            //if (File.Exists(assemblyPath))
            //    extSysAssembly = Assembly.LoadFile(assemblyPath);

            try
            {
                return Fluently.Configure() //<- with this you don't have to set up nHibernate in the web.config file.
                           .Database(MsSqlConfiguration.MsSql2012
                                .ConnectionString(ConnectionString)
                                .ShowSql)
                           .Mappings(m =>
                           {    //check that the external entity is not null
                               foreach (Assembly extAssembly in AssemblyLoaderUtil.ServiceStackAssemblies)
                               {
                                   m.FluentMappings.AddFromAssembly(extAssembly);
                               }

                               m.FluentMappings.AddFromAssemblyOf<JobBaseMap>()
                               //.Conventions
                               //.Add<JARS.Data.Context.NH.HiLoGeneratorConvention>() //<- set the HiLo convention
                               .Conventions
                               .Add(PrimaryKey.Name.Is(x => "Id"), ForeignKey.EndsWith("Id"));
                           }).BuildConfiguration();//<- set the default ID convention

                //this is required for the hi lo values to look at the correct value in the HiLo IDs table 
                //JARS.Data.Context.NH.HiLoGeneratorConvention.CreateHighLowScript(_currentConfig);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);

                throw ex;

            }
        }

        public override void PopulateDefaultData()
        {
            //create groups and users

            using (ISession s = SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {

                }
            }
        }

    }
}
