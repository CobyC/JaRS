using JARS.Core.Attributes;
using JARS.Core.Entities;
using System;
using System.Collections.Generic;

namespace JARS.Entities
{
    /// <summary>
    /// The base classes contain the minimal properties available in the JaRS system.
    /// To extend this class inherit from it and add the additional properties to the parent class.
    /// </summary>
    [Serializable]
       public abstract class JarsJobLineBase : IntegratableEntityBase, IJarsJobLine
    {
        public JarsJobLineBase()
        {
            this.ExternalJobRef = "0";//jams.OrderNo;            
            this.LineNum = 0;//jams.DetailNo;
            this.LineCode = "";//jams.SorCd
            this.ShortDescription = "";//jams.SorDesc
            this.FullDescription = "";//jams.FullDesc
            this.OriginalQty = 0;//jams.SorQty
            this.AllocatedQty = 0;//jams.AllocQty;
            this.RevisedQty = 0;//jams.SorQtyRev = SorQty;
            this.CompletedQty = 0;//jams.SorQtyCom
            this.RemainingQty = 0;//jams.QtyRemain
            this.TotalQtyCompleted = 0;//jars.TotalQtyComp;
            this.Uom = "";//jams.Uom;
            this.PayRate = 0;//jams.PayRate;
            this.IsShared = false;//jams.SplitSORs = false;
            this.ProcessStatus = "NEW";//jams.ComStatus = "NEW";
            this.ModifiedBy = "user";//jams.UserName
            this.IntegrationStatus = 0;//jams.IntegrationStatus
            this.Splits = new List<JarsJobLineSplit>();//jams.Splits

        }

        private string _ExternalJobRef;
        private JarsResource _Resource;
        private int _LineNum;
        private string _LineCode;
        private string _ShortDescription;
        private string _FullDescription;
        private decimal? _OriginalQty;
        private decimal? _AllocatedQty;
        private decimal? _RevisedQty;
        private decimal? _CompletedQty;
        private decimal? _RemainingQty;
        private decimal? _TotalQtyCompleted;
        private string _Uom;
        private float? _PayRate;
        private bool? _IsShared;
        private string _ProcessStatus;
        private IList<JarsJobLineSplit> _Splits;



        /// <summary>
        /// The external system reference id that is used to link this item to the job in the external system.
        /// Lines can be sent out to the mobile devices more than once, so we have quantities that represent the current state and the overall state of a job line.
        /// </summary>
         
        public virtual string ExternalJobRef
        {
            get => _ExternalJobRef;
            set
            {
                _ExternalJobRef = value;
                OnPropertyChanged(() => ExternalJobRef);
            }
        }

        /// <summary>
        /// The id that this line is assigned to.
        /// because jobs can be assigned to multiple operatives, the line needs to indicate what operative/resource it belongs to.
        /// </summary>
         
        public virtual JarsResource Resource 
        {
            get => _Resource;
            set
            {
                _Resource = value;
                OnPropertyChanged(() => Resource);
            }
        }

        /// <summary>
        /// The position or the line number of this Job line.
        /// </summary>
         
        public virtual int LineNum
        {
            get => _LineNum;
            set
            {
                _LineNum = value;
                OnPropertyChanged(() => LineNum);
            }
        }

        /// <summary>
        /// The code used to identify what type of task needs to be performed for this line.
        /// ie. ELDW01 - could be a standard code for electrical day work.
        /// </summary>
         
        public virtual string LineCode
        {
            get => _LineCode;
            set
            {
                _LineCode = value;
                OnPropertyChanged(() => LineCode);
            }
        }

        /// <summary>
        /// The short description given to the line, describing the line code. 
        /// </summary>
         
        public virtual string ShortDescription
        {
            get => _ShortDescription;
            set
            {
                _ShortDescription = value;
                OnPropertyChanged(() => ShortDescription);
            }
        }

        /// <summary>
        /// The long description given to the line, describing the line code. 
        /// </summary>
         
