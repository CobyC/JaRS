namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface indicates that the implementing object needs to have a property holding a value for IsActive.    
    /// </summary>
    public interface IEntityWithIsActive : IEntityBase
    {
        /// <summary>
        /// Indicates if the current entity is in an active or deactivated state.
        /// </summary>
        bool IsActive { get; set; }
    }
}
