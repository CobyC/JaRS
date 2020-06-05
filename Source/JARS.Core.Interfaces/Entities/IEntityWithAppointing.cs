using System;

namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface indicates that the implementing class has the minimal values required for it to be able to present an appointment. 
    /// </summary>
    public interface IEntityWithAppointing : IEntityBase
    {
        /// <summary>
        /// Get or set the subject for the appointable entity, this can be set to a reference number or any other subject.
        /// This will map to the Subject property of an appointment if not overridden by another method or property
        /// </summary>
        string Subject { get; set; }

        /// <summary>
        /// Get or set the description of the Job (appointment)
        /// This could represent  A long description of the work that needs to be carried out
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Get or set the time the job (appointment) starts.
        /// </summary>
        DateTime StartDate { get; set; }

        /// <summary>
        /// Get or set the time the job (appointment) finish.
        /// </summary>
        DateTime EndDate { get; set; }

        /// <summary>
        /// Get or set the operative/resource id linked to this job, this does not indicate the trade, only the operative/resource
        /// </summary>
        int ResourceId { get; set; }

        /// <summary>
        /// Get or set the guid value for the entity, this is a unique id used for synchronizing appointments.
        /// </summary>
        string GuidValue { get; set; }
    }
}