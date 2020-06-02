using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{
    public class ApptStatusMap : ClassMap<ApptStatus>
    {
        public ApptStatusMap()
        {
            Table($"{typeof(ApptStatus).Name}es");
            //persistent fields
            Id(x => x.Id)
                .GeneratedBy
                .Identity();
            //specifies lazy loading
            LazyLoad();
            //other properties
            Map(x => x.StatusName);
            Map(x => x.StatusCriteria);
            Map(x => x.UseInterfaceType);
            Map(x => x.ColourRGB);
            Map(x => x.ViewName);
            Map(x => x.SortIndex);
        }
    }
}
