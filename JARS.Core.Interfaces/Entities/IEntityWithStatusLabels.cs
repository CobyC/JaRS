namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface indicates that the implementing object needs to have a properties holding the values for StatusKey and LabelKey.
    /// This helps with showing the appointment created for this entity has the correct status and label assigned to it.
    /// </summary>
    public interface IEntityWithStatusLabels : IEntityBase
    {
        /// <summary>
        /// Get or set the Label of the appointment this jobs represents in the DevExpress controls (if using DevExpress)
        /// </summary>
        string LabelKey { get; set; }

        /// <summary>
        /// Get or set the Status of the appointment this jobs represents in the DevExpress controls (if using DevExpress)
        /// </summary>
        string StatusKey { get; set; }
    }
}
