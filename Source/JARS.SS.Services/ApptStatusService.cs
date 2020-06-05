using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Repositories;
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
    public class ApptStatusService : ServicesBase
    {
        /// <summary>
        /// Returns a single ApptStatus where the id in the request matches the record ID in the database.
        /// </summary>
        /// <param name="request">The request used for requesting the record from the database,it only looks at the ID value and none of the other parameters..</param>
        /// <returns>The ApptStatus record as a fully loaded object</returns>
        public virtual ApptStatusesResponse Any(GetApptStatus request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                ApptStatusesResponse response = new ApptStatusesResponse();

                //IApptStatusRepository _repository = _DataRepositoryFactory.GetDataRepository<IApptStatusRepository>();
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ApptStatus, IDataContextNhJars>>();
                response.Statuses.Add(_repository.GetById(request.Id).ConvertTo<ApptStatusDto>());
                return response;
            });
        }




        /// <summary>
        /// Update or create a single ApptStatus.                
        /// </summary>
        /// <param name="request">The request containing the ApptStatus  that needs to be created or updated</param>
        /// <returns>The updated ApptStatus will be returned.</returns>
        public virtual ApptStatusResponse Any(StoreApptStatus request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                ApptStatusResponse response = new ApptStatusResponse();
                //IApptStatusRepository _repository = _DataRepositoryFactory.GetDataRepository<IApptStatusRepository>();
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ApptStatus, IDataContextNhJars>>();
                response.Status = StoreStatus(request.Status, CurrentSessionUsername, _repository);
                return response;
            });
        }

        /// <summary>
        /// Update or create a list of ApptStatuses
        /// </summary>
        /// <param name="request">The request containing the ApptStatuses that needs to be created or updated</param>
        /// <returns>The updated ApptStatuses will be returned.</returns>
        public virtual ApptStatusesResponse Any(StoreApptStatuses request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                ApptStatusesResponse response = new ApptStatusesResponse();
                //IApptStatusRepository _repository = _DataRepositoryFactory.GetDataRepository<IApptStatusRepository>();
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ApptStatus, IDataContextNhJars>>();
                response.Statuses = StoreStatuses(request.Statuses, CurrentSessionUsername, _repository);
                return response;
            });
        }

        public ApptStatusesResponse Any(FindApptStatuses request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                ApptStatusesResponse response = new ApptStatusesResponse();
                if (request != null)
                {
                    Expression<Func<ApptStatus, bool>> query = LinqExpressionBuilder.True<ApptStatus>();

                    //DisplayName
                    if (!request.ViewType.IsNullOrEmpty())
                        query = query.And(j => j.ViewName == request.ViewType);

                    //IsActive
                    if (!request.StatusName.IsNullOrEmpty())
                        query = query.And(o => o.StatusName.Like($"{request.StatusName}%"));

                    //var _repository = _DataRepositoryFactory.GetDataRepository<IApptStatusRepository>();
                    var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ApptStatus, IDataContextNhJars>>();

                    IList<ApptStatus> resList = _repository.Where(query).ToList();
                    if (resList.Count == 0 && request.ViewType != "" && request.StatusName == null)
                    {
                        CreateDefaultRecord(request, _repository);
                        response.Statuses = _repository.Where(query).ToList().ConvertAll(s => s.ConvertTo<ApptStatusDto>());
                    }
                    else
                        response.Statuses = resList.ToList().ConvertAll(s => s.ConvertTo<ApptStatusDto>());

                    //call the extension method to remove any sub child parent references.
                    //response.UpdateChildReferences();
                }
                return response;
            });
        }

        public virtual void Any(DeleteApptStatus request)
        {
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ApptStatus, IDataContextNhJars>>();
            _repository.Delete(request.Id);
            TrySendDeleteNotificationToChannel(typeof(ApptStatus).Name, new[] { request.Id.ToString() });
        }

        /// <summary>
        /// Send CRUD notifications for a ApptStatus Entity or Entities
        /// Note! :
        /// This Method is a special method used by the service when ServerEvents are being used.(serviceStack). 
        /// If the service does not implement serverEvents this will throw an error. 
        /// This will send a notification to all subscribed clients (including the client the request originated from) where the chanel name is the name of the entity type.
        /// This will only process SelectorTypes.store and SelectorTypes.delete notifications.
        /// The notification sent to subscribers will be a ServerEventMessage (serviceStack) where the data(json) is set as ServerEventMessageData (Jars) object.
        /// </summary>
        /// <param name="crud">The notification request indicating a store or delete event that will be sent to other subscribers.</param>
        public virtual void Any(ApptStatusesNotification crud)
        {
            ExecuteFaultHandledMethod(() =>
            {
                //check that the sender has subscribed to the service
                //SubscriptionInfo subscriber = ServerEvents.GetSubscriptionInfo(crud.FromSubscriptionId);
                //if (subscriber == null)
                //    throw HttpError.NotFound($"Subscriber {crud.FromUserName} does not exist.");

                //do some job updates here using the info from the the crud
                //IApptStatusRepository _repository = _DataRepositoryFactory.GetDataRepository<IApptStatusRepository>();
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ApptStatus, IDataContextNhJars>>();               

                if (crud.Selector == SelectorTypes.store)
                {
                    List<ApptStatusDto> notifyList = _repository.Where(l => crud.Ids.Contains(l.Id)).ConvertAllTo<ApptStatusDto>().ToList();
                    TrySendStoreNotificationToChannel(typeof(ApptStatus).Name, notifyList.ToJson());
                }

                if (crud.Selector == SelectorTypes.delete)
                {
                    TrySendDeleteNotificationToChannel(typeof(ApptStatus).Name, crud.Ids.ConvertTo<List<string>>().ToArray());//, true);
                }
            });
        }


        void CreateDefaultRecord(FindApptStatuses request, IGenericEntityRepositoryBase<ApptStatus, IDataContextNhJars> repository)
        {

            ApptStatus sts = new ApptStatus()
            {
                UseInterfaceType = request.InterfaceTypeName,
                ViewName = request.ViewType,
                StatusCriteria = ""
            };
            sts = repository.CreateUpdate(sts, "SYSTEM");

            Type t = Type.GetType(request.InterfaceTypeName);
            if (t == typeof(IEntityWithStatusLabels))
            {
                sts.StatusCriteria = $"([StatusKey] = '{sts.Id}')";
                repository.CreateUpdate(sts, "SYSTEM");
            }
        }

        private ApptStatusDto StoreStatus(ApptStatusDto status, string modifyUser, IDataRepositoryCrudBase<ApptStatus> _repository)
        {
            ApptStatus sts = status.ConvertTo<ApptStatus>();
            if (sts.Id == 0)
            {
                //then save the label, to get an ID,
                sts = _repository.CreateUpdate(sts, modifyUser);
                //then but also set the criteria to the correct value and save it again..
                if (Type.GetType(sts.UseInterfaceType) == typeof(IEntityWithStatusLabels))
                    sts.StatusCriteria = $"([{nameof(IEntityWithStatusLabels.StatusKey)}] = '{sts.Id}')";
            }
            return _repository.CreateUpdate(sts, modifyUser).ConvertTo<ApptStatusDto>();
        }

        private List<ApptStatusDto> StoreStatuses(List<ApptStatusDto> statuses, string modifyUser, IDataRepositoryCrudBase<ApptStatus> _repository)
        {
            List<ApptStatus> saveList = new List<ApptStatus>();
            foreach (var status in statuses)
            {
                ApptStatus sts = status.ConvertTo<ApptStatus>();
                if (sts.Id == 0)
                {
                    //then save the label, to get an ID,
                    sts = _repository.CreateUpdate(sts, modifyUser);
                    //then but also set the criteria to the correct value and save it again..
                    if (Type.GetType(sts.UseInterfaceType) == typeof(IEntityWithStatusLabels))
                        sts.StatusCriteria = $"([{nameof(IEntityWithStatusLabels.StatusKey)}] = '{sts.Id}')";
                }
                saveList.Add(sts);
            }
            return _repository.CreateUpdateList(saveList, modifyUser).ConvertAllTo<ApptStatusDto>().ToList();
        }
    }
}


