namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface indicates that the implementing object needs to have a property holding a value for LineOfWork.
    /// (This could also be interpreted as Trade, Group, Skill
    /// </summary>
    public interface IEntityWithLineOfWork : IEntityBase
    {
        /// <summary>
        /// Get or set the line of work (trade, group, department) code for the entity.
        /// ie PLUMB, GENCARE, INSPECTOR
        /// </summary>
        string LineOfWork { get; set; }
    }
}
