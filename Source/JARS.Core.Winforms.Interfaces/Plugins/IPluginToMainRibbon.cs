using DevExpress.XtraBars.Ribbon;

namespace JARS.Core.WinForms.Interfaces.Plugins
{

    /// <summary>
    /// Use this interface when creating a plugin that needs to have access to the scheduler control
    /// </summary>
    public interface IPluginToMainRibbon : IPluginWinForms
    {
        /// <summary>
        /// The Scheduler control that will be interacted with
        /// This is required for any interaction with the scheduler control
        /// </summary>
        RibbonControl MainRibbon { get; set; }
    }
}
