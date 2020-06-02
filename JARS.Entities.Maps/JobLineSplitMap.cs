using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{
    public class JobLineSplitMap : ClassMap<JarsJobLineSplit>
    {
        public JobLineSplitMap()
        {
            Table($"{typeof(JarsJobLineSplit).Name}s");
            //persistent fields
            Id(x => x.Id)
                .GeneratedBy
                .Identity();
            Map(x => x.CreatedDate);
            Map(x => x.CreatedBy);
            Map(x => x.ModifiedDate);
            Map(x => x.ModifiedBy);
            //specifies lazy loading
            LazyLoad();
            //other properties
            Map(x => x.ExternalJobRef);
            Map(x => x.LineNum);
            Map(x => x.LineCode);
            Map(x => x.SplitQty);
            Map(x => x.OwningOperativeId);
            Map(x => x.SharedOperativeId);
            Map(x => x.SharedOperativeName);
            References(x => x.SourceJobLine)
                .Column($"{typeof(JarsJobLineBase).Name}Id".Replace("Base", ""));
        }
    }
}
