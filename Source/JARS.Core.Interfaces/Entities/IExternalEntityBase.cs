using System;

namespace JARS.Core.Interfaces.Entities
{

    public interface IExternalEntityBase<TKeyType> : IEntityBase<TKeyType>, IEntityWithExternalReference, IEntityWithLineOfWork, IEntityWithLocation, IEntityWithPriority, IEntityWithTargetDate
        where TKeyType : struct
    {

        /// <summary>
        /// A long description of the work that needs to be carried out
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The estimate duration of the time (in hours) the job/work might take to complete.
        /// </summary>
        double Duration { get; set; }

        /// <summary>
        /// The date time stamp for when this record was last modified in the external system.
        /// </summary>
        DateTime? RecordLastModifiedOn { get; set; }
    }
}
