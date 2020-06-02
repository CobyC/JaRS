using JARS.Core.Interfaces.Attributes.UI;

namespace JARS.Core.Interfaces.Attributes
{
    /// <summary>
    /// This interface describes the metadata for a plugin that acts as a behaviour option plugin.
    /// This is useful with MEF (Lazy&lt;gt&; method)
    /// This interface is used for identification of the plugin.
    /// </summary>
    public interface IPluginAsBehaviourMetadata : IPluginToMainRibbonMetadata
    {
       //behaviours are things like show available dates, highlight double bookings, show travel appointments etc..
    }
}
