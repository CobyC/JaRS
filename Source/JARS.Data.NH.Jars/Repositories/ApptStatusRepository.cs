using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IApptStatusRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ApptStatusRepository : DataRepositoryNhCrudBase<ApptStatus>, IApptStatusRepository
    {
        [ImportingConstructor()]
        public ApptStatusRepository(IDataContextNhJars DbContext) : base(DbContext)
        {
        }
    }
}
