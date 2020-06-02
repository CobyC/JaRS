using JARS.Core.Data.Interfaces.DataContext;
using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Interfaces.Entities;
using JARS.Data.NH.Interfaces;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Repositories
{
    /// <summary>
    /// This repository can be used for any entity that inherits from IEntityBase.
    /// This should reduce the need to create a separate repository for any new entity classes.
    /// This Repository also implements the DataRepositoryEagerlyBase class, so entities with child dependencies will also work.
    /// </summary>
    /// <typeparam name="TEntity">Any Entity that implements the IEntityBase interface (the basic Interface for all persistent Entities)</typeparam>
    [Export(typeof(IGenericEntityRepositoryBase<,>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GenericEntityRepository<TEntity, TContextInterface> : DataRepositoryNhCrudBase<TEntity>, IGenericEntityRepositoryBase<TEntity, TContextInterface>// DataRepositoryNhEagerlyBase<TEntity>, IGenericEntityRepositoryBase<TEntity, TContextInterface>
        where TEntity : class, IEntityBase
        where TContextInterface : IDataContextBaseNh
    {
        public IDataContextBase CurrentDataContext { get; private set; }

        [ImportingConstructor()]
        public GenericEntityRepository(TContextInterface DbContext) : base(DbContext)
        {
            CurrentDataContext = DBContext;
        }
        public GenericEntityRepository()
        {
        }
    }

    /// <summary>
    /// This read only repository can be used for any entity that inherits from IEntityBase.
    /// This should reduce the need to create a separate repository for any new entity classes.
    /// This Repository implements the read only functions, so no updates can be performed with this repository
    /// </summary>
    /// <typeparam name="TEntity">Any Entity that implements the IEntityBase interface (the basic Interface for all persistent Entities)</typeparam>
    [Export(typeof(IGenericEntityReadOnlyRepositoryBase<,>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GenericEntityReadOnlyRepository<TEntity, TContextInterface> : DataRepositoryNhCrudBase<TEntity>, IGenericEntityReadOnlyRepositoryBase<TEntity, TContextInterface>// DataRepositoryNhEagerlyBase<TEntity>, IGenericEntityRepositoryBase<TEntity, TContextInterface>
        where TEntity : class, IEntityBase
        where TContextInterface : IDataContextBaseNh
    {
        public IDataContextBase CurrentDataContext { get; private set; }

        [ImportingConstructor()]
        public GenericEntityReadOnlyRepository(TContextInterface DbContext) : base(DbContext)
        {
            CurrentDataContext = DBContext;
        }
        public GenericEntityReadOnlyRepository()
        {
        }
    }
}
