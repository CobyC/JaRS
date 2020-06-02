namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface indicates that the implementing object needs to have a property holding a value for Location.
    /// This could be an address line, postcode, gps coordinates etc..
    /// </summary>
    public interface IEntityWithLocation : IEntityBase
    {
        /// <summary>
        /// The location (full address) of the job/order/work that needs to be carried out.
        /// </summary>
        string Location { get; set; }

        /// <summary>
        /// The code is calculated depending on a regex rule applied within your code, depending on the Location property.
        /// The default setting is UK postcodes, if the location property does not contain a UK postcode or is blank the code will also be blank.
        /// </summary>
        string LocationCode { get; }
    }
}
