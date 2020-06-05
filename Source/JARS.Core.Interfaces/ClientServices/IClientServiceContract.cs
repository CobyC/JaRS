namespace JARS.Core.Interfaces.ClientServices
{
    /// <summary>
    /// This interface enables us to call the service through the ClientServiceFactory.
    /// Although the same services are used on both sides of the wire (business and client) the IClientContract
    /// interface only impacts the client side and should not be used on the server (business) side
    /// </summary>
    public interface IClientServiceContract
    {
    }
}
