using JARS.Entities;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    /// <summary>
    /// single record
    /// </summary>
    [DataContract]
    public class ApptStatusResponse
    {
        [DataMember]
        public ApptStatusDto Status { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }

    /// <summary>
    /// multiple records
    /// </summary>
    [DataContract]
    public class ApptStatusesResponse
    {
        [DataMember]
        public List<ApptStatusDto> Statuses { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
