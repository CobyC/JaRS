using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{

    /// <summary>
    /// Single record
    /// </summary>
    [DataContract]
    public class ApptLabelResponse
    {
        [DataMember]
        public ApptLabelDto Label { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }

    /// <summary>
    /// Multiple records
    /// </summary>
    [DataContract]
    public class ApptLabelsResponse
    {
        [DataMember]
        public List<ApptLabelDto> Labels { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