        public virtual string FullDescription
        {
            get => _FullDescription;
            set
            {
                _FullDescription = value;
                OnPropertyChanged(() => FullDescription);
            }
        }

        /// <summary>
        /// The quantity originally assigned to the line.
        /// if it comes from an external system this will be what the external system value is.
        /// if it is a new line added within JaRS the value should be 0.
        /// </summary>
         
        public virtual decimal? OriginalQty
        {
            get => _OriginalQty;
            set
            {
                _OriginalQty = value;
                OnPropertyChanged(() => OriginalQty);
            }
        }

        /// <summary>
        /// This shows how much of the quantity is assigned to the operative/resource this line is linked to.
        /// </summary>
         
        public virtual decimal? AllocatedQty
        {
            get => _AllocatedQty;
            set
            {
                _AllocatedQty = value;
                OnPropertyChanged(() => AllocatedQty);
            }
        }

        /// <summary>
        /// This is the new quantity assigned to the line, this can be used to indicate a variation.
        /// </summary>
         
        public virtual decimal? RevisedQty
        {
            get => _RevisedQty;
            set
            {
                _RevisedQty = value;
                OnPropertyChanged(() => RevisedQty);
            }
        }

        /// <summary>
        /// This indicated the quantity that was completed on this line.
        /// if a line was not fully completed this should differ from the allocated quantity.
        /// This represents the quantity completed for this cycle of the job. it could be that on this attempt only a part of the line was completed.
        /// </summary>
         
        public virtual decimal? CompletedQty
        {
            get => _CompletedQty;
            set
            {
                _CompletedQty = value;
                OnPropertyChanged(() => CompletedQty);
            }
        }

        /// <summary>
        /// This indicates what quantity is still outstanding on this line.
        /// </summary>
         
        public virtual decimal? RemainingQty
        {
            get => _RemainingQty;
            set
            {
                _RemainingQty = value;
                OnPropertyChanged(() => RemainingQty);
            }
        }

        /// <summary>
        /// This indicates the total quantity completed on this line.
        /// a job might be sent out more than once, so we need to keep track of how many lines have been completed in total.
        /// </summary>
         
        public virtual decimal? TotalQtyCompleted
        {
            get => _TotalQtyCompleted;
            set
            {
                _TotalQtyCompleted = value;
                OnPropertyChanged(() => TotalQtyCompleted);
            }
        }


        /// <summary>
        /// Indicates if the line is split between operatives.
        /// this value is assigned after the job has been assigned.
        /// </summary>
         
        public virtual bool? IsShared
        {
            get => _IsShared;
            set
            {
                _IsShared = value;
                OnPropertyChanged(() => IsShared);
            }
        }

        /// <summary>
        /// Indicates the unit of measure
        /// </summary>
         
        public virtual string Uom
        {
            get => _Uom;
            set
            {
                _Uom = value;
                OnPropertyChanged(() => Uom);
            }
        }

        /// <summary>
        /// Indicates the rate of pay.
        /// </summary>
         
        public virtual float? PayRate
        {
            get => _PayRate;
            set
            {
                _PayRate = value;
                OnPropertyChanged(() => PayRate);
            }
        }


        /// <summary>
        /// The list of lines where this job line was shared with someone else.
        /// </summary>
        public virtual IList<JarsJobLineSplit> Splits
        {
            get => _Splits;
            set
            {
                _Splits = value;
                OnPropertyChanged(() => Splits);
            }
        }


        /// <summary>
        /// Shows the status of where this job is in regards to being completed.
        /// </summary>
        [LookupValue(DefaultCategoryCode = "PROGR_STATUS", DefaultFirstValue = "NEW")]
         
        public virtual string ProcessStatus
        {
            get => _ProcessStatus;
            set
            {
                _ProcessStatus = value;
                OnPropertyChanged(() => ProcessStatus);
            }
        }
    }
}
