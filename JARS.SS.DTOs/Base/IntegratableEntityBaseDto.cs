using JARS.Core.Interfaces.Entities;
using System;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs.Base
{
    [DataContract]
    public abstract class IntegratableEntityBaseDto : AuditableEntityBaseDto, IEntityWithIntegration
    {
        [DataMember]
        public virtual int IntegrationStatus { get; set; }
        /// <summary>
        /// This column is intended for use by SX , Get or Set when the job was last run through a script or process.
        /// </summary>
        [DataMember]
        public virtual DateTime? IntegrationDate { get; set; }
        /// <summary>
        /// Get or Set the Message indicating if the integration was successful or failed with messages.
        /// </summary>
        [DataMember]
        public virtual string IntegrationMessage { get; set; }
    }
}
