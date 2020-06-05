using JARS.Core.Interfaces.Repositories;
using JARS.Entities;

namespace JARS.Core.Data.Interfaces.Repositories
{
    public interface IResourceGroupRepositoryBase : IDataRepositoryCrudBase<JarsResourceGroup>
    {     
        JarsResourceGroup AddResourceToGroup(JarsResourceGroup currentGroup, JarsResource resourceToAdd);
        void RemoveResourceFromGroup(JarsResourceGroup currentGroup, JarsResource resourceToRemove);
    }
}
