using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IResourceSkillRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ResourceSkillRepository : DataRepositoryNhCrudBase<ResourceSkill>, IResourceSkillRepository
    {
        [ImportingConstructor()]
        public ResourceSkillRepository(IDataContextNhJars DbContext) : base(DbContext)
        {
        }
    }
}
