using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class JarsSettingsResponse
    {
        public JarsSettingsResponse()
        {
            Settings = new List<JarsSettingDto>();
        }
        [DataMember]
        public List<JarsSettingDto> Settings { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
