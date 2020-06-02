using JARS.SS.DTOs.Base;
using JARS.SS.DTOs.Requests.Base;
using ServiceStack;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{
    
    [Route("/joblinesplits/{Id}", "GET")]
    public class GetJobLineSplit : IReturn<JobLineSplitsResponse>
    {
        public int Id { get; set; }
    }

    [Route("/joblinesplits/find", "GET")]
    public class FindJobLineSplits : RequestBase<JobLineSplitsResponse>
    {
        public string SharedOperativeName { get; set; }
        public string SharedOperativeID { get; set; }
        public string OwningOperativeID { get; set; }
        public decimal? SplitQty { get; set; }
        public string LineCode { get; set; }
        public int? LineNum { get; set; }
        public int ExternalJobRef { get; set; }
        public JarsJobLineBaseDto SourceJobLine { get; set; }
    }

    [Route("/joblinesplits/store", "POST")]
    public class StoreJobLineSplits : StoreRequestBase, IReturn<JobLineSplitsResponse>
    {
        public StoreJobLineSplits()
        {
            Splits = new List<JobLineSplitDto>();
        }

        public List<JobLineSplitDto> Splits { get; set; }
    }

    [Route("/joblinesplits/{Id}", "DELETE")]
    public class DeleteJobLineSplit : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Route("/channels/notify/joblinesplits", "POST")]
    public class JobLineSplitsNotification : NotificationBaseDto<JobLineSplitDto>, IReturnVoid
    {
        public JobLineSplitsNotification()
        {
            Splits = new List<JobLineSplitDto>();
        }
        public List<JobLineSplitDto> Splits { get; set; }

    }
}
