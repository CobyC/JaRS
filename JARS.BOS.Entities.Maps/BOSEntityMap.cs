using FluentNHibernate.Mapping;

namespace JARS.BOS.Entities.Maps
{
    public class BOSEntityMap : ClassMap<BOSEntity>
    {
        public BOSEntityMap()
        {
            DynamicUpdate();//this should only update changed values if used with the Merge(obj) method.

            Table("BOSEntities");
            //specifies lazy loading
            LazyLoad();
            //persistent fields
            Id(x => x.Id); //EntityBase property
            Map(x => x.GuidValue);

            //IAppointableEntity properties
            Map(x => x.Description);
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            Map(x => x.ResourceId);
            
            //IEntityWithActualDates
            Map(x => x.ActualStartDate);
            Map(x => x.ActualEndDate);

            //IExternalReferencedEntity property
            Map(x => x.ExtRefId);

            //IStatusLabledEntity properties
            Map(x => x.LabelKey);
            Map(x => x.StatusKey);

            //ILocatableEntity
            Map(x => x.Location);

            //ILineOfWorkEntity
            Map(x => x.LineOfWork);

            //IProgressStatusEntity
            Map(x => x.ProgressStatus);

            //IPrioritisedEntity
            Map(x => x.Priority);

            //IAuditableEntity
            Map(x => x.CreatedBy);
            Map(x => x.CreatedDate);
            Map(x => x.ModifiedBy);
            Map(x => x.ModifiedDate);

            //IIntegratableEntity
            Map(x => x.IntegrationDate);
            Map(x => x.IntegrationStatus);
            Map(x => x.IntegrationMessage);

            //IActiveEntity
            Map(x => x.IsActive);

            //ITargetDatedEntity
            Map(x => x.TargetDate);

            //IShowOnMobileEntity 
            Map(x => x.ShowOnMobile);

            //Added Properties
            Map(x => x.AddedNotes);
        }
    }
}
