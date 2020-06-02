using System;

namespace JARS.Entities
{
    /// <summary>
    /// This class represents the exception in a list of recursive appointments.
    /// </summary>
    [Serializable]
    public class StandardAppointmentException : AppointmentBase
    {
        public StandardAppointmentException()
        { }

        /// <summary>
        /// Get or set the type of appointment  (see DevExpress documentation)
        /// </summary>
         
        public virtual int PaternType { get; set; }

        /// <summary>
        /// The standard appointment this exception belongs to
        /// </summary>
         
        public virtual StandardAppointment StandardAppointment { get; set; }
    }
}
