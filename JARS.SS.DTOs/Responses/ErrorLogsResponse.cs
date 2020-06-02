using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class ErrorLogsResponse
    {
        public ErrorLogsResponse()
        {
            ErrorLogs = new List<ErrorLogDto>();
        }
        [DataMember]
        public List<ErrorLogDto> ErrorLogs { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
