using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Rules;
using JARS.Core.Utils;
using JARS.Data.NH.Jars.Interfaces;
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
    public class JarsRuleService : ServicesBase
    {
        public JarsRuleResponse Any(GetJarsRules request)
        {
            JarsRuleResponse response = new JarsRuleResponse();
            if (request != null)
            {
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsRule, IDataContextNhJars>>();
                //because the EntityRule is a core class and is used as is, there is no need for dtos or transformations. :-)
                response.Rule = _repository.GetById(request.Id);
            }
            return response;
        }

        public void Any(DeleteJarsRules request)
        {
            if (request != null)
            {
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsRule, IDataContextNhJars>>();
                //because the EntityRule is a core class and is used as is, there is no need for dtos or transformations. :-)
                _repository.Delete(request.Id);
                TrySendDeleteNotificationToChannel(typeof(JarsRule).Name, new[] { request.Id.ToString() });
            }
        }

        public object Any(FindJarsRules request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            JarsRulesResponse response = new JarsRulesResponse();
            if (request != null)
            {
                var query = BuildQuery<JarsRule>(request);
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsRule, IDataContextNhJars>>();
                //because the EntityRule is a core class and is used as is, there is no need for dtos or transformations. :-)
                response.Rules = _repository.Where(query, true).ToList();
            }
            return response;
            //});

        }

        /// <summary>
        /// Update or create a single EntityRule.
        /// </summary>
        /// <param name="request">The request containing the EntityRule to be created or updated</param>
        /// <returns>the updated value</returns>
        public virtual JarsRuleResponse Any(StoreJarsRule request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            JarsRuleResponse response = new JarsRuleResponse();
            IGenericEntityRepositoryBase<JarsRule, IDataContextNhJars> _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsRule, IDataContextNhJars>>();
            response.Rule = _repository.CreateUpdate(request.Rule, CurrentSessionUsername);
            return response;
            // });
        }

        /// <summary>
        /// Update or create a list of EntityRules.
        /// </summary>
        /// <param name="request">The request containing the EntityRules that needs to be created or updated</param>
        /// <returns>the list of values updated or created will be returned.</returns>
        public virtual JarsRulesResponse Any(StoreJarsRules request)
        {
            //return ExecuteFaultHandledMethod(() =>
            //{
            JarsRulesResponse response = new JarsRulesResponse();
            IGenericEntityRepositoryBase<JarsRule, IDataContextNhJars> _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsRule, IDataContextNhJars>>();
            response.Rules = _repository.CreateUpdateList(request.Rules, CurrentSessionUsername).ToList();
            return response;
            // });
        }


        /// <summary>
        /// A helper method used for building the query, depending on if the LoadLazy option was set.
        /// </summary>
        /// <typeparam name="T">A query over object required by the QueryOver Method on the repository</typeparam>
        /// <param name="request">The same request passed into the service</param>
        /// <param name="hasWhere">a bool value indicating if there is a where string</param>
        /// <returns></returns>
        private Expression<Func<T, bool>> BuildQuery<T>(FindJarsRules request) where T : JarsRule
        {
            Expression<Func<T, bool>> query = LinqExpressionBuilder.True<T>();

            if (!request.TargetEntityTypeName.IsNullOrEmpty())
                query = query.And(o => o.TargetTypeName == request.TargetEntityTypeName);

            if (!request.SourceEntityTypeName.IsNullOrEmpty())
                query = query.And(o => o.SourceTypeName == request.SourceEntityTypeName);

            return query;
        }

        /// <summary>
        /// Send CRUD notifications for a EntityCondition Entities
        /// Note! :
        /// This Method is a special method used by the service when ServerEvents are being used.(serviceStack). 
        /// If the service does not implement serverEvents this will throw an error. 
        /// This will send a notification to all subscribed clients (including the client the request originated from) where the chanel name is the name of the entity type.
        /// This will only process SelectorTypes.store and SelectorTypes.delete notifications.
        /// The notification sent to subscribers will be a ServerEventMessage (serviceStack) where the data(json) is set as ServerEventMessageData (Jars) object.
        /// </summary>
        /// <param name="crud">The notification request indicating a store or delete event that will be sent to other subscribers.</param>
        public virtual void Any(JarsRulesNotification crud)
        {
            //ExecuteFaultHandledMethod(() =>
            //{

            //check that the sender has subscribed to the service
            //SubscriptionInfo subscriber = ServerEvents.GetSubscriptionInfo(crud.FromSubscriptionId);
            //if (subscriber == null)
            //    throw HttpError.NotFound($"Subscriber {crud.FromUserName} does not exist.");

            if (crud.Selector == SelectorTypes.store)
            {
                var _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsRule, IDataContextNhJars>>();
                var ruleList = _repository.Where(r => crud.Ids.Contains(r.Id)).ToList();
                TrySendStoreNotificationToChannel(typeof(JarsRule).Name, ruleList.ToJson());
            }

            if (crud.Selector == SelectorTypes.delete)
            {
                TrySendDeleteNotificationToChannel(typeof(JarsRule).Name, crud.Ids.ConvertTo<List<string>>().ToArray());
            }
            //});
        }
    }
}
