using JARS.Core;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using NHibernate;
using System;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IResourceGroupRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ResourceGroupRepository : DataRepositoryNhEagerlyBase<ResourceGroup>, IResourceGroupRepository
    {
        [ImportingConstructor()]
        public ResourceGroupRepository(IDataContextNhJars context) : base(context)
        {

        }
        /// <summary>
        /// This will link the worker to the group, it does not check if the group already has a link to the worker.
        /// </summary>
        /// <param name="group">The group that will contain the link to the worker</param>
        /// <param name="resourceToAdd">The worker that will be linked to the group</param>
        /// <returns></returns>
        public ResourceGroup AddResourceToGroup(ResourceGroup group, Resource resourceToAdd)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        group.Resources.Add(resourceToAdd);
                        s.SaveOrUpdate(group);
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
            return group;
        }

        /// <summary>
        /// This will remove the link between the worker and the group.
        /// </summary>
        /// <param name="currentGroup">The group containing the link.</param>
        /// <param name="resourceToRemove">The worker that will be removed from the group</param>
        public void RemoveResourceFromGroup(ResourceGroup currentGroup, Resource resourceToRemove)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {
                        currentGroup.Resources.Remove(resourceToRemove);
                        s.Update(currentGroup);
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
        }

    }
}
