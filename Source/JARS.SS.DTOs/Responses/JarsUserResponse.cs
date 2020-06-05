using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    //[Authenticate]

    /// <summary>
    /// Respond with single record
    /// </summary>
    [DataContract]
    //[Authenticate]
    public class JarsUserResponse
    {
        public JarsUserResponse()
        { }

        [DataMember]
        public JarsUserDto UserAccount { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }

    /// <summary>
    /// Response with multiple records
    /// </summary>
    [DataContract]
    //[Authenticate]
    public class JarsUsersResponse
    {
        public JarsUsersResponse()
        {
            UserAccounts = new List<JarsUserDto>();
        }

        [DataMember]
        public List<JarsUserDto> UserAccounts { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }

    [DataContract]
    //[Authenticate]
    public class ResetJarsUserPasswordResponse
    {
        public ResetJarsUserPasswordResponse()
        { }

        [DataMember]
        public bool ResetSuccess { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
