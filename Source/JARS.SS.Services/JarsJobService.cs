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
    public class JarsJobService : ServicesBase
    {
        /// <summary>
        /// Update or create a single job or a list of jobs, depending on whether the Job or Jobs property has got a value set.
        /// If the Job property is set the Job will be created or updated and the Jobs property will be ignored.
        /// To create or update more than one job, assign a list to the Jobs property and make sure Job is set to nothing/null.
        /// </summary>
        /// <param name="request">The request containing the job or jobs that needs to be created or updated</param>
        /// <returns>depending on the values supplied, the updated job or jobs will be returned.</returns>
        public virtual JarsJobsResponse Any(StoreJobs request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            JarsJobsResponse response = new JarsJobsResponse();
            IJarsJobRepository _repository = _DataRepositoryFactory.GetDataRepository<IJarsJobRepository>();

            response.Jobs = _repository.CreateUpdateList(request.Jobs.ConvertAllTo<JarsJob>().ToList(), CurrentSessionUsername).ConvertAllTo<JarsJobDto>().ToList();
            return response;
            //});
        }

        /// <summary>
        /// Returns a single job where the id in the request matches the record ID in the database.
        /// </summary>
        /// <param name="request">The request used for requesting the record from the database.</param>
        /// <returns>If FetchAsDto was set to false, the JarsJob item will be populated, if not then the JobDto record will have it's values set.</returns>
        public virtual JarsJobsResponse Any(GetJarsJob request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            JarsJobsResponse response = new JarsJobsResponse();
            IJarsJobRepository _repository = _DataRepositoryFactory.GetDataRepository<IJarsJobRepository>();
            response.Jobs.Add(_repository.GetById(request.Id, request.FetchEagerly).ConvertTo<JarsJobDto>());
            return response;
            //});
        }


        /// <summary>
        /// Find a Job by specifying values for the properties available in the request.
        /// Date values: Start date will go from date forward, end date will be from end date back, and if both has values, 
        /// it will be used as from (between) start to end
        /// </summary>
        /// <param name="request">The request used for building the find parameters</param>
        /// <returns>If LoadLazy was true, then a list of JarsJobBase items, otherwise a list of fully loaded JarsJobs</returns>
        public virtual JarsJobsResponse Any(FindJarsJobs request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            JarsJobsResponse response = new JarsJobsResponse();
            if (request != null)
            {
                var query = BuildQuery(request);
                var _repository = _DataRepositoryFactory.GetDataRepository<IJarsJobRepository>();
                response.Jobs = _repository.Where(query,request.FetchEagerly).ConvertAllTo<JarsJobDto>().ToList();

            }
            return response;
            ////});
        }

        /// <summary>
        /// A helper method used for building the query, depending on if the LoadLazy option was set.
        /// </summary>
        /// <typeparam name="T">A query over object required by the QueryOver Method on the repository</typeparam>
        /// <param name="request">The same request passed into the service</param>       
        /// <returns></returns>
        private Expression<Func<JarsJob, bool>> BuildQuery(FindJarsJobs request)
        {
            Expression<Func<JarsJob, bool>> query = LinqExpressionBuilder.True<JarsJob>();

            //Id
            if (request.Id != 0)
                query = LinqExpressionBuilder.True<JarsJob>().And(j => j.Id == request.Id);

            //ExtRefID
            if (request.ExtRefID != "0")
                query = query.And(j => j.ExtRefId == request.ExtRefID);

            //ResourceID
            if (request.ResourceId != 0)
                query = query.And(j => j.ResourceId == request.ResourceId);

            //Location
            if (request.Location != null)
                query = query.And(j => j.Location == request.Location);

            //CompletionDate
            if (request.ProgressStatus != null)
                query = query.And(j => j.ProgressStatus == request.ProgressStatus);

            //StartDateTime
            if (request.StartDate != DateTime.MinValue)
                query = query.And(j => j.StartDate >= request.StartDate);

            //EndDateTime
            if (request.EndDateTime != DateTime.MinValue)
                query = query.And(j => j.EndDate <= request.EndDateTime);

            return query;
        }


        /// <summary>
        /// Send CRUD notifications for a JarsJob Entity or Entities
        /// Note! :
        /// This Method is a special method used by the service when ServerEvents are being used.(serviceStack). 
        /// If the service does not implement serverEvents this will throw an error. 
        /// This will send a notification to all subscribed clients (including the client the request originated from) where the chanel name is the name of the entity type.
        /// This will only process SelectorTypes.store and SelectorTypes.delete notifications.
        /// The notification sent to subscribers will be a ServerEventMessage (serviceStack) where the data(json) is set as ServerEventMessageData (Jars) object.
        /// </summary>
        /// <param name="crud">The notification request indicating a store or delete event that will be sent to other subscribers.</param>
        public virtual void Any(JarsJobsNotification crud)
        {
            //ExecuteFaultHandledMethod(() =>
            //{
            //check that the sender has subscribed to the service
            //SubscriptionInfo subscriber = ServerEvents.GetSubscriptionInfo(crud.From);
            List<SubscriptionInfo> subscriber = ServerEvents.GetSubscriptionInfosByUserId(crud.FromUserName);
            if (subscriber == null)
                throw HttpError.NotFound($"Subscriber {crud.FromUserName} does not exist.");

            //do some job updates here using the info from the the crud
            IJarsJobRepository _repository = _DataRepositoryFactory.GetDataRepository<IJarsJobRepository>();

            //first determine if the Dto objects are full or not, if they are then fill the counterpart objects            
            if (crud.Selector == SelectorTypes.store)
            {
                crud.Jobs = _repository.CreateUpdateList(crud.Jobs.ConvertAllTo<JarsJob>().ToList(), crud.FromUserName).ConvertAllTo<JarsJobDto>().ToList();
                ServerEvents.NotifyChannel(typeof(JarsJob).Name, crud.Selector, crud.Jobs);
            }

            if (crud.Selector == SelectorTypes.delete)

            {
                _repository.DeleteList(crud.Jobs.ConvertAllTo<JarsJob>().ToList());
                ServerEvents.NotifyChannel(typeof(JarsJob).Name, crud.Selector, crud.Jobs.Select(j=>j.Id));
            }
            //});
        }
    }
}
