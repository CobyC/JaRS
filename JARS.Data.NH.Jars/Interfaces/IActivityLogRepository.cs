using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Interfaces.Repositories;
using JARS.Entities;

namespace JARS.Data.NH.Jars.Interfaces
{
    public interface IActivityLogRepository : IActivityLogRepositoryBase, IDataRepositoryCrudBase<ActivityLog>
    {
    }
}
