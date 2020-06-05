using JARS.SS.DTOs.Base;
using JARS.SS.DTOs.Requests.Base;
using ServiceStack;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{      

    [Route("/jobLines/{Id}", "GET")]
    public class GetJobLineBase : RequestBase<JobLinesResponse>
    {
        public int Id { get; set; }
    }

    [Route("/jobLines/find", "GET")]
    public class FindJobLines : RequestBase<JobLinesResponse>
    {
        public string[] AssignedToOperatives { get; set; }
        public int[] LinksToExtRefIds { get; set; }
    }

    [Route("/jobLines/store", "POST")]
    public class StoreJobLines : StoreRequestBase, IReturn<JobLinesResponse>
    {
        public StoreJobLines()
        {
            JobLines = new List<JarsJobLineDto>();
        }

        public List<JarsJobLineDto> JobLines { get; set; }
    }

    [Route("/jobLines/{Id}", "DELETE")]
    public class DeleteJobLineBase :IReturnVoid
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Route("/channels/notify/joblines", "POST")]
    public class JobLinesNotification : NotificationBaseDto<JarsJobLineDto>, IReturnVoid
    {
        public JobLinesNotification()
        {
            JobLines = new List<JarsJobLineDto>();
        }
        public List<JarsJobLineDto> JobLines { get; set; }

    }
}
