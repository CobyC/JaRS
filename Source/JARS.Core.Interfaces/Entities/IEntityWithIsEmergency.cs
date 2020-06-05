namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface indicates that the implementing object needs to have a property holding a value for IsEmergency.    
    /// </summary>
    public interface IEntityWithIsEmergency:IEntityBase
    {

        /// <summary>
        /// Indicates if the current entity is in an emergency or not.
        /// </summary>
        bool IsEmergency { get; set; }
    }
}
