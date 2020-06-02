using JARS.Core.Entities;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    /// <summary>
    /// The default appointment class represents a preset appointment.
    /// This class will be transformed into a standard appointment, and does not really have any other purpose apart from creating default standard appointments
    /// </summary>
    /// 
    [DataContract]    
    public class JarsDefaultAppointmentDto : EntityBase<int>
    {
        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public double DefaultDuration { get; set; }

        [DataMember]
        public bool IsAllDay { get; set; }

        [DataMember]
        public bool ShowOnMobile { get; set; }
    }
}
