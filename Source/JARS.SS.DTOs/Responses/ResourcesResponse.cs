using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    /// <summary>
    /// Single record
    /// </summary>
    [DataContract]
    public class ResourceResponse
    {
        [DataMember]
        public ResourceDto Resource { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }

    /// <summary>
    /// Multiple Records
    /// </summary>
    [DataContract]
    public class ResourcesResponse
    {
        [DataMember]
        public List<ResourceDto> Resources { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }

    /// <summary>
    /// Use this to get a resource without all the bells and whistles (no skills, groups etc)
    /// similar item where there is no need for the full details of the resource.
    /// </summary>
    [DataContract]
    public class BasicResourceResponse
    {
        [DataMember]
        public BasicResourceDto Resource { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    /// <summary>
    /// Use this to get resources that are used for loading the resource tree or 
    /// similar items where there is no need for the full details of the resource.
    /// </summary>
    [DataContract]
    public class BasicResourcesResponse
    {
        [DataMember]
        public List<BasicResourceDto> Resources { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    /// <summary>
    /// Use this to get a resource without all the bells and whistles (no skills, groups etc)
    /// similar item where there is no need for the full details of the resource.
    /// </summary>
    [DataContract]
    public class MobileResourceResponse
    {
        [DataMember]
        public MobileResourceDto Resource { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    /// <summary>
    /// Use this to get resources that are used for loading the resource tree or 
    /// similar items where there is no need for the full details of the resource.
    /// </summary>
    [DataContract]
    public class MobileResourcesResponse
    {
        [DataMember]
        public List<MobileResourceDto> Resources { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
}
