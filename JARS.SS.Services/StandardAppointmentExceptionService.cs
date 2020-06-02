using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Utils;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Entities;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JARS.SS.Services
{
    [Authenticate]
    public class StandardAppointmentExceptionService : ServicesBase
    {
        
        /// <summary>
        /// Returns a single StandardAppointmentException where the id in the request matches the record ID in the database.
        /// </summary>
        /// <param name="request">The request used for requesting the record from the database.</param>
        /// <returns>If FetchLazy was set to false, the JarsStandardAppointmentException item will be populated, if not then the JarsStandardAppointmentExceptionBase record will be filled with data.</returns>
        public virtual StandardAppointmentExceptionsResponse Any(GetStandardAppointmentException request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                StandardAppointmentExceptionsResponse response = new StandardAppointmentExceptionsResponse();

                //IStandardAppointmentExceptionRepository _repository = _DataRepositoryFactory.GetDataRepository<IStandardAppointmentExceptionRepository>();
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<StandardAppointmentException, IDataContextNhJars>>();
                response.AppointmentException = _repository.GetById(request.Id, request.FetchEagerly).ConvertTo<StandardAppointmentExceptionDto>();
                return response;
            });
        }


        /// <summary>
        /// Find a StandardAppointmentException by specifying values for the properties available in the request.
        /// Date values: Start date will go from date forward, end date will be from end date back, and if both has values, 
        /// it will be used as from (between) start to end
        /// </summary>
        /// <param name="request">The request used for building the find parameters</param>
        /// <returns>If LoadLazy was true, then a list of JarsStandardAppointmentExceptionBase items, otherwise a list of fully loaded JarsStandardAppointmentExceptions</returns>
        public virtual StandardAppointmentExceptionsResponse Any(FindStandardAppointmentExceptions request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                StandardAppointmentExceptionsResponse response = new StandardAppointmentExceptionsResponse();
                if (request != null)
                {
                    var query = BuildQuery<StandardAppointmentException>(request);
                    //var _repository = _DataRepositoryFactory.GetDataRepository<IStandardAppointmentExceptionRepository>();
                    var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<StandardAppointmentException, IDataContextNhJars>>();

                    response.AppointmentExceptions = _repository.Where(query).ToList().ConvertAll(x => x.ConvertTo<StandardAppointmentExceptionDto>());
                }
                return response;
            });
        }

        /// <summary>
        /// Update or create a single StandardAppointmentException or a list of StandardAppointmentExceptions, depending on whether the StandardAppointmentException or StandardAppointmentExceptions property has got a value set.
        /// If the StandardAppointmentException property is set the StandardAppointmentException will be created or updated and the StandardAppointmentExceptions property will be ignored.
        /// To create or update more than one StandardAppointmentException, assign a list to the StandardAppointmentExceptions property and make sure StandardAppointmentException is set to nothing/null.
        /// </summary>
        /// <param name="request">The request containing the StandardAppointmentException or StandardAppointmentExceptions that needs to be created or updated</param>
        /// <returns>depending on the values supplied, the updated StandardAppointmentException or StandardAppointmentExceptions will be returned.</returns>
        public virtual StandardAppointmentExceptionsResponse Any(StoreStandardAppointmentExceptions request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                StandardAppointmentExceptionsResponse response = new StandardAppointmentExceptionsResponse();
                //IStandardAppointmentExceptionRepository _repository = _DataRepositoryFactory.GetDataRepository<IStandardAppointmentExceptionRepository>();
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<StandardAppointmentException, IDataContextNhJars>>();
                IList<StandardAppointmentException> exList = request.AppointmentExceptions.ConvertAll(x => x.ConvertTo<StandardAppointmentException>());
                exList = _repository.CreateUpdateList(exList, CurrentSessionUsername).ToList();
                response.AppointmentExceptions = exList.ToList().ConvertAll(x => x.ConvertTo<StandardAppointmentExceptionDto>());
                TrySendStoreNotificationToChannel(typeof(StandardAppointmentException).Name, exList, request.IsAppointment);
                return response;
            });
        }

        /// <summary>
        /// Delete a standard appointment and all the exceptions that links to it.
        /// </summary>
        /// <param name="request"></param>
        public virtual void Any(DeleteStandardAppointmentException request)
        {
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<StandardAppointmentException, IDataContextNhJars>>();
            _repository.Delete(request.Id);
            TrySendDeleteNotificationToChannel(typeof(StandardAppointmentException).Name, new[] { request.Id.ToString() });
        }

        /// <summary>
        /// A helper method used for building the query, depending on if the LoadLazy option was set.
        /// </summary>
        /// <typeparam name="T">A query over object required by the QueryOver Method on the repository</typeparam>
        /// <param name="request">The same request passed into the service</param>
        /// <param name="hasWhere">a bool value indicating if there is a where string</param>
        /// <returns></returns>
        private Expression<Func<T, bool>> BuildQuery<T>(FindStandardAppointmentExceptions request) where T : StandardAppointmentException
        {
            Expression<Func<T, bool>> query = LinqExpressionBuilder.True<T>();
            //Id
            if (request.Id != 0)
                query = query.And(a => a.Id == request.Id);

            //StartDateTime
            if (request.FromStartDate != DateTime.MinValue)
                query = query.And(a => a.StartDate >= request.FromStartDate);

            //EndDateTime
            if (request.ToEndDate != DateTime.MinValue)
                query = query.And(a => a.EndDate <= request.ToEndDate);

            return query;
        }


        /// <summary>
        /// Send CRUD notifications for a StandardAppointmentException Entity or Entities
        /// Note! :
        /// This Method is a special method used by the service when ServerEvents are being used.(serviceStack). 
        /// If the service does not implement serverEvents this will throw an error. 
        /// This will send a notification to all subscribed clients (including the client the request originated from) where the chanel name is the name of the entity type.
        /// This will only process SelectorTypes.store and SelectorTypes.delete notifications.
        /// The notification sent to subscribers will be a ServerEventMessage (serviceStack) where the data(json) is set as ServerEventMessageData (Jars) object.
        /// </summary>
        /// <param name="crud">The notification request indicating a store or delete event that will be sent to other subscribers.</param>
        public virtual void Any(StandardAppointmentExceptionsCrudNotification crud)
        {
            ExecuteFaultHandledMethod(() =>
            {
                //check that the sender has subscribed to the service
                //SubscriptionInfo subscriber = ServerEvents.GetSubscriptionInfo(crud.From);
                List<SubscriptionInfo> subscriber = ServerEvents.GetSubscriptionInfosByUserId(crud.FromUserName);
                if (subscriber == null)
                    throw HttpError.NotFound($"Subscriber {crud.FromUserName} does not exist.");

                //do some StandardAppointmentException updates here using the info from the the crud
                //IStandardAppointmentExceptionRepository _repository = _DataRepositoryFactory.GetDataRepository<IStandardAppointmentExceptionRepository>();
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<StandardAppointmentException, IDataContextNhJars>>();

                if (crud.Selector == SelectorTypes.store)
                {
                    IList<StandardAppointmentException> exList = crud.AppointmentExceptions.ToList().ConvertAll(x => x.ConvertTo<StandardAppointmentException>());
                    exList = _repository.CreateUpdateList(exList, crud.FromUserName).ToList();
                    crud.AppointmentExceptions = exList.ToList().ConvertAll(x => x.ConvertTo<StandardAppointmentExceptionDto>());
                    ServerEvents.NotifyChannel(typeof(StandardAppointmentException).Name, crud.AppointmentExceptions);//.ConvertToJarsSyncStoreEvent());
                }

                if (crud.Selector == SelectorTypes.delete)
                {
                    IList<StandardAppointmentException> exList = crud.AppointmentExceptions.ToList().ConvertAll(x => x.ConvertTo<StandardAppointmentException>());
                    _repository.DeleteList(exList);
                    ServerEvents.NotifyChannel(typeof(StandardAppointmentException).Name, crud.AppointmentExceptions.Select(a => a.Id));
                }
            });
        }
    }
}
