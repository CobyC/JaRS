using JARS.Data.NH.Jars.Interfaces;
using JARS.Entities;
using JARS.SS.RequestResponseDtos;
using JARS.SS.RequestResponseDtos.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JARS.SS.Services
{
    public class JarsUserRoleService : ServicesBase
    {
        /// <summary>
        /// Update or create a single JarsUserRole or a list of Roles, depending on whether the Role or Roles property has got a value set.
        /// If the Role property is set the Role will be created or updated and the Roles property will be ignored.
        /// To create or update more than one Role, assign a list to the Roles property and make sure Role is set to nothing/null.
        /// </summary>
        /// <param name="request">The request containing the Role or Roles that needs to be created or updated</param>
        /// <returns>depending on the values supplied, the updated Role or Roles will be returned.</returns>
        public virtual JarsUserRolesResponse Any(StoreJarsUserRoles request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                JarsUserRolesResponse response = new JarsUserRolesResponse();
                IJarsUserRoleRepository _repository = _DataRepositoryFactory.GetDataRepository<IJarsUserRoleRepository>();
                bool isSingleEnt = false;
                if (request.Role != null)
                    isSingleEnt = true;

                if (request.Role != null)
                    response.Role = _repository.CreateUpdate(request.Role, request.ModifiedBy);
                else
                    response.Roles = _repository.CreateUpdateList(request.Roles, request.ModifiedBy).ToList();
                return response;
            });
        }

        public object Any(GetJarsUserRole request)
        {
            IJarsUserRoleRepository repository = _DataRepositoryFactory.GetDataRepository<IJarsUserRoleRepository>();
            GetJarsUserRoleResponse response = new GetJarsUserRoleResponse();
            if (request.Id != 0)
            {
                if (request.FetchAsDto)
                {
                    var expr = new List<Expression<Func<JarsUserRole, object>>>();
                    expr.Add(g => g.UserAccounts);
                    response.RoleDto = repository.GetByIdEagerly((int)request.Id, expr).ConvertTo<JarsUserRoleDto>();
                }
                else
                {
                    var expr = new List<Expression<Func<JarsUserRole, object>>>();
                    expr.Add(g => g.UserAccounts);
                    response.Role = repository.GetByIdEagerly(request.Id, expr);
                }
            }
            else
            {
                if (request.Name != "")
                {
                    if (request.FetchAsDto)
                        response.RoleDto = repository.GetByNameEagerly(request.Name).ConvertTo<JarsUserRoleDto>();
                    else
                        response.Role = repository.GetByNameEagerly(request.Name);
                }
            }

            return response;
        }

        public object Any(FindJarsUserRoles request)
        {
            IJarsUserRoleRepository repository = _DataRepositoryFactory.GetDataRepository<IJarsUserRoleRepository>();
            JarsUserRolesResponse response = new JarsUserRolesResponse();

            if (request.FetchAsDto)
            {
                if (request.Name != null)
                {
                    response.RoleDtos = repository.Where(g => g.Name == request.Name).ToList().ConvertTo<List<JarsUserRoleDto>>();
                }
                if (request.ContainingUserAccountId != 0)
                {
                    response.RoleDtos = repository.Where(g => g.UserAccounts.Contains(g.UserAccounts.FirstOrDefault(u => u.Id == request.ContainingUserAccountId))).ToList().ConvertTo<List<JarsUserRoleDto>>();
                }
            }
            else
            {
                var plist = new List<Expression<Func<JarsUserRole, object>>>();
                plist.Add(p => p.UserAccounts);

                if (request.Name != null)
                {
                    response.Roles = repository.WhereQueryOverEagerly(g => g.Name == request.Name, plist).ToList();
                }
                else if (request.ContainingUserAccountId != 0)
                {
                    response.Roles = repository.WhereQueryOverEagerly(g => g.UserAccounts.Contains(g.UserAccounts.FirstOrDefault(u => u.Id == request.ContainingUserAccountId)), plist).ToList();
                }
                else
                {
                    response.Roles = repository.GetAllEagerly(plist).ToList();
                }
            }
            response.UpdateChildReferences();
            return response;
        }


        /// <summary>
        /// Send CRUD notifications for a JarsUserRole Entity or Entities
        /// Note! :
        /// This Method is a special method used by the service when ServerEvents are being used.(serviceStack). 
        /// If the service does not implement serverEvents this will throw an error. 
        /// This will send a notification to all subscribed clients (including the client the request originated from) where the chanel name is the name of the entity type.
        /// This will only process SelectorTypes.store and SelectorTypes.delete notifications.
        /// The notification sent to subscribers will also be a JarsSyncEventStore or JarsSyncEventStore Dto object.
        /// </summary>
        /// <param name="crud">The notification request indicating a store or delete event that will be sent to other subscribers.</param>
        public virtual void Any(JarsUserRolesCrudNotification crud)
        {
            ExecuteFaultHandledMethod(() =>
            {
                //check that the sender has subscribed to the service
                SubscriptionInfo subscriber = ServerEvents.GetSubscriptionInfo(crud.FromSubscriptionId);
                if (subscriber == null)
                    throw HttpError.NotFound($"Subscriber {crud.FromUserId} does not exist.");

                //do some job updates here using the info from the the crud
                IJarsUserRoleRepository _repository = _DataRepositoryFactory.GetDataRepository<IJarsUserRoleRepository>();

                if (crud.Selector == SelectorTypes.store)
                    if (crud.Role != null)
                    {
                        crud.Role = _repository.CreateUpdate(crud.Role, crud.FromUserId);
                        ServerEvents.NotifyChannel(crud.Channel, crud.Selector, crud.Role.ConvertToJarsSyncEventStore());
                    }
                    else
                    {
                        crud.Roles = _repository.CreateUpdateList(crud.Roles, crud.FromUserId).ToList();
                        ServerEvents.NotifyChannel(crud.Channel, crud.Selector, crud.Roles.ConvertToJarsSyncEventStore());
                    }

                if (crud.Selector == SelectorTypes.delete)
                    if (crud.Role != null)
                    {
                        _repository.Delete(crud.Role);
                        ServerEvents.NotifyChannel(crud.Channel, crud.Selector, crud.Role.ConvertToJarsSyncEventDelete());
                    }
                    else
                    {
                        _repository.DeleteList(crud.Roles);
                        ServerEvents.NotifyChannel(crud.Channel, crud.Selector, crud.Roles.ConvertToJarsSyncEventDelete());
                    }
            });
        }
    }
}
