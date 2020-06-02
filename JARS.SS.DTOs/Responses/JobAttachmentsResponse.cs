using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class JobAttachmentsResponse
    {
        public JobAttachmentsResponse()
        {
            Attachments = new List<JobAttachmentDto>();
        }
        [DataMember]
        public List<JobAttachmentDto> Attachments { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
