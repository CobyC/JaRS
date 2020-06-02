using JARS.Core.Entities;
using System;

namespace JARS.Entities
{
    /// <summary>
    /// The default appointment class represents a preset appointment.
    /// This class will be transformed into a standard appointment, and does not really have any other purpose apart from creating default standard appointments
    /// </summary>
    /// 
    [Serializable]
    public class JarsDefaultAppointment : EntityBase<int>
    {
        public virtual string Subject { get; set; }

        public virtual string Description { get; set; }

        public virtual double DefaultDuration { get; set; }

        public virtual bool IsAllDay { get; set; }

        public virtual bool ShowOnMobile { get; set; }

    }
}
