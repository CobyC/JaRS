using JARS.Core.Interfaces.Repositories;
using JARS.Entities;

namespace JARS.Core.Data.Interfaces.Repositories
{
    public interface IJobLineRepositoryBase<T> : IDataRepositoryCrudBase<T>
        where T:class, IJarsJobLine
    {
    }
}
