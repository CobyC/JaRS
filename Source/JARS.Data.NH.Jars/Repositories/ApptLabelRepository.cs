using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IApptLabelRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ApptLabelRepository : DataRepositoryNhCrudBase<ApptLabel>, IApptLabelRepository
    {
        [ImportingConstructor()]
        public ApptLabelRepository(IDataContextNhJars DbContext) : base(DbContext)
        {
        }
    }
}
