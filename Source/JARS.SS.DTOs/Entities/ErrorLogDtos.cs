using JARS.Core.Entities;
using System;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class ErrorLogDto : EntityBase<int>
    {
        public ErrorLogDto()
        { }

        /// <summary>
        /// The name of the user(logged in windows user) associated with the error
        /// </summary>
        [DataMember]
        public virtual string EnvironmentUserName { get; set; }

        /// <summary>
        /// Error text
        /// </summary>
        [DataMember]
        public virtual string ErrorText { get; set; }

        /// <summary>
        /// The time the error occurred.
        /// </summary>
        [DataMember]
        public virtual DateTime? ErrorTime { get; set; }

        /// <summary>
        /// The type of error that was caught.
        /// </summary>
        [DataMember]
        public virtual string ErrorType { get; set; }       
    }
}
