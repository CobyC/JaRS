using System;

namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface indicates that the implementing object needs to have a properties related to auditing.
    /// Auditing is when the record has been created and modified, it also keeps track of who made the last modification.
    /// </summary>
    public interface IEntityWithAudit: IEntityBase
    {
        /// <summary>
        /// Get or set the created on date (will default to the current date time if not specified)
        /// </summary>        
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// Get or set the created by user (default is null if not assigned)
        /// </summary>        
        string CreatedBy { get; set; }

        /// <summary>
        /// The last date and time the record was modified on.
        /// </summary>        
        DateTime ModifiedDate { get; set; }

        /// <summary>
        /// get or set who the record was last modified by.
        /// </summary>        
        string ModifiedBy { get; set; }
    }
}
