using JARS.Core;
using JARS.Core.Exceptions;
using JARS.Core.Interfaces.Processors;
using JARS.Core.Interfaces.Repositories;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace JARS.SS.Services
{
    /// <summary>
    /// This class is used as the Service base for any service that implements ServiceStack services
    /// </summary>
    public class ServicesBase : Service
    {
        //here we can make use of the DI repository factory
        [Import]
        public IDataRepositoryFactory _DataRepositoryFactory;

        //we call on the processor factory where we can request access to the IJarsBusinessProcessors
        [Import]
        public IProcessorFactory _JarsProcessorFactory;

        /// <summary>
        /// This is used for keeping record of all subscriptions and connections.
        /// It is kept in memory.
        /// </summary>
        public IServerEvents ServerEvents { get; set; }

        public ServicesBase()
        {
            //We call this satisfy imports here because wcf resolves their dependencies after it has been constructed.
            //post construction resolve.
            if (JarsCore.Container != null)
                JarsCore.Container.SatisfyImportsOnce(this);//this will resolve any [import] through MEF.
        }

        public ServicesBase(IDataRepositoryFactory DataRepositoryFactory)
        {
            _DataRepositoryFactory = DataRepositoryFactory;
        }

        public string CurrentSessionUsername { get => Request.GetSession().UserName; }

        /// <summary>
        /// Use this method to send a notification to all subscribers of the chanel in regards to entities being stored (created or updated)
        /// </summary>
        /// <param name="channel">The channel to notify</param>
        /// <param name="msgData">The message object being sent (this needs be the list of changed entities)</param>
        /// <param name="IsAppointment">Indicate if the store is related to an appointment on the scheduler</param>
        /// <param name="jSonScopeSettings">the way in which json is serialized by ServiceStack can be managed (see website) default is eti</param>
        protected void TrySendStoreNotificationToChannel(string channel, string jsonData, string jSonScopeSettings = "eti")
        {
            try
            {
                if (ServerEvents != null && ServerEvents.GetSubscriptionsDetails(channel).Count > 0)
                {
                    //https://github.com/ServiceStack/ServiceStack/wiki/Customize-JSON-Responses#custom-json-settings
                    using (JsConfig.CreateScope(jSonScopeSettings))
                    {
                        var en = new ServerEventMessageData()
                        {
                            From = CurrentSessionUsername,
                            //IsAppointment = IsAppointment,
                            jsonDataString = jsonData
                        };
                        ServerEvents.NotifyChannel(channel, SelectorTypes.store, en);
                    }
                }

            }
            catch (Exception notifyEx)
            {
                Logger.Error("Error in Store ServerEventMessageData", notifyEx);
            }
        }

        /// <summary>
        /// Use this method to send a notification to all subscribers of the chanel in regards to entities being stored (created or updated)
        /// </summary>
        /// <param name="channel">The channel to notify</param>
        /// <param name="msgData">The message object being sent (this could be the entities)</param>
        /// <param name="IsAppointment">Indicate if the store is related to an appointment on the scheduler</param>
        protected async Task TrySendStoreNotificationToChannelAsync(string channel, string msgJsonData, string jSonScopeSettings = "eti")
        {
            try
            {
                if (ServerEvents != null && ServerEvents.GetSubscriptionsDetails(channel).Count > 0)
                {
                    using (JsConfig.CreateScope(jSonScopeSettings))
                    {
                        var en = new ServerEventMessageData()
                        {
                            From = CurrentSessionUsername,
                            //IsAppointment = IsAppointment,
                            jsonDataString = msgJsonData
                        };
                        await ServerEvents.NotifyChannelAsync(channel, SelectorTypes.store, en);
                    }
                }

            }
            catch (Exception notifyEx)
            {
                Logger.Error("Error in Store ServerEventMessageData", notifyEx);
            }
        }

        /// <summary>
        /// Use this method to send a notification to all subscribers of the chanel in regards to entities being deleted (removed permanently)
        /// </summary>
        /// <param name="channel">The channel to notify</param>
        /// <param name="message">The message object being sent (this could be the list of Ids)</param>
        /// <param name="IsAppointment">Indicate if the delete is related to an appointment on the scheduler</param>
        protected void TrySendDeleteNotificationToChannel(string channel, string[] ids)//, bool IsAppointment = false)
        {
            try
            {
                if (ServerEvents != null && ServerEvents.GetSubscriptionsDetails(channel).Count > 0)
                {
                    var en = new ServerEventMessageData()
                    {
                        From = CurrentSessionUsername,
                        //IsAppointment = IsAppointment,
                        jsonDataString = ids.ToJson()
                    };
                    ServerEvents.NotifyChannel(channel, SelectorTypes.delete, en);
                }
            }
            catch (Exception notifyEx)
            {
                Logger.Error("Error in Delete ServerEventMessageData", notifyEx);
            }
        }

        /// <summary>
        /// Use this method to send a notification to all subscribers of the chanel in regards to entities being deleted (removed permanently)
        /// </summary>
        /// <param name="channel">The channel to notify</param>
        /// <param name="message">The message object being sent (this could be the list of Ids)</param>
        /// <param name="IsAppointment">Indicate if the delete is related to an appointment on the scheduler</param>
        protected async Task TrySendDeleteNotificationToChannelAsync(string channel, string[] ids)//, bool IsAppointment = false)
        {
            try
            {
                if (ServerEvents != null && ServerEvents.GetSubscriptionsDetails(channel).Count > 0)
                {
                    var en = new ServerEventMessageData()
                    {
                        //IsAppointment = IsAppointment,
                        jsonDataString = ids.ToJson()
                    };
                    await ServerEvents.NotifyChannelAsync(channel, SelectorTypes.delete, en);
                }
            }
            catch (Exception notifyEx)
            {
                Logger.Error("Error in Delete ServerEventMessageData", notifyEx);
            }
        }

        /// <summary>
        /// This method wraps the code passes to it with exception handling, 
        /// It returns whatever is returned from within the encapsulated code
        /// if it fails it will return with a FaultException.
        /// </summary>
        /// <typeparam name="T">The type or result that will be returned if code was successful</typeparam>
        /// <param name="codeToExecute">The code to executed (in lambda expression). return (()=>{return code;}); </param>
        /// <returns>The result specified as T (Generic return from within the code), FaultException if failed</returns>
        protected T ExecuteFaultHandledMethod<T>(Func<T> codeToExecute)
        {
            try// wrap all of this in a try catch, so that if there is an error we can make WCF handle this gracefully, instead of just breaking the service
            {
                return codeToExecute.Invoke();
            }
            catch (ClientNotFoundException fx)
            {
                throw HttpError.NotFound(fx.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method wraps the code passes to it with exception handling, but does not return anything         
        /// if it fails it will return with a FaultException.
        /// </summary>      
        /// <param name="codeToExecute">The code(or action) to execute, usually in a lambda expression. ()=>{code;}</param>
        protected void ExecuteFaultHandledMethod(Action codeToExecute)
        {
            try// wrap all of this in a try catch, so that if there is an error we can make WCF handle this gracefully, instead of just breaking the service
            {
                codeToExecute.Invoke();
            }
            catch (ClientNotFoundException fx)
            {
                throw HttpError.NotFound(fx.Message);
            }
            catch (Exception ex)
            {
                this.Response.StatusDescription = ex.Message;
                this.Response.StatusCode = 420;
                throw ex; //throwing fault exception will stop the wcf service from going into a faulted state
            }
        }
    }
}
