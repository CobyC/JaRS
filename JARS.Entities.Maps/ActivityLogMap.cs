using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps

{
    /// <summary>
    /// this class is used for recording activity from the mobile device.
    /// things like travel, collecting materials, breaks etc..
    /// </summary>
    public class ActivityLogMap : ClassMap<ActivityLog>
    {

        public ActivityLogMap()
        {
            Table($"{typeof(ActivityLog).Name}s");
            //persistent fields
            Id(x => x.Id)
                .GeneratedBy
                .Identity();
            //specifies lazy loading
            LazyLoad();
            //other properties
            Map(x => x.Name);
            Map(x => x.Category);
            Map(x => x.StartDateTime);
            Map(x => x.EndDateTime);
            Map(x => x.LinkedId);
            Map(x => x.LinkedType);
            Map(x => x.ResourceId);
        }
    }
}
