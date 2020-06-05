using JARS.Core.Entities;
using JARS.Core.Interfaces.Rules.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace JARS.Entities
{

    /// <summary>
    /// This class represent a operative/resource available in the JaRS system.
    /// Each operative/resource will have a calendar in the JaRS UI
    /// </summary>
    [AllowRuleProcessing]
    [Serializable]
    public class JarsResource : AuditableEntityBase//, IResource
    {
        public JarsResource()
        {
            this._Groups = new List<JarsResourceGroup>();
            this._Skills = new List<JarsResourceSkill>();
        }

        private string _ExtRef1;
        private string _ExtRef2;
        private string _FirstName;
        private string _LastName;
        private string _DisplayName;
        private string _ExtRef;
        private string _VehicleRegistration;
        private string _EMail;
        private string _MobileNo;
        private string _Memo;
        private TimeSpan? _DayStartTime;
        private TimeSpan? _DayEndTime;
        private string _DayStartLocation;
        private string _LastRecordedLocation;
        private bool? _IsMobileResource;
        private bool _IsActive;
        private int _SortIndex;


        /// <summary>
        /// This is an additional reference value that can be used to identify the operative/resource in an external system.
        /// </summary>
         
        public virtual string ExtRef1
        {
            get
            { return _ExtRef1; }
            set
            {
                _ExtRef1 = value;
                OnPropertyChanged(() => ExtRef1);
            }
        }

        /// <summary>
        /// This is a secondary value that can be used to identify a operative/resource in an external system
        /// </summary>
         
        public virtual string ExtRef2
        {
            get
            { return _ExtRef2; }
            set
            {
                _ExtRef2 = value;
                OnPropertyChanged(() => ExtRef2);
            }
        }


        /// <summary>
        /// The first name of the operative/resource.
        /// </summary>
         
        public virtual string FirstName
        {
            get
            { return _FirstName; }
            set
            {
                _FirstName = value;
                OnPropertyChanged(() => FirstName);
            }
        }

        /// <summary>
        /// The last name of the operative/resource.
        /// </summary>
         
        public virtual string LastName
        {
            get
            { return _LastName; }
            set
            {
                _LastName = value;
                OnPropertyChanged(() => LastName);
            }
        }

        /// <summary>
        /// The display name of the operative/resource on the JaRS UI.
        /// </summary>
         
        public virtual string DisplayName
        {
            get
            { return _DisplayName; }
            set
            {
                _DisplayName = value;
                OnPropertyChanged(() => DisplayName);
            }
        }

        /// <summary>
        /// This is an unique external reference that will be used by the system to identify/link the operative/resource to another system.        
        /// </summary>
         
        public virtual string ExtRef
        {
            get
            { return _ExtRef; }
            set
            {
                _ExtRef = value;
                OnPropertyChanged(() => ExtRef);
            }
        }

        /// <summary>
        /// The vehicle registration linked to this operative/resource, usually linked for vehicle inspections, or tracking stock in a van.
        /// </summary>
         
        public virtual string VehicleRegistration
        {
            get
            { return _VehicleRegistration; }
            set
            {
                _VehicleRegistration = value;
                OnPropertyChanged(() => VehicleRegistration);
            }
        }

        /// <summary>
        /// The email for the particular operative/resource. can be used for sending emails.
        /// </summary>
         
        public virtual string eMail
        {
            get
            { return _EMail; }
            set
            {
                _EMail = value;
                OnPropertyChanged(() => eMail);
            }
        }

        /// <summary>
        /// The main contact number for the operative/resource.
        /// </summary>
         
        public virtual string MobileNo
        {
            get
            { return _MobileNo; }
            set
            {
                _MobileNo = value;
                OnPropertyChanged(() => MobileNo);
            }
        }


        public virtual string Memo
        {
            get
            { return _Memo; }
            set
            {
                _Memo = value;
                OnPropertyChanged(() => Memo);
            }
        }

        /// <summary>
        /// The default time of day the operative/resource starts working.
        /// </summary>
         
        public virtual TimeSpan? DayStartTime
        {
            get
            { return _DayStartTime; }
            set
            {
                _DayStartTime = value;
                OnPropertyChanged(() => DayStartTime);
            }
        }

        /// <summary>
        /// the default time the operative/resource stops working.
        /// </summary>
         
        public virtual TimeSpan? DayEndTime
        {
            get
            { return _DayEndTime; }
            set
            {
                _DayEndTime = value;
                OnPropertyChanged(() => DayEndTime);
            }
        }

        /// <summary>
        /// The default starting location of the resource.
        /// this could be something line a log.lat code or a description.
        /// </summary>
         
        public virtual string DayStartLocation
        {
            get
            { return _DayStartLocation; }
            set
            {
                _DayStartLocation = value;
                OnPropertyChanged(() => DayStartLocation);
            }
        }

        /// <summary>
        /// This is the last known location of the resource.
        /// this will probably be a log.lat value.
        /// </summary>
         
        public virtual string LastRecordedLocation
        {
            get
            { return _LastRecordedLocation; }
            set
            {
                _LastRecordedLocation = value;
                OnPropertyChanged(() => LastRecordedLocation);
            }
        }

        /// <summary>
        /// Indicates if the operative/resource has a mobile device, or if it is just a system resource.
        /// </summary>
         
        public virtual bool? IsMobileResource
        {
            get
            { return _IsMobileResource; }
            set
            {
                _IsMobileResource = value;
                OnPropertyChanged(() => IsMobileResource);
            }
        }

        /// <summary>
        /// Indicates if the operative/resource is active, this can also be used to show or draw the operative/resource calendar.
        /// </summary>
         
        public virtual bool IsActive
        {
            get
            { return _IsActive; }
            set
            {
                _IsActive = value;
                OnPropertyChanged(() => IsActive);
            }
        }

        /// <summary>
        /// This can be used to sort the item in a list.
        /// </summary>
         
        public virtual int SortIndex
        {
            get
            { return _SortIndex; }
            set
            {
                _SortIndex = value;
                OnPropertyChanged(() => SortIndex);
            }
        }

      
        //List<ResourceSkill> _SkillsList;
        private IList<JarsResourceSkill> _Skills;
        /// <summary>
        /// This list of skills can be used to determine how skillful an operative/resource is.
        /// it is used to calculate the amount of time an operative/resource will spend on a particular type of job.
        /// </summary>
         
        public virtual IList<JarsResourceSkill> Skills
        {
            get
            {
                //if (_SkillsList == null)
                //    _SkillsList = (_Skills == null) ? new List<ResourceSkill>() : new List<ResourceSkill>(_Skills);
                return _Skills;
            }
            set
            {
                _Skills = value;
                OnPropertyChanged(() => Skills);
            }
        }

        //IList<ResourceGroup> _GroupsList;
        private IList<JarsResourceGroup> _Groups;
        /// <summary>
        /// The list of groups this operative/resource belongs to
        /// </summary>
         
        public virtual IList<JarsResourceGroup> Groups
        {
            get
            {
                //if (_GroupsList == null)
                //    _GroupsList = (_Groups == null) ? new List<ResourceGroup>() : new List<ResourceGroup>(_Groups);
                return _Groups;
            }
            set
            {
                _Groups = value;
                OnPropertyChanged(() => Groups);
            }
        }



        //TimeSpan _StdAvailabilityPerDay;
        ///// <summary>
        ///// Gets the total amount of time the operative/resource is available in a standard day
        ///// </summary>
        ////[NotMapped]
        // 
        //public virtual TimeSpan StdAvailabilityPerDay
        //{
        //    get
        //    {
        //        if (_StdAvailabilityPerDay == null)
        //            _StdAvailabilityPerDay = TimeSpan.Zero;

        //        if (DayStartTime.HasValue && DayEndTime.HasValue && _StdAvailabilityPerDay == TimeSpan.Zero)
        //        {
        //            _StdAvailabilityPerDay = DayEndTime.Value.Subtract(DayStartTime.Value);
        //        }

        //        return _StdAvailabilityPerDay;
        //    }
        //}

        ///// <summary>
        ///// This indicates the average time the worker is available per group.
        ///// If the operative belongs to 3 groups this time would be the value of StandardHours divided by number of groups.
        ///// If no group or 1 group the result will be the same as StandardDayHours
        ///// </summary>
        ////[NotMapped]
        // 
        //public virtual TimeSpan AvgAvailabilityInGroup
        //{
        //    get
        //    {
        //        if (StdAvailabilityPerDay != TimeSpan.Zero && Groups.Count > 0)
        //            return new TimeSpan((StdAvailabilityPerDay.Ticks / Groups.Count));
        //        else
        //            return StdAvailabilityPerDay;
        //    }
        //}

        public virtual string GenerateDisplayName(bool includeExtRef, bool IncludeFirstName, bool IncludeLastName)
        {
            StringBuilder sb = new StringBuilder();
            if (includeExtRef)
                sb.Append(ExtRef).Append(" ");

            if (IncludeFirstName)
                sb.Append(FirstName).Append(" ");

            if (IncludeLastName)
                sb.Append(LastName);

            return sb.ToString();
        }

    }
}
