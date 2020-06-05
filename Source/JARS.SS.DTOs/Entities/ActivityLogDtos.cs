using JARS.Core.Entities;
using JARS.Core.Interfaces.Entities;
using System;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class ActivityLogDto : EntityBaseDto<int>, IActivityLog
    {

        /// <summary>
        /// Get or Set a value to indicate the type of activity. ie Traveling, Lunch, break.
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// Get or set the category the activity belongs to ie, ActiveTime, SickCover, HolidayCover, MobileActions etc..
        /// </summary>
        [DataMember]
        public virtual string Category { get; set; }

        ///<summary>
        ///get or set the start date and time of the activity
        ///</summary>
        [DataMember]
        public virtual DateTime? StartDateTime { get; set; }

        /// <summary>
        /// Get or set the end time of the activity
        /// </summary>
        [DataMember]
        public virtual DateTime? EndDateTime { get; set; }

        /// <summary>
        /// Set an external reference number or any other reference used to link this to an activity
        /// </summary>
        [DataMember]
        public virtual string LinkedId { get; set; }

        /// <summary>
        /// This is to indicate what entity type (typeof(ent)).name this activity links to.
        /// </summary>
        [DataMember]
        public virtual string LinkedType { get; set; }

        /// <summary>
        /// get or set the operative/resource id the log belongs to
        /// </summary>
        [DataMember]
        public virtual int ResourceId { get; set; }       
    }
}
