using JARS.SS.DTOs.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]  
    public class StandardAppointmentBasicDto : AppointmentBaseDto
    {
        public StandardAppointmentBasicDto()
        {}
        
        //does not contain a back reference to the exceptions again.
        //this should help with serialization.

        /// <summary>
        /// Get or Set the list of appointment exception (change in recursive appointments).
        /// </summary>
        //[DataMember]
        //public virtual List<StandardAppointmentExceptionDto> StandardAppointmentExceptions { get; set; }
    }
}
