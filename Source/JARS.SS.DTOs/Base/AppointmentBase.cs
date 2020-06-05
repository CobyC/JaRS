using JARS.Core.Interfaces.Entities;
using System;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs.Base
{
    /// <summary>
    /// This class represents the structure of the DevExpress appointment.
    /// It's not a direct copy, but it contains the information required to set up an appointment.
    /// It contains extra properties that will be used in JARS.
    /// </summary>
    [DataContract]
    public abstract class AppointmentBaseDto : AppointableBaseDto<int>, IEntityWithLocation, IEntityWithStatusLabels, IEntityWithShowOnMobile
    {

        [DataMember]
        public virtual string Location { get; set; }

        [DataMember]
        public virtual string LocationCode { get; set; }


        [DataMember]
        public virtual long? DurationTicks { get; set; }

        //[DataMember]
        //public virtual bool IsRecurring { get; set; }

        [DataMember]
        public virtual bool IsAllDay { get; set; }

        //[DataMember]
        //public virtual bool LongerThanADay { get; set; }

        //[DataMember]
        //public virtual int RecurrenceIntervalPeriod { get; set; }

        //[DataMember]
        //public virtual int RecurrenceIntervalType { get; set; }

        //[DataMember]
        //public virtual int RecurrenceIntervalRange { get; set; }

        //[DataMember]
        //public virtual int DaysOfWeek { get; set; }

        //[DataMember]
        //public virtual int WeekOfMonth { get; set; }

        //[DataMember]
        //public virtual int MonthOfYear { get; set; }

        //[DataMember]
        //public virtual int? RecurrenceCount { get; set; }

        //[DataMember]
        //public virtual int IndexPosition { get; set; }

        /// <summary>
        /// Used only by the Pattern appointment
        /// </summary>
        [DataMember]
        public virtual string RecurrenceInfo { get; set; }

        /// <summary>
        /// Used by both Pattern and Occurrence appointments
        /// </summary>
        [DataMember]
        public virtual Guid RecurrenceId { get; set; }    //used by main and recurrence 

        /// <summary>
        /// Used only by the Occurrence appointment.
        /// </summary>
        [DataMember]
        public virtual int RecurrenceIndex { get; set; } //used only by occurrence (changed/deleted)

        [DataMember]
        public virtual string StatusKey { get; set; }

        [DataMember]
        public virtual string LabelKey { get; set; }

        [DataMember]
        public virtual string ApptTypeCode { get; set; }

        [DataMember]
        public virtual bool ShowOnMobile { get; set; }
    }
}
