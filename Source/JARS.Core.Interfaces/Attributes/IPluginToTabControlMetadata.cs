namespace JARS.Core.Interfaces.Attributes.UI
{
    /// <summary>
    /// This interface describes the metadata required for a plugin that needs to be plugged into to the external source panel
    /// This is useful with MEF (Lazy&lt;gt&; method)
    /// </summary>
    public interface IPluginToTabControlMetadata : IPluginMetadata
    {

        /// <summary>
        /// The index order the panel will be shown in.
        /// </summary>
        int PositionIndex { get; }       
    }
}
