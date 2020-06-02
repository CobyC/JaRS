namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface indicates that the implementing object needs to have a property holding a value for priority.    
    /// </summary>
    public interface IEntityWithPriority : IEntityBase
    {
        /// <summary>
        /// get or set he priority that might be assigned to the job/work.        
        /// </summary>
        string Priority { get; set; }
    }
}
