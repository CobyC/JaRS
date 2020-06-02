using JARS.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Repositories
{
    /// <summary>
    /// This class enables us to request a repository as we need it within a function.
    /// It removes the need to use [Import] in a class if the repository is only used within a function, this frees up system resources.
    /// </summary>
    [Export(typeof(IDataRepositoryFactory))]//<-- used by MEF to know what class to instantiate.
    [PartCreationPolicy(CreationPolicy.NonShared)]//<-- sets the creation policy to non shared (single instance) as oppose to singleton (similar to static)
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        public TDataRepository GetDataRepository<TDataRepository>() where TDataRepository : IDataRepositoryBase
        {
            return JarsCore.Container.GetExportedValue<TDataRepository>();
        }
    }
}
