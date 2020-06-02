using JARS.SS.DTOs.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    [DataContract]  
    public class StandardAppointmentDto : AppointmentBaseDto
    {
        public StandardAppointmentDto()
        {
            //StandardAppointmentExceptions = new List<StandardAppointmentExceptionDto>();
        }
        
        ///// <summary>
        ///// Get or Set the list of appointment exception (change in recursive appointments).
        ///// </summary>
        //[DataMember]
        //public virtual List<StandardAppointmentExceptionDto> StandardAppointmentExceptions { get; set; }
    }
}
