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
    public class StandardAppointmentService : ServicesBase
    {

        /// <summary>
        /// Returns a single StandardAppointment where the id in the request matches the record ID in the database.
        /// </summary>
        /// <param name="request">The request used for requesting the record from the database.</param>
        /// <returns>If FetchLazy was set to false, the JarsStandardAppointment item will be populated, if not then the JarsStandardAppointmentBase record will be filled with data.</returns>
        public virtual StandardAppointmentsResponse Any(GetStandardAppointment request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                StandardAppointmentsResponse response = new StandardAppointmentsResponse();

                //IStandardAppointmentRepository _repository = _DataRepositoryFactory.GetDataRepository<IStandardAppointmentRepository>();
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<StandardAppointment, IDataContextNhJars>>();
                response.Appointments.Add(_repository.GetById(request.Id, true).ToStandardAppointmentDto());
                return response;
            });
        }

        /// <summary>
        /// Find a StandardAppointment by specifying values for the properties available in the request.
        /// Date values: Start date will go from date forward, end date will be from end date back, and if both has values, 
        /// it will be used as from (between) start to end
        /// </summary>
        /// <param name="request">The request used for building the find parameters</param>
        /// <returns>If LoadLazy was true, then a list of JarsStandardAppointmentBase items, otherwise a list of fully loaded JarsStandardAppointments</returns>
        public virtual StandardAppointmentsResponse Any(FindStandardAppointments request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                StandardAppointmentsResponse response = new StandardAppointmentsResponse();
                if (request != null)
                {
                    var query = BuildQuery<StandardAppointment>(request);
                    //var _repository = _DataRepositoryFactory.GetDataRepository<IStandardAppointmentRepository>();
                    var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<StandardAppointment, IDataContextNhJars>>();

                    IList<StandardAppointment> list = _repository.Where(query, true);
                    response.Appointments = list.ToStandardAppointmentDtoList().ToList();

                }
                return response;
            });
        }

        /// <summary>
        /// Update or create a single StandardAppointment.                
        /// </summary>
        /// <param name="request">The request containing the StandardAppointment that needs to be created or updated</param>
        /// <returns>The updated StandardAppointment will be returned.</returns>
        public virtual StandardAppointmentResponse Any(StoreStandardAppointment request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{

            StandardAppointmentResponse response = new StandardAppointmentResponse();
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<StandardAppointment, IDataContextNhJars>>();

            //this fixes the references in case an appointment has exceptions ??
            //foreach (var apptEx in request.Appointment.StandardAppointmentExceptions)
            //{
            //    apptEx.StandardAppointment = request.Appointment;
            //}
            StandardAppointment appt = request.Appointment.FromStandardAppointmentDto();
            response.Appointment = _repository.CreateUpdate(appt, CurrentSessionUsername).ToStandardAppointmentDto();

            return response;
            //});
        }

        /// <summary>
        /// Update or create a single StandardAppointment or a list of StandardAppointments, depending on whether the StandardAppointment or StandardAppointments property has got a value set.
        /// If the StandardAppointment property is set the StandardAppointment will be created or updated and the StandardAppointments property will be ignored.
        /// To create or update more than one StandardAppointment, assign a list to the StandardAppointments property and make sure StandardAppointment is set to nothing/null.
        /// </summary>
        /// <param name="request">The request containing the StandardAppointment or StandardAppointments that needs to be created or updated</param>
        /// <returns>depending on the values supplied, the updated StandardAppointment or StandardAppointments will be returned.</returns>
        public virtual StandardAppointmentsResponse Any(StoreStandardAppointments request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            StandardAppointmentsResponse response = new StandardAppointmentsResponse();
            //IStandardAppointmentRepository _repository = _DataRepositoryFactory.GetDataRepository<IStandardAppointmentRepository>();
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<StandardAppointment, IDataContextNhJars>>();

            //foreach (var appointment in request.Appointments)
            //{
            //    foreach (var apptEx in appointment.StandardAppointmentExceptions)
            //    {
            //        apptEx.StandardAppointment = appointment;
            //    }
            //}
            List<StandardAppointment> appts = request.Appointments.FromStandardAppointmentDtoList();
            response.Appointments = _repository.CreateUpdateList(appts, CurrentSessionUsername).ToList().ToStandardAppointmentDtoList().ToList();

            return response;
            //});
        }

        /// <summary>
        /// Delete a standard appointment and all the exceptions that links to it.
        /// </summary>
        /// <param name="request"></param>
        public virtual void Any(DeleteStandardAppointment request)
        {
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<StandardAppointment, IDataContextNhJars>>();
            _repository.Delete(request.Id);
            TrySendDeleteNotificationToChannel(typeof(StandardAppointment).Name, new[] { request.Id.ToString() });
        }

        /// <summary>
        /// A helper method used for building the query, depending on if the LoadLazy option was set.
        /// </summary>
        /// <typeparam name="T">A query over object required by the QueryOver Method on the repository</typeparam>
        /// <param name="request">The same request passed into the service</param>
        /// <param name="hasWhere">a bool value indicating if there is a where string</param>
        /// <returns></returns>
        private Expression<Func<T, bool>> BuildQuery<T>(FindStandardAppointments request) where T : StandardAppointment
        {
            Expression<Func<T, bool>> query = LinqExpressionBuilder.True<T>();

            //the default value of loading recurring appointments will always be added.
            query = query.Or(a => a.RecurrenceInfo != null);

            //StartDateTime
            if (request.FromStartDate.HasValue && request.FromStartDate != DateTime.MinValue)
                query = query.And(a => a.StartDate >= request.FromStartDate);

            //EndDateTime
            if (request.ToEndDate.HasValue && request.ToEndDate != DateTime.MinValue)
                query = query.And(a => a.EndDate <= request.ToEndDate);

            //resourcelist
            if (!request.InCalendarForResources.IsNullOrEmpty())
            {
                string[] resIds = request.InCalendarForResources.Split(',');
                query = query.And(a => resIds.Contains(a.ResourceId.ToString()));
            }

            //with description like
            if (request.WithAppointmentDescription != null && request.WithAppointmentDescription != string.Empty)
                query = query.And(a => a.Subject.Contains(request.WithAppointmentDescription) || a.Description.Contains(request.WithAppointmentDescription));

            //Id
            if (request.Id != 0)
                query = query.And(a => a.Id == request.Id);

            return query;
        }



        /// <summary>
        /// Send CRUD notifications for a StandardAppointment Entity or Entities
        /// Note! :
        /// This Method is a special method used by the service when ServerEvents are being used.(serviceStack). 
        /// If the service does not implement serverEvents this will throw an error. 
        /// This will send a notification to all subscribed clients (including the client the request originated from) where the chanel name is the name of the entity type.
        /// This will only process SelectorTypes.store and SelectorTypes.delete notifications.
        /// The notification sent to subscribers will be a ServerEventMessage (serviceStack) where the data(json) is set as ServerEventMessageData (Jars) object.
        /// </summary>
        /// <param name="crud">The notification request indicating a store or delete event that will be sent to other subscribers.</param>
        public virtual void Any(StandardAppointmentsNotification crud)
        {
            ExecuteFaultHandledMethod(() =>
            {
                //Task.Delay(200).Wait();
                //check that the sender has subscribed to the service
                //SubscriptionInfo subscriber = ServerEvents.GetSubscriptionInfo(crud.From);
                List<SubscriptionInfo> subscriber = ServerEvents.GetSubscriptionInfosByUserId(crud.FromUserName);
                if (subscriber == null)
                    throw HttpError.NotFound($"Subscriber {crud.FromUserName} does not exist.");

                //do some StandardAppointment updates here using the info from the the crud
                //IStandardAppointmentRepository _repository = _DataRepositoryFactory.GetDataRepository<IStandardAppointmentRepository>();
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<StandardAppointment, IDataContextNhJars>>();

                if (crud.Selector == SelectorTypes.store)
                {
                    IList<StandardAppointmentDto> storeApptList = _repository.Where(a => crud.Ids.Contains(a.Id), true).ToStandardAppointmentDtoList();
                    TrySendStoreNotificationToChannel(typeof(StandardAppointment).Name, storeApptList.ToJson());//, true);
                }

                if (crud.Selector == SelectorTypes.delete)
                {
                    TrySendDeleteNotificationToChannel(typeof(StandardAppointment).Name, crud.Ids.ConvertTo<List<string>>().ToArray());//, true);
                }
            });
        }
    }
}
