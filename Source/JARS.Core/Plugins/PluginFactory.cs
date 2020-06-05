using JARS.Core.Interfaces.Plugins;
using System;
using System.ComponentModel.Composition;

namespace JARS.Core.Plugins
{
    /// <summary>
    /// This class enables us to request a winform plugin as we need it within a function.
    /// It removes the need to use [Import] in a class if the repository is only used within a function, this frees up system resources.
    /// </summary>
    [Export(typeof(IPluginFactory))]//<-- used by MEF to know what class to instantiate.
    [PartCreationPolicy(CreationPolicy.NonShared)] //<-- sets the creation policy to non shared (single instance) as oppose to singleton (similar to static)
    public class PluginFactory : IPluginFactory
    {
        public TPlugin GetJarsPlugin<TPlugin>() where TPlugin : IPluginBase
        {
            return JarsCore.Container.GetExportedValue<TPlugin>();
        }

        public TPlugin GetJarsPlugin<TPlugin>(string contractName) where TPlugin : IPluginBase
        {
            TPlugin plugin = default(TPlugin);
            try
            {
                plugin = JarsCore.Container.GetExportedValue<TPlugin>(contractName);
            }
            catch (ImportCardinalityMismatchException iEx)
            {
                Logger.Error(iEx.Message, iEx);
#if DEBUG
                throw iEx;
#endif
            }
            catch (CompositionContractMismatchException mEx)
            {
                Logger.Error(mEx.Message);
#if DEBUG
                throw mEx;
#endif
            }
            catch (CompositionException cEx)
            {
                Logger.Error(cEx.Message);
#if DEBUG
                throw cEx;
#endif
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
#if DEBUG
                throw ex;
#endif
            }
            return plugin;
        }
    }
}
