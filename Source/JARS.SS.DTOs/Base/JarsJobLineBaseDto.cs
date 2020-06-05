using JARS.Core.Entities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs.Base
{
    /// <summary>
    /// The base classes contain the minimal properties available in the JaRS system.
    /// To extend this class inherit from it and add the additional properties to the parent class.
    /// </summary>
    [DataContract]    
    public abstract class JarsJobLineBaseDto : IntegratableEntityBaseDto
    {
        public JarsJobLineBaseDto()
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
            this.Splits = new List<JobLineSplitDto>();//jams.Splits

        }

        /// <summary>
        /// The external system reference id that is used to link this item to the job in the external system.
        /// Lines can be sent out to the mobile devices more than once, so we have quantities that represent the current state and the overall state of a job line.
        /// </summary>
        [DataMember]
        public virtual string ExternalJobRef { get; set; }

        /// <summary>
        /// The id that this line is assigned to.
        /// because jobs can be assigned to multiple operatives, the line needs to indicate what operative/resource it belongs to.
        /// </summary>
        [DataMember]
        public virtual ResourceDto Resource { get; set; }

        /// <summary>
        /// The position or the line number of this Job line.
        /// </summary>
        [DataMember]
        public virtual int LineNum { get; set; }

        /// <summary>
        /// The code used to identify what type of task needs to be performed for this line.
        /// ie. ELDW01 - could be a standard code for electrical day work.
        /// </summary>
        [DataMember]
        public virtual string LineCode { get; set; }

        /// <summary>
        /// The short description given to the line, describing the line code. 
        /// </summary>
        [DataMember]
        public virtual string ShortDescription { get; set; }

        /// <summary>
        /// The long description given to the line, describing the line code. 
        /// </summary>
        [DataMember]
        public virtual string FullDescription { get; set; }

        /// <summary>
        /// The quantity originally assigned to the line.
        /// if it comes from an external system this will be what the external system value is.
        /// if it is a new line added within JaRS the value should be 0.
        /// </summary>
        [DataMember]
        public virtual decimal? OriginalQty { get; set; }

        /// <summary>
        /// This shows how much of the quantity is assigned to the operative/resource this line is linked to.
        /// </summary>
        [DataMember]
        public virtual decimal? AllocatedQty { get; set; }

        /// <summary>
        /// This is the new quantity assigned to the line, this can be used to indicate a variation.
        /// </summary>
        [DataMember]
        public virtual decimal? RevisedQty { get; set; }

        /// <summary>
        /// This indicated the quantity that was completed on this line.
        /// if a line was not fully completed this should differ from the allocated quantity.
        /// This represents the quantity completed for this cycle of the job. it could be that on this attempt only a part of the line was completed.
        /// </summary>
        [DataMember]
        public virtual decimal? CompletedQty { get; set; }

        /// <summary>
        /// This indicates what quantity is still outstanding on this line.
        /// </summary>
        [DataMember]
        public virtual decimal? RemainingQty { get; set; }

        /// <summary>
        /// This indicates the total quantity completed on this line.
        /// a job might be sent out more than once, so we need to keep track of how many lines have been completed in total.
        /// </summary>
        [DataMember]
        public virtual decimal? TotalQtyCompleted { get; set; }


        /// <summary>
        /// Indicates if the line is split between operatives.
        /// this value is assigned after the job has been assigned.
        /// </summary>
        [DataMember]
        public virtual bool? IsShared { get; set; }

        /// <summary>
        /// Indicates the unit of measure
        /// </summary>
        [DataMember]
        public virtual string Uom { get; set; }

        /// <summary>
        /// Indicates the rate of pay.
        /// </summary>
        [DataMember]
        public virtual float? PayRate { get; set; }


        /// <summary>
        /// The list of lines where this job line was shared with someone else.
        /// </summary>
        public virtual IList<JobLineSplitDto> Splits { get; set; }


        /// <summary>
        /// Shows the status of where this job is in regards to being completed.
        /// </summary>       
        [DataMember]
        public virtual string ProcessStatus { get; set; }
    }
}
