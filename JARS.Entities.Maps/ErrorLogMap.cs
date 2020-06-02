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
    public class ErrorLogMap : ClassMap<ErrorLog>
    {

        public ErrorLogMap()
        {
            Table($"{typeof(ErrorLog).Name}s");
            //persistent fields
            Id(x => x.Id)
                .GeneratedBy
                .Identity();
            //specifies lazy loading
            LazyLoad();
            //other properties
            Map(x => x.ErrorType);
            Map(x => x.EnvironmentUserName);
            Map(x => x.ErrorText);
            Map(x => x.ErrorTime);
        }
    }
}
