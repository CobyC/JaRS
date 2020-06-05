namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface indicates that the implementing object needs to have a property holding a value for Progress Status.
    /// ie STARTED, NEW, COMPLETED, ONGOING or 1, 2, 3 OR STAGE1Of5 etc..
    /// </summary>
    public interface IEntityWithProgressStatus : IEntityBase
    {
        /// <summary>
        /// Get or Set the status or process state of the job ie 'NEW', 'ASSIGNED', 'INPROGRESS', whatever the state of the job is
        /// </summary>te
        string ProgressStatus { get; set; }
    }
}
