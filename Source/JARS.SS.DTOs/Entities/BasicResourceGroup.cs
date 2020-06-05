using JARS.SS.DTOs.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    /// <summary>
    /// The basic classes does not contain any of the child relations.
    /// i.e. this resource group class does not contain any reference to the resource it might be linked to.
    /// to get the class with the resource, the <see cref="ResourceGroupDto"/> can be used.
    /// </summary>
    [DataContract]
    public class BasicResourceGroupDto : AuditableEntityBaseDto//, IResourceGroup
    {
        public BasicResourceGroupDto()
        {            
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
    }
}
