using JARS.Core.Data.Interfaces.Repositories;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Entities;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using NHibernate.Criterion;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;

namespace JARS.SS.Services
{
    [Authenticate]
    public class JarsDefaultsAppointmentService : ServicesBase
    {

        /// <summary>
        /// Save or update the a  single records.        
        /// </summary>
        /// <param name="request">The request containing the entity that needs to be stored (created or updated)</param>
        /// <returns></returns>
        public JarsDefaultAppointmentResponse Any(StoreJarsDefaultAppointment request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                IGenericEntityRepositoryBase<JarsDefaultAppointment, IDataContextNhJars> repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsDefaultAppointment, IDataContextNhJars>>();

                var appts = repository.CreateUpdate(request.Appointment.ConvertTo<JarsDefaultAppointment>(), CurrentSessionUsername);

                JarsDefaultAppointmentResponse response = new JarsDefaultAppointmentResponse
                { Appointment = appts.ConvertTo<JarsDefaultAppointmentDto>() };

                return response;
            });
        }

        /// <summary>
        /// Save or update aa list of records.       
        /// </summary>
        /// <param name="request">The request containing the entities that needs to be stored (created or updated)</param>
        /// <returns></returns>
        public JarsDefaultAppointmentsResponse Any(StoreJarsDefaultAppointments request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                IGenericEntityRepositoryBase<JarsDefaultAppointment, IDataContextNhJars> repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsDefaultAppointment, IDataContextNhJars>>();

                var appts = repository.CreateUpdateList(request.Appointments.ConvertAllTo<JarsDefaultAppointment>().ToList(), CurrentSessionUsername);

                JarsDefaultAppointmentsResponse response = new JarsDefaultAppointmentsResponse
                {
                    Appointments = appts.ConvertAllTo<JarsDefaultAppointmentDto>().ToList()
                };

                return response;
            });
        }

        /// <summary>
        /// Find a list of appointments depending on the criteria given in the Find request.
        /// If the request does not contain any values for filtering all the records will be returned.
        /// </summary>
        /// <param name="request">The request containing the search criteria.</param>
        /// <returns>a list of StandardAppointmentDefault entities.</returns>
        public JarsDefaultAppointmentsResponse Any(FindJarsDefaultAppointments request)
        {
            IGenericEntityRepositoryBase<JarsDefaultAppointment, IDataContextNhJars> repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsDefaultAppointment, IDataContextNhJars>>();
            JarsDefaultAppointmentsResponse response = new JarsDefaultAppointmentsResponse();

            if (request.DescriptionLike != null)
                response.Appointments = repository.Where(d => d.Description.IsLike(request.DescriptionLike)).ToList().ConvertAll(s => s.ConvertTo<JarsDefaultAppointmentDto>());
            else
                response.Appointments = repository.GetAll().ToList().ConvertAll(s => s.ConvertTo<JarsDefaultAppointmentDto>());

            return response;
        }

        public void Any(DeleteJarsDefaultAppointment request)
        {
            IGenericEntityRepositoryBase<JarsDefaultAppointment, IDataContextNhJars> repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsDefaultAppointment, IDataContextNhJars>>();
            repository.Delete(request.Id);
            TrySendDeleteNotificationToChannel(typeof(JarsDefaultAppointment).Name, new string[] { request.Id.ToString() });//, request.IsAppointment);
        }

        /// <summary>
        /// Send CRUD notifications for an Entity or Entities
        /// Note! :        
        /// If the service does not implement serverEvents this will throw an error. 
        /// This will send a notification to all subscribed clients (including the client the request originated from) where the chanel name is the name of the entity type.
        /// This will only process SelectorTypes.store and SelectorTypes.delete notifications.
        /// The notification sent to subscribers will be a ServerEventMessage (serviceStack) where the data(json) is set as ServerEventMessageData (Jars) object.
        /// </summary>
        /// <param name="crud">The notification request indicating a store or delete event that will be sent to other subscribers.</param>
        public virtual void Any(JarsDefaultAppointmentNotification crud)
        {
            //ExecuteFaultHandledMethod(() =>
            //{
            //check that the sender has subscribed to the service
            //SubscriptionInfo subscriber = ServerEvents.GetSubscriptionInfo(crud.From);
            List<SubscriptionInfo> subscriber = ServerEvents.GetSubscriptionInfosByUserId(crud.FromUserName);
            if (subscriber == null)
                throw HttpError.NotFound($"Subscriber {crud.FromUserName} does not exist.");

            //do some job updates here using the info from the the crud
            IGenericEntityRepositoryBase<JarsDefaultAppointment, IDataContextNhJars> _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsDefaultAppointment, IDataContextNhJars>>();

            if (crud.Selector == SelectorTypes.store)
            {
                List<JarsDefaultAppointmentDto> storeList = _repository.Where(a => crud.Ids.Contains(a.Id)).ConvertAllTo<JarsDefaultAppointmentDto>().ToList();
                TrySendStoreNotificationToChannel(typeof(JarsDefaultAppointment).Name, storeList.ToJson());
            }

            if (crud.Selector == SelectorTypes.delete)
            {
                TrySendDeleteNotificationToChannel(typeof(JarsDefaultAppointment).Name, crud.Ids.ConvertTo<List<string>>().ToArray());//, true);
            }
            //});
        }
    }
}
