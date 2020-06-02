namespace JARS.Core.Interfaces.ClientServices
{
    public interface IClientServiceFactory
    {
        T CreateClient<T>() where T : IClientServiceContract;
    }
}
