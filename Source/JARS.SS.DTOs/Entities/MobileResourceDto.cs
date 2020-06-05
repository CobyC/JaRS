
using JARS.Core.Entities;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    /// <summary>
    /// The basic classes does not contain any of the child relations.
    /// i.e. this resource class does not contain any skills or groups.
    /// to get the class with the skills and groups, the <see cref="ResourceDto"/> can be used.
    /// </summary>
    [DataContract]
    public class MobileResourceDto : EntityBaseDto<int>
    {
        public MobileResourceDto()
        {
        }

        /// <summary>
        /// This is an unique external reference that will be used by the system to identify/link the operative/resource to another system.        
        /// </summary>
        [DataMember]
        public virtual string ExtRef { get; set; }

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
        /// This is the last known location of the resource.
        /// this will probably be a log.lat value.
        /// </summary>
        [DataMember]
        public virtual string LastRecordedLocation { get; set; }

    }
}
