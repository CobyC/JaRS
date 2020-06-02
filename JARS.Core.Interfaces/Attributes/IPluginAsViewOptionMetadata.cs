using JARS.Core.Interfaces.Attributes.UI;
using System;

namespace JARS.Core.Interfaces.Attributes
{
    /// <summary>
    /// This interface describes the metadata required for a plugin that acts as a view option plugin.
    /// This is useful with MEF (Lazy&lt;gt&; method)
    /// </summary>
    public interface IPluginAsViewOptionMetadata : IPluginToMainRibbonMetadata
    {
        string ViewOptionCategory { get; }
        Type ApplyToEntityInterfaceType { get; }
    }
}
