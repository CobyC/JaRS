using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.WinForms.Plugins.CustomForms
{
    public class StandardAppointmentInplaceEditorControl : SchedulerInplaceEditorEx
    {
        //StandardAppointmentInplaceEditor editor;
        public StandardAppointmentInplaceEditorControl(SchedulerInplaceEditorEventArgs inplaceEditorArgs)
            : base(inplaceEditorArgs)
        { }
        public Appointment EditAppointment { get { return base.Appointment; } }
        public SchedulerControl SchedulerControl { get { return base.Control; } }
        public IInplaceEditor EditEditor { get { return base.Editor; } }
        
    }
}
