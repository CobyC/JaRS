using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IJarsSettingsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JarsSettingsRepository : DataRepositoryNhCrudBase<JarsSetting>, IJarsSettingsRepository
    {
        [ImportingConstructor()]
        public JarsSettingsRepository(IDataContextNhJars DbContext) : base(DbContext)
        {
        }
    }
}
