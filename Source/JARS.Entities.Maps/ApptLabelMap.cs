using FluentNHibernate.Mapping;
using JARS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{    
    public class ApptLabelMap : ClassMap<ApptLabel>
    {
        public ApptLabelMap()
        {
            Table($"{typeof(ApptLabel).Name}s");
            //persistent fields
            Id(x => x.Id)
                .GeneratedBy
                .Identity();
            //specifies lazy loading
            LazyLoad();
            //other properties
            Map(x => x.LabelName);
            Map(x => x.LabelCriteria);
            Map(x => x.UseInterfaceType);
            Map(x => x.ColourRGB);
            Map(x => x.ForeColourRGB);
            Map(x => x.ViewName);
            Map(x => x.SortIndex);
        }
    }
}
