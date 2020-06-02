using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IJobLineSplitRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JobLineSplitRepository : DataRepositoryNhCrudBase<JarsJobLineSplit>, IJobLineSplitRepository
    {
        [ImportingConstructor()]
        public JobLineSplitRepository(IDataContextNhJars DbContext) : base(DbContext)
        {
        }
    }
}
