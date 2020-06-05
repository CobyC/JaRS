using JARS.Core.Entities;
using JARS.Core.Interfaces.Entities;
using System;
using System.Text.RegularExpressions;

namespace JARS.Entities
{
    /// <summary>
    /// This is the default job class, it contain all properties used in the JaRS system.
    /// To extend this class inherit from it and add the additional properties to the parent class.
    /// </summary>
    [Serializable]
    public abstract class JarsJobBase : EntityBase<int>,//the base entity required by all entities 
        IEntityWithAppointing, //Enforces the properties used for by an appointment (start, End, Description, ResourceId)
        IEntityWithExternalReference, //Enforces the ExtRefId property - should be the reference to the external system ID.
        IEntityWithStatusLabels, // Enforces the properties for holding the status and label key values (useful in view options)
        IEntityWithLocation, // Enforces the Location property, will be used as appointment location and useful in view options)
        IEntityWithLineOfWork, // Enforces the LineOfWork property, useful in view options and processors
        IEntityWithProgressStatus, // Enforces the progress status property, this indicates the progress on an entity (NEW, COMP, WAITING) and useful in view options.
        IEntityWithPriority, // Enforces the Priority property, useful in processors, view options and general priority indication.
        IEntityWithAudit, //Enforces the properties for keeping track of auditing, when a record was created, who created it and when it was last modified.
        IEntityWithIntegration, // Enforces the properties used for integrating into other systems, integration status, date, message
        IEntityWithIsActive, // Enforces the IsActive property, useful for indicating if an entity is still active or not.
        IEntityWithTargetDate,// Enforces the TargetDate Property, useful for holding a target date property
        IEntityWithShowOnMobile, // Enforces a property that indicates if the appointment will be sent to a mobile device.
        IEntityWithCompletionDate // Enforces a property that indicates a completion Date.
    {

        public JarsJobBase()
        {
            ProgressStatus = "NEW";
            ShowOnMobile = true;
            StatusKey = "1";
            LabelKey = "1";
        }

        private int? _CopyOfId; //this is for the use of jobs booked over multiple days.

        private DateTime _StartDate;
        private DateTime _EndDate;
        private string _StatusKey;
        private string _LabelKey;

        private string _Location;
        string _LocationCode;
        private string _Description;
        private string _ExtRefId;
        private string _LineOfWork;
        private string _Priority;

        private DateTime? _ActualStartDateTime;
        private DateTime? _ActualEndDateTime;
        private string _ProgressStatus;
        private DateTime? _CompletionDate;
        private DateTime? _TargetDate;
        private string _GuidValue;

        int _IntegrationStatus;
        DateTime? _IntegrationDate;
        string _IntegrationMessage;

        string _Subject;
        int _ResourceId;

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

        /// <summary>
        /// Get or set the reference of the reference number on the external system. (works order).
        /// </summary>

        public virtual string ExtRefId
        {
            get => _ExtRefId;
            set
            {
                _ExtRefId = value;
                OnPropertyChanged(() => ExtRefId);
            }
        }

        /// <summary>
        /// This will need to be assigned the value of the main job ID. 
        /// This is to help with jobs running longer than a day.
        /// </summary>

        public virtual int? CopyOfId
        {
            get => _CopyOfId;
            set
            {
                _CopyOfId = value;
                OnPropertyChanged(() => CopyOfId);
            }
        }

        /// <summary>
        /// Get or set the time the job (appointment) starts.
        /// </summary>

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
        /// The date and time the job was actually started (this value can be recorded from external devices like mobile phones)
        /// </summary>

        public virtual DateTime? ActualStartDate
        {
            get => _ActualStartDateTime;
            set
            {
                _ActualStartDateTime = value;
                OnPropertyChanged(() => ActualStartDate);
            }
        }
        /// <summary>
        /// Get or set the time the job (appointment) finish.
        /// </summary>

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
        /// The date and time the job was actually ended (this value can be recorded from external devices like mobile phones)
        /// </summary>

        public virtual DateTime? ActualEndDate
        {
            get => _ActualEndDateTime;
            set
            {
                _ActualEndDateTime = value;
                OnPropertyChanged(() => ActualEndDate);
            }
        }
        /// <summary>
        /// Get or set the Status of the appointment this jobs represents in the DevExpress controls (if using DevExpress)
        /// </summary>

        public virtual string StatusKey
        {
            get => _StatusKey;
            set
            {
                _StatusKey = value;
                OnPropertyChanged(() => StatusKey);
            }
        }

        /// <summary>
        /// Get or set the Label of the appointment this jobs represents in the DevExpress controls (if using DevExpress)
        /// </summary>

        public virtual string LabelKey
        {
            get => _LabelKey;
            set
            {
                _LabelKey = value;
                OnPropertyChanged(() => LabelKey);
            }
        }


        /// <summary>
        /// Set or set the line of work (could be like a trade code) for the job.
        /// </summary>

        public virtual string LineOfWork
        {
            get
            {
                return _LineOfWork;
            }
            set
            {
                _LineOfWork = value;
            }
        }

        /// <summary>
        /// Get or set the description of the Job (appointment)
        /// </summary>

        public virtual string Description
        {
            get => _Description;
            set
            {
                _Description = value;
                OnPropertyChanged(() => Description);
            }
        }

        /// <summary>
        /// Gets or sets the Address/location of the Job 
        /// </summary>

        public virtual string Location
        {
            get => _Location;
            set
            {
                _Location = value;
                OnPropertyChanged(() => Location);
            }
        }


        public virtual string LocationCode
        {
            get
            {
                if (Location != string.Empty && Location != null)
                {
                    Regex regex = new Regex("(([gG][iI][rR] {0,}0[aA]{2})|(([aA][sS][cC][nN]|[sS][tT][hH][lL]|[tT][dD][cC][uU]|[bB][bB][nN][dD]|[bB][iI][qQ][qQ]|[fF][iI][qQ][qQ]|[pP][cC][rR][nN]|[sS][iI][qQ][qQ]|[iT][kK][cC][aA]) {0,}1[zZ]{2})|((([a-pr-uwyzA-PR-UWYZ][a-hk-yxA-HK-XY]?[0-9][0-9]?)|(([a-pr-uwyzA-PR-UWYZ][0-9][a-hjkstuwA-HJKSTUW])|([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y][0-9][abehmnprv-yABEHMNPRV-Y]))) {0,}[0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2}))");
                    Match match = regex.Match(Location);
                    _LocationCode = match.Value;
                }
                return _LocationCode;
            }
        }

        /// <summary>
        /// Get or Set the status or process state of the job ie 'NEW', 'ASSIGNED', 'INPROGRESS', whatever the state of the job is
        /// </summary>te        
        public virtual string ProgressStatus
        {
            get => _ProgressStatus;
            set
            {
                _ProgressStatus = value;
                OnPropertyChanged(() => ProgressStatus);
            }
        }

        /// <summary>
        /// Get or set the date and time the job was completed on
        /// </summary>

        public virtual DateTime? CompletionDate
        {
            get => _CompletionDate;
            set
            {
                _CompletionDate = value;
                OnPropertyChanged(() => CompletionDate);
            }
        }

        /// <summary>
        /// The date the job was originally targeted for to be completed
        /// </summary>

        public virtual DateTime? TargetDate
        {
            get => _TargetDate;

            set
            {
                _TargetDate = value;
                OnPropertyChanged(() => TargetDate);
            }
        }


        /// <summary>
        /// Get or set the priority for the job
        /// </summary>

        public virtual string Priority
        {
            get
            {
                return _Priority;
            }
            set
            {
                _Priority = value;
                OnPropertyChanged(() => Priority);
            }
        }

        /// <summary>
        /// this column is intended to be used by QX(if used), Get or set this to check the status or trigger QX (or whatever process). 
        /// </summary>           
        public virtual int IntegrationStatus
        {
            get => _IntegrationStatus;
            set
            {
                _IntegrationStatus = value;
                OnPropertyChanged(() => IntegrationStatus);
            }
        }
        /// <summary>
        /// This column is intended for use by SX , Get or Set when the job was last run through a script or process.
        /// </summary>

        public virtual DateTime? IntegrationDate
        {
            get => _IntegrationDate;
            set
            {
                _IntegrationDate = value;
                OnPropertyChanged(() => IntegrationDate);
            }
        }
        /// <summary>
        /// Get or Set the Message indicating if the integration was successful or failed with messages.
        /// </summary>

        public virtual string IntegrationMessage
        {
            get => _IntegrationMessage;
            set
            {
                _IntegrationMessage = value;
                OnPropertyChanged(() => IntegrationMessage);
            }
        }

        private DateTime _CreatedDate;
        /// <summary>
        /// Get or set the created on date (will default to the current date time if not specified)
        /// </summary>

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

        public virtual string ModifiedBy
        {
            get { return _ModifiedBy; }
            set
            {
                _ModifiedBy = value;
                OnPropertyChanged(() => ModifiedBy);
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

        bool _IsActive;
        public virtual bool IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
                OnPropertyChanged(() => IsActive);
            }
        }
        bool _ShowOnMobile;
        public virtual bool ShowOnMobile
        {
            get
            {
                return _ShowOnMobile;
            }
            set
            {
                _ShowOnMobile = value;
                OnPropertyChanged(() => ShowOnMobile);
            }
        }
    }
}
