using JARS.Core.Interfaces;
using JARS.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Data.Interfaces.Repositories
{
    interface IGenericEntityRepositoryBase<T> : IDataRepositoryBase<T>
        where T : class, IEntityBase
    {
    }
}
