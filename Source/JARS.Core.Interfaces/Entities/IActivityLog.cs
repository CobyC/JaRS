using System;

namespace JARS.Core.Interfaces.Entities
{
    public interface IActivityLog
    {
        /// <summary>
        /// Get or set the category the activity belongs to ie, ActiveTime, SickCover, HolidayCover, MobileActions etc..
        /// </summary>
        string Category { get; set; }
        /// <summary>
        /// Get or set the end time of the activity
        /// </summary>
        DateTime? EndDateTime { get; set; }
        /// <summary>
        /// Set an external reference number or any other reference used to link this to an activity
        /// </summary>
        string LinkedId { get; set; }
        /// <summary>
        /// This is to indicate what entity type (typeof(ent)).name this activity links to.
        /// </summary>
        string LinkedType { get; set; }
        /// <summary>
        /// Get or Set a name to indicate the kind of activity. ie Traveling, Lunch, break etc..
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// get or set the operative/resource id the log belongs to
        /// </summary>
        int ResourceId { get; set; }
        ///<summary>
        ///get or set the start date and time of the activity
        ///</summary>
        DateTime? StartDateTime { get; set; }
    }
}
