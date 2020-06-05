namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface implements the ExtRefID property that indicates the implementing class has a reference to an external record.
    /// The ExtRefID is the link to the external record.
    /// </summary>
    public interface IEntityWithExternalReference: IEntityBase
    {
        /// <summary>
        /// Get or set a value that links to the original external entity
        /// The order or work reference number from the external system
        /// </summary>
        string ExtRefId { get; set; }
    }
}
