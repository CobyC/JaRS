using JARS.SS.DTOs.Base;
using JARS.SS.DTOs.Requests.Base;
using ServiceStack;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{

    [Route("/externaljobs/{Id}")]
    public class GetExternalJob : IReturn<ExternalJobsResponse>
    {
        public int Id { get; set; }
    }

    [Route("/externaljobs/find")]
    public class FindExternalJobs : RequestBase<ExternalJobsResponse>
    {
        public string ViewType { get; set; }
        public string ExternalJobName { get; set; }
        public int ColourRGB { get; set; }
    }

    [Route("/externaljobs/store")]
    public class StoreExternalJobs : StoreRequestBase, IReturn<ExternalJobsResponse>
    {
        public StoreExternalJobs()
        {
            ExternalJobs = new List<ExternalJob>();
        }

        public List<IExternalJob> ExternalJobs { get; set; }
    }


    [Route("/externaljobs/{Id}")]
    public class DeleteExternalJob : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Route("/channels/{channel}/externaljobsnotification")]
    public class ExternalJobsCrudNotification : CrudNotificationBaseDto<IExternalJob>, IReturnVoid
    {
        public ExternalJobsCrudNotification()
        {
            ExternalJobs = new List<IExternalJob>();
        }
        public List<IExternalJob> ExternalJobs { get; set; }

    }

}
