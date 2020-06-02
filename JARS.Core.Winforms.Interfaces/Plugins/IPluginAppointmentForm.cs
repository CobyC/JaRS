using DevExpress.XtraScheduler;

namespace JARS.Core.WinForms.Interfaces.Plugins
{
    public interface IPluginAppointmentForm : IPluginWinForms
    {
        void ShowAppointmentForm(SchedulerControl schedulerControl, Appointment appointment);
    }
}
