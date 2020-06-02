using System;

namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface indicates that the implementing object needs to have a property holding a value for TargetDate.    
    /// </summary>
    public interface IEntityWithTargetDate : IEntityBase
    {
        /// <summary>
        /// The Targeted date for the entity to change or be worked on (affected) etc.
        /// The date from the external system of when the job was targeted to be worked on (completed)
        /// </summary>
        DateTime? TargetDate { get; set; }
    }
}
