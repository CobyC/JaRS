using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    /// <summary>
    /// store of many appointments
    /// </summary>
    [DataContract]
    public class StandardAppointmentsResponse
    {
        public StandardAppointmentsResponse()
        {
            Appointments = new List<StandardAppointmentDto>();
        }

        [DataMember]
        public List<StandardAppointmentDto> Appointments { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }

    /// <summary>
    /// Store on a single Appointment
    /// </summary>
    [DataContract]    
    public class StandardAppointmentResponse
    {
        public StandardAppointmentResponse()
        { }

        [DataMember]
        public StandardAppointmentDto Appointment { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
