using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(ILookupValueRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoopupValueRepository : DataRepositoryNhCrudBase<LookupValue>, ILookupValueRepository
    {
        [ImportingConstructor()]
        public LoopupValueRepository(IDataContextNhJars DbContext) : base(DbContext)
        {
        }
    }
}
