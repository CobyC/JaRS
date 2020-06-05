using FluentNHibernate.Mapping;

namespace JARS.SS.Auth.Entities.Maps
{

    public class JarsUserAuthMap : ClassMap<JarsUserAuth>
    {
        public JarsUserAuthMap()
        {
            Table($"JarsUsers");
            //LazyLoad();
            //EntityName(nameof(JarsUserAuth));
            
            Id(x => x.Id)
                .GeneratedBy
                .Identity();

            Map(x => x.CreatedDate);
            Map(x => x.DisplayName);
            Map(x => x.Email);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.ModifiedDate);
            Map(x => x.PasswordHash);
            Map(x => x.PrimaryEmail);
            Map(x => x.Salt);
            Map(x => x.UserName);

            HasManyToMany(x => x.PermissionsBase)
                .Table("JarsUsers_Permissions")
                .ParentKeyColumn("UserAuthId")
                .Element("Permission")
                //.Access.PascalCaseField(Prefix.Underscore)
                .Not.LazyLoad();

            HasManyToMany(x => x.RolesBase)
                .Table("JarsUsers_Roles")
                .ParentKeyColumn("UserAuthId")
                .Element("Role")
                //.Access.PascalCaseField(Prefix.Underscore)
                .Not.LazyLoad();

        }
    }
}
