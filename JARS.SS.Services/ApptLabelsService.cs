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
    public class ApptLabelsService : ServicesBase
    {

        /// <summary>
        /// Returns a single apptLabel where the id in the request matches the record ID in the database.
        /// </summary>
        /// <param name="request">The request used for requesting the record from the database,it only looks at the ID value and none of the other parameters..</param>
        /// <returns>The apptLabel record as a fully loaded object</returns>
        public virtual ApptLabelResponse Any(GetApptLabel request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            ApptLabelResponse response = new ApptLabelResponse();
            //IApptLabelRepository _repository = _DataRepositoryFactory.GetDataRepository<IApptLabelRepository>();
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ApptLabel, IDataContextNhJars>>();
            response.Label = _repository.GetById(request.Id).ConvertTo<ApptLabelDto>();
            return response;
            //});
        }

        /// <summary>
        /// Find labels according to the request values supplied (if just a blank request is sent all labels will be returned
        /// </summary>
        /// <param name="request">The request containing the filter criteria</param>
        /// <returns>a list of labels if any were found</returns>
        public ApptLabelsResponse Any(FindApptLabels request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            ApptLabelsResponse response = new ApptLabelsResponse();
            if (request != null)
            {
                var query = BuildQuery<ApptLabel>(request);

                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ApptLabel, IDataContextNhJars>>();

                IList<ApptLabel> resList = _repository.Where(query).ToList();
                if (resList.Count == 0 && request.ViewType != "" && request.LabelName == null)
                {
                    CreateDefaultRecord(request, _repository);
                    response.Labels = _repository.Where(query).ConvertAllTo<ApptLabelDto>().ToList();
                }
                else
                    response.Labels = resList.ConvertAllTo<ApptLabelDto>().ToList();
            }
            return response;
            //});

        }

        /// <summary>
        /// Update or create a single apptLabel.                
        /// </summary>
        /// <param name="request">The request containing the apptLabel that needs to be created or updated</param>
        /// <returns>the updated apptLabel will be returned.</returns>
        public virtual ApptLabelResponse Any(StoreApptLabel request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            ApptLabelResponse response = new ApptLabelResponse();
            //IApptLabelRepository _repository = _DataRepositoryFactory.GetDataRepository<IApptLabelRepository>();
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ApptLabel, IDataContextNhJars>>();
            response.Label = StoreLabel(request.Label, CurrentSessionUsername, _repository);

            return response;
            //});
        }

        /// <summary>
        /// Update or create a list of apptLabels                
        /// </summary>
        /// <param name="request">The request containing the apptLabels that needs to be created or updated</param>
        /// <returns>the apptLabels will be returned.</returns>
        public virtual ApptLabelsResponse Any(StoreApptLabels request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            ApptLabelsResponse response = new ApptLabelsResponse();
            //IApptLabelRepository _repository = _DataRepositoryFactory.GetDataRepository<IApptLabelRepository>();
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ApptLabel, IDataContextNhJars>>();
            response.Labels = StoreLabels(request.Labels, CurrentSessionUsername, _repository);

            return response;
            //});
        }

        public virtual void Any(DeleteApptLabel request)
        {
            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ApptLabel, IDataContextNhJars>>();
            _repository.Delete(request.Id);
            TrySendDeleteNotificationToChannel(typeof(ApptLabel).Name, new[] { request.Id.ToString() });
        }

        /// <summary>
        /// A helper method used for building the query, depending on if the LoadLazy option was set.
        /// </summary>
        /// <typeparam name="T">A query over object required by the QueryOver Method on the repository</typeparam>
        /// <param name="request">The same request passed into the service</param>
        /// <param name="hasWhere">a bool value indicating if there is a where string</param>
        /// <returns></returns>
        private Expression<Func<ApptLabel, bool>> BuildQuery<T>(FindApptLabels request)
        {
            Expression<Func<ApptLabel, bool>> query = LinqExpressionBuilder.True<ApptLabel>();

            //DisplayName
            if (request.ViewType != null && request.ViewType != string.Empty)
                query = query.And(j => j.ViewName == request.ViewType);

            //IsActive
            if (request.LabelName != null && request.LabelName != string.Empty)
                query = query.And(o => o.LabelName.Like($"{request.LabelName}%"));

            return query;
        }

        /// <summary>
        /// Send CRUD notifications for a ApptLabel Entity or Entities
        /// Note! :
        /// This Method is a special method used by the service when ServerEvents are being used.(serviceStack). 
        /// If the service does not implement serverEvents this will throw an error. 
        /// This will send a notification to all subscribed clients (including the client the request originated from) where the chanel name is the name of the entity type.
        /// This will only process SelectorTypes.store and SelectorTypes.delete notifications.
        /// The notification sent to subscribers will be a ServerEventMessage (serviceStack) where the data(json) is set as ServerEventMessageData (Jars) object.
        /// </summary>
        /// <param name="crud">The notification request indicating a store or delete event that will be sent to other subscribers.</param>
        public virtual void Any(ApptLabelsNotification crud)
        {
            //ExecuteFaultHandledMethod(() =>
            //{
            //check that the sender has subscribed to the service
            var subscriber = ServerEvents.GetSubscriptionsDetails(typeof(ApptLabel).Name);
            if (subscriber == null)
                return;

            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<ApptLabel, IDataContextNhJars>>();

            if (crud.Selector == SelectorTypes.store)
            {
                List<ApptLabelDto> notifyList = _repository.Where(l => crud.Ids.Contains(l.Id)).ConvertAllTo<ApptLabelDto>().ToList();
                TrySendStoreNotificationToChannel(typeof(ApptLabel).Name, notifyList.ToJson());
            }

            if (crud.Selector == SelectorTypes.delete)
            {
                TrySendDeleteNotificationToChannel(typeof(ApptLabel).Name, crud.Ids.ConvertTo<List<string>>().ToArray());//, true);
            }
            //});
        }


        void CreateDefaultRecord(FindApptLabels request, IGenericEntityRepositoryBase<ApptLabel, IDataContextNhJars> repository)
        {
            ApptLabel lbl = new ApptLabel()
            {
                UseInterfaceType = request.InterfaceTypeName,
                ViewName = request.ViewType,
                LabelCriteria = ""
            };
            lbl = repository.CreateUpdate(lbl, "SYSTEM");

            Type t = Type.GetType(request.InterfaceTypeName);
            if (t == typeof(IEntityWithStatusLabels))
            {
                lbl.LabelCriteria = $"([LabelKey] = '{lbl.Id}')";
                lbl = repository.CreateUpdate(lbl, "SYSTEM");
            }
        }

        /// <summary>
        /// Store the label to the database and sets the default criteria string only if the id is 0.
        /// Otherwise just return a the record.
        /// </summary>
        /// <param name="labels">The label check if its new.</param>
        /// <param name="modifyUser">the user to log as the created or modified user</param>
        /// <param name="_repository">repository used for saving info</param>
        /// <returns></returns>
        private ApptLabelDto StoreLabel(ApptLabelDto label, string modifyUser, IDataRepositoryCrudBase<ApptLabel> _repository)
        {
            ApptLabel lbl = label.ConvertTo<ApptLabel>();
            if (label.Id == 0)
            {
                //then save the label, to get an ID,
                lbl = _repository.CreateUpdate(lbl, modifyUser);
                //then but also set the criteria to the correct value and save it again..
                if (Type.GetType(lbl.UseInterfaceType) == typeof(IEntityWithStatusLabels))
                    lbl.LabelCriteria = $"([LabelKey] = '{lbl.Id}')";
            }
            return _repository.CreateUpdate(lbl, modifyUser).ConvertTo<ApptLabelDto>();
        }

        /// <summary>
        /// Store the label to the database and sets the default criteria string only if the id is 0.
        /// Otherwise just return a the record.
        /// </summary>
        /// <param name="labels">The label check if its new.</param>
        /// <param name="modifyUser">the user to log as the created or modified user</param>
        /// <param name="_repository">repository used for saving info</param>
        /// <returns></returns>
        private List<ApptLabelDto> StoreLabels(List<ApptLabelDto> labels, string modifyUser, IDataRepositoryCrudBase<ApptLabel> _repository)
        {
            List<ApptLabel> saveList = new List<ApptLabel>();
            foreach (var label in labels)
            {
                ApptLabel lbl = label.ConvertTo<ApptLabel>();
                if (label.Id == 0)
                {
                    //then save the label, to get an ID,
                    lbl = _repository.CreateUpdate(lbl, modifyUser);
                    //then but also set the criteria to the correct value and save it again..
                    if (Type.GetType(lbl.UseInterfaceType) == typeof(IEntityWithStatusLabels))
                        lbl.LabelCriteria = $"([LabelKey] = '{lbl.Id}')";
                }
                saveList.Add(lbl);
            }
            return _repository.CreateUpdateList(saveList, modifyUser).ConvertAllTo<ApptLabelDto>().ToList();
        }



    }
}
