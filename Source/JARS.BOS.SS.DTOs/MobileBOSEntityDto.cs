using JARS.Core.Entities;
using System;
using System.Runtime.Serialization;

namespace JARS.BOS.SS.DTOs
{
    [DataContract]
    public class MobileBOSEntityDto : EntityBaseDto<int>
    {

        [DataMember]
        public string ExtRefId { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Priority { get; set; }

        [DataMember]
        public string ProgressStatus { get; set; }

        [DataMember]
        public int ResourceId { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; } // "StartDate": "2020-02-08T17:17:35.564Z",

        [DataMember]
        public DateTime EndDate { get; set; } // "EndDate": "2020-02-08T17:17:35.564Z",   

        [DataMember]
        public DateTime? ModifiedDate { get; set; } // "ModifiedDate": "2020-02-08T17:17:35.564Z",

        [DataMember]
        public DateTime? ActualStartDate { get; set; } // "ActualStartDate": "2020-02-08T17:17:35.564Z",

        [DataMember]
        public DateTime? ActualEndDate { get; set; } //  "ActualEndDate": "2020-02-08T17:17:35.564Z",

        [DataMember]
        public DateTime? CompletionDate { get; set; } // "CompletionDate": "2020-02-08T17:17:35.564Z",

        [DataMember]
        public string AddedNotes { get; set; }
    }

}
