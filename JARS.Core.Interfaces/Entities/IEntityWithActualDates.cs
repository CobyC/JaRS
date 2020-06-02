using System;

namespace JARS.Core.Interfaces.Entities
{
    public interface IEntityWithActualDates : IEntityBase
    {
        /// <summary>
        /// The date and time the job was actually started (this value can be recorded from external devices like mobile phones)
        /// </summary>
        DateTime? ActualStartDate { get; set; }
        /// <summary>
        /// The date and time the job was actually ended (this value can be recorded from external devices like mobile phones)
        /// </summary>
        DateTime? ActualEndDate { get; set; }

    }
}
