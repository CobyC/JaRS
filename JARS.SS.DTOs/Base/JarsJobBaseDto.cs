using System;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs.Base
{
    /// <summary>
    /// This is the basic job class, it contain the minimal properties available in the JaRS system.
    /// To extend this class inherit from it and add the additional properties to the parent class.
    /// This class does not have any other relations linked to it other than the operative/resource the job might be assigned to.
    /// </summary>
    [DataContract]
    public abstract class JarsJobBaseDto : IntegratableEntityBaseDto
    {

        public JarsJobBaseDto()
        {
            ProgressStatus = "NEW";
        }

        [DataMember]
        public virtual string GuidValue { get; set; }
        
        /// <summary>
        /// Get or set the reference of the reference number on the external system. (works order).
        /// </summary>
        [DataMember]
        public virtual string ExtRefId { get; set; }
        /// <summary>
        /// This will have need to be assigned the value of the main job ID. 
        /// This is to help with jobs running longer than a day.
        /// </summary>
        [DataMember]
        public virtual int? CopyOfId { get; set; }

        /// <summary>
        /// Get or set the time the job (appointment) starts.
        /// </summary>
        [DataMember]
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// The date and time the job was actually started (this value can be recorded from external devices like mobile phones)
        /// </summary>
        [DataMember]
        public virtual DateTime? ActualStartDate { get; set; }
        /// <summary>
        /// Get or set the time the job (appointment) finish.
        /// </summary>
        [DataMember]
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// The date and time the job was actually ended (this value can be recorded from external devices like mobile phones)
        /// </summary>
        [DataMember]
        public virtual DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// Get or set the Status of the appointment this jobs represents in the DevExpress controls (if using DevExpress)
        /// </summary>
        [DataMember]
        public virtual string StatusKey { get; set; }

        /// <summary>
        /// Get or set the Label of the appointment this jobs represents in the DevExpress controls (if using DevExpress)
        /// </summary>
        [DataMember]
        public virtual string LabelKey { get; set; }

        /// <summary>
        /// Set or set the line of work (could be like a trade code) for the job.
        /// </summary>
        [DataMember]
        public virtual string LineOfWork { get; set; }

        /// <summary>
        /// Get or set the description of the Job (appointment)
        /// </summary>
        [DataMember]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the Address/location of the Job 
        /// </summary>
        [DataMember]
        public virtual string Location { get; set; }

        /// <summary>
        /// Get or set the operative/resource linked to this job, this does not indicate the trade, only the operative
        /// </summary>
        [DataMember]
        public virtual int ResourceId { get; set; }

        /// <summary>
        /// Get or Set the status or process state of the job ie 'NEW', 'ASSIGNED', 'INPROGRESS', whatever the state of the job is
        /// </summary>    
        [DataMember]
        public virtual string ProgressStatus { get; set; }

        /// <summary>
        /// Get or set the date and time the job was completed on
        /// </summary>
        [DataMember]
        public virtual DateTime? CompletionDate { get; set; }

        /// <summary>
        /// The date the job was originally targeted for to be completed
        /// </summary>
        [DataMember]
        public virtual DateTime? TargetDate { get; set; }


        /// <summary>
        /// Get or set the priority for the job
        /// </summary>
        [DataMember]
        public virtual string Priority { get; set; }

        [DataMember]
        public virtual bool IsActive { get; set; }

        [DataMember]
        public virtual bool ShowOnMobile { get; set; }

    }
}
