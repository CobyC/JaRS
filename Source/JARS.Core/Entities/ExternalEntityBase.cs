using JARS.Core.Interfaces.Entities;
using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace JARS.Core.Entities
{
    /// <summary>
    /// This abstract class represents what the minimal data needs to contain for it to be added to the JARS system.
    /// This class is the base for the information that will be dragged from the source grid on to the scheduler area.
    /// </summary>
    /// <typeparam name="TKeyType">The ID key type (int, long, guid) but can not be a string.</typeparam>
    [DataContract]
    public abstract class ExternalEntityBase<TKeyType> : EntityBase<TKeyType>, IExternalEntityBase<TKeyType>
        where TKeyType : struct
    {

        public ExternalEntityBase()
        { }

        string _Location;
        string _LocationCode;
        string _Description;
        string _ExtRefID;
        string _LineOfWork;
        DateTime? _TargetDate;
        double _Duration;
        string _Priority;
        DateTime? _RecordLastModifiedOn;

        /// <summary>
        /// The location (full address) of the job/order/work that needs to be carried out.
        /// </summary>
        [DataMember]
        public virtual string Location
        {
            get
            {
                return _Location;
            }
            set
            {
                _Location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        [DataMember]
        public virtual string LocationCode
        {
            get
            {
                //this needs to be changed so that it could be used according to geo location.. will have to look into this..??
                if (Location != string.Empty)
                {
                    Regex regex = new Regex("(([gG][iI][rR] {0,}0[aA]{2})|(([aA][sS][cC][nN]|[sS][tT][hH][lL]|[tT][dD][cC][uU]|[bB][bB][nN][dD]|[bB][iI][qQ][qQ]|[fF][iI][qQ][qQ]|[pP][cC][rR][nN]|[sS][iI][qQ][qQ]|[iT][kK][cC][aA]) {0,}1[zZ]{2})|((([a-pr-uwyzA-PR-UWYZ][a-hk-yxA-HK-XY]?[0-9][0-9]?)|(([a-pr-uwyzA-PR-UWYZ][0-9][a-hjkstuwA-HJKSTUW])|([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y][0-9][abehmnprv-yABEHMNPRV-Y]))) {0,}[0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2}))");
                    Match match = regex.Match(Location);
                    _LocationCode = match.Value;
                }
                return _LocationCode;
            }
        }

        /// <summary>
        /// A long description of the work that needs to be carried out
        /// </summary>
        [DataMember]
        public virtual string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

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
        /// This can be the trade (type of work) that needs to be carried out.
        /// </summary>
        [DataMember]
        public virtual string LineOfWork
        {
            get
            {
                return _LineOfWork;
            }
            set
            {
                _LineOfWork = value;
                OnPropertyChanged(nameof(LineOfWork));
            }
        }

        /// <summary>
        /// The date from the external system of when the job was targeted to be worked on (completed)
        /// </summary>
        [DataMember]
        public virtual DateTime? TargetDate
        {
            get
            {
                return _TargetDate;
            }
            set
            {
                _TargetDate = value;
                OnPropertyChanged(nameof(TargetDate));
            }
        }

        /// <summary>
        /// The estimate duration of the time (in hours) the job/work might take to complete.
        /// </summary>
        [DataMember]
        public virtual double Duration
        {
            get
            {
                return _Duration;
            }
            set
            {
                _Duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        /// <summary>
        /// The priority that might be assigned to the job/work.
        /// </summary>
        [DataMember]
        public virtual string Priority
        {
            get
            {
                return _Priority;
            }
            set
            {
                _Priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }

        /// <summary>
        /// Gets or sets the date and time the entity was last modified.
        /// </summary>
        [DataMember]
        public virtual DateTime? RecordLastModifiedOn
        {
            get
            {
                return _RecordLastModifiedOn;
            }
            set
            {
                _RecordLastModifiedOn = value;
                OnPropertyChanged(nameof(RecordLastModifiedOn));
            }
        }

    }
}
