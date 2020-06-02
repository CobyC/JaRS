using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.UI;
using System;
using System.ComponentModel;

namespace JARS.WinForms.Plugins.CustomForms
{
    public partial class CustomStandardEditAppointmentForm : AppointmentRibbonForm
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CustomStandardEditAppointmentForm(SchedulerControl schedulerControl, Appointment appointment) : base(schedulerControl, appointment)
        {
            InitializeComponent();            
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (this.Controller.AppointmentType == AppointmentType.Pattern)
                ShowRecurrenceForm(new AppointmentRecurrenceForm(Controller.EditedAppointmentCopy, FirstDayOfWeek.Monday, this.Controller));
            
        }
    }
}
