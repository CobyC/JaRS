using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IJobLineRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JobLineRepository : DataRepositoryNhEagerlyBase<JarsJobLine>, IJobLineRepository
    {
        [ImportingConstructor()]
        public JobLineRepository(IDataContextNhJars DbContext) : base(DbContext)
        {
        }
    }
}
