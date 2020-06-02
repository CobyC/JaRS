using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]
    public class StandardAppointmentExceptionsResponse
    {
        public StandardAppointmentExceptionsResponse()
        {
            AppointmentExceptions = new List<StandardAppointmentExceptionDto>();
        }
        [DataMember]
        public StandardAppointmentExceptionDto AppointmentException { get; set; }

        [DataMember]
        public List<StandardAppointmentExceptionDto> AppointmentExceptions { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
