using DevExpress.XtraScheduler;

namespace JARS.Core.WinForms.Interfaces.Plugins
{
    /// <summary>
    /// Use this interface when creating a plugin that needs to have access to the DateNavigator control
    /// </summary>
    public interface IPluginToDateNavigator : IPluginWinForms
    {
        /// <summary>
        /// The DateNavigator control that will be interacted with (this is linked to the scheduler control).
        /// This is required for any interaction with the date navigator control.
        /// </summary>
        DateNavigator dateNavigator { get; set; }
    }
}
