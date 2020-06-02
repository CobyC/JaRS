using JARS.Core.Entities;
using JARS.Core.Interfaces.Entities;
using System;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs.Base
{
    /// <summary>
    /// This class represents the base structure for an appointable entity.
    /// The TPrimaryKeyType is passed to the EntityBase class that this class inherits from
    /// </summary>
    [DataContract]    
    public abstract class AppointableBaseDto<TPrimaryKeyType> : EntityBaseDto<TPrimaryKeyType>, IEntityWithAppointing
    {       

        [DataMember]
        public virtual string GuidValue { get; set; }

        [DataMember]
        public virtual string Subject { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        [DataMember]
        public virtual int ResourceId { get; set; }

        [DataMember]
        public virtual DateTime StartDate { get; set; }

        [DataMember]
        public virtual DateTime EndDate { get; set; }
    }
}
