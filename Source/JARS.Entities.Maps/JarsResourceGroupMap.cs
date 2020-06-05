using FluentNHibernate.Mapping;

namespace JARS.Entities.Maps
{
    public class JarsResourceGroupMap : ClassMap<JarsResourceGroup>
    {
        public JarsResourceGroupMap()
        {
            Table($"{typeof(JarsResourceGroup).Name}s");
            //specifies lazy loading
            LazyLoad();
            //persistent fields
            Id(x => x.Id)
                .GeneratedBy
                .Identity();
            //audit mapping
            Map(x => x.CreatedDate);
            Map(x => x.CreatedBy);
            Map(x => x.ModifiedDate);
            Map(x => x.ModifiedBy);

            //other properties
            Map(x => x.Name);
            Map(x => x.Code);
            Map(x => x.SortIndex);
            Map(x => x.IsActive);
            HasManyToMany(x => x.Resources)
                .Table("JarsResourcesToGroups")
                .Not.LazyLoad()
                .Cascade.Merge()
                .Inverse();//<-- this indicates that the operative/resource will be saved before the group if the object tree is sent.
            //.Cascade //<-- because the worker and the group items are not relying on each other as parent child, the cascade can be removed (this will default to none)
            //.SaveUpdate(); // <-- only do save-update because we dont want the linked workers 
            //to be deleted if not associated with a group
            //the other side will also be marked as inverse because the group will be the owner (for nhibernate purposes one side needs to be the owner)
        }
    }
}
