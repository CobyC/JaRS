using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class JobLineSplitsResponse
    {
        public JobLineSplitsResponse()
        {
            Splits = new List<JobLineSplitDto>();
        }
        [DataMember]
        public List<JobLineSplitDto> Splits { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
