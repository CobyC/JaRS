using JARS.Core.Entities;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    /// <summary>
    /// This class is used for holding settings that relate to a certain user account.
    /// </summary>
    [DataContract]
    public class JarsSettingDto : EntityBase<int>
    {
        public JarsSettingDto()
        { }
      
        /// <summary>
        /// Get or set the platform the settings needs to be applied to, ie Web, Mobile, Desktop
        /// </summary>
        [DataMember]
        public virtual string Platform { get; set; }

        /// <summary>
        /// The name of the setting record, this can be used to retrieve settings for a particular part of the system.
        /// ie. 'ExternalGridLayout'
        /// </summary>
        [DataMember]
        public virtual string PartName { get; set; }

        /// <summary>
        /// The binary representation of the settings that needs to be applied/saved
        /// depending on how the settings are saved to/from the system, this byte array will have to be serialized/deserialized accordingly
        /// </summary>
        [DataMember]
        public virtual byte[] SettingData { get; set; }

    }
}
