using JARS.SS.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class JarsUserDto : AuditableEntityBaseDto
    {
        public JarsUserDto()
        {
            Roles = new List<string>();
            Permissions = new List<string>();
            Settings = new List<JarsSettingDto>();
        }

        [DataMember]
        public virtual string UserName { get; set; }

        [DataMember]
        public virtual string DisplayName { get; set; }

        [DataMember]
        public virtual string FirstName { get; set; }

        [DataMember]
        public virtual string LastName { get; set; }

        [DataMember]
        public virtual string Email { get; set; }

        /// <summary>
        /// This could be a short code used to identify the user within JaRS or an external system.
        /// ie.JDOE1
        /// </summary>
        [DataMember]
        public virtual string UserCode { get; set; }

        /// <summary>
        /// This is a code that can be used to identify the same person in an external system
        /// ie.JO.DOE1
        /// </summary>
        [DataMember]
        public virtual string UserCode1 { get; set; }

        ///<summary>
        /// This is a code that can be used to identify the same person in a secondary external system
        /// ie.JO_DOE2_FIN
        /// </summary>
        [DataMember]
        public virtual string UserCode2 { get; set; }
                
        [DataMember]
        public virtual bool IsActive { get; set; }
        /// <summary>
        /// This is the list of setting that a user can have within the system
        /// The layout of a certain grid or control within a particular part of the application can be added or found in this list.
        /// </summary>
        [DataMember]
        public virtual List<JarsSettingDto> Settings { get; set; }

        [DataMember]
        public virtual List<string> Roles { get; set; }

        [DataMember]
        public virtual List<string> Permissions { get; set; }

        [DataMember]
        public virtual string ApiKey { get; set; }

    }
}
