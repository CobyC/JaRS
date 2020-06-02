using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    /// <summary>
    /// This enables single or multiple jobs to be created or updated.
    /// </summary>

    [DataContract]
    public class JarsJobsResponse
    {
        /// <summary>
        /// If the request was made with FetchLazy set to true this property will be populated.
        /// </summary>
        [DataMember]
        public virtual List<JarsJobDto> Jobs { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
