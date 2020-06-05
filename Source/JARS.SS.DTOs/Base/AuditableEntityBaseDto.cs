using JARS.Core.Entities;
using JARS.Core.Interfaces.Entities;
using System;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs.Base
{
    [DataContract]
    public abstract class AuditableEntityBaseDto : EntityBaseDto<int>, IEntityWithAudit
    {
        public AuditableEntityBaseDto()
        { }

        /// <summary>
        /// Get or set the created on date (will default to the current date time if not specified)
        /// </summary>
        [DataMember]
        public virtual DateTime CreatedDate { get; set; }

        /// <summary>
        /// Get or set the for who or what the record was created by
        /// </summary>
        [DataMember]
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// The last date and time the record was modified on.
        /// </summary>
        [DataMember]
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// get or set who the record was last modified by.
        /// </summary>
        [DataMember]
        public virtual string ModifiedBy { get; set; }
    }
}
