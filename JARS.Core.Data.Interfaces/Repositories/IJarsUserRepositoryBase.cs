using JARS.Core.Interfaces.Repositories;
using JARS.Entities;

namespace JARS.Core.Data.Interfaces.Repositories
{
    public interface IJarsUserRepositoryBase : IDataRepositoryCrudBase<JarsUser>
    {
        JarsUser GetByUserName(string adUsername);

        //JarsUser GetByUserNameEagerly(string adUsername);
    }
}
