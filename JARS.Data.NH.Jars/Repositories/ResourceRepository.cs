using JARS.Core;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using NHibernate;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IResourceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ResourceRepository : DataRepositoryNhEagerlyBase<Resource>, IResourceRepository
    {
        [ImportingConstructor()]
        public ResourceRepository(IDataContextNhJars DbContext) : base(DbContext)
        {
        }

        /// <summary>
        /// Assigns a group to a worker, this assumes that the group does not exist and will link the group regardless if the link already exists or not.
        /// </summary>
        /// <param name="worker">The worker that contains the link to the group</param>
        /// <param name="groupToAdd">The group that will be linked to the user.</param>
        /// <returns></returns>
        public Resource AddGroupToResource(Resource worker, ResourceGroup groupToAdd)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        worker.Groups.Add(groupToAdd);
                        s.SaveOrUpdate(worker);
                        t.Commit();
                    }
                    catch (Exception ex)
                    {
                        t.Rollback();
                        Logger.Error(ex.Message, ex);
                        throw ex;
                    }
                }
            }
            return worker;
        }

        /// <summary>
        /// This will remove the link between the group and the worker. 
        /// The Worker that is passed as a parameter needs to contain the group in its lis.
        /// If it doesn't contain the group in the list the Function will first make a call to the database to get the fully loaded Worker
        /// and then remove the group if it exists.
        /// </summary>
        /// <param name="currentWorker">The worker that has a link to the group</param>
        /// <param name="groupToRemove">The group that will be unlinked from the worker</param>
        public void RemoveGroupFromResource(Resource currentWorker, ResourceGroup groupToRemove)
        {

            if (currentWorker.Groups.Count > 0)
            {
                currentWorker.Groups.Remove(groupToRemove);
                this.CreateUpdate(currentWorker, "SYSTEM");
            }
            else //load the full list and then do a remove
            {


                using (ISession s = DBContext.SessionFactory.OpenSession())
                {
                    using (ITransaction t = s.BeginTransaction())
                    {
                        try
                        {
                            var query = s.QueryOver<Resource>()
                                .Where(r => r.Id == currentWorker.Id)
                                .Future(); //<- indicate that the query is for the root of Group
                            s.QueryOver<Resource>()
                                .Fetch(SelectMode.FetchLazyProperties, w => w.Groups)
                                //.Fetch(w => w.Groups).Eager
                                .Future();//<- load the users as a separate query

                            currentWorker = query.FirstOrDefault();
                            //now remove the group
                            currentWorker.Groups.Remove(currentWorker.Groups.FirstOrDefault(g => g.Id == groupToRemove.Id));
                            s.Update(currentWorker);
                        }
                        catch (Exception ex)
                        {
                            t.Rollback();
                            Logger.Error(ex.Message, ex);
                            throw ex;
                        }
                    }
                }
            }
        }
    }
}
