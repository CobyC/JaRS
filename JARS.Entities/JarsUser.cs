using JARS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JARS.Entities
{
    /// <summary>
    /// This class represents a user account in JaRS.
    /// These are accounts linked to people using he JaRS application.    
    /// </summary>  
    /// 
    [Serializable]
    public class JarsUser : JarsUserBase
    {
        public JarsUser()
        {           
            Settings = new List<JarsSetting>();
        }
        
        private IList<JarsSetting> _Settings;
        /// <summary>
        /// This is the list of setting that a user can have within the system
        /// The layout of a certain grid or control within a particular part of the application can be added or found in this list.
        /// </summary>
        public virtual IList<JarsSetting> Settings
        {
            get => _Settings;
            set
            {
                _Settings = value;
                OnPropertyChanged(() => Settings);
            }
        }
                
        public virtual string ApiKey { get; set; }
    }
}
