using JARS.Core.Interfaces.Repositories;
using JARS.Entities;

namespace JARS.Core.Data.Interfaces.Repositories
{
    public interface IResourceRepositoryBase : IDataRepositoryCrudBase<JarsResource>
    {
        JarsResource AddGroupToResource(JarsResource currentResource, JarsResourceGroup groupToAdd);
        void RemoveGroupFromResource(JarsResource currentResource, JarsResourceGroup groupToRemove);
    }
}
