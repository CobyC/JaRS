using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Security;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Entities;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using ServiceStack.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JARS.SS.Services
{

    //the jars user uses the 
    [Authenticate]

    public class JarsUserService : ServicesBase
    {

        //this is the only request that does not require admin role
        public JarsUserResponse Any(GetJarsUser request)
        {
            if (request.EmailOrUserName.IsNullOrEmpty())
                return null;

            var sessionUserName = Request.GetSession().UserName;
            var sessionUserEmail = Request.GetSession().Email;
            IAuthRepository ssAuthRepo = ServiceStackHost.Instance.GetAuthRepository();
            IUserAuth ssUser = ssAuthRepo.GetUserAuthByUserName(request.EmailOrUserName);

            if (ssUser == null)
                throw HttpError.NotFound("User not found");
            if (ssUser.LockedDate != null)
                throw HttpError.Unauthorized("User account locked");

            if (ssUser.Roles.Count == 0 || ssUser.Permissions.Count == 0)
            {
                IUserAuth newUserA = new UserAuth();
                newUserA.PopulateWith(ssUser);
                if (ssUser.Roles.Count == 0)
                    newUserA.Roles.Add("Guest");
                if (ssUser.Permissions.Count == 0)
                    newUserA.Permissions.Add("ViewOnly");
                ssUser = ssAuthRepo.UpdateUserAuth(ssUser, newUserA);
            }

            IJarsUserRepository repository = _DataRepositoryFactory.GetDataRepository<IJarsUserRepository>();
            JarsUser acc = repository.Where(u => u.UserName == ssUser.UserName || u.Email == ssUser.Email, request.FetchEagerly).SingleOrDefault();
            if (acc == null)
            {
                acc = ssUser.ConvertTo<JarsUser>();
                acc.Id = 0;
                acc = repository.CreateUpdate(acc, sessionUserName);
            }
            else
            {
                //we have to change the id because the 2 tables differ and id's wont match.
                int accId = acc.Id;
                acc.PopulateWith(ssUser);
                acc.Id = accId;
                acc = repository.CreateUpdate(acc, sessionUserName);
            }

            JarsUserResponse response = new JarsUserResponse
            {
                UserAccount = acc.ConvertTo<JarsUserDto>()
            };
            //response.jarsUserAccount = FakeDataHelper.FakeUserAccount;
            return response;
        }

        public JarsUsersResponse Any(FindJarsUsers request)
        {

            IAuthRepository ssAuthRepo = ServiceStackHost.Instance.GetAuthRepository();

            var _headerRepository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsUserHeader, IDataContextNhJars>>();
            //get all users in ss
            List<IUserAuth> ssUsers = ssAuthRepo.GetUserAuths();

            //get all users in jars
            List<JarsUserHeader> jUsers = _headerRepository.GetAll().ToList();

            ////create jars user for every missing auth user
            List<JarsUserHeader> newUsers = new List<JarsUserHeader>();

            foreach (var ssUser in ssUsers)
            {
                var jUser = jUsers.Where(j => j.UserName == ssUser.UserName).FirstOrDefault();
                if (jUser == null)
                {
                    jUser = ssUser.ConvertTo<JarsUserHeader>();
                    jUser.Id = 0;
                    jUser.IsActive = false;
                    newUsers.Add(jUser);
                }
            }

            if (newUsers.Count > 0)
                _headerRepository.CreateUpdateList(newUsers, CurrentSessionUsername);



            JarsUsersResponse response = new JarsUsersResponse();
            IList<JarsUser> dataBaseList = new List<JarsUser>();
            if (request.FetchEagerly)
            {
                IJarsUserRepository repository = _DataRepositoryFactory.GetDataRepository<IJarsUserRepository>();
                var plist = new List<Expression<Func<JarsUser, object>>>
                {
                    //plist.Add(p => p.Roles);
                    p => p.Settings
                };

                if (request.IsActive.HasValue)
                {
                    dataBaseList = repository.Where(a => a.IsActive == request.IsActive.Value, request.FetchEagerly, plist);
                }
                else
                {
                    dataBaseList = repository.Where(g => g.Id != 0, request.FetchEagerly, plist);
                }
            }
            else
            {
                if (request.IsActive.HasValue)
                {
                    dataBaseList = _headerRepository.Where(g => g.IsActive == request.IsActive.Value, false).ConvertAllTo<JarsUser>().ToList(); ;
                }
                else
                {
                    dataBaseList = _headerRepository.Where(g => g.Id != 0, false).ConvertAllTo<JarsUser>().ToList(); ;

                }
            }

            ////add the api key if the current user is admin
            //IUserAuth usr = ssAuthRepo.GetUserAuthByUserName(CurrentSessionUsername);
            //if (usr != null && usr.Roles.Intersect(new[] { JarsRoles.Admin }).Any())
            //{
            //    var apiRepo = (IManageApiKeys)HostContext.TryResolve<IAuthRepository>();

            //    foreach (var u in dataBaseList)
            //    {
            //        IUserAuth ua = ssUsers.Find(ssu => ssu.UserName == u.UserName);
            //        if (ua != null && apiRepo != null)
            //        {
            //            var apiKeys = apiRepo.GetUserApiKeys(ua.RefIdStr);
            //            u.ApiKey = apiKeys.Find(k => k.KeyType == "publishable" && k.Environment == "live").Id;
            //        }
            //    }
            //}

            response.UserAccounts = dataBaseList.ConvertAllTo<JarsUserDto>().ToList();
            return response;
        }

        /// <summary>
        /// Save or update the list of records
        /// </summary>
        /// <param name="request">the request containing the entities</param>
        /// <returns></returns>
        public JarsUserResponse Any(StoreJarsUser request)
        {
            return ExecuteFaultHandledMethod(() =>
            {
                IAuthRepository ssAuthRepo = ServiceStackHost.Instance.GetAuthRepository();
                IUserAuth ssUser = ssAuthRepo.GetUserAuthByUserName(request.UserAccount.UserName);

                if (ssUser == null && request.UserAccount.Id != 0)
                    throw HttpError.NotFound(request.UserAccount.UserName);

                UserAuth newUserA = new UserAuth();
                if (request.UserAccount.Id == 0)
                {
                    newUserA.PopulateWith(request.UserAccount);
                    newUserA.LockedDate = DateTime.UtcNow;
                    newUserA.RecoveryToken = Guid.NewGuid().ToString("N");
                    ssUser = ssAuthRepo.CreateUserAuth(newUserA, "Password123");
                    //sendemailtouser
                }
                else
                {
                    newUserA.PopulateWith(ssUser);
                    newUserA.PopulateWith(request.UserAccount);
                    ssAuthRepo.UpdateUserAuth(ssUser, newUserA);
                }


                //so the user should be updated here..

                IJarsUserRepository repository = _DataRepositoryFactory.GetDataRepository<IJarsUserRepository>();
                JarsUserResponse response = new JarsUserResponse();
                if (request.UserAccount != null)
                {
                    JarsUser dbUser = new JarsUser();
                    if (request.UserAccount.Id != 0)
                        dbUser = repository.GetById(request.UserAccount.Id, true);
                    else
                        dbUser.IsActive = false;

                    dbUser.PopulateWith(ssUser);
                    dbUser.Id = request.UserAccount.Id;
                    dbUser.Settings = request.UserAccount.Settings.ConvertAllTo<JarsSetting>().ToList();

                    dbUser = repository.CreateUpdate(dbUser, CurrentSessionUsername);
                    response.UserAccount = dbUser.ConvertTo<JarsUserDto>();
                }
                //else
                //    response.UserAccounts = repository.CreateUpdateList(request.UserAccounts).ToList();
                return response;
            });
        }

        public void Any(DeleteJarsUser request)
        {

        }

        /// <summary>
        /// Send CRUD notifications for a JarsUserAccount Entity or Entities
        /// Note! :
        /// This Method is a special method used by the service when ServerEvents are being used.(serviceStack). 
        /// If the service does not implement serverEvents this will throw an error. 
        /// This should only send a notification to the user, not globally as the settings are only editable by the admin  account.
        /// </summary>
        /// <param name="crud">The notification request indicating a store or delete event that will be sent to other subscribers.</param>
        public virtual void Any(JarsUserNotification crud)
        {

            //!!!TODO!!!
            //ExecuteFaultHandledMethod(() =>
            //{
            //    //This notification should only be sent to the user, and not to the whole channel.
            //    //this only affects the user it belongs to and not a global group.
            //    IAuthRepository ssAuthRepo = ServiceStackHost.Instance.GetAuthRepository();
            //    IUserAuth ssUser = ssAuthRepo.GetUserAuthDetails(crud.(request.UserAccount.UserName);


            //    //check that the sender has subscribed to the service
            //    SubscriptionInfo subscriber = ServerEvents.GetSubscriptionInfosByUserId(crud.);
            //    //if (subscriber == null)
            //    //    throw HttpError.NotFound($"Subscriber {crud.FromUserName} does not exist.");

            //    //do some job updates here using the info from the the crud
            //    IJarsUserRepository _repository = _DataRepositoryFactory.GetDataRepository<IJarsUserRepository>();


            //    if (crud.Selector == SelectorTypes.store)
            //    {
            //        crud.Users = _repository.CreateUpdateList(crud.Users.ConvertAllTo<JarsUser>().ToList(), crud.FromUserName).ToList().ConvertAll(s => s.ConvertTo<JarsUserDto>());
            //        ServerEvents.NotifyChannel(typeof(JarsUser).Name, crud.Selector, crud.Users);//.ConvertToJarsSyncStoreEvent());
            //    }

            //    if (crud.Selector == SelectorTypes.delete)
            //    {
            //        _repository.DeleteList(crud.Users.ConvertAll(s => s.ConvertTo<JarsUser>()));
            //        IEnumerable<string> Ids = crud.Users.Select(u => u.Id.ToString());
            //        ServerEvents.NotifyChannel(typeof(JarsUser).Name, crud.Selector, Ids);
            //    }
            //});
        }

        public ResetJarsUserPasswordResponse Any(ResetJarsUserPassword request)
        {
            var res = new ResetJarsUserPasswordResponse();
            IAuthRepository ssAuthRepo = ServiceStackHost.Instance.GetAuthRepository();
            IUserAuth ssUser = ssAuthRepo.GetUserAuthByUserName(request.EmailOrUserName);
            if (ssUser == null)
                throw HttpError.NotFound("User not found");

            return res;
        }

        #region subclass loading
        //if (request.InRoles != null)
        //{
        //    //https://stackoverflow.com/questions/9593017/restrict-queryover-by-child-collection-using-nhibernate
        //    //we have to make the query on the child first then, get the values of the parents.
        //    //JarsUser userAccount = null;
        //    //var subQuery = QueryOver.Of<JarsUserRole>()
        //    //    .WhereRestrictionOn(g => g.Id).IsIn(request.InRoles)
        //    //    .JoinQueryOver(g => g.UserAccounts, () => userAccount)
        //    //    .Where(Restrictions.Ge(Projections.Count(() => userAccount.Id), request.InRoles.Length))
        //    //    .Select(Projections.Group(() => userAccount.Id));

        //    //var mainQuery = QueryOver.Of<JarsUser>().WithSubquery.WhereProperty(a => a.Id).In(subQuery);
        //    //var mainQuery = QueryOver.Of<JarsUser>().AndRestrictionOn(u => u.Roles).IsIn(request.InRoles);
        //    //response.UserAccounts = repository.Where(mainQuery).ToList();
        //    response.UserAccounts = repository.Where(r => request.InRoles.IsIn(r.Roles))
        //        .ConvertAllTo<JarsUserDto>().ToList();
        //}
        //}
        #endregion

    }
}
