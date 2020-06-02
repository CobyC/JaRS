namespace JARS.Core.Interfaces.Repositories
{
    public interface IDataRepositoryFactory
    {
        /// <summary>
        /// Get a DataRepository represented by T, any repository that inherits from IDataRepository."/>.
        /// </summary>
        /// <typeparam name="T">Any repository that implements the IDataRepository interface</typeparam>
        /// <returns></returns>
        T GetDataRepository<T>() where T : IDataRepositoryBase;
    }
}
