using JARS.Core;
using JARS.Core.Entities;
using System;

namespace JARS.Entities
{
    /// <summary>
    /// This class represents the status assigned to the appointments that represent a job.
    /// The status are a visual aid that is used in the scheduler control, but it can be utilized on the job appointment for added meaning.
    /// The status is the small strip on the left hand side of the appointment.
    /// </summary>
    [Serializable]
    public class ApptStatus : EntityBase<int>
    {
        public ApptStatus()
        {
            StatusName = "Default";
            ColourRGB = JarsCore.defaultBackColour.ToArgb();           
            SortIndex = 99;
            //StatusCriteria = "([StatusKey] = '0')";
        }

        private int _SortIndex;
        private string _ViewName;
        private int _ColourRGB;
        private string _StatusName;

        /// <summary>
        /// The display name of the status.
        /// </summary>
         
        public virtual string StatusName
        {
            get => _StatusName; set
            {
                _StatusName = value;
                OnPropertyChanged(() => StatusName);
            }
        }

        string _StatusCriteria;
        /// <summary>
        /// Get or set the criteria used to match with this status.
        /// this value is build up using the filter control
        /// </summary>
         
        public virtual string StatusCriteria
        {
            get
            {
                return _StatusCriteria;
            }
            set
            {
                _StatusCriteria = value;
                OnPropertyChanged(() => StatusCriteria);
            }
        }

        /// <summary>
        /// The integer number representing the RGB colour values.
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
        /// The name of the view type the status is used for ie. 
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
