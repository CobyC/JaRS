using JARS.Core;
using JARS.Core.Entities;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class ApptLabelDto : EntityBase<int>
    {

        public ApptLabelDto()
        { }

        /// <summary>
        /// The display name of the label.
        /// </summary>
        [DataMember]
        public virtual string LabelName { get; set; }

        /// <summary>
        /// Get or set the value represented by this label.
        /// useful when matching criterias in the ViewOptions.
        /// </summary>
        [DataMember]
        public virtual string LabelCriteria { get; set; }

        /// <summary>
        /// The integer representation of the RGB colour values
        /// </summary>
        [DataMember]
        public virtual int ColourRGB { get; set; }

        /// <summary>
        /// The integer representation of the RGB colour values of the foreground (text)
        /// </summary>
        [DataMember]
        public virtual int ForeColourRGB { get; set; }

        /// <summary>
        /// The name of the view type the label is used for ie. 
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
