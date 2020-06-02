using JARS.SS.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class ResourceDto : AuditableEntityBaseDto//, IResource
    {
        public ResourceDto()
        {
            this.Groups = new List<BasicResourceGroupDto>();
            this.Skills = new List<BasicResourceSkillDto>();
        }

        /// <summary>
        /// This is an additional reference value that can be used to identify the operative/resource in an external system.
        /// </summary>
        [DataMember]
        public virtual string ExtRef1 { get; set; }

        /// <summary>
        /// This is a secondary value that can be used to identify a operative/resource in an external system
        /// </summary>
        [DataMember]
        public virtual string ExtRef2 { get; set; }

        /// <summary>
        /// The first name of the operative/resource.
        /// </summary>
        [DataMember]
        public virtual string FirstName { get; set; }

        /// <summary>
        /// The last name of the operative/resource.
        /// </summary>
        [DataMember]
        public virtual string LastName { get; set; }

        /// <summary>
        /// The display name of the operative/resource on the JaRS UI.
        /// </summary>
        [DataMember]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// This is an unique external reference that will be used by the system to identify/link the operative/resource to another system.        
        /// </summary>
        [DataMember]
        public virtual string ExtRef { get; set; }

        /// <summary>
        /// The vehicle registration linked to this operative/resource, usually linked for vehicle inspections, or tracking stock in a van.
        /// </summary>
        [DataMember]
        public virtual string VehicleRegistration { get; set; }

        /// <summary>
        /// The email for the particular operative/resource. can be used for sending emails.
        /// </summary>
        [DataMember]
        public virtual string eMail { get; set; }

        /// <summary>
        /// The main contact number for the operative/resource.
        /// </summary>
        [DataMember]
        public virtual string MobileNo { get; set; }
        
        [DataMember]
        public virtual string Memo { get; set; }

        /// <summary>
        /// The default time of day the operative/resource starts working.
        /// </summary>
        [DataMember]
        public virtual TimeSpan? DayStartTime { get; set; }

        /// <summary>
        /// the default time the operative/resource stops working.
        /// </summary>
        [DataMember]
        public virtual TimeSpan? DayEndTime { get; set; }

        /// <summary>
        /// The default starting location of the resource.
        /// this could be something line a log.lat code or a description.
        /// </summary>
        [DataMember]
        public virtual string DayStartLocation { get; set; }

        /// <summary>
        /// This is the last known location of the resource.
        /// this will probably be a log.lat value.
        /// </summary>
        [DataMember]
        public virtual string LastRecordedLocation { get; set; }

        /// <summary>
        /// Indicates if the operative/resource has a mobile device, or if it is just a system resource.
        /// </summary>
        [DataMember]
        public virtual bool? IsMobileResource { get; set; }

        /// <summary>
        /// Indicates if the operative/resource is active, this can also be used to show or draw the operative/resource calendar.
        /// </summary>
        [DataMember]
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// This can be used to sort the item in a list.
        /// </summary>
        [DataMember]
        public virtual int SortIndex { get; set; }

        /// <summary>
        /// This list of skills can be used to determine how skillful an operative/resource is.
        /// it is used to calculate the amount of time an operative/resource will spend on a particular type of job.
        /// NOTE!! that this class is the -basic- skill class (to prevent circular references when serializing.)
        /// </summary>
        [DataMember]
        public virtual IList<BasicResourceSkillDto> Skills { get; set; }


        /// <summary>
        /// The list of groups this operative/resource belongs to
        /// NOTE!! that this class is the -basic- skill class (to prevent circular references when serializing.)
        /// </summary>
        [DataMember]
        public virtual IList<BasicResourceGroupDto> Groups { get; set; }

    }
}
