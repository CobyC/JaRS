using JARS.SS.DTOs.Base;
using ServiceStack;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{

    [Route("/jarssettings/{Id}", "GET")]
    public class GetJarsSettings : IReturn<JarsSettingsResponse>
    {
        public int Id { get; set; }
    }

    [Route("/jarssettings/find", "GET")]
    public class FindJarsSettings : IReturn<JarsSettingsResponse>
    {
        //only need user account ID? or we need to see how we can use authentication..
        //public JarsUserAccount UserAccount { get; set; }
        public int UserAccountId { get; set; }
        public string PartName { get; set; }
        public string Platform { get; set; }

    }

    [Route("/jarssettings/store", "POST")]
    public class StoreJarsSettings : StoreRequestBase, IReturn<JarsSettingsResponse>
    {
        public StoreJarsSettings()
        {
            Settings = new List<JarsSettingDto>();
        }
        public List<JarsSettingDto> Settings { get; set; }
    }

    [Route("/jarssettings/{Id}", "DELETE")]
    public class DeleteJarsSettings : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Route("/channels/notify/jarssettings", "POST")]
    public class JarsSettingsNotification : NotificationBaseDto<JarsSettingDto>, IReturnVoid
    {
        public JarsSettingsNotification()
        {
            Settings = new List<JarsSettingDto>();
        }
        public List<JarsSettingDto> Settings { get; set; }

    }
}
