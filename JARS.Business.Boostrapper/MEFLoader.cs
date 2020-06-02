using JARS.Core.Data.Interfaces.DataContext;
using JARS.Core.Extensions;
using JARS.Core.Interfaces.Repositories;
using JARS.Core.Repositories;
using JARS.Core.Utils;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
using System.Configuration;

namespace JARS.Business.Bootstrap
{
    /// <summary>
    /// This class is used to register all the assemblies, on the business side (server side, data access side), that has been marked as 'Export'
    /// This is to enable dependency injection for the application
    /// it is a static class so that the information only needs to be created once.
    /// This will have to be called as soon as possible when an application start up.
    /// </summary>
    public static class MEFBusinessLoader
    {

        /// <summary>
        /// This method sets up the MEF loader and discovers all the classes that we know of in the business side
        /// </summary>
        /// <returns>returns the filled composition container</returns>
        public static CompositionContainer Init()
        {
            return Init(null);
        }

        /// <summary>
        /// This Method allows for additional catalogs to be added to the main MEF catalog.
        /// This is so that the Client application can have other classes/plugins that needs to be instantiated by MEF.        
        /// </summary>
        /// <param name="catalogParts"></param>
        /// <returns>returns the filled composition container</returns>
        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts)
        {
            AggregateCatalog catalog = new AggregateCatalog();

            if (catalogParts == null)
                catalogParts = new List<ComposablePartCatalog>();


            RegistrationBuilder dataBuilder = new RegistrationBuilder();
            dataBuilder
                .ForTypesDerivedFrom<IDataContextBase>()
                .Export()
                .ExportInterfaces();
            dataBuilder
                .ForTypesDerivedFrom<IDataRepositoryBase>()
                .Export()
                .ExportInterfaces();

            if (ConfigurationManager.AppSettings["UseWebPath"].ToLower() == "true")
            {
                AssemblyCatalog assemCat1 = new AssemblyCatalog(typeof(DataRepositoryFactory).Assembly, dataBuilder);
                assemCat1.FixCatalogForRegistrationBuilderBug();
                catalog.Catalogs.Add(assemCat1);
                /* this worked..
                 AssemblyCatalog assemCat2 = new AssemblyCatalog(typeof(ApptLabelRepository).Assembly, dataBuilder);
                 assemCat2.FixCatalogForRegistrationBuilderBug();
                 catalog.Catalogs.Add(assemCat2);
                  */
                var catDir = new DirectoryCatalog(AssemblyLoaderUtil._executingDirectory, "*.data.*");
                catDir.FixCatalogForRegistrationBuilderBug();
                catalog.Catalogs.Add(catDir);
            }
            else
            {
                AssemblyCatalog assemCat1 = new AssemblyCatalog(typeof(DataRepositoryFactory).Assembly, dataBuilder);
                assemCat1.FixCatalogForRegistrationBuilderBug();
                catalog.Catalogs.Add(assemCat1);

                foreach (var assem in AssemblyLoaderUtil.DataAssemblies)
                {
                    AssemblyCatalog assemCat = new AssemblyCatalog(assem, dataBuilder);
                    assemCat.FixCatalogForRegistrationBuilderBug();
                    catalogParts.Add(assemCat);
                }
            }

            //add items to catalog here
            //we only have to give in any one of the classes in the assembly that we want to load,
            // it will automatically look for any other classes down the chain, marked as 'Export' in that assembly.

            if (catalogParts != null)
                foreach (var part in catalogParts)
                    catalog.Catalogs.Add(part);

            CompositionContainer container = new CompositionContainer(catalog);
            return container;
        }

    }
}
