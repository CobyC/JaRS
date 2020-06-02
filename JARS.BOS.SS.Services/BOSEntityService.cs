using JARS.BOS.Data;
using JARS.BOS.Entities;
using JARS.BOS.SS.DTOs;
using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Utils;
using JARS.SS.DTOs.Utils;
using JARS.SS.Services;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JARS.BOS.SS.Services
{

    [Authenticate]
    public class BOSEntityService : ServicesBase
    {
        public BOSEntityService() : base()
        {
        }
        /// <summary>
        /// Returns a single BOSRecord where the id in the request matches the record ID in the database.
        /// </summary>
        /// <param name="request">The request used for requesting the record from the database.</param>
        /// <returns>If FetchLazy was set to false, the JarsBOSRecord item will be populated, if not then the JarsBOSRecordBase record will be filled with data.</returns>
        public virtual BOSEntityResponse Any(GetBOSEntity request)
        {

            BOSEntityResponse response = new BOSEntityResponse();
            IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS> _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS>>();
            response.BOSEntity = _repository.GetById(request.Id, true).ConvertTo<BOSEntityDto>();
            return response;

        }


        /// <summary>
        /// Find a BOSRecord by specifying values for the properties available in the request.
        /// Date values: Start date will go from date forward, end date will be from end date back, and if both has values, 
        /// it will be used as from (between) start to end
        /// </summary>
        /// <param name="request">The request used for building the find parameters</param>
        /// <returns>If LoadLazy was true, then a list of JarsBOSRecordBase items, otherwise a list of fully loaded JarsBOSRecords</returns>
        public virtual BOSEntitiesResponse Any(FindBOSEntities request)
        {

            BOSEntitiesResponse response = new BOSEntitiesResponse();
            if (request != null)
            {
                Expression<Func<BOSEntity, bool>> query = LinqExpressionBuilder.True<BOSEntity>();

                //Id
                if (request.ResourceId != null)
                {
                    query = query.And(a => a.ResourceId == int.Parse(request.ResourceId));
                }

                //StartDateTime
                if (request.StartDate.HasValue && request.StartDate != DateTime.MinValue)
                    query = query.And(a => a.StartDate.Date >= request.StartDate.Value.Date);

                //EndDateTime (unlike the mobile version we dont set the end date = start date if the end date is empty)
                if (request.EndDate.HasValue && request.EndDate != DateTime.MinValue)
                    query = query.And(a => a.EndDate.Date <= request.EndDate.Value.Date);


                //statuses
                if (!request.Statuses.IsNullOrEmpty())
                {
                    //if the status value contains a , then split it, otherwise just use as is
                    if (request.Statuses.Contains(','))
                    {
                        string[] arrStatus = request.Statuses.Split(new[] { ',' });
                        query = query.And(a => arrStatus.Contains(a.ProgressStatus));
                    }
                    else
                        query = query.And(a => a.ProgressStatus == request.Statuses);
                }

                IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS> _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS>>();
                response.BOSEntities = _repository.Where(query, true).ConvertAllTo<BOSEntityDto>().ToList();

            }
            return response;

        }

        /// <summary>
        /// Find a BOSRecord by specifying values for the properties available in the request.
        /// Date values: Start date will go from date forward, end date will be from end date back, and if both has values, 
        /// it will be used as from (between) start to end
        /// </summary>
        /// <param name="request">The request used for building the find parameters</param>
        /// <returns>If LoadLazy was true, then a list of JarsBOSRecordBase items, otherwise a list of fully loaded JarsBOSRecords</returns>
        public virtual object Any(FindMobileBOSEntities request)
        {
            //https://github.com/ServiceStack/ServiceStack.Text#global-default-json-configuration

            MobileBOSEntitiesResponse response = new MobileBOSEntitiesResponse();
            if (request != null)
            {
                Expression<Func<BOSEntity, bool>> query = LinqExpressionBuilder.True<BOSEntity>();

                //Id
                if (request.ResourceId != null)
                {
                    query = query.And(a => a.ResourceId == int.Parse(request.ResourceId));
                }

                //StartDateTime
                if (request.StartDate.HasValue && request.StartDate != DateTime.MinValue)
                    query = query.And(a => a.StartDate.Date >= request.StartDate.Value.Date);

                //EndDateTime
                if (request.EndDate.HasValue && request.EndDate != DateTime.MinValue)
                    query = query.And(a => a.EndDate.Date <= request.EndDate.Value.Date);
                else
                {
                    //set end date to start date so that we only get one day by default
                    if (request.StartDate.HasValue && request.StartDate != DateTime.MinValue)
                        query = query.And(a => a.EndDate.Date <= request.StartDate.Value.Date);
                }

                //statuses
                if (!request.Statuses.IsNullOrEmpty())
                {
                    //if the status value contains a , then split it, otherwise just use as is
                    if (request.Statuses.Contains(','))
                    {
                        string[] arrStatus = request.Statuses.Split(new[] { ',' });
                        query = query.And(a => arrStatus.Contains(a.ProgressStatus));
                    }
                    else
                        query = query.And(a => a.ProgressStatus == request.Statuses);
                }

                IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS> _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS>>();
                response.BOSEntities = _repository.Where(query, true).ConvertAllTo<MobileBOSEntityDto>().ToList();
            }

            return new HttpResult(response)
            {
                ResultScope = () =>
                    JsConfig.With(new Config
                    {
                        AssumeUtc = true,
                        DateHandler = DateHandler.ISO8601DateTime
                    })
            };
            //using (JsConfig.With(new Config { DateHandler = DateHandler.ISO8601 }))
            //{
            //    return response;
            //}

        }

        public virtual BOSEntityResponse Any(StoreBOSEntity request)
        {

            BOSEntityResponse response = new BOSEntityResponse();
            IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS> _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS>>();
            if (request.BOSEntity != null)
            {
                response.BOSEntity = _repository.CreateUpdate(request.BOSEntity.ConvertTo<BOSEntity>(), CurrentSessionUsername).ConvertTo<BOSEntityDto>();
            }
            return response;

        }

        /// <summary>
        /// Update or create a single BOSRecord or a list of BOSRecords, depending on whether the BOSRecord or BOSRecords property has got a value set.
        /// If the BOSRecord property is set the BOSRecord will be created or updated and the BOSRecords property will be ignored.
        /// To create or update more than one BOSRecord, assign a list to the BOSRecords property and make sure BOSRecord is set to nothing/null.
        /// </summary>
        /// <param name="request">The request containing the BOSRecord or BOSRecords that needs to be created or updated</param>
        /// <returns>depending on the values supplied, the updated BOSRecord or BOSRecords will be returned.</returns>
        public virtual BOSEntitiesResponse Any(StoreBOSEntities request)
        {

            BOSEntitiesResponse response = new BOSEntitiesResponse();
            IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS> _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS>>();

            response.BOSEntities = _repository.CreateUpdateList(request.BOSEntities.ConvertAllTo<BOSEntity>().ToList(), CurrentSessionUsername).ConvertAllTo<BOSEntityDto>().ToList();
            return response;

        }

        public virtual object Any(StoreMobileBOSEntity request)
        {

            MobileBOSEntityResponse response = new MobileBOSEntityResponse();
            IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS> _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS>>();
            if (request.BOSEntity != null)
            {
                BOSEntity old = _repository.GetById(request.BOSEntity.Id);
                BOSEntity updt = old.PopulateWith(request.BOSEntity);
                updt = _repository.CreateUpdate(updt, CurrentSessionUsername);

                response.BOSEntity = updt.ConvertTo<MobileBOSEntityDto>();

                TrySendStoreNotificationToChannel(typeof(BOSEntity).Name, new List<BOSEntity> { updt }.ToJson());
            }

            return new HttpResult(response)
            {
                ResultScope = () =>
                    JsConfig.With(new Config { DateHandler = DateHandler.ISO8601 })
            };
            //using (JsConfig.With(new Config { DateHandler = DateHandler.ISO8601 }))
            //{
            //    return response;
            //}
        }

        /// <summary>
        /// Update or create a single BOSRecord or a list of BOSRecords, depending on whether the BOSRecord or BOSRecords property has got a value set.
        /// If the BOSRecord property is set the BOSRecord will be created or updated and the BOSRecords property will be ignored.
        /// To create or update more than one BOSRecord, assign a list to the BOSRecords property and make sure BOSRecord is set to nothing/null.
        /// </summary>
        /// <param name="request">The request containing the BOSRecord or BOSRecords that needs to be created or updated</param>
        /// <returns>depending on the values supplied, the updated BOSRecord or BOSRecords will be returned.</returns>
        public virtual object Any(StoreMobileBOSEntities request)
        {

            MobileBOSEntitiesResponse response = new MobileBOSEntitiesResponse();

            if (request.BOSEntities.Any())
            {
                IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS> _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS>>();
                List<int> ids = request.BOSEntities.Select(e => e.Id).ToList();
                IList<BOSEntity> oldies = _repository.Where(e => ids.Contains(e.Id)).ToList();
                foreach (var reqEntity in request.BOSEntities)
                {
                    BOSEntity old = oldies.SingleOrDefault(o => o.Id == reqEntity.Id);
                    if (old != null)
                        old.PopulateWith(reqEntity);
                }

                List<BOSEntity> updtList = _repository.CreateUpdateList(oldies, CurrentSessionUsername).ToList();

                response.BOSEntities = updtList.ConvertAllTo<MobileBOSEntityDto>().ToList();

                TrySendStoreNotificationToChannel(typeof(BOSEntity).Name, updtList.ToJson());

                //response.BOSEntities = _repository.MergeList(request.BOSEntities.ConvertAllTo<BOSEntity>().ToList(), CurrentSessionUsername).ConvertAllTo<MobileBOSEntityDto>().ToList();
            }
            return new HttpResult(response)
            {
                ResultScope = () =>
                    JsConfig.With(new Config { DateHandler = DateHandler.ISO8601 })
            };
            //using (JsConfig.With(new Config { DateHandler = DateHandler.ISO8601 }))
            //{
            //    return response;
            //}

        }

        public virtual void Any(DeleteBOSEntity request)
        {

            var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS>>();
            _repository.Delete(request.Id);
            TrySendDeleteNotificationToChannel(typeof(BOSEntity).Name, new[] { request.Id.ToString() });

        }

        /// <summary>
        /// Send CRUD notifications for a BOSRecord Entity or Entities
        /// Note! :
        /// This Method is a special method used by the service when ServerEvents are being used.(serviceStack). 
        /// If the service does not implement serverEvents this will throw an error. 
        /// This will send a notification to all subscribed clients (including the client the request originated from) where the chanel name is the name of the entity type.
        /// This will only process SelectorTypes.store and SelectorTypes.delete notifications.
        /// The notification sent to subscribers will also be a JarsSyncEventStore or JarsSyncEventStore Dto object.
        /// </summary>
        /// <param name="crud">The notification request indicating a store or delete event that will be sent to other subscribers.</param>
        public virtual void Any(BOSEntitiesNotification crud)
        {

            //do some BOSRecord updates here using the info from the the crud
            IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS> _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS>>();

            if (crud.Selector == SelectorTypes.store)
            {
                List<BOSEntityDto> notifyList = _repository.Where(d => crud.Ids.Contains(d.Id)).ConvertAllTo<BOSEntityDto>().ToList();
                TrySendStoreNotificationToChannel(typeof(BOSEntity).Name, notifyList.ToJson());
            }

            if (crud.Selector == SelectorTypes.delete)
            {
                TrySendDeleteNotificationToChannel(typeof(BOSEntity).Name, crud.Ids.ConvertTo<List<string>>().ToArray());//, true);
            }

        }
    }
}
