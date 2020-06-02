using FluentNHibernate.Mapping;
using System.Linq;

namespace JARS.SS.Auth.Entities.Maps
{
    public class JarsUserAuthDetailsMap : ClassMap<JarsUserAuthDetails>
    {
        public JarsUserAuthDetailsMap()
        {
            Table($"{nameof(JarsUserAuthDetails)}");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.AccessToken);
            Map(x => x.AccessTokenSecret);
            Map(x => x.CreatedDate);
            Map(x => x.DisplayName);
            Map(x => x.Email);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.ModifiedDate);
            Map(x => x.Provider);
            Map(x => x.RequestToken);
            Map(x => x.RequestTokenSecret);
            Map(x => x.UserAuthId);
            Map(x => x.UserId);
            Map(x => x.UserName);

            HasMany(x => x.ItemsBase)
                .AsMap<string>(
                    index => index.Column("`Key`").Type<string>(),
                    element => element.Column("Value").Type<string>())
                .KeyColumn("JarsUserAuthDetailsID")
                .Table("JarsUserAuthDetails_Items")
                .Not.LazyLoad()
                .Cascade.All();
        }
    }
}
