using JARS.Core.Interfaces.Entities;
using System;
using System.Runtime.Serialization;

namespace JARS.Core.Entities
{
    /// <summary>
    /// This class is the base entity class that will be inherited by all classes in the system.
    /// it is marked as abstract because we dont need a table created for this class.
    /// </summary>    
    [DataContract]
    public abstract class AuditableEntityBase : EntityBase<int>, IEntityWithAudit
    {
        public AuditableEntityBase()
        { }

        private DateTime _CreatedDate;
        /// <summary>
        /// Get or set the created on date (will default to the current date time if not specified)
        /// </summary>
        [DataMember]
        public virtual DateTime CreatedDate
        {
            get
            {
                if (_CreatedDate == null || _CreatedDate == DateTime.MinValue)
                    _CreatedDate = DateTime.Now;
                return _CreatedDate;
            }
            set
            {
                _CreatedDate = value;
                OnPropertyChanged(() => CreatedDate);
            }
        }

        string _CreatedBy;
        /// <summary>
        /// Get or set the for who or what the record was created by
        /// </summary>
        [DataMember]
        public virtual string CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                _CreatedBy = value;
                OnPropertyChanged(() => CreatedBy);
            }
        }

        private DateTime _ModifiedDate;
        /// <summary>
        /// The last date and time the record was modified on.
        /// </summary>
        [DataMember]
        public virtual DateTime ModifiedDate
        {
            get
            {
                if (_ModifiedDate == null || _ModifiedDate == DateTime.MinValue)
                    _ModifiedDate = DateTime.Now;
                return _ModifiedDate;
            }
            set
            {
                _ModifiedDate = value;
                OnPropertyChanged(() => ModifiedDate);
            }
        }

        string _ModifiedBy;
        /// <summary>
        /// get or set who the record was last modified by.
        /// </summary>
        [DataMember]
        public virtual string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                _ModifiedBy = value;
                OnPropertyChanged(() => ModifiedBy);
            }
        }

    }
}
