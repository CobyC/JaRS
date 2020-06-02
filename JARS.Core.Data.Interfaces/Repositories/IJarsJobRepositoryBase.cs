using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Repositories;

namespace JARS.Core.Data.Interfaces.Repositories
{
    public interface IJarsJobRepositoryBase<T> : IDataRepositoryCrudBase<T>
        where T : class, IEntityBase
    {

    }
}
