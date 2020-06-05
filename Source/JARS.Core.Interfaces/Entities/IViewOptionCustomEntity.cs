namespace JARS.Core.Interfaces.Entities
{
    public interface IViewOptionCustomEntity :
       IEntityWithAppointing,
       IEntityWithLocation,
       IEntityWithLineOfWork,
       IEntityWithProgressStatus,
       IEntityWithPriority,
       IEntityWithAudit,
       IEntityWithIntegration,
       IEntityWithIsActive,
       IEntityWithIsEmergency,
       IEntityWithShowOnMobile,
       IEntityWithTargetDate
    {
        //this interface is used by the CustomViewOption plugin.
        //this enables rules to be added to any of the implementing entities.
    }
}
