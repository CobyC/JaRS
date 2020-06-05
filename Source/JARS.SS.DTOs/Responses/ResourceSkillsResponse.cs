using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class ResourceSkillsResponse
    {
        public ResourceSkillsResponse()
        {
            Skills = new List<ResourceSkillDto>();
        }
        [DataMember]
        public List<ResourceSkillDto> Skills { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
