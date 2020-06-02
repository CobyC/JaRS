using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{
    public class StandardAppointmentExceptionMap : ClassMap<StandardAppointmentException>
    {
        public StandardAppointmentExceptionMap()
        {
            Table($"{typeof(StandardAppointmentException).Name}s");
            LazyLoad();
            //persistent fields
            Id(x => x.Id)
                .GeneratedBy
                .Identity();
            //Map(x => x.RecordCreatedOn);
            //Map(x => x.RecordCreatedBy);
            //Map(x => x.RecordModifiedOn);
            //Map(x => x.RecordModifiedBy);
            //other properties
            Map(x => x.GuidValue);
            Map(x => x.Subject);
            Map(x => x.Description);
            Map(x => x.Location);
            Map(x => x.ResourceId);
            Map(x => x.StartDate);
            Map(x => x.DurationTicks);
            Map(x => x.EndDate);
            Map(x => x.IsAllDay);
            Map(x => x.PaternType);
            Map(x => x.ApptTypeCode);
            //Map(x => x.IndexPosition);
            Map(x => x.StatusKey);
            Map(x => x.LabelKey);
            References(x => x.StandardAppointment)
                .ForeignKey();

        }
    }
}
