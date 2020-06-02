using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Interfaces.Repositories;
using JARS.Entities;

namespace JARS.Data.NH.Jars.Interfaces
{
    /// <summary>
    /// The idea for this interface is to be inherited by the repository that will implement it.
    /// This can be used stand alone, but it is better to inherit from this as the JOBS can differ from system to system.
    /// </summary>    
    public interface IJarsJobRepository : IJarsJobRepositoryBase<JarsJob>, IDataRepositoryCrudBase<JarsJob>                
    { }
}
