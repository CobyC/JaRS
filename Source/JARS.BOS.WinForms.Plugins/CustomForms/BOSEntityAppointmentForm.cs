using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.UI;
using JARS.BOS.Entities;
using System;

namespace JARS.BOS.WinForms.Plugins.CustomForms
{
    public partial class BOSEntityAppointmentForm : AppointmentForm
    {
        Appointment _Appointment;

        public BOSEntityAppointmentForm(SchedulerControl schedulerControl, Appointment appointment) : base(schedulerControl, appointment)
        {
            InitializeComponent();
            _Appointment = appointment;
            BOSEntity editEntity = _Appointment.CustomFields["ENTITY"] as BOSEntity;
            bosEntityBindingSource.DataSource = editEntity;
            bosEntityBindingSource.ResetBindings(false);           
        }
        public override string Text { get => "Back Office Entity Details"; set => base.Text = value; }
        
        protected override void OnLoad(EventArgs e)
        {           
            base.OnLoad(e);            
        }

        public override bool SaveFormData(Appointment appointment)
        {
            appointment.CustomFields["ENTITY"] = bosEntityBindingSource.Current as BOSEntity;
            return base.SaveFormData(appointment);

        }

        private void BOSEntityAppointmentForm_Load(object sender, EventArgs e)
        {

        }
    }
}
