using JARS.Core.Interfaces.Attributes;
using JARS.Core.Interfaces.Processors;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Engines
{
    /// <summary>
    /// This class is used as a factory class to get an instance of the processor that is requested.
    /// This makes it easier to request a business processor as we need it instead of having to import all the business engines that might only be used in a particular function.
    /// </summary>
    [Export(typeof(IProcessorFactory))]//<- Makes this available to MEF
    [PartCreationPolicy(CreationPolicy.NonShared)]//<- sets it to be non shared (the default is shared)
    public class ProcessorFactory : IProcessorFactory
    {
        public TProcess GetJarsProcessor<TProcess>(string contractName) where TProcess : IProcessor
        {
            return JarsCore.Container.GetExportedValue<TProcess>(contractName);//<- use the core container to get an instance of the exported value
        }
      
    }
}
