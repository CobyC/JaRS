using DevExpress.XtraScheduler;
using JARS.Core.Interfaces.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JARS.Core.WinForms.Interfaces.Processors
{
    public interface IProcessorForLoadingEntityData
    {
        /// <summary>
        /// Load the appointment data that will be represented as appointments on the scheduler control. 
        /// </summary>
        /// <param name="isRefresh">if true then only load appointments that changed, if false load everything</param>
        Task LoadOrRefreshEntityDataAsync(bool isRefresh = false);

        /// <summary>
        /// Load the appointment data into the scheduler control, the appointment data should have been gathered in the LoadOrUpdateAppointmentsDataAsync method before calling this method.
        /// </summary>
        /// <param name="schedulerControl">The scheduler control where the appointments will be added.</param>
        void LoadOrRefreshEntityData(SchedulerControl schedulerControl);
    }
    /// <summary>
    /// This interface adds the implementation requirement for loading appointment data that needs to be displayed on the scheduler control.
    /// This interface should be used in conjunction with plugins that are implementing the interface IJarsPluginWithSchedulerControl 
    /// T represents the entity type that is held in the generic DataList property
    /// </summary>
    public interface IJarsProcessorForLoadingEntityData<T> : IProcessorForLoadingEntityData
        where T : IEntityBase
    {
        IList<T> DataList { get; set; }

    }
}
