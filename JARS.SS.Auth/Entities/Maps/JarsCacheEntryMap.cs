using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.SS.Auth.Entities.Maps
{
    public class JarsCacheEntryMap : ClassMap<JarsCacheEntry>
    {
        public JarsCacheEntryMap()
        {
            Id(x => x.Id);
            Map(x => x.Data).Length(1000);
            Map(x => x.ExpiryDate);
            Map(x => x.CreatedDate);
            Map(x => x.ModifiedDate);
        }
    }
}
