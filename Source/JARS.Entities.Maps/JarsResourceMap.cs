using FluentNHibernate.Mapping;

namespace JARS.Entities.Maps
{
    public class JarsResourceMap : ClassMap<JarsResource>
    {
        public JarsResourceMap()
        {
            Table($"{typeof(JarsResource).Name}s");
             //specifies lazy loading
            LazyLoad();
            //persistent fields
            Id(x => x.Id)
                .GeneratedBy
                .Identity();
            //dynamic updates
            DynamicUpdate();//<-- this indicates that only the changed properties will be part of the update statement.
            //audit mapping
            Map(x => x.CreatedDate);
            Map(x => x.CreatedBy);
            Map(x => x.ModifiedDate);
            Map(x => x.ModifiedBy);
           
            //other properties
            Map(x => x.ExtRef1);
            Map(x => x.ExtRef2);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.DisplayName);
            Map(x => x.ExtRef);
            Map(x => x.VehicleRegistration);
            Map(x => x.eMail);
            Map(x => x.MobileNo);
            Map(x => x.Memo);
            Map(x => x.DayStartTime);
            Map(x => x.DayEndTime);
            Map(x => x.DayStartLocation);
            Map(x => x.LastRecordedLocation);
            Map(x => x.IsMobileResource);
            Map(x => x.IsActive);            
            Map(x => x.SortIndex);

            //HasMany(x => x.Skills)
            //    .Cascade.All(); // the operative/resource can have multiple skills          

            HasManyToMany(x => x.Groups).Table("JarsResourcesToGroups");//.Inverse();
            //.Cascade.SaveUpdate() //<-- default cascade is none, we can remove this because the worker group is linked but does not depend on each other.
            //.Inverse();//<- indicates that this will be the 'child' side ie. the group will be saved before the worker is saved.
        }
    }
}
