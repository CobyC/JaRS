using JARS.Core.Interfaces.Entities;
using System;
using System.Linq;

namespace JARS.Entities
{
    public interface IJarsJobLine: IEntityBase<int>
    {
        /// <summary>
        /// This shows how much of the quantity is assigned to the operative/resource this line is linked to.
        /// </summary>
        decimal? AllocatedQty { get; set; }
        /// <summary>
        /// This indicated the quantity that was completed on this line.
        /// if a line was not fully completed this should differ from the allocated quantity.
        /// This represents the quantity completed for this cycle of the job. it could be that on this attempt only a part of the line was completed.
        /// </summary>
        decimal? CompletedQty { get; set; }
        /// <summary>
        /// Shows the status of where this job is in regards to being completed.
        /// </summary>
        string ProcessStatus { get; set; }
        /// <summary>
        /// The external system reference id that is used to link this item to the job in the external system.
        /// Lines can be sent out to the mobile devices more than once, so we have quantities that represent the current state and the overall state of a job line.
        /// </summary>
        string ExternalJobRef { get; set; }
        /// <summary>
        /// The long description given to the line, describing the line code. 
        /// </summary>
        string FullDescription { get; set; }
        /// <summary>
        /// Indicates if the line is split between operatives/resources.
        /// this value is assigned after the job has been assigned.
        /// </summary>
        bool? IsShared { get; set; }
        /// <summary>
        /// The code used to identify what type of task needs to be performed for this line.
        /// ie. ELDW01 - could be a standard code for electrical day work.
        /// </summary>
        string LineCode { get; set; }
        /// <summary>
        /// The position or the line number of this Job line.
        /// </summary>
        int LineNum { get; set; }
        /// <summary>
        /// The quantity originally assigned to the line.
        /// if it comes from an external system this will be what the external system value is.
        /// if it is a new line added within JaRS the value should be 0.
        /// </summary>
        decimal? OriginalQty { get; set; }
        /// <summary>
        /// Indicates the rate of pay.
        /// </summary>
        float? PayRate { get; set; }
        /// <summary>
        /// This indicates what quantity is still outstanding on this line.
        /// </summary>
        decimal? RemainingQty { get; set; }
        /// <summary>
        /// The id that this line is assigned to.
        /// because jobs can be assigned to multiple operatives/resources, the line needs to indicate what operative/resource it belongs to.
        /// </summary>
        JarsResource Resource { get; set; }
        /// <summary>
        /// This is the new quantity assigned to the line, this can be used to indicate a variation.
        /// </summary>
        decimal? RevisedQty { get; set; }
        /// <summary>
        /// The short description given to the line, describing the line code. 
        /// </summary>
        string ShortDescription { get; set; }
        /// <summary>
        /// This indicates the total quantity completed on this line.
        /// a job might be sent out more than once, so we need to keep track of how many lines have been completed in total.
        /// </summary>
        decimal? TotalQtyCompleted { get; set; }
        /// <summary>
        /// Indicates the unit of measure
        /// </summary>
        string Uom { get; set; }
    }
}
