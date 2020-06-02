using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{
    public class JarsDefaultAppointmentMap:ClassMap<JarsDefaultAppointment>
    {
        public JarsDefaultAppointmentMap()
        {
            Table($"{nameof(JarsDefaultAppointment)}s");
            LazyLoad();
            Id(x => x.Id);
            Map(x => x.Subject);
            Map(x => x.Description);
            Map(x => x.DefaultDuration);
            Map(x => x.IsAllDay);
            Map(x => x.ShowOnMobile);            
        }
    }
}
