using System;
using System.Collections.Generic;

namespace JARS.Entities
{
    /// <summary>
    /// This class represents the standard appointment that can be shown in the JaRS ui.
    /// </summary>
    [Serializable]
    public class StandardAppointment : AppointmentBase
    {
        public StandardAppointment()
        {
            //StandardAppointmentExceptions = new List<StandardAppointmentException>();
        }
      
    }
}
