using JARS.SS.DTOs.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class ResourceGroupDto : AuditableEntityBaseDto//, IResourceGroup
    {
        public ResourceGroupDto()
        {
            _Resources = new List<BasicResourceDto>();
        }

        /// <summary>
        /// Get or set the name of the group.
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// Get or set the code that represents a group.
        /// ie. ELEC = Electrical, PLUMB= Plumbing, DOOR = door fitter. etc..
        /// </summary>
        [DataMember]
        public virtual string Code { get; set; }
        /// <summary>
        /// Get or set the position of this group when sort is not done by name or by ID
        /// </summary>
        [DataMember]
        public virtual int? SortIndex { get; set; }

        /// <summary>
        /// Is used to indicate if the group is still active or not.
        /// </summary>
        [DataMember]
        public virtual bool? IsActive { get; set; }

        private IList<BasicResourceDto> _Resources;
        /// <summary>
        /// Get or set the list of workers that belong to the roup
        /// The JarsCalendarManager will use this list to generate a JarsCalendar for each user in this list/ per group
        /// see JarsCalendarManager for more details.
        /// NOTE!! that this class is the -basic- skill class (to prevent circular references when serializing.)
        /// </summary>        
        [DataMember]
        public virtual IList<BasicResourceDto> Resources { get; set; }
    }
}
