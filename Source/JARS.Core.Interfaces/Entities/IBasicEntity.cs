namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface combines the IExternalReferenceEntity and IAppointableEntity interfaces.
    /// Entities implementing this interface will be able to be represented as an appointment with a reference to an external record.
    /// </summary>
    public interface IBasicEntity : IEntityWithExternalReference, IEntityWithAppointing
    { }
}
