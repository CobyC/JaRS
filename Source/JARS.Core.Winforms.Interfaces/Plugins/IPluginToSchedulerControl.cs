using DevExpress.XtraScheduler;

namespace JARS.Core.WinForms.Interfaces.Plugins
{

    /// <summary>
    /// Use this interface when creating a plugin that needs to have access to the scheduler control
    /// Warning!! be careful when adding events to the scheduler control as it will trigger globally.
    /// </summary>
    public interface IPluginToSchedulerControl : IPluginWinForms
    {
        /// <summary>
        /// The Scheduler control that will be interacted with
        /// This is required for any interaction with the scheduler control
        /// </summary>
        SchedulerControl schedulerControl { get; set; }
    }
}
