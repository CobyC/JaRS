using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class ExternalJobsResponse
    {
        public ExternalJobsResponse()
        {
            ExternalJobs = new List<IExternalJob>();
        }
        [DataMember]
        public List<IExternalJob> ExternalJobs { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
