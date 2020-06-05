using JARS.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace JARS.Core.Interfaces.Repositories
{

    /// <summary>
    /// The default interface declared for helping with use with DI and MEF, because there is a possibility that not all repositories
    /// are going to be generic, we need to be able to use a non generic interface.
    /// </summary>
    public interface IDataRepositoryBase
    { }

    public interface IDataRepositoryBase<TEntity> : IDataRepositoryBase
        where TEntity : class, IEntityBase
    { }

    /// <summary>
    /// The default generic read only interface will be used for all repositories that require read only and where an entity implements the IEntityBase interface.
    /// </summary>
    /// <typeparam name="TEntity">The entity type that derives from IEntityBase</typeparam>
    public interface IDataRepositoryReadOnlyBase<TEntity> : IDataRepositoryBase<TEntity>
        where TEntity : class, IEntityBase
    {
        #region Read Only Functions
        /// <summary>
        /// Get all the records of <typeparamref name="TEntity"/> from the database, this could be a long running process if there are loads of information that needs to be sent back.
        /// for a long running process use the async overload of this method.        
        /// </summary>
        /// <param name="eagerly"> means that if the implementing class has the option information will be fetched eagerly. (child entities and their child entities will be fetched).</param>
        /// <param name="childProperies"> When fetching eagerly, and only certain child properties need to be fetched eagerly, then they can be specified.</param>
        /// <returns>All the records from the database</returns>
        IList<TEntity> GetAll(bool eagerly = false, List<Expression<Func<TEntity, object>>> childProperies = null);
        /// <summary>
        /// This is the Async overload of the GetAll() method.        
        /// </summary>
        /// <param name="eagerly"> means that if the implementing class has the option information will be fetched eagerly. (child entities and their child entities will be fetched).</param>
        /// <param name="childProperies"> When fetching eagerly, and only certain child properties need to be fetched eagerly, then they can be specified.</param>
        /// <returns>a list of <typeparamref name="TEntity"/></returns>
        Task<IList<TEntity>> GetAllAsync(bool eagerly = false, List<Expression<Func<TEntity, object>>> childProperies = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Data keys can be anything that is specified as a key (GUID, string, int etc) so this overload is to enable us to handle them.
        /// if this value is used in conjunction with a database that requires an int, please make sure that you parse the value in the implementation.        
        /// </summary>
        /// <param name="idKey">The Key or id of the record</param>
        /// <param name="eagerly"> means that if the implementing class has the option information will be fetched eagerly. (child entities and their child entities will be fetched).</param>
        /// <param name="childProperies"> When fetching eagerly, and only certain child properties need to be fetched eagerly, then they can be specified.</param>
        TEntity GetById(object idKey, bool eagerly = false, List<Expression<Func<TEntity, object>>> childProperies = null);
        Task<TEntity> GetByIdAsync(object idKey, bool eagerly = false, List<Expression<Func<TEntity, object>>> childProperies = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Supply a simple linq query that will be used for getting the data
        /// The query is supplied to the "Where()" expression of the NHibernate QueryOver().Where(whereQuery) function.        
        /// </summary>
        /// <param name="whereExpr">The Linq expression query</param>
        /// <param name="eagerly"> means that if the implementing class has the option information will be fetched eagerly. (child entities and their child entities will be fetched).</param>
        /// <param name="childProperies"> When fetching eagerly, and only certain child properties need to be fetched eagerly, then they can be specified.</param>
        /// <returns>a list of <typeparamref name="TEntity"/></returns>
        IList<TEntity> Where(Expression<Func<TEntity, bool>> whereExpr, bool eagerly = false, List<Expression<Func<TEntity, object>>> childProperies = null);

        /// <summary>
        /// Supply a simple linq query that will be used for getting the data asynchronously
        /// The query is supplied to the "Where()" expression of the NHibernate QueryOver().Where(whereQuery) function.        
        /// </summary>
        /// <param name="whereExpr">The Linq Expression query</param>
        /// <param name="eagerly"> means that if the implementing class has the option information will be fetched eagerly. (child entities and their child entities will be fetched).</param>
        /// <param name="childProperies"> When fetching eagerly, and only certain child properties need to be fetched eagerly, then they can be specified.</param>
        /// <returns>List of <typeparamref name="TEntity"/></returns>
        Task<IList<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> whereExpr, bool eagerly = false, List<Expression<Func<TEntity, object>>> childProperies = null, CancellationToken cancellationToken = default);

        #endregion

    }

    /// <summary>
    /// The default generic interface will be used for all repositories where an entity implements the IEntityBase interface.
    /// </summary>
    /// <typeparam name="TEntity">The entity Type</typeparam>
    public interface IDataRepositoryCrudBase<TEntity> : IDataRepositoryReadOnlyBase<TEntity>
        where TEntity : class, IEntityBase
    {

        #region Write or update methods

        //void Create(T entity, string createdBy = null);

        /// <summary>
        /// Create or update the entity passed to this function. 
        /// It will return the updated entity.
        /// </summary>
        /// <param name="entity">the entity to update or create</param>
        /// <param name="modifiedBy">what or who modified the entity</param>
        /// <returns></returns>
        TEntity CreateUpdate(TEntity entity, string modifiedBy);//, string createdBy = null);
        Task<TEntity> CreateUpdateAsync(TEntity entity, string modifiedBy, CancellationToken cancellationToken);//, string createdBy = null);

        IList<TEntity> CreateUpdateList(IList<TEntity> entityList, string modifiedBy);//, string createdBy = null);

        Task<IList<TEntity>> CreateUpdateListAsync(IList<TEntity> entityList, string modifiedBy, CancellationToken cancellationToken);//, string createdBy = null);

        /// <summary>
        /// Update only the changed fields in an entity passed to this function. 
        /// It will return the updated entity.
        /// </summary>
        /// <param name="entity">the entity to update</param>
        /// <param name="modifiedBy">what or who modified the entity</param>
        /// <returns></returns>
        TEntity Merge(TEntity entity, string modifiedBy);

        Task<TEntity> MergeAsync(TEntity entity, string modifiedBy, CancellationToken cancellationToken);

        /// <summary>
        /// Merge only the changed fields in an entity passed to this function. 
        /// It will return the updated entity.
        /// </summary>
        /// <param name="entity">the entity to update</param>
        /// <param name="modifiedBy">what or who modified the entity</param>
        /// <returns></returns>
        IList<TEntity> MergeList(IList<TEntity> entityList, string modifiedBy);

        Task<IList<TEntity>> MergeListAsync(IList<TEntity> entityList, string modifiedBy, CancellationToken cancellationToken);


        /// <summary>
        ///Data keys can be anything that is specified as a key (GUID, string, int etc) so this overload is to enable us to handle them.
        /// if this value is used in conjunction with a database that requires an int, please make sure that you parse the value in the implementation.
        /// </summary>
        /// <param name="idKey">The Key or id of the record</param>
        void Delete(object idKey);
        Task DeleteAsync(object idKey, CancellationToken cancellationToken);

        /// <summary>
        /// delete the passed in entity from the database
        /// </summary>
        /// <param name="entity">the entity that will be deleted.</param>
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        /// <summary>
        /// Delete a list of entities from the database.
        /// </summary>
        /// <param name="entityList">The entities that will be deleted</param>
        void DeleteList(IList<TEntity> entityList);
        Task DeleteListAsync(IList<TEntity> entityList, CancellationToken cancellationToken);

        /// <summary>
        /// Delete entities by using a list of Ids.
        /// </summary>
        /// <param name="idKeysList">a list of ids used for deleting the corresponding entities</param>
        void DeleteListByIds(IList<object> idKeysList);

        Task DeleteListByIdsAsync(IList<object> idKeysList, CancellationToken cancellationToken);
        #endregion

    }
}
