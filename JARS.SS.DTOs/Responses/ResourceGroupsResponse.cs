using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class ResourceGroupResponse
    {
        public ResourceGroupResponse()
        { }

        [DataMember]
        public ResourceGroupDto Group { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }


    [DataContract]
    public class ResourceGroupsResponse
    {
        public ResourceGroupsResponse()
        {
            Groups = new List<ResourceGroupDto>();
        }

        [DataMember]
        public List<ResourceGroupDto> Groups { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }

}
