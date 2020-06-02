using System;
using System.Collections.Generic;

namespace JARS.Core.Interfaces.Entities
{
    public interface IResource : IEntityBase<int>
    {
        /// <summary>
        /// the default time the operative/resource stops working.
        /// </summary>
        TimeSpan? DayEndTime { get; set; }
        /// <summary>
        /// The default starting location of the resource.
        /// this could be something line a log.lat code or a description.
        /// </summary>
        string DayStartLocation { get; set; }
        /// <summary>
        /// The default time of day the operative/resource starts working.
        /// </summary>
        TimeSpan? DayStartTime { get; set; }
        /// <summary>
        /// The display name of the operative/resource on the JaRS UI.
        /// </summary>
        string DisplayName { get; set; }
        /// <summary>
        /// The email for the particular operative/resource. can be used for sending emails.
        /// </summary>
        string eMail { get; set; }
        /// <summary>
        /// This is an unique external reference that will be used by the system to identify/link the operative/resource to another system.        
        /// </summary>
        string ExtRef { get; set; }
        /// <summary>
        /// This is an additional reference value that can be used to identify the operative/resource in an external system.
        /// </summary>
        string ExtRef1 { get; set; }
        /// <summary>
        /// This is a secondary value that can be used to identify a operative/resource in an external system
        /// </summary>
        string ExtRef2 { get; set; }
        /// <summary>
        /// The first name of the operative/resource.
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// The list of groups this operative/resource belongs to
        /// </summary>
        IList<IResourceGroup> Groups { get; set; }
        /// <summary>
        /// Indicates if the operative/resource is active, this can also be used to show or draw the operative/resource calendar.
        /// </summary>
        bool IsActive { get; set; }
        /// <summary>
        /// Indicates if the operative/resource has a mobile device, or if it is just a system resource.
        /// </summary>
        bool? IsMobileResource { get; set; }
        /// <summary>
        /// The last name of the operative/resource.
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// This is the last known location of the resource.
        /// this will probably be a log.lat value.
        /// </summary>
        string LastRecordedLocation { get; set; }
        string Memo { get; set; }
        /// <summary>
        /// The main contact number for the operative/resource.
        /// </summary>
        string MobileNo { get; set; }
        ///// <summary>
        ///// This list of skills can be used to determine how skillful an operative/resource is.
        ///// it is used to calculate the amount of time an operative/resource will spend on a particular type of job.
        ///// </summary>
        //IList<ResourceSkill> Skills { get; set; }
        /// <summary>
        /// This can be used to sort the item in a list.
        /// </summary>
        int SortIndex { get; set; }
        /// <summary>
        /// The vehicle registration linked to this operative/resource, usually linked for vehicle inspections, or tracking stock in a van.
        /// </summary>
        string VehicleRegistration { get; set; }
    }
}
