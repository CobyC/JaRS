using System;

namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface indicates that the implementing object needs to have a properties related to a record that keeps track of integration information.    
    /// </summary>
    public interface IEntityWithIntegration : IEntityBase
    {
        /// <summary>
        /// get or set an integer value to indicate what status or stage of integration the entity is in or at.
        /// </summary>
        int IntegrationStatus { get; set; }

        /// <summary>
        /// Get or Set when the job was integrated.
        /// </summary>
        DateTime? IntegrationDate { get; set; }

        /// <summary>
        /// Get or Set the message indicating if the integration was successful or failed.
        /// </summary>
        string IntegrationMessage { get; set; }
    }
}
