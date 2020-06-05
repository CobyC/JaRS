using DevExpress.XtraScheduler;
using JARS.Entities;
using JARS.Core.WinForms.Interfaces.Plugins;
using System.ComponentModel.Composition;

namespace JARS.Winforms.Plugins.CustomForms
{
    [Export(nameof(JarsDefaultAppointment), typeof(IPluginAppointmentForm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JarsDefaultAppointmentEditFormPlugin : IPluginAppointmentForm
    {
        public string PluginText => "Jars Default Appointment";

        public void ShowAppointmentForm(SchedulerControl schedulerControl, Appointment appointment)
        {
            JarsDefaultAppointmentEditForm form = new JarsDefaultAppointmentEditForm(schedulerControl, appointment);
            try
            {
                form.ShowDialog();
            }
            finally { form.Dispose(); }
        }
    }
}
