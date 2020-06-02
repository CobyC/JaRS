using System.Collections.Generic;

namespace JARS.Core.Interfaces.Plugins
{
    public interface IPluginWithSettings
    {
        Dictionary<string, object> PluginSettings { get; set; }
       
    }
}
