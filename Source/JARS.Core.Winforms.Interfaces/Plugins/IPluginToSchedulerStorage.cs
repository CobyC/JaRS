using DevExpress.XtraScheduler;

namespace JARS.Core.WinForms.Interfaces.Plugins
{

    /// <summary>
    /// Use this interface when creating a plugin that needs to have access to the scheduler control storage
    /// </summary>
    public interface IPluginToSchedulerStorage : IPluginWinForms
    {
        /// <summary>
        /// The Scheduler control storage that will be interacted with
        /// This is required for any interaction with the scheduler control storage
        /// </summary>
        ISchedulerStorage schedulerDataStorage { get; set; }
    }
}
