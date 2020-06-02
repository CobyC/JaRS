using System.Collections.Generic;

namespace JARS.Core.Interfaces.Entities
{
    public interface IResourceGroup : IEntityBase<int>
    {
        /// <summary>
        /// Get or set the code that represents a group.
        /// ie. ELEC = Electrical, PLUMB= Plumbing, DOOR = door fitter. etc..
        /// </summary>
        string Code { get; set; }
        /// <summary>
        /// Is used to indicate if the group is still active or not.
        /// </summary>
        bool? IsActive { get; set; }
        /// <summary>
        /// Get or set the name of the group.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Get or set the list of workers that belong to the roup
        /// The JarsCalendarManager will use this list to generate a JarsCalendar for each user in this list/ per group
        /// see JarsCalendarManager for more details.
        /// </summary>
        IList<IResource> Resources { get; set; }
        /// <summary>
        /// Get or set the position of this group when sort is not done by name or by ID
        /// </summary>
        int? SortIndex { get; set; }
    }
}
