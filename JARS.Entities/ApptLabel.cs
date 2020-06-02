using JARS.Core;
using JARS.Core.Entities;
using JARS.Core.Interfaces.Entities;
using System;

namespace JARS.Entities
{
    /// <summary>
    /// This class represents the  labels assigned to the appointments that represent a job.
    /// The labels are a visual aid that is used in the scheduler control, but it can be utilized on the job appointment for added meaning.
    /// The Label is the big area of the appointment.
    /// </summary>
    [Serializable]
    public class ApptLabel : EntityBase<int>
    {

        public ApptLabel()
        {
            LabelName = "Default";
            ColourRGB = JarsCore.defaultBackColour.ToArgb();
            SortIndex = 99;
            //RecordCreatedOn = DateTime.Now;            
        }

        private int _SortIndex;
        private string _ViewName;
        private string _LabelName;
        private int _ColourRGB;
        private int _ForeColourRGB;

        /// <summary>
        /// The display name of the label.
        /// </summary>

        public virtual string LabelName
        {
            get => _LabelName;
            set
            {
                _LabelName = value;
                OnPropertyChanged(() => LabelName);
            }
        }

        string _LabelCriteria;
        /// <summary>
        /// Get or set the value represented by this label.
        /// useful when matching criterias in the ViewOptions.
        /// </summary>

        public virtual string LabelCriteria
        {
            get
            {
                return _LabelCriteria;
            }
            set
            {
                _LabelCriteria = value;
                OnPropertyChanged(() => LabelCriteria);
            }
        }

        /// <summary>
        /// The integer representation of the RGB colour values
        /// </summary>
        public virtual int ColourRGB
        {
            get
            {
                if (_ColourRGB == 0)
                    _ColourRGB = JarsCore.defaultBackColour.ToArgb();

                return _ColourRGB;
            }
            set
            {
                _ColourRGB = value;
                OnPropertyChanged(() => ColourRGB);
            }
        }

        /// <summary>
        /// The integer representation of the RGB colour values
        /// </summary>
        public virtual int ForeColourRGB
        {
            get
            {
                if (_ForeColourRGB == 0)
                    _ForeColourRGB = JarsCore.defaultForeColour.ToArgb();

                return _ForeColourRGB;
            }
            set
            {
                _ForeColourRGB = value;
                OnPropertyChanged(() => ForeColourRGB);
            }
        }

        /// <summary>
        /// The name of the view type the label is used for ie. 
        /// The default view name is DEFAULT (as specified in the view option Plugin).        
        /// </summary>

        public virtual string ViewName
        {
            get => _ViewName;
            set
            {
                _ViewName = value;
                OnPropertyChanged(() => ViewName);
            }
        }

        /// <summary>
        /// The position this item will be in a list. ie in a dropdown that is orders by index and not name
        /// </summary>

        public virtual int SortIndex
        {
            get => _SortIndex;
            set
            {
                _SortIndex = value;
                OnPropertyChanged(() => SortIndex);
            }
        }

        string _UseInterfaceType;
        /// <summary>
        /// The name of the Interface this label will expect the matching entity will implement.
        /// </summary>

        public virtual string UseInterfaceType
        {
            get
            {
                return _UseInterfaceType;
            }
            set
            {
                _UseInterfaceType = value;
                OnPropertyChanged(() => UseInterfaceType);
            }
        }
    }
}
