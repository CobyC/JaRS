using JARS.SS.DTOs.Base;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class JobLineSplitDto : IntegratableEntityBaseDto
    {
        /// <summary>
        /// The external job/order reference number
        /// </summary>
        [DataMember]
        public virtual int ExternalJobRef { get; set; }

        /// <summary>
        /// The line number that was split
        /// </summary>
        [DataMember]
        public virtual int? LineNum { get; set; }

        /// <summary>
        /// The code of the line that was split.
        /// </summary>
        [DataMember]
        public virtual string LineCode { get; set; }

        /// <summary>
        /// The quantity that was assigned to the operative/resource the original line was split by.
        /// </summary>
        [DataMember]
        public virtual decimal? SplitQty { get; set; }

        /// <summary>
        /// the id of the operative/resource to which the line was originally assigned to.
        /// </summary>
        [DataMember]
        public virtual string OwningOperativeId { get; set; }

        /// <summary>
        /// The operative/resource id that the JobLine was shared with.
        /// </summary>
        [DataMember]
        public virtual string SharedOperativeId { get; set; }

        /// <summary>
        /// The name of the operative/resource the JobLine was shared with.
        /// </summary>
        [DataMember]
        public virtual string SharedOperativeName { get; set; }

        /// <summary>
        /// Access to the JobLine that this split is linked to.
        /// </summary>
        [DataMember]
        public virtual JarsJobLineBaseDto SourceJobLine { get; set; }
    }
}
