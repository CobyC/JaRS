using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IActivityLogRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ActivityLogRepository : DataRepositoryNhCrudBase<ActivityLog>, IActivityLogRepository
    {
        [ImportingConstructor()]
        public ActivityLogRepository(IDataContextNhJars DbContext) : base(DbContext)
        { }
    }
}
