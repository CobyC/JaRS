using JARS.Core.Extensions;
using JARS.Core.Plugins;
using JARS.Core.Utils;
using JARS.Core.WinForms.Interfaces.Plugins;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;

namespace JARS.Client.Bootstrap
{
    /// <summary>
    /// This class uses MEF to satisfy the Client side plugins.
    /// This is to enable dependency injection for the application
    /// it is a static class so that the information only needs to be created once.
    /// This will have to be called as soon as possible when an application start up.
    /// </summary>
    public static class MEFClientLoader
    {
        /// <summary>
        /// This method sets up the MEF loader and discovers all the classes that we know of in the client side
        /// </summary>
        /// <returns></returns>
        public static CompositionContainer Init()
        {
            List<ComposablePartCatalog> parts = new List<ComposablePartCatalog>();
            return Init(parts);
        }

        /// <summary>
        /// This Method allows for additional catalogs to be added to the main MEF catalog.
        /// This is so that the Client application can have other classes/plugins that needs to be instantiated by MEF.        
        /// </summary>
        /// <param name="catalogParts"></param>
        /// <returns></returns>
        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts)
        {
            AggregateCatalog catalog = new AggregateCatalog();

            RegistrationBuilder pluginBuilder = new RegistrationBuilder();
            pluginBuilder.ForTypesDerivedFrom<IPluginWinForms>()
                .Export()
                .ExportInterfaces();

            foreach (var assembly in AssemblyLoaderUtil.WinFormsPlugins)
            {
                AssemblyCatalog assemCatalog = new AssemblyCatalog(assembly, pluginBuilder);
                assemCatalog.FixCatalogForRegistrationBuilderBug();
                catalogParts.Add(assemCatalog);
            }
            //The plugin factory assembly
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(PluginFactory).Assembly));

            if (catalogParts != null)
                foreach (var part in catalogParts)
                    catalog.Catalogs.Add(part);

            CompositionContainer container = new CompositionContainer(catalog);
            return container;
        }
    }
}
