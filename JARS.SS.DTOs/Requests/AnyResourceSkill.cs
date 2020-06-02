using JARS.SS.DTOs.Base;
using ServiceStack;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{

    [Route("/resourceskills/{Id}", "GET")]
    public class GetResourceSkill : IReturn<ResourceSkillsResponse>
    {
        public int Id { get; set; }
    }

    [Route("/resourceskills/find", "GET")]
    public class FindResourceSkills : IReturn<ResourceSkillsResponse>
    {
        public string ViewType { get; set; }
        public string SkillName { get; set; }
        public int ColourRGB { get; set; }
    }

    [Route("/resourceskills/store", "POST")]
    public class StoreResourceSkills : StoreRequestBase, IReturn<ResourceSkillsResponse>
    {
        public StoreResourceSkills()
        {
            Skills = new List<ResourceSkillDto>();
        }

        public List<ResourceSkillDto> Skills { get; set; }
    }

    [Route("/resourceskills/{Id}", "DELETE")]
    public class DeleteResourceSkill : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Route("/channels/notify/resourceskills", "GET")]
    public class ResourceSkillsNotification : NotificationBaseDto<ResourceSkillDto>, IReturnVoid
    {
        public ResourceSkillsNotification()
        {
            Skills = new List<ResourceSkillDto>();
        }
        public List<ResourceSkillDto> Skills { get; set; }

    }
}
