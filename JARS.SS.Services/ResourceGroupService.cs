using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Utils;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Entities;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using NHibernate.Linq;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JARS.SS.Services
{
    [Authenticate]
    public class ResourceGroupService : ServicesBase
    {

        /// <summary>
        /// Returns a single group where the id in the request matches the record ID in the database.
        /// </summary>
        /// <param name="request">The request used for requesting the record from the database,it only looks at the ID value and none of the other parameters..</param>
        /// <returns>The group record as a fully loaded object</returns>
        public virtual ResourceGroupResponse Any(GetResourceGroup request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                ResourceGroupResponse response = new ResourceGroupResponse();

                //IResourceGroupRepository _repository = _DataRepositoryFactory.GetDataRepository<IResourceGroupRepository>();
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsResourceGroup, IDataContextNhJars>>();
                response.Group = _repository.GetById(request.Id, true).ConvertTo<ResourceGroupDto>();
                return response;
            });
        }

        /// <summary>
        /// Update or create a single resource Group.       
        /// </summary>
        /// <param name="request">The request containing the Group that needs to be created or updated</param>
        /// <returns>The updated Group will be returned.</returns>
        public virtual ResourceGroupResponse Any(StoreResourceGroup request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            ResourceGroupResponse response = new ResourceGroupResponse();
            //IResourceGroupRepository _repository = _DataRepositoryFactory.GetDataRepository<IResourceGroupRepository>();
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsResourceGroup, IDataContextNhJars>>();
            response.Group = _repository.CreateUpdate(request.Group.ConvertTo<JarsResourceGroup>(), CurrentSessionUsername).ConvertTo<ResourceGroupDto>();
            return response;
            //});
        }

        /// <summary>
        /// Update or create  a list of resource Groups                
        /// </summary>
        /// <param name="request">The request containing the Groups that needs to be created or updated</param>
        /// <returns>The updated Groups will be returned.</returns>
        public virtual ResourceGroupsResponse Any(StoreResourceGroups request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            ResourceGroupsResponse response = new ResourceGroupsResponse();
            //IResourceGroupRepository _repository = _DataRepositoryFactory.GetDataRepository<IResourceGroupRepository>();
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsResourceGroup, IDataContextNhJars>>();
            response.Groups = _repository.CreateUpdateList(request.Groups.ConvertAllTo<JarsResourceGroup>().ToList(), CurrentSessionUsername).ConvertAllTo<ResourceGroupDto>().ToList();
            return response;
            //});
        }

        /// <summary>
        /// Find a group or groups depending on the values supplied in the request.
        /// </summary>
        /// <param name="request">the request that will be used for finding (searching) the required group</param>
        /// <returns>The group or groups found, otherwise nothing.</returns>
        public object Any(FindResourceGroups request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            ResourceGroupsResponse response = new ResourceGroupsResponse();
            if (request != null)
            {
                Expression<Func<JarsResourceGroup, bool>> query = LinqExpressionBuilder.True<JarsResourceGroup>();

                if (!request.GroupName.IsNullOrEmpty())
                    query = query.And(o => o.Name.Like($"%{request.GroupName}%"));

                //IsActive
                if (request.IsActive.HasValue)
                    query = query.And(o => o.IsActive == request.IsActive.Value);

                //var _repository = _DataRepositoryFactory.GetDataRepository<IResourceGroupRepository>();
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsResourceGroup, IDataContextNhJars>>();
                var res = _repository.Where(query, request.FetchEagerly).ToList();

                var resDto = res.ConvertAll(g => g.ConvertTo<ResourceGroupDto>());
                response.Groups = resDto;
            }
            return response;
            //});
        }
        public virtual void Any(DeleteResourceGroup request)
        {
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsResourceGroup, IDataContextNhJars>>();
            _repository.Delete(request.Id);
            TrySendDeleteNotificationToChannel(typeof(JarsResourceGroup).Name, new[] { request.Id.ToString() });
        }

        /// <summary>
        /// Send CRUD notifications for a OperativeGroup Entity or Entities
        /// Note! :
        /// This Method is a special method used by the service when ServerEvents are being used.(serviceStack). 
        /// If the service does not implement serverEvents this will throw an error. 
        /// This will send a notification to all subscribed clients (including the client the request originated from) where the chanel name is the name of the entity type.
        /// This will only process SelectorTypes.store and SelectorTypes.delete notifications.
        /// The notification sent to subscribers will be a ServerEventMessage (serviceStack) where the data(json) is set as ServerEventMessageData (Jars) object.
        /// </summary>
        /// <param name="crud">The notification request indicating a store or delete event that will be sent to other subscribers.</param>
        public virtual void Any(ResourceGroupsNotification crud)
        {
            //ExecuteFaultHandledMethod(() =>
            //{
            //check that the sender has subscribed to the service
            //SubscriptionInfo subscriber = ServerEvents.GetSubscriptionInfo(crud.FromSubscriptionId);
            //if (subscriber == null)
            //    throw HttpError.NotFound($"Subscriber {crud.FromUserName} does not exist.");

            //do some job updates here using the info from the the crud
            //IResourceGroupRepository _repository = _DataRepositoryFactory.GetDataRepository<IResourceGroupRepository>();
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsResourceGroup, IDataContextNhJars>>();

            if (crud.Selector == SelectorTypes.store)
            {
                List<ResourceGroupDto> notifyList = _repository.Where(l => crud.Ids.Contains(l.Id)).ConvertAllTo<ResourceGroupDto>().ToList();
                TrySendStoreNotificationToChannel(typeof(JarsResourceGroup).Name, notifyList.ToJson());
            }

            if (crud.Selector == SelectorTypes.delete)
            {
                TrySendDeleteNotificationToChannel(typeof(JarsResourceGroup).Name, crud.Ids.ConvertTo<List<string>>().ToArray());//, true);
            }

            //});
        }
    }
}
