using JARS.Core.Entities;
using JARS.Core.Interfaces.Entities;
using System;

namespace JARS.Entities
{
    /// <summary>
    /// this class is used for recording activity from the mobile device.
    /// things like travel, collecting materials, breaks etc..
    /// </summary>
    [Serializable]
    public class ActivityLog : EntityBase<int>, IActivityLog
    {
        string _Name;
        string _Category;
        DateTime? _StartDateTime;
        DateTime? _EndDateTime;
        string _LinkedId;
        string _LinkedType;
        int _ResourceId;

        /// <summary>
        /// Get or Set a name to indicate the kind of activity. ie Traveling, Lunch, break etc..
        /// </summary>

        public virtual string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged(() => Name);
            }
        }

        /// <summary>
        /// Get or set the category the activity belongs to ie, ActiveTime, SickCover, HolidayCover, MobileActions etc..
        /// </summary>

        public virtual string Category
        {
            get
            {
                return _Category;
            }
            set
            {
                _Category = value;
                OnPropertyChanged(() => Category);
            }
        }


        ///<summary>
        ///get or set the start date and time of the activity
        ///</summary>

        public virtual DateTime? StartDateTime
        {
            get
            {
                return _StartDateTime;
            }
            set
            {
                _StartDateTime = value;
                OnPropertyChanged(() => StartDateTime);
            }
        }

        /// <summary>
        /// Get or set the end time of the activity
        /// </summary>

        public virtual DateTime? EndDateTime
        {
            get
            {
                return _EndDateTime;
            }
            set
            {
                _EndDateTime = value;
                OnPropertyChanged(() => EndDateTime);
            }
        }

        /// <summary>
        /// Set an external reference number or any other reference used to link this to an activity
        /// </summary>

        public virtual string LinkedId
        {
            get
            {
                return _LinkedId;
            }
            set
            {
                _LinkedId = value;
                OnPropertyChanged(() => LinkedId);
            }
        }

        /// <summary>
        /// This is to indicate what entity type (typeof(ent)).name this activity links to.
        /// </summary>
        public virtual string LinkedType
        {
            get
            {
                return _LinkedType;
            }
            set
            {
                _LinkedType = value;
                OnPropertyChanged(() => LinkedType);
            }
        }

        /// <summary>
        /// get or set the operative/resource id the log belongs to
        /// </summary>

        public virtual int ResourceId
        {
            get
            {
                return _ResourceId;
            }
            set
            {
                _ResourceId = value;
                OnPropertyChanged(() => ResourceId);
            }
        }

    }
}
