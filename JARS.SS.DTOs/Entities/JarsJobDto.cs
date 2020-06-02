using JARS.Entities;
using JARS.SS.DTOs.Base;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class JarsJobDto : JarsJobBaseDto
    {
        public JarsJobDto()
        { }
        /// <summary>
        /// This is an additional property to show that any non base properties need to be added to the particular base class.
        /// Other Job classes might not need this property and will not be available in the other entities derived from JobBase.
        /// </summary>
        [DataMember]
        public virtual string AssignedBy { get; set; }


        /// <summary>
        /// This is a list of attachments that are linked to this job, this could be images and documents etc..
        /// </summary>
        [DataMember]
        public virtual IList<JobAttachmentDto> Attachments { get; set; }
        /// <summary>
        /// Job lines (also known as SOR lines) that are linked to this job.
        /// </summary>
        [DataMember]
        public virtual IList<JarsJobLineDto> JobLines { get; set; }

    }
}
