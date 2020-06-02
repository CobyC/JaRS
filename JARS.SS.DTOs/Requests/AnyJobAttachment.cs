using JARS.SS.DTOs.Base;
using ServiceStack;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{    

    [Route("/jobattachments/{Id}", "GET")]
    public class GetJobAttachment : IReturn<JobAttachmentsResponse>
    {
        public int Id { get; set; }
    }

    [Route("/jobattachments/find", "GET")]
    public class FindJobAttachments : IReturn<JobAttachmentsResponse>
    {
        public string AttachmentName { get; set; }
        public int AttachedToJobId { get; set; }
    }

    [Route("/jobattachments/store", "POST")]
    public class StoreJobAttachments : StoreRequestBase, IReturn<JobAttachmentsResponse>
    {
        public StoreJobAttachments()
        {
            Attachments = new List<JobAttachmentDto>();
        }

        public List<JobAttachmentDto> Attachments { get; set; }
    }

    [Route("/jobattachments/{Id}", "DELETE")]
    public class DeleteJobAttachment : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Route("/channels/notify/jobattachments", "POST")]
    public class JobAttachmentsNotification : NotificationBaseDto<JobAttachmentDto>, IReturnVoid
    {
        public JobAttachmentsNotification()
        {
            Attachments = new List<JobAttachmentDto>();
        }
        public List<JobAttachmentDto> Attachments { get; set; }
    }
}
