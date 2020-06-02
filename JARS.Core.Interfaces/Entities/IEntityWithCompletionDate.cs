using System;

namespace JARS.Core.Interfaces.Entities
{
    public interface IEntityWithCompletionDate : IEntityBase
    {/// <summary>
     /// Get or set the date and time the job was completed on
     /// </summary>
        DateTime? CompletionDate { get; set; }
    }
}
