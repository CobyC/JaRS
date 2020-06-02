using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Utils;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Entities;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using System;
using System.Linq;

namespace JARS.SS.Services
{
    [Authenticate]
    public class ActivityLogService : ServicesBase
    {
        /// <summary>
        /// Save or update the list of records
        /// </summary>
        /// <param name="request">the request containing the entities</param>
        /// <returns></returns>
        public object Any(StoreActivityLogs request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                //IActivityLogRepository repository = _DataRepositoryFactory.GetDataRepository<IActivityLogRepository>();
                var repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ActivityLog, IDataContextNhJars>>();
                ActivityLogsResponse response = new ActivityLogsResponse();
                response.Logs = repository.CreateUpdateList(request.Logs.ConvertAllTo<ActivityLog>().ToList(), CurrentSessionUsername).ConvertAllTo<ActivityLogDto>().ToList();
                return response;
            });
        }

        public object Any(FindActivityLogs request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                //IActivityLogRepository repository = _DataRepositoryFactory.GetDataRepository<IActivityLogRepository>();
                var repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ActivityLog, IDataContextNhJars>>();
                //FindActivityLogsResponse response = new FindActivityLogsResponse();
                ActivityLogsResponse response = new ActivityLogsResponse();

                //var ret = response.Logs.AsQueryable();
                //ret.Where(a => a.Category == "");

                var query = LinqExpressionBuilder.True<ActivityLog>();//.And(a => a.Id != null);//QueryOver.Of<ActivityLog>();

                if (!request.Category.IsNullOrEmpty())
                    query.And(a => a.Category == request.Category);

                if (!request.LinkedId.IsNullOrEmpty())
                    query.And(a => a.LinkedId == request.LinkedId);

                if (request.ResourceId != 0)
                    query.And(a => a.ResourceId == request.ResourceId);

                if (!request.Name.IsNullOrEmpty())
                    query.And(a => a.Name == request.Name);

                if (request.StartDateTime.HasValue)
                    query.And(a => a.StartDateTime >= request.StartDateTime);

                if (request.EndDateTime.HasValue)
                    query.And(a => a.EndDateTime >= request.EndDateTime);

                response.Logs = repository.Where(query).ConvertAllTo<ActivityLogDto>().ToList();

                return response;
            });
        }

        public object Any(GetActivityLog request)
        {
            //IActivityLogRepository repository = _DataRepositoryFactory.GetDataRepository<IActivityLogRepository>();
            var repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ActivityLog, IDataContextNhJars>>();
            ActivityLogsResponse response = new ActivityLogsResponse();
            try
            {
                response.Logs.Add(repository.GetById(request.Id).ConvertTo<ActivityLogDto>());
            }
            catch (Exception ex)
            {
                response.ResponseStatus = new ServiceStack.ResponseStatus("420", ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Send CRUD notifications for a ActivityLog Entity or Entities
        /// Note! :
        /// This Method is a special method used by the service when ServerEvents are being used.(serviceStack). 
        /// If the service does not implement serverEvents this will throw an error. 
        /// This will send a notification to all subscribed clients (including the client the request originated from) where the chanel name is the name of the entity type.
        /// This will only process SelectorTypes.store and SelectorTypes.delete notifications.
        /// The notification sent to subscribers will be a ServerEventMessage (serviceStack) where the data(json) is set as ServerEventMessageData (Jars) object.
        /// </summary>
        /// <param name="crud">The notification request indicating a store or delete event that will be sent to other subscribers.</param>
        public virtual void Any(ActivityLogsNotification crud)
        {
            ExecuteFaultHandledMethod(() =>
           {

               SubscriptionInfo subscriber = ServerEvents.GetSubscriptionInfo(crud.FromUserName);
               if (subscriber == null)
                   throw HttpError.NotFound($"Subscriber {crud.FromUserName} does not exist.");

               //do some job updates here using the info from the the crud
               //IActivityLogRepository _repository = _DataRepositoryFactory.GetDataRepository<IActivityLogRepository>();
               var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ActivityLog, IDataContextNhJars>>();
               if (crud.Selector == SelectorTypes.store)
               {
                   crud.Logs = _repository.CreateUpdateList(crud.Logs.ConvertAllTo<ActivityLog>().ToList(), crud.FromUserName).ConvertAllTo<ActivityLogDto>().ToList();
                    ServerEvents.NotifyChannel(typeof(ActivityLog).Name, crud.Selector, crud.Logs);//.ConvertToJarsSyncStoreEvent());
               }

               if (crud.Selector == SelectorTypes.delete)
               {
                   _repository.DeleteList(crud.Logs.ConvertAllTo<ActivityLog>().ToList());
                   ServerEvents.NotifyChannel(typeof(ActivityLog).Name, crud.Selector, crud.Logs.Select(l=>l.Id));
               }
           });
        }
    }
}
