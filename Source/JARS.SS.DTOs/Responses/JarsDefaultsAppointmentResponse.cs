using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    /// <summary>
    /// This response is gets returned from when a single default appointment records needs updating
    /// </summary>
    [DataContract]
    public class JarsDefaultAppointmentResponse
    {
        [DataMember]
        public JarsDefaultAppointmentDto Appointment { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }

    /// <summary>
    /// This response is gets returned from when multiple default appointment records needs updating
    /// </summary>
    [DataContract]
    public class JarsDefaultAppointmentsResponse
    {        
        [DataMember]        
        public List<JarsDefaultAppointmentDto> Appointments { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
