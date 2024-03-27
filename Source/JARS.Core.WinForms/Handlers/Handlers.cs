using DevExpress.Portable.Input;
using DevExpress.Services;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Handlers
{
    public class ResourceMouseHandlerService : MouseHandlerServiceWrapper
    {
        IServiceProvider _schedulerProvider;

        public ResourceMouseHandlerService(IServiceProvider schedulerProvider, IMouseHandlerService originalHandlerService)
            : base(originalHandlerService)
        {
            this._schedulerProvider = schedulerProvider;
        }

        public override void OnMouseDown(PortableMouseEventArgs e)
        {
            SchedulerControl control = _schedulerProvider as SchedulerControl;
            SchedulerHitInfo hitInfo = control.ActiveView.ViewInfo.CalcHitInfo(e.Location, false);
            if (hitInfo.HitTest == SchedulerHitTest.ResourceHeader)
            {
                ResourceHeader resHeader = hitInfo.ViewInfo as ResourceHeader;                
                Rectangle rect = new Rectangle(resHeader.Bounds.X + 2, resHeader.Bounds.Y + 2, 16, 16);
                if (rect.Contains(e.Location))
                {                    
                    return;
                }
            }
            base.OnMouseDown(e);            
        }
        
    }
}
