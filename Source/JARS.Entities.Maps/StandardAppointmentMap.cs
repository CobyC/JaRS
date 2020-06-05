using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{
    public class StandardAppointmentMap : ClassMap<StandardAppointment>
    {
        protected StandardAppointmentMap()
        {
            Table($"{typeof(StandardAppointment).Name}s");
            //persistent fields
            Id(x => x.Id)
                .GeneratedBy
                .Identity();
            //Map(x => x.RecordCreatedOn);
            //Map(x => x.RecordCreatedBy);
            //Map(x => x.RecordModifiedOn);
            //Map(x => x.RecordModifiedBy);
            //specifies lazy loading
            LazyLoad();
            //other properties
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            Map(x => x.StatusKey);
            Map(x => x.LabelKey);
            Map(x => x.Description).Length(1000);
            Map(x => x.Location).Length(1000);
            Map(x => x.ResourceId);
            Map(x => x.Subject).Length(1000);
            Map(x => x.GuidValue);
            Map(x => x.ApptTypeCode);
            //Map(x => x.IndexPosition);
            Map(x => x.ShowOnMobile);

            //recurrence appointment related info
            Map(x => x.RecurrenceInfo).Length(1000);
            Map(x => x.RecurrenceId);
            Map(x => x.RecurrenceIndex);
            //Map(x => x.RecurrenceCount);
            //Map(x => x.MonthOfYear);
            //Map(x => x.WeekOfMonth);
            //Map(x => x.DaysOfWeek);
            //Map(x => x.RecurrenceIntervalRange);
            //Map(x => x.RecurrenceIntervalType);
            //Map(x => x.RecurrenceIntervalPeriod);
            //Map(x => x.LongerThanADay);
            Map(x => x.IsAllDay);
            //Map(x => x.IsRecurring);
            Map(x => x.DurationTicks);
            //HasMany(x => x.StandardAppointmentExceptions)
            //    .Cascade
            //    .All();//<-- set to all because if the appointment recurrence is deleted all the recurring exceptions needs to be removed as well.
        }
    }
}
