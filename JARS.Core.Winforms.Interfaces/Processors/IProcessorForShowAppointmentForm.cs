using DevExpress.XtraScheduler;
using JARS.Core.Interfaces.Processors;

namespace JARS.Core.WinForms.Interfaces.Processors
{
    public interface IProcessorForShowAppointmentForm: IProcessor
    {
        void ShowAppointmentForm(SchedulerControl schedulerControl, Appointment appointment);
    }
}
