using JARS.Core.Entities;
using JARS.Core.Interfaces.Entities;
using System;

namespace JARS.Entities
{
    /// <summary>
    /// This class represents the base structure for an appointable entity.
    /// The TPrimaryKeyType is passed to the EntityBase class that this class inherits from
    /// </summary>
    [Serializable]    
    public abstract class AppointableBase<TPrimaryKeyType> : EntityBase<TPrimaryKeyType>, IEntityWithAppointing
    {

        private DateTime _StartDate;
        private DateTime _EndDate;
        private string _Subject;
        private string _Description;
        private int _ResourceId;
        private string _GuidValue;

       
        public virtual string GuidValue
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

       
        public virtual string Subject
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

       
        public virtual string Description
        {
            get => _Description;
            set
            {
                _Description = value;
                OnPropertyChanged(() => Description);
            }
        }

       
        public virtual int ResourceId
        {
            get => _ResourceId;
            set
            {
                _ResourceId = value;
                OnPropertyChanged(() => ResourceId);
            }
        }

         
        public virtual DateTime StartDate
        {
            get => _StartDate;
            set
            {
                _StartDate = value;
                OnPropertyChanged(() => StartDate);
            }
        }

         
        public virtual DateTime EndDate
        {
            get => _EndDate;
            set
            {
                _EndDate = value;
                OnPropertyChanged(() => EndDate);
            }
        }

    }
}
