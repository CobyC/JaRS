using JARS.Core.Interfaces.Entities;
using System;
using System.Runtime.Serialization;

namespace JARS.Core.Entities
{

    public class BasicEntity : EntityBase<int>, IBasicEntity
    {
        private string _ExtRefID;
        string _Subject;
        private int _ResourceId;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private string _Description;

        /// <summary>
        /// The order or work reference number from the external system
        /// </summary>
        [DataMember]
        public virtual string ExtRefId
        {
            get
            {
                return _ExtRefID;
            }
            set
            {
                _ExtRefID = value;
                OnPropertyChanged(nameof(ExtRefId));
            }
        }

        /// <summary>
        /// Get or set the operative/resource linked to this job, this does not indicate the trade, only the operative
        /// </summary>
        [DataMember]
        public virtual int ResourceId
        {
            get => _ResourceId;
            set
            {
                _ResourceId = value;
                OnPropertyChanged(() => ResourceId);
            }
        }

        /// <summary>
        /// Get or set the time the job (appointment) starts.
        /// </summary>
        [DataMember]
        public virtual DateTime StartDate
        {
            get => _StartDate;
            set
            {
                _StartDate = value;
                OnPropertyChanged(() => StartDate);
            }
        }

        /// <summary>
        /// Get or set the time the job (appointment) finish.
        /// </summary>
        [DataMember]
        public virtual DateTime EndDate
        {
            get => _EndDate;
            set
            {
                _EndDate = value;
                OnPropertyChanged(() => EndDate);
            }
        }

        /// <summary>
        /// Get or set the description of the Job (appointment)
        /// </summary>
        [DataMember]
        public virtual string Description
        {
            get => _Description;
            set
            {
                _Description = value;
                OnPropertyChanged(() => Description);
            }
        }

        string _GuidValue;
        public string GuidValue
        {
            get
            {
                if (_GuidValue == null || _GuidValue == string.Empty)
                    _GuidValue = Guid.NewGuid().ToString();
                return _GuidValue;
            }
            set
            {
                _GuidValue = value;
                OnPropertyChanged(() => GuidValue);
            }
        }

        /// <summary>
        /// This subject can be assigned to the ExtRefID..
        /// </summary>
        public string Subject
        {
            get
            {
                return _Subject;
            }
            set
            {
                _Subject = value;
                OnPropertyChanged(() => Subject);
            }
        }
    }
}
