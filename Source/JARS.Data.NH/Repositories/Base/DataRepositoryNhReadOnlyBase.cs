using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Repositories;
using JARS.Data.NH.Interfaces;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace JARS.Data.NH.Repositories
{
    /// <summary>
    /// This is the base Read Only repository item.
    /// It has a property DBContext that holds the connection to the database.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class DataRepositoryNhReadOnlyBase<TEntity> : DataRepositoryNhBase, IDataRepositoryReadOnlyBase<TEntity>
       where TEntity : class, IEntityBase
        //where TContextInterface : IDataContextNhBase
    {
        public DataRepositoryNhReadOnlyBase() { }
        public DataRepositoryNhReadOnlyBase(IDataContextBaseNh context) : base(context)
        { }

        #region eager example
        //DO NOT USE Distinct in Eager loading!!
        //retList = s.QueryOver<Group>().Fetch(g => g.UsersInGroup).Eager.List().Distinct().ToList(); //<-wrong!
        #endregion

        #region Eagerly helper
        /// <summary>
        /// Extract the properties of type IList (child collections) to help with the eager loading functions 
        /// where you need to pass in the collection properties.
        /// </summary>
        /// <returns></returns>
        internal List<Expression<Func<TEntity, object>>> GetListPropertiesAsExpressionList()
        {
            List<Expression<Func<TEntity, object>>> list = new List<Expression<Func<TEntity, object>>>();
            IEnumerable<PropertyInfo> pi = typeof(TEntity).GetProperties().Select(p => p).Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(IList<>));
            foreach (var p in pi)
            {
                var param = Expression.Parameter(typeof(TEntity));
                var field = Expression.PropertyOrField(param, p.Name);
                list.Add(Expression.Lambda<Func<TEntity, object>>(field, param));
            }
            return list;
        }

        ///// <summary>
        ///// Set the child properties on the future query, this is for doing eager loading of properties.
        ///// </summary>
        ///// <param name="childProperties">The list of properties that needs to load eagerly</param>
        ///// <param name="s">The nHibernate current session</param>
        //internal IQueryable<TEntity> SetFutureProperties(List<Expression<Func<TEntity, object>>> childProperties, IQueryable<TEntity> qry)
        //{
        //    foreach (var expr in childProperties)
        //    {
        //        qry.Fetch(expr) //<-- this is used in Query
        //        .ToFuture();//.Future();//<-- this is used in queryOver
        //    }
        //    return qry;
        //}


        /// <summary>
        /// Help populate the child collections using reflection.
        /// Iterate over the properties that are child objects of the main object.
        /// Then asking for the properties hydrates the children via the proxy that is still open.
        /// The method has to live within a live session.
        /// </summary>
        /// <param name="childProperies">The child properties to hydrate</param>
        /// <param name="parentEntity">The entity that needs has the child properties.</param>
        internal void TryPopulateChildProperties(List<Expression<Func<TEntity, object>>> childProperies, TEntity parentEntity)
        {
            foreach (var cp in childProperies)
            {
                var expression = (MemberExpression)cp.Body;
                string name = expression.Member.Name;
                var x = parentEntity.GetType().GetProperty(name);
                if (x.PropertyType.GetGenericTypeDefinition() == typeof(IList<>))
                { var hydrateCount = ((IList)x.GetValue(parentEntity)).Count; }

            }
        }
        #endregion

        /// <summary>
        /// Get all the entities of a certain type.
        /// When the <paramref name="eagerly"/> is set to true the load all entities eagerly 
        /// meaning sub entities will also be loaded.(parent containing child entities).
        /// If the <paramref name="childProperies"/> is set only those sub entities will be loaded.
        /// otherwise if <paramref name="childProperies"/> is null all child entities found in the parent entity
        /// will be loaded
        /// to specify sub entities pass a list of expressions 
        /// <example>Example of use for eager loading properties:
        /// <code>
        ///     var expr = new List&lt;Expression&lt;Func&lt;T, object&gt;&gt;&gt;();  
        ///     expr.Add(j => j.Children); 
        ///     expr.Add(j => j.Subs); 
        ///     GetAll(true, expr); 
        /// </code>    
        /// </example>  
        /// </summary>      
        /// <param name="childProperies">List&lt;Expression&lt;Func&lt;T, object&gt;&gt;&gt;{ expr1, expr2, expr3 }</param>
        /// <returns>returns a result if found</returns>
        public IList<TEntity> GetAll(bool eagerly = false, List<Expression<Func<TEntity, object>>> childProperies = null)
        {
            IList<TEntity> resList;
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    if (!eagerly)
                        resList = s.CreateCriteria(typeof(TEntity)).List<TEntity>();
                    else
                    {
                        if (childProperies == null)
                            childProperies = GetListPropertiesAsExpressionList();

                        var query = s.QueryOver<TEntity>().Future();
                        //s.QueryOver<TEntity>().Fetch(SelectMode.JoinOnly, childProperties.ToArray());
                        //SetFutureProperties(childProperties, s);
                        resList = query.ToList();
                        foreach (var entity in resList)
                            TryPopulateChildProperties(childProperies, entity);
                    }
                }
            }

            //if (!eagerly)
            //    resList.DisableNHibernateLazyProxyReferences();
            return resList;
        }

        /// <summary>
        /// This is the async version of the <see cref="GetAll(bool, List{Expression{Func{TEntity, object}}})"/> function, it works in the same way except it is async.        
        /// </summary>
        /// <param name="eagerly"></param>
        /// <param name="childProperies"></param>
        /// <returns></returns>
        public async Task<IList<TEntity>> GetAllAsync(bool eagerly = false, List<Expression<Func<TEntity, object>>> childProperies = null, CancellationToken cancellationToken = default)
        {
            IList<TEntity> retList;
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    if (!eagerly)
                        retList = await s.CreateCriteria<TEntity>().ListAsync<TEntity>(cancellationToken);
                    else
                    {
                        if (childProperies == null)
                            childProperies = GetListPropertiesAsExpressionList();

                        var query = s.Query<TEntity>();//.Future();
                                                       //SetFutureProperties(childProperties, s);
                        var resList = await query.ToListAsync(cancellationToken);
                        retList = await Task.Run(() =>
                        {
                            //this will trigger eager loading, and could slow down the data retrieval
                            foreach (var entity in resList)
                                TryPopulateChildProperties(childProperies, entity);
                            return resList;
                        });

                    }
                }
            }
            //if (!eagerly)
            //    retList.DisableNHibernateLazyProxyReferences();
            return retList;
        }

        /// <summary>
        /// Use this method returns a single entity that matches the Id (this is the ID in the database). 
        /// To load the entity eagerly so that it contain sub entities (parent containing child entities), 
        /// set the <paramref name="eagerly"/> to true.
        /// if the<paramref name="childProperies"/> is not set all the sub entities will be loaded, but to only load specific sub entities
        /// the property can be set.
        /// <example>Example of use for eager loading properties:
        /// <code>
        ///     var expr = new List&lt;Expression&lt;Func&lt;T, object&gt;&gt;&gt;();  
        ///     expr.Add(j => j.Children); 
        ///     expr.Add(j => j.Subs); 
        ///     GetById(1,true, expr); 
        /// </code>    
        /// </example>  
        /// </summary>             
        /// <param name="eagerly">Set this to true to fetch entities eagerly (with child entities)</param>
        /// <param name="childProperies">List&lt;Expression&lt;Func&lt;T, object&gt;&gt;&gt;{ expr1, expr2, expr3 }</param>
        /// <returns>returns a result if found</returns>
        public TEntity GetById(object idKey, bool eagerly = false, List<Expression<Func<TEntity, object>>> childProperies = null)
        {
            TEntity retEntity = default(TEntity);
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    if (!eagerly)
                        retEntity = s.Get<TEntity>(idKey);
                    else
                    {
                        if (childProperies == null)
                            childProperies = GetListPropertiesAsExpressionList();

                        var query = s.QueryOver<TEntity>()
                       .Where(x => x.Id == idKey);
                        //.Future();
                        //SetFutureProperties(childProperties, s);
                        TEntity entity = query.SingleOrDefault();
                        //this will trigger eager loading, and could slow down the data retrieval                        
                        TryPopulateChildProperties(childProperies, entity);
                        retEntity = entity;
                    }
                }
            }
            //if (!eagerly)
            //    retEntity.DisableNHibernateLazyProxyReferences();

            return retEntity;
        }

        /// <summary>
        /// This is the async version of the <see cref="GetById(object idKey, bool, List{Expression{Func{TEntity, object}}})"/> function, it works in the same way except it is async.        
        /// </summary>
        /// <param name="idKey">the Id of the record in the data store</param>
        /// <param name="eagerly"></param>
        /// <param name="childProperies"></param>
        /// <returns></returns>
        public async Task<TEntity> GetByIdAsync(object idKey, bool eagerly = false, List<Expression<Func<TEntity, object>>> childProperies = null, CancellationToken cancellationToken = default)
        {
            TEntity retEntity = default(TEntity);
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    if (!eagerly)
                        retEntity = await s.GetAsync<TEntity>(idKey, cancellationToken);
                    else
                    {
                        if (childProperies == null)
                            childProperies = GetListPropertiesAsExpressionList();

                        var query = s.QueryOver<TEntity>();
                        //.Where(x => x.Id == idKey);
                        //.Future();
                        //SetFutureProperties(childProperties, s);
                        var entity = await query.Where(j => j.Id == idKey).SingleOrDefaultAsync(cancellationToken);
                        retEntity = await Task.Run(() =>
                        {
                            //this will trigger eager loading, and could slow down the data retrieval                           
                            TryPopulateChildProperties(childProperies, entity);
                            return entity;
                        });
                    }
                }
            }

            //if (!eagerly)
            //    retEntity.DisableNHibernateLazyProxyReferences();
            return retEntity;
        }

        /// <summary>
        /// Supply a simple linq query that will be used for getting the data
        /// The query is supplied to the "Where()" expression of the NHibernate QueryOver().Where(whereQuery) function.
        /// The eagerly flag will indicate if child object also needs to be loaded.
        /// to load only specific child objects when eager loading is selected, supply an expression in the <paramref name="childProperies"/> parameter.
        ///<example>Example of use for eager loading properties:
        /// <code>
        ///     var expr = new List&lt;Expression&lt;Func&lt;T, object&gt;&gt;&gt;();  
        ///     expr.Add(j => j.Children); 
        ///     expr.Add(j => j.Subs); 
        ///     Where(x=> x.yz=="ABC",true, expr); 
        /// </code>    
        /// </example>  
        /// </summary> 
        /// <param name="query">The Linq expression query</param>
        /// <returns>a list of T</returns>
        public IList<TEntity> Where(Expression<Func<TEntity, bool>> whereExpr, bool eagerly = false, List<Expression<Func<TEntity, object>>> childProperies = null)
        {
            IList<TEntity> resList = default(IList<TEntity>);
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    if (!eagerly)
                        resList = s.Query<TEntity>().Where(whereExpr).ToList();
                    else
                    {
                        if (childProperies == null)
                            childProperies = GetListPropertiesAsExpressionList();

                        var query = s.Query<TEntity>().Where(whereExpr);
                        resList = query.ToList();
                        //this will trigger eager loading, and could slow down the data retrieval
                        foreach (var entity in resList)
                            TryPopulateChildProperties(childProperies, entity);
                    }
                }
            }
            //if (!eagerly)
            //    resList.DisableNHibernateLazyProxyReferences();

            return resList;
        }



        /// <summary>
        /// This is the async version of the <see cref="Where(Expression{Func{TEntity, bool}}, bool, List{Expression{Func{TEntity, object}}})"/> function, it works in the same way except it is async.        
        /// </summary>
        /// <param name="whereExpr">the linq expression to pass to the query</param>
        /// <param name="eagerly"></param>
        /// <param name="childProperies"></param>
        /// <returns></returns>
        public async Task<IList<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> whereExpr, bool eagerly = false, List<Expression<Func<TEntity, object>>> childProperies = null, CancellationToken cancellationToken = default)
        {
            IList<TEntity> retList = null;
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    if (!eagerly)
                    {
                        //return await s.QueryOver<TEntity>().Where(whereExpr).ListAsync<TEntity>();
                        var query = s.Query<TEntity>().Where(whereExpr);
                        retList = await query.ToListAsync(cancellationToken);
                    }
                    else
                    {
                        if (childProperies == null)
                            childProperies = GetListPropertiesAsExpressionList();

                        var query = s.Query<TEntity>()
                       .Where(whereExpr);
                        var resList = await query.ToListAsync(cancellationToken);

                        retList = await Task.Run(() =>
                        {
                            //this will trigger eager loading, and could slow down the data retrieval
                            foreach (var entity in resList)
                                TryPopulateChildProperties(childProperies, entity);
                            return resList;
                        });
                    }
                }
            }
            //if (!eagerly)
            //    retList.DisableNHibernateLazyProxyReferences();

            return retList;
        }
    }
}
