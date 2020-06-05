using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class JobLinesResponse
    {
        public JobLinesResponse()
        {
            JobLines = new List<JarsJobLineDto>();
        }
        [DataMember]
        public List<JarsJobLineDto> JobLines { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
