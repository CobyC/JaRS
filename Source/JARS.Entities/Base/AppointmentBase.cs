using JARS.Core.Interfaces.Entities;
using System;
using System.Text.RegularExpressions;

namespace JARS.Entities
{
    /// <summary>
    /// This class represents the structure of the DevExpress appointment.
    /// It's not a direct copy, but it contains the information required to set up an appointment.
    /// It contains extra properties that will be used in JARS.
    /// </summary>
    [Serializable]
    public abstract class AppointmentBase : AppointableBase<int>, IEntityWithLocation, IEntityWithStatusLabels, IEntityWithShowOnMobile
    {

        private string _StatusKey;
        private string _LabelKey;
        private string _Location;
        private string _LocationCode;
        private string _ApptTypeCode;

        private bool _ShowOnMobile;

        private string _RecurrenceInfo; //used by main appt
        private Guid _RecurrenceId;    //used by main and recurrence 
        private int _RecurrenceIndex; //used only by occurrence (changed/deleted)

        
        private bool _IsAllDay;
        
        private long? _DurationTicks;



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
                //this needs to be changed so that it could be used according to geo location.. will have to look into this..??
                if (Location != null && Location != string.Empty)
                {
                    Regex regex = new Regex("(([gG][iI][rR] {0,}0[aA]{2})|(([aA][sS][cC][nN]|[sS][tT][hH][lL]|[tT][dD][cC][uU]|[bB][bB][nN][dD]|[bB][iI][qQ][qQ]|[fF][iI][qQ][qQ]|[pP][cC][rR][nN]|[sS][iI][qQ][qQ]|[iT][kK][cC][aA]) {0,}1[zZ]{2})|((([a-pr-uwyzA-PR-UWYZ][a-hk-yxA-HK-XY]?[0-9][0-9]?)|(([a-pr-uwyzA-PR-UWYZ][0-9][a-hjkstuwA-HJKSTUW])|([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y][0-9][abehmnprv-yABEHMNPRV-Y]))) {0,}[0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2}))");
                    Match match = regex.Match(Location);
                    _LocationCode = match.Value;
                }
                return _LocationCode;
            }
            private set { _Location = value; }
        }


        public virtual long? DurationTicks
        {
            get
            {
                if (StartDate != null && EndDate != null)
                {
                    _DurationTicks = EndDate.Subtract(StartDate).Ticks;
                }
                return _DurationTicks;
            }
            set
            {
                _DurationTicks = value;
                OnPropertyChanged(() => DurationTicks);
            }
        }


        


        public virtual bool IsAllDay
        {
            get => _IsAllDay;
            set
            {
                _IsAllDay = value;
                OnPropertyChanged(() => IsAllDay);
            }
        }


       

        public virtual string RecurrenceInfo
        {
            get => _RecurrenceInfo;
            set
            {
                _RecurrenceInfo = value;
                OnPropertyChanged(() => RecurrenceInfo);
            }
        }

        public virtual Guid RecurrenceId
        {
            get => _RecurrenceId;
            set
            {
                _RecurrenceId = value;
                OnPropertyChanged(() => RecurrenceId);
            }
        }

        public virtual int RecurrenceIndex
        {
            get => _RecurrenceIndex;
            set
            {
                _RecurrenceIndex = value;
                OnPropertyChanged(() => RecurrenceIndex);
            }
        }


        public virtual string StatusKey
        {
            get => _StatusKey;
            set
            {
                _StatusKey = value;
                OnPropertyChanged(() => StatusKey);
            }
        }


        public virtual string LabelKey
        {
            get => _LabelKey;
            set
            {
                _LabelKey = value;
                OnPropertyChanged(() => LabelKey);
            }
        }


        public virtual string ApptTypeCode
        {
            get => _ApptTypeCode;
            set
            {
                _ApptTypeCode = value;
                OnPropertyChanged(() => ApptTypeCode);
            }
        }


        public virtual bool ShowOnMobile
        {
            get => _ShowOnMobile;
            set
            {
                _ShowOnMobile = value;
                OnPropertyChanged(() => ShowOnMobile);
            }
        }
    }
}
