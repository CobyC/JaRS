using JARS.Core.Entities;
using System;

namespace JARS.Entities
{
    /// <summary>
    /// This class is used for holding settings that relate to a certain user account.
    /// </summary>
    [Serializable]
    public class JarsSetting : EntityBase<int>
    {
        public JarsSetting()
        { }

       // private JarsUserAccount _UserAccount;
        private string _PartName;
        private byte[] _SettingData;
        private string _Platform;


        /// <summary>
        /// Get or set the platform the settings needs to be applied to, ie Web, Mobile, Desktop
        /// </summary>
         
        public virtual string Platform
        {
            get
            { return _Platform; }
            set
            {
                _Platform = value;
                OnPropertyChanged(() => Platform);
            }
        }

        /// <summary>
        /// The name of the setting record, this can be used to retrieve settings for a particular part of the system.
        /// ie. 'ExternalGridLayout'
        /// </summary>
         
        public virtual string PartName
        {
            get => _PartName;
            set
            {
                _PartName = value;
                OnPropertyChanged(() => PartName);
            }
        }

        /// <summary>
        /// The binary representation of the settings that needs to be applied/saved
        /// depending on how the settings are saved to/from the system, this byte array will have to be serialized/deserialized accordingly
        /// </summary>
         
        public virtual byte[] SettingData
        {
            get => _SettingData;
            set
            {
                _SettingData = value;
                OnPropertyChanged(() => SettingData);
            }
        }

    }
}
