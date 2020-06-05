using DevExpress.XtraTreeList;

namespace JARS.Core.WinForms.Interfaces.Plugins
{
    /// <summary>
    /// Use this interface when creating a plugin that needs to have access to the Resource Tree
    /// </summary>
    public interface IPluginToResourceTree : IPluginWinForms
    {
        /// <summary>
        /// The TreeList control that will be interacted with
        /// This is required for any interaction with the resource tree.
        /// </summary>
        TreeList resourceTree { get; set; }
    }
}
