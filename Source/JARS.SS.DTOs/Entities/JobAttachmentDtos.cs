using JARS.Core.Entities;
using System;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class JobAttachmentDto : EntityBase<int>
    {
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// The byte array that needs to be serialized or deserialized into a stream to get the representing information.
        /// </summary>
        [DataMember]
        public virtual byte[] AttachmentData { get; set; }

        /// <summary>
        /// The timestamp generated when the attachment was created.
        /// </summary>
        [DataMember]
        public virtual DateTime TimeAttached { get; set; }
    }
}
