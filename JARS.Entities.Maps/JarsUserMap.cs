using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{
    public class JarsUserMap : ClassMap<JarsUser>
    {
        public JarsUserMap()
        {
            Table($"{typeof(JarsUser).Name}s");
            //specifies lazy loading
            //LazyLoad();
            //EntityName(nameof(JarsUser));
            Id(x => x.Id)
                .GeneratedBy
                .Identity();

            //audit fields
            Map(x => x.CreatedDate);
            Map(x => x.CreatedBy);
            Map(x => x.ModifiedDate);
            Map(x => x.ModifiedBy);

            //default system properties
            Map(x => x.UserName);
            Map(x => x.DisplayName);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Email);
            Map(x => x.UserCode);
            Map(x => x.UserCode1);
            Map(x => x.UserCode2);
            Map(x => x.IsActive);

            // Map(x => x.IsActive);
            HasMany(x => x.Settings).Cascade.AllDeleteOrphan();//.Not.LazyLoad();
            Map(x => x.Roles).CustomType(typeof(string)).Access.PascalCaseField(Prefix.Underscore).Length(1000);
            Map(x => x.Permissions).CustomType(typeof(string)).Access.PascalCaseField(Prefix.Underscore).Length(1000);

            //DynamicUpdate();
        }
    }
}
