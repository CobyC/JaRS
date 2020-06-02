using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.WinForms.Interfaces.Processors
{
    public interface IProcessorWithSettings
    {
        /// <summary>
        /// Dictionary object that contains settings related to a processor
        /// </summary>
        Dictionary<string, object> ProcessorSettings { get; set; }
    }
}
