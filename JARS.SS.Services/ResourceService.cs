using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Utils;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Entities;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JARS.SS.Services
{
    [Authenticate]
    public class ResourceService : ServicesBase
    {
        /// <summary>
        /// Returns a single resource where the id in the request matches the record ID in the database.
        /// </summary>
        /// <param name="request">The request used for requesting the record from the database,it only looks at the ID value and none of the other parameters..</param>
        /// <returns>The resource record as a fully loaded object</returns>
        public virtual ResourceResponse Any(GetResource request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            ResourceResponse response = new ResourceResponse();
            //IResourceRepository _repository = _DataRepositoryFactory.GetDataRepository<IResourceRepository>();
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsResource, IDataContextNhJars>>();
            response.Resource = _repository.GetById(request.Id, true).ConvertTo<ResourceDto>();
            return response;
            //});
        }

        public ResourcesResponse Any(FindResources request)
        {

            //return ExecuteFaultHandledMethod(() =>
            //{
            ResourcesResponse response = new ResourcesResponse();
            var query = BuildQuery<JarsResource>(request);
            //var _repository = _DataRepositoryFactory.GetDataRepository<IResourceRepository>();
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsResource, IDataContextNhJars>>();
            var res = _repository.Where(query, request.FetchEagerly).ToList();
            var resDto = res.ConvertAllTo<ResourceDto>().ToList();
            response.Resources = resDto;

            return response;
            // });
        }

        public MobileResourcesResponse Any(FindMobileResources request)
        {          
            MobileResourcesResponse response = new MobileResourcesResponse();
            Expression<Func<JarsResource, bool>> query = LinqExpressionBuilder.True<JarsResource>();

            //default query for mobile resources
            query = query.And(o => o.IsMobileResource == true && o.IsActive == true);

            //Id
            if (request.Id != 0)
                query = query.And(o => o.Id == request.Id);

            //ExternalRef
            if (request.ExternalRef != null && request.ExternalRef != string.Empty)
                query = query.And(j => j.ExtRef == request.ExternalRef);

            //var _repository = _DataRepositoryFactory.GetDataRepository<IResourceRepository>();
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsResource, IDataContextNhJars>>();
            var res = _repository.Where(query, false).ToList();
            var resDto = res.ConvertAllTo<MobileResourceDto>().ToList();

            response.Resources = resDto;

            return response;        
        }

        /// <summary>
        /// Update or create a single resource or a list of resources, depending on whether the Resource or Resources property has got a value set.
        /// If the singular property is set a single record will be created or updated and the list of records will be ignored.
        /// To create or update more than one record, assign a list of values to the multiple property and make sure single value is set to nothing/null.
        /// </summary>
        /// <param name="request">The request containing the resource or resources that needs to be created or updated</param>
        /// <returns>depending on the values supplied, the updated single value or list of values will be returned.</returns>
        public virtual ResourceResponse Any(StoreResource request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            ResourceResponse response = new ResourceResponse();
            //IResourceRepository _repository = _DataRepositoryFactory.GetDataRepository<IResourceRepository>();
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsResource, IDataContextNhJars>>();
            response.Resource = _repository.CreateUpdate(request.Resource.ConvertTo<JarsResource>(), CurrentSessionUsername).ConvertTo<ResourceDto>();
            return response;
            //});
        }

        /// <summary>
        /// Update or create a single resource or a list of resources, depending on whether the Resource or Resources property has got a value set.
        /// If the singular property is set a single record will be created or updated and the list of records will be ignored.
        /// To create or update more than one record, assign a list of values to the multiple property and make sure single value is set to nothing/null.
        /// </summary>
        /// <param name="request">The request containing the resource or resources that needs to be created or updated</param>
        /// <returns>depending on the values supplied, the updated single value or list of values will be returned.</returns>
        public virtual ResourcesResponse Any(StoreResources request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            ResourcesResponse response = new ResourcesResponse();
            //IResourceRepository _repository = _DataRepositoryFactory.GetDataRepository<IResourceRepository>();
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsResource, IDataContextNhJars>>();
            response.Resources = _repository.CreateUpdateList(request.Resources.ConvertAllTo<JarsResource>().ToList(), CurrentSessionUsername).ConvertAllTo<ResourceDto>().ToList();
            return response;
            //});
        }

        public virtual void Any(DeleteResource request)
        {
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsResource, IDataContextNhJars>>();
            _repository.Delete(request.Id);
            TrySendDeleteNotificationToChannel(typeof(JarsResource).Name, new[] { request.Id.ToString() });
        }

        /// <summary>
        /// A helper method used for building the query, depending on if the LoadLazy option was set.
        /// </summary>
        /// <typeparam name="T">A query over object required by the QueryOver Method on the repository</typeparam>
        /// <param name="request">The same request passed into the service</param>
        /// <param name="hasWhere">a bool value indicating if there is a where string</param>
        /// <returns></returns>
        private Expression<Func<T, bool>> BuildQuery<T>(FindResources request) where T : Entities.JarsResource
        {
            Expression<Func<T, bool>> query = LinqExpressionBuilder.True<T>();

            //Id
            if (request.Id != 0)
                query = query.And(o => o.Id == request.Id);

            //ExternalRef
            if (request.ExternalRef != null && request.ExternalRef != string.Empty)
                query = query.And(j => j.ExtRef == request.ExternalRef);

            //ExternalRef1
            if (request.ExternalRef1 != null && request.ExternalRef1 != string.Empty)
                query = query.And(O => O.ExtRef1 == request.ExternalRef1);

            //ExternalRef2
            if (request.ExternalRef2 != null && request.ExternalRef2 != string.Empty)
                query = query.And(O => O.ExtRef2 == request.ExternalRef2);

            //DisplayName
            if (request.DisplayName != null && request.DisplayName != string.Empty)
                query = query.And(j => j.DisplayName == request.DisplayName);

            //IsActive
            if (request.IsActive.HasValue)
                query = query.And(o => o.IsActive == request.IsActive.Value);

            return query;
        }

        /// <summary>
        /// Send CRUD notifications for a Resource Entity or Entities
        /// Note! :
        /// This Method is a special method used by the service when ServerEvents are being used.(serviceStack). 
        /// If the service does not implement serverEvents this will throw an error. 
        /// This will send a notification to all subscribed clients (including the client the request originated from) where the chanel name is the name of the entity type.
        /// This will only process SelectorTypes.store and SelectorTypes.delete notifications.
        /// The notification sent to subscribers will be a ServerEventMessage (serviceStack) where the data(json) is set as ServerEventMessageData (Jars) object.
        /// </summary>
        /// <param name="crud">The notification request indicating a store or delete event that will be sent to other subscribers.</param>
        public virtual void Any(ResourcesNotification crud)
        {
            //ExecuteFaultHandledMethod(() =>
            //{

            //check that the sender has subscribed to the service
            //SubscriptionInfo subscriber = ServerEvents.GetSubscriptionInfo(crud.FromSubscriptionId);
            //if (subscriber == null)
            //    throw HttpError.NotFound($"Subscriber {crud.FromUserName} does not exist.");

            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsResource, IDataContextNhJars>>();

            if (crud.Selector == SelectorTypes.store)
            {
                List<ResourceDto> notifyList = _repository.Where(l => crud.Ids.Contains(l.Id)).ConvertAllTo<ResourceDto>().ToList();
                TrySendStoreNotificationToChannel(typeof(JarsResource).Name, notifyList.ToJson());
            }

            if (crud.Selector == SelectorTypes.delete)
            {
                TrySendDeleteNotificationToChannel(typeof(JarsResource).Name, crud.Ids.ConvertTo<List<string>>().ToArray());//, true);
            }
            // });
        }
    }
}
