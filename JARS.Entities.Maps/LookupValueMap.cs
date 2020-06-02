using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{
    public class LookupValueMap : ClassMap<LookupValue>
    {
        public LookupValueMap()
        {
            Table($"{typeof(LookupValue).Name}s");
            //persistent fields
            Id(x => x.Id)
                .GeneratedBy
                .Identity();
            //specifies lazy loading
            LazyLoad();
            //other properties
            Map(x => x.CategoryCode);
            Map(x => x.Value);
            Map(x => x.DisplayText);
        }
    }
}
