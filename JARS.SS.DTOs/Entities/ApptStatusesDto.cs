using JARS.Core;
using JARS.Core.Entities;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class ApptStatusDto : EntityBase<int>
    {
        public ApptStatusDto()
        { }


        /// <summary>
        /// The display name of the status.
        /// </summary>
        [DataMember]
        public virtual string StatusName { get; set; }

        /// <summary>
        /// Get or set the criteria used to match with this status.
        /// this value is build up using the filter control
        /// </summary>
        [DataMember]
        public virtual string StatusCriteria { get; set; }

        /// <summary>
        /// The integer number representing the RGB colour values.
        /// </summary>
        [DataMember]
        public virtual int ColourRGB { get; set; }

        /// <summary>
        /// The name of the view type the status is used for ie. 
        /// The default view name is DEFAULT (as specified in the view option Plugin).        
        /// </summary>
        [DataMember]
        public virtual string ViewName { get; set; }

        /// <summary>
        /// The position this item will be in a list. ie in a dropdown that is orders by index and not name
        /// </summary>
        [DataMember]
        public virtual int SortIndex { get; set; }

        /// <summary>
        /// The name of the Interface this label will expect the matching entity will implement.
        /// </summary>
        [DataMember]
        public virtual string UseInterfaceType { get; set; }
    }
}
