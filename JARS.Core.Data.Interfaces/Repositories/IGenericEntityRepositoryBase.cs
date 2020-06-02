using JARS.Core.Data.Interfaces.DataContext;
using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Repositories;

namespace JARS.Core.Data.Interfaces.Repositories
{
    /// <summary>
    /// This repository interface represents the interface that can be used for all entity repositories for any entities that inherits from IEntityBase.    
    /// This Repository interface also implements the IDataRepository<typeparamref name="TDataContextInterface"/>[T].
    /// </summary>
    /// <typeparam name="TEntity">Any Entity that implements the IEntityBase interface (the basic Interface for all persistent Entities)</typeparam>    
    public interface IGenericEntityRepositoryBase<TEntity, TDataContextInterface> : IDataRepositoryCrudBase<TEntity>
        where TEntity : class, IEntityBase
        where TDataContextInterface : IDataContextBase
    {
        IDataContextBase CurrentDataContext { get; }
    }

    /// <summary>
    /// This read only repository interface represents the read only interface that can be used for all entity repositories for any entities that inherits from IEntityBase.    
    /// This Repository interface also implements the IDataRepository<typeparamref name="TDataContextInterface"/>[T].
    /// </summary>
    /// <typeparam name="TEntity">Any Entity that implements the IEntityBase interface (the basic Interface for all persistent Entities)</typeparam>    
    public interface IGenericEntityReadOnlyRepositoryBase<TEntity, TDataContextInterface> : IDataRepositoryReadOnlyBase<TEntity>
        where TEntity : class, IEntityBase
        where TDataContextInterface : IDataContextBase
    {
        IDataContextBase CurrentDataContext { get; }
    }
}
