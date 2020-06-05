using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IJarsJobRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JarsJobRepository : DataRepositoryNhCrudBase<JarsJob>, IJarsJobRepository
    {
        [ImportingConstructor()]
        public JarsJobRepository(IDataContextNhJars DbContext) : base(DbContext)
        {
        }
    }
}
