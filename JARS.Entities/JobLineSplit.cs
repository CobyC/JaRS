using JARS.Core.Entities;
using System;

namespace JARS.Entities
{
    /// <summary>
    /// This class represents the information used for showing how a JobLine was split between operatives/resources.
    /// </summary>
    [Serializable]
    public class JarsJobLineSplit : IntegratableEntityBase
    {
        public JarsJobLineSplit()
        { }

        private string _SharedOperativeName;
        private string _SharedOperativeId;
        private string _OwningOperativeId;
        private decimal? _SplitQty;
        private string _LineCode;
        private int? _LineNum;
        private int _ExernalJobRef;
        private JarsJobLineBase _SourceJobLine;


        /// <summary>
        /// The external job/order reference number
        /// </summary>
         
        public virtual int ExternalJobRef
        {
            get
            {
                return _ExernalJobRef;
            }
            set
            {
                _ExernalJobRef = value;
                OnPropertyChanged(() => ExternalJobRef);
            }
        }

        /// <summary>
        /// The line number that was split
        /// </summary>
         
        public virtual int? LineNum
        {
            get
            {
                return _LineNum;
            }
            set
            {
                _LineNum = value;
                OnPropertyChanged(() => LineNum);
            }
        }

        /// <summary>
        /// The code of the line that was split.
        /// </summary>
         
        public virtual string LineCode
        {
            get
            {
                return _LineCode;
            }
            set
            {
                _LineCode = value;
                OnPropertyChanged(() => LineCode);
            }
        }

        /// <summary>
        /// The quantity that was assigned to the operative/resource the original line was split by.
        /// </summary>
         
        public virtual decimal? SplitQty
        {
            get
            {
                return _SplitQty;
            }
            set
            {
                _SplitQty = value;
                OnPropertyChanged(() => SplitQty);
            }
        }

        /// <summary>
        /// the id of the operative/resource to which the line was originally assigned to.
        /// </summary>
         
        public virtual string OwningOperativeId
        {
            get
            {
                return _OwningOperativeId;
            }
            set
            {
                _OwningOperativeId = value;
                OnPropertyChanged(() => OwningOperativeId);
            }
        }

        /// <summary>
        /// The operative/resource id that the JobLine was shared with.
        /// </summary>
         
        public virtual string SharedOperativeId
        {
            get
            {
                return _SharedOperativeId;
            }
            set
            {
                _SharedOperativeId = value;
                OnPropertyChanged(() => SharedOperativeId);
            }
        }

        /// <summary>
        /// The name of the operative/resource the JobLine was shared with.
        /// </summary>
         
        public virtual string SharedOperativeName
        {
            get
            {
                return _SharedOperativeName;
            }
            set
            {
                _SharedOperativeName = value;
                OnPropertyChanged(() => SharedOperativeName);
            }
        }

        /// <summary>
        /// Access to the JobLine that this split is linked to.
        /// </summary>
         
        public virtual JarsJobLineBase SourceJobLine
        {
            get { return _SourceJobLine; }
            set
            {
                _SourceJobLine = value;
                OnPropertyChanged(() => SourceJobLine);
            }
        }
    }
}
