using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IErrorLogRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ErrorLogRepository : DataRepositoryNhCrudBase<ErrorLog>, IErrorLogRepository
    {
        [ImportingConstructor()]
        public ErrorLogRepository(IDataContextNhJars DbContext) : base(DbContext)
        {
        }
    }
}
