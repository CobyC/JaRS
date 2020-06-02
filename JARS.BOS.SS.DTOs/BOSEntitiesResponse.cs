using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.BOS.SS.DTOs
{

    /// <summary>
    /// The responses will always return with a single entity as the result.    
    /// </summary>
    [DataContract]
    public class BOSEntityResponse
    {
        [DataMember]
        public virtual BOSEntityDto BOSEntity { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }

    /// <summary>
    /// This is the DTo object that is returned as a response to a request.
    /// The result is a list
    /// </summary>
    [DataContract]
    public class BOSEntitiesResponse
    {

        [DataMember]
        public virtual List<BOSEntityDto> BOSEntities { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }

    /// <summary>
    /// The responses will always return with a single mobile bos entity as the result.    
    /// </summary>
    [DataContract]
    public class MobileBOSEntityResponse
    {
        [DataMember]
        public virtual MobileBOSEntityDto BOSEntity { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }

    /// <summary>
    /// This is the DTo object that is returned as a response to a request.
    /// The result is a list
    /// </summary>
    [DataContract]
    public class MobileBOSEntitiesResponse
    {

        [DataMember]
        public virtual List<MobileBOSEntityDto> BOSEntities { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
