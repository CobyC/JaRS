using JARS.Core;
using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Repositories;
using JARS.Data.NH.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JARS.Data.NH.Repositories
{
    /// <summary>
    /// This is the base CRUD repository item.
    /// It has a property DBContext that holds the connection to the database.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DataRepositoryNhCrudBase<TEntity> : DataRepositoryNhReadOnlyBase<TEntity>, IDataRepositoryCrudBase<TEntity>
    where TEntity : class, IEntityBase
        //where TContextInterface : IDataContextNhBase
    {
        public DataRepositoryNhCrudBase() { }
        public DataRepositoryNhCrudBase(IDataContextBaseNh context) : base(context)
        { }
        /// <summary>
        /// Internal method used to assign values to the auditable properties if a entity implements the IAuditableEntity interface
        /// </summary>
        /// <param name="entity">The entity that implements the IAuditableEntity interface</param>
        /// <param name="modifiedBy">The value that will be assigned to the ModifiedBy property.</param>
        void SetAuditableValues(IEntityWithAudit entity, string modifiedBy)
        {
            if (entity.CreatedBy == null)
                entity.CreatedBy = modifiedBy;

            if (entity.CreatedDate == DateTime.MinValue)
                entity.CreatedDate = DateTime.Now;

            entity.ModifiedBy = modifiedBy;
            if (entity.ModifiedDate == DateTime.MinValue)
                entity.ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// Create or update an entity in the database.
        /// If the entity does not exist it will be created, if it does exist it will be updated with the changed values.
        /// </summary>
        /// <param name="entity">The entity to update or create</param>
        /// <param name="modifiedBy">a value indicating what or who the entity was modified by. if it is a non existing entity
        /// this value will also be used for the createdBy value.</param>
        /// <returns>the created or updated entity.</returns>
        public TEntity CreateUpdate(TEntity entity, string modifiedBy)//, string createdBy = null)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        if (entity is IEntityWithAudit)
                            SetAuditableValues(entity as IEntityWithAudit, modifiedBy);

                        s.SaveOrUpdate(entity);
                        t.Commit();
                    }
                    catch (StaleStateException ssx)
                    {
                        t.Rollback();
                        entity = default(TEntity);
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("Update<T>(T entity)) Error:{0}", ssx.Message));
                    }
                    catch (Exception tx)
                    {
                        t.Rollback();
                        Logger.Error(tx.Message);
                        throw tx;
                    }
                }
            }
            return entity;
        }

        /// <summary>
        /// The async version of the CreateUpdate Method
        /// </summary>
        public async Task<TEntity> CreateUpdateAsync(TEntity entity, string modifiedBy, CancellationToken cancellationToken = default)//, string createdBy = null)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        if (entity is IEntityWithAudit)
                            SetAuditableValues(entity as IEntityWithAudit, modifiedBy);

                        await s.SaveOrUpdateAsync(entity, cancellationToken);
                        await t.CommitAsync();
                    }
                    catch (StaleStateException ssx)
                    {
                        await t.RollbackAsync();
                        entity = default(TEntity);
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("CreateUpdateAsync<T>(T entity)) Error:{0}", ssx.Message));
                    }
                    catch (Exception tx)
                    {
                        await t.RollbackAsync();
                        Logger.Error(tx.Message);
                        throw tx;
                    }
                }
            }
            return entity;
        }

        public IList<TEntity> CreateUpdateList(IList<TEntity> entityList, string modifiedBy)//, string createdBy = null)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        foreach (TEntity entity in entityList)
                        {
                            if (entity is IEntityWithAudit)
                                SetAuditableValues(entity as IEntityWithAudit, modifiedBy);

                            s.SaveOrUpdate(entity);
                        }
                        t.Commit();
                    }
                    catch (StaleStateException ssx)
                    {
                        t.Rollback();
                        entityList = default(IList<TEntity>);
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("Update<T>(T entity)) Error:{0}", ssx.Message));
                    }
                    catch (Exception tx)
                    {
                        t.Rollback();
                        Logger.Error(tx.Message);
                        throw tx;
                    }
                }
            }
            return entityList;
        }

        public async Task<IList<TEntity>> CreateUpdateListAsync(IList<TEntity> entityList, string modifiedBy, CancellationToken cancellationToken = default)//, string createdBy = null)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        foreach (TEntity entity in entityList)
                        {
                            if (entity is IEntityWithAudit)
                                SetAuditableValues(entity as IEntityWithAudit, modifiedBy);

                            await s.SaveOrUpdateAsync(entity, cancellationToken);
                        }
                        await t.CommitAsync();
                    }
                    catch (StaleStateException ssx)
                    {
                        await t.RollbackAsync();
                        entityList = default(IList<TEntity>);
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("CreateUpdateListAsync<T>(T entity)) Error:{0}", ssx.Message));
                    }
                    catch (Exception tx)
                    {
                        await t.RollbackAsync();
                        Logger.Error(tx.Message);
                        throw tx;
                    }
                }
            }
            return entityList;
        }


        public TEntity Merge(TEntity entity, string modifiedBy)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        if (entity is IEntityWithAudit)
                            SetAuditableValues(entity as IEntityWithAudit, modifiedBy);

                        entity = s.Merge(entity);
                        t.Commit();
                    }
                    catch (StaleStateException ssx)
                    {
                        t.Rollback();
                        entity = default(TEntity);
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("Merge<T>(T entity)) Error:{0}", ssx.Message));
                    }
                    catch (Exception tx)
                    {
                        t.Rollback();
                        Logger.Error(tx.Message);
                        throw tx;
                    }
                }
            }
            return entity;
        }

        public async Task<TEntity> MergeAsync(TEntity entity, string modifiedBy, CancellationToken cancellationToken = default)//, string createdBy = null)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        if (entity is IEntityWithAudit)
                            SetAuditableValues(entity as IEntityWithAudit, modifiedBy);

                        entity = await s.MergeAsync(entity, cancellationToken);
                        await t.CommitAsync();
                    }
                    catch (StaleStateException ssx)
                    {
                        await t.RollbackAsync();
                        entity = default(TEntity);
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("MergeAsync<T>(T entity)) Error:{0}", ssx.Message));
                    }
                    catch (Exception tx)
                    {
                        await t.RollbackAsync();
                        Logger.Error(tx.Message);
                        throw tx;
                    }
                }
            }
            return entity;
        }

        public IList<TEntity> MergeList(IList<TEntity> entityList, string modifiedBy)//, string createdBy = null)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        foreach (TEntity entity in entityList)
                        {
                            if (entity is IEntityWithAudit)
                                SetAuditableValues(entity as IEntityWithAudit, modifiedBy);

                            s.Merge(entity);
                        }
                        t.Commit();
                    }
                    catch (StaleStateException ssx)
                    {
                        t.Rollback();
                        entityList = default(IList<TEntity>);
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("Merge<T>(T entity)) Error:{0}", ssx.Message));
                    }
                    catch (Exception tx)
                    {
                        t.Rollback();
                        Logger.Error(tx.Message);
                        throw tx;
                    }
                }
            }
            return entityList;
        }

        public async Task<IList<TEntity>> MergeListAsync(IList<TEntity> entityList, string modifiedBy, CancellationToken cancellationToken = default)//, string createdBy = null)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        foreach (TEntity entity in entityList)
                        {
                            if (entity is IEntityWithAudit)
                                SetAuditableValues(entity as IEntityWithAudit, modifiedBy);

                            await s.MergeAsync(entity, cancellationToken);
                        }
                        await t.CommitAsync();
                    }
                    catch (StaleStateException ssx)
                    {
                        await t.RollbackAsync();
                        entityList = default;
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("MergeListAsync<T>(T entity)) Error:{0}", ssx.Message));
                    }
                    catch (Exception tx)
                    {
                        await t.RollbackAsync();
                        Logger.Error(tx.Message);
                        throw tx;
                    }
                }
            }
            return entityList;
        }


        public void Delete(TEntity entity)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        TEntity ent = s.Get<TEntity>(entity.Id);
                        if (ent != null)
                        {
                            s.Delete(entity);
                            t.Commit();
                        }
                    }
                    catch (StaleStateException ssx)
                    {
                        t.Rollback();
                        entity = default(TEntity);
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("(Delete(T delObj)) Error:{0}", ssx.Message));
                    }
                    catch (Exception dx)
                    {
                        t.Rollback();
                        Logger.Error(dx.Message);
                        throw dx;
                    }
                }
            }
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        TEntity ent = await s.GetAsync<TEntity>(entity.Id);
                        if (ent != null)
                        {
                            await s.DeleteAsync(entity, cancellationToken);
                            await t.CommitAsync();
                        }
                    }
                    catch (StaleStateException ssx)
                    {
                        await t.RollbackAsync();
                        entity = default(TEntity);
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("(DeleteAsync(T delObj)) Error:{0}", ssx.Message));
                    }
                    catch (Exception dx)
                    {
                        await t.RollbackAsync();
                        Logger.Error(dx.Message);
                        throw dx;
                    }
                }
            }
        }

        public void Delete(object idKey)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        TEntity ent = s.Get<TEntity>(idKey);
                        if (ent != null)
                        {
                            s.Delete(ent);
                            t.Commit();
                        }
                    }
                    catch (StaleStateException ssx)
                    {
                        t.Rollback();
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("(Delete(int id)) Error:{0}", ssx.Message));
                    }
                    catch (Exception dx)
                    {
                        t.Rollback();
                        Logger.Error(dx.Message);
                        throw dx;
                    }
                }
            }
        }

        public async Task DeleteAsync(object idKey, CancellationToken cancellationToken = default)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        object ent = await s.GetAsync<TEntity>(idKey);
                        if (ent != null)
                        {
                            await s.DeleteAsync(ent, cancellationToken);
                            await t.CommitAsync();
                        }
                    }
                    catch (StaleStateException ssx)
                    {
                        await t.RollbackAsync();
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("(DeleteAsync(int id)) Error:{0}", ssx.Message));
                    }
                    catch (Exception dx)
                    {
                        await t.RollbackAsync();
                        Logger.Error(dx.Message);
                        throw dx;
                    }
                }
            }
        }

        public void DeleteListByIds(IList<object> idKeysList)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        bool doCommit = false;
                        foreach (var item in idKeysList)
                        {
                            TEntity ent = s.Get<TEntity>(item.ToString());
                            if (ent != null)
                            {
                                doCommit = true;
                                s.Delete(ent);
                            }
                        }
                        if (doCommit)
                            t.Commit();
                    }
                    catch (StaleStateException ssx)
                    {
                        t.Rollback();
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("(Delete(List<idKeys> keys)) Error:{0}", ssx.Message));
                    }
                    catch (Exception dx)
                    {
                        t.Rollback();
                        Logger.Error(dx.Message);
                        throw dx;
                    }
                }
            }
        }

        public async Task DeleteListByIdsAsync(IList<object> idKeysList, CancellationToken cancellationToken = default)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        bool doCommit = false;
                        foreach (var item in idKeysList)
                        {
                            TEntity ent = await s.GetAsync<TEntity>(item.ToString(), cancellationToken);
                            if (ent != null)
                            {
                                doCommit = true;
                                await s.DeleteAsync(ent);
                            }
                        }
                        if (doCommit)
                            await t.CommitAsync();
                    }
                    catch (StaleStateException ssx)
                    {
                        await t.RollbackAsync();
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("(DeleteListAsync(List<idKeys> keys)) Error:{0}", ssx.Message));
                    }
                    catch (Exception dx)
                    {
                        await t.RollbackAsync();
                        Logger.Error(dx.Message);
                        throw dx;
                    }
                }
            }
        }

        /// <summary>
        /// Delete a list of items from the database asynchronously.
        /// </summary>
        /// <param name="entityList">the list of items that needs to be deleted</param>
        /// <returns> async task</returns>
        public async Task DeleteListAsync(IList<TEntity> entityList, CancellationToken cancellationToken = default)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        bool doCommit = false;
                        foreach (var item in entityList)
                        {
                            TEntity ent = await s.GetAsync<TEntity>(item.Id, cancellationToken);
                            if (ent != null)
                            {
                                doCommit = true;
                                await s.DeleteAsync(ent, cancellationToken);
                            }
                        }
                        if (doCommit)
                            await t.CommitAsync();
                    }
                    catch (StaleStateException ssx)
                    {
                        await t.RollbackAsync();
                        Logger.Error(ssx.Message);
                        throw new StaleStateException(string.Format("(DeleteListAsync(IList<T> delObj)) Error:{0}", ssx.Message));
                    }
                    catch (Exception dx)
                    {
                        await t.RollbackAsync();
                        Logger.Error(dx.Message);
                        throw dx;
                    }
                }
            }
        }

        /// <summary>
        /// Delete a list of items from the database.
        /// </summary>
        /// <param name="entityList">the list of items that needs to be deleted</param>        
        public void DeleteList(IList<TEntity> entityList)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        bool doCommit = false;
                        foreach (var item in entityList)
                        {
                            TEntity ent = s.Get<TEntity>(item.Id);
                            if (ent != null)
                            {
                                doCommit = true;
                                s.Delete(ent);
                            }
                        }
                        if (doCommit)
                            t.Commit();
                    }
                    catch (StaleStateException ssx)
                    {
                        t.Rollback();
                        throw new StaleStateException(string.Format("(DeleteListAsync(IList<T> delObj)) Error:{0}", ssx.Message));
                    }
                    catch (Exception dx)
                    {
                        t.Rollback();
                        throw dx;
                    }
                }
            }
        }
    }
}
