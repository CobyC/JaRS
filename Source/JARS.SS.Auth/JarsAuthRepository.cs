using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using JARS.Core;
using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Interfaces.Repositories;
using JARS.Data.NH.Jars.Interfaces;
using JARS.SS.Auth.Entities;
using NHibernate.Tool.hbm2ddl;
using ServiceStack;
using ServiceStack.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace JARS.SS.Auth
{
    public class JarsAuthRepository : IUserAuthRepository, IManageRoles, IManageApiKeys, IRequiresSchema, ICustomUserAuth//, IClearable//
    {
        [Import]
        internal IDataRepositoryFactory _DataRepositoryFactory;

        private const string MODIEFIED_BY = "JARSAUTH";

        public JarsAuthRepository()
        {
            if (JarsCore.Container != null)
                JarsCore.Container.SatisfyImportsOnce(this);
        }

        public JarsAuthRepository(IDataRepositoryFactory dataRepositoryFactory) : this()
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        IGenericEntityRepositoryBase<JarsUserAuthDetails, IDataContextNhJars> _UserAuthDetailsRepo;
        internal IGenericEntityRepositoryBase<JarsUserAuthDetails, IDataContextNhJars> UserAuthDetailsRepository
        {
            get
            {
                if (_UserAuthDetailsRepo == null)
                    _UserAuthDetailsRepo = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsUserAuthDetails, IDataContextNhJars>>();

                return _UserAuthDetailsRepo;
            }
        }

        IGenericEntityRepositoryBase<JarsUserAuth, IDataContextNhJars> _UserAuthRepo;
        internal IGenericEntityRepositoryBase<JarsUserAuth, IDataContextNhJars> UserAuthRepository
        {
            get
            {
                if (_UserAuthRepo == null)
                    _UserAuthRepo = _DataRepositoryFactory
                .GetDataRepository<IGenericEntityRepositoryBase<JarsUserAuth, IDataContextNhJars>>();

                return _UserAuthRepo;
            }
        }

        IGenericEntityRepositoryBase<JarsApiKey, IDataContextNhJars> _ApiKeyRepo;
        internal IGenericEntityRepositoryBase<JarsApiKey, IDataContextNhJars> ApiKeyRepository
        {
            get
            {
                if (_ApiKeyRepo == null)
                    _ApiKeyRepo = _DataRepositoryFactory
                .GetDataRepository<IGenericEntityRepositoryBase<JarsApiKey, IDataContextNhJars>>();

                return _ApiKeyRepo;
            }
        }

        public bool hasInitSchema;

        public bool ForceCaseInsensitiveUserNameSearch { get; set; } = true;

        #region IUserAuthRepository

        public IUserAuth CreateUserAuth(IUserAuth newUser, string password)
        {
            newUser.ValidateNewUser(password);

            AssertNoExistingUser(newUser);

            newUser.PopulatePasswordHashes(password);
            newUser.CreatedDate = DateTime.UtcNow;
            newUser.ModifiedDate = newUser.CreatedDate;

            newUser = UserAuthRepository.CreateUpdate(new JarsUserAuth(newUser), MODIEFIED_BY);

            return newUser;
        }

        private void AssertNoExistingUser(IUserAuth newUser, IUserAuth exceptForExistingUser = null)
        {
            if (newUser.UserName != null)
            {
                var existingUser = GetUserAuthByUserName(newUser.UserName);
                if (existingUser != null
                    && (exceptForExistingUser == null || existingUser.Id != exceptForExistingUser.Id))
                    throw new ArgumentException(string.Format(ErrorMessages.UserAlreadyExistsTemplate1, newUser.UserName.SafeInput()));
            }

            if (newUser.Email != null)
            {
                var existingUser = GetUserAuthByUserName(newUser.Email);
                if (existingUser != null
                    && (exceptForExistingUser == null || existingUser.Id != exceptForExistingUser.Id))
                    throw new ArgumentException(string.Format(ErrorMessages.EmailAlreadyExistsTemplate1, newUser.Email.SafeInput()));
            }
        }

        public IUserAuth UpdateUserAuth(IUserAuth existingUser, IUserAuth newUser, string password)
        {
            newUser.ValidateNewUser(password);

            AssertNoExistingUser(newUser, existingUser);

            newUser.Id = existingUser.Id;
            newUser.PopulatePasswordHashes(password, existingUser);
            newUser.CreatedDate = existingUser.CreatedDate;
            newUser.ModifiedDate = DateTime.UtcNow;

            newUser = UserAuthRepository.CreateUpdate(new JarsUserAuth(newUser), MODIEFIED_BY);

            return newUser;
        }

        public IUserAuth UpdateUserAuth(IUserAuth existingUser, IUserAuth newUser)
        {
            newUser.ValidateNewUser();

            AssertNoExistingUser(newUser, existingUser);

            newUser.Id = existingUser.Id;
            newUser.PasswordHash = existingUser.PasswordHash;
            newUser.Salt = existingUser.Salt;
            newUser.CreatedDate = existingUser.CreatedDate;
            newUser.ModifiedDate = DateTime.UtcNow;

            newUser = UserAuthRepository.CreateUpdate(new JarsUserAuth(newUser), MODIEFIED_BY);

            return newUser;
        }

        public IUserAuth GetUserAuthByUserName(string userNameOrEmail)
        {
            if (!hasInitSchema)
                InitSchema();

            if (userNameOrEmail == null)
                return null;

            bool isEmail = userNameOrEmail.Contains("@");
            string lowerUserName = userNameOrEmail.ToLower();

            if (HostContext.GetPlugin<AuthFeature>()?.SaveUserNamesInLowerCase == true)
            {
                return isEmail
                    ? UserAuthRepository.Where(u => u.Email == lowerUserName).FirstOrDefault()
                    : UserAuthRepository.Where(u => u.UserName == lowerUserName).FirstOrDefault();
            }

            // Try an exact search using index first
            JarsUserAuth userAuth = isEmail
                 ? UserAuthRepository.Where(u => u.Email == userNameOrEmail).FirstOrDefault()
                 : UserAuthRepository.Where(u => u.UserName == userNameOrEmail).FirstOrDefault();

            if (userAuth != null)
                return userAuth;

            // Fallback to a non-index search if no exact match is found
            if (ForceCaseInsensitiveUserNameSearch)
            {
                userAuth = isEmail
                    ? UserAuthRepository.Where(u => u.Email == lowerUserName).FirstOrDefault()
                    : UserAuthRepository.Where(u => u.UserName == lowerUserName).FirstOrDefault();
            }

            return userAuth;
        }

        public bool TryAuthenticate(string userName, string password, out IUserAuth userAuth)
        {
            userAuth = GetUserAuthByUserName(userName);
            if (userAuth == null)
                return false;

            if (userAuth.VerifyPassword(password, out var needsRehash))
            {
                this.RecordSuccessfulLogin(userAuth, needsRehash, password);
                return true;
            }

            this.RecordInvalidLoginAttempt(userAuth);

            userAuth = null;
            return false;
        }

        public bool TryAuthenticate(Dictionary<string, string> digestHeaders, string privateKey, int nonceTimeOut, string sequence, out IUserAuth userAuth)
        {
            userAuth = GetUserAuthByUserName(digestHeaders["username"]);
            if (userAuth == null)
                return false;

            if (userAuth.VerifyDigestAuth(digestHeaders, privateKey, nonceTimeOut, sequence))
            {
                this.RecordSuccessfulLogin(userAuth);
                return true;
            }

            this.RecordInvalidLoginAttempt(userAuth);

            userAuth = null;
            return false;
        }

        public void DeleteUserAuth(string userAuthId)
        {
            int userId = int.Parse(userAuthId);
            UserAuthRepository.Delete(userId);
            UserAuthDetailsRepository.Delete(UserAuthDetailsRepository.Where(u => u.UserAuthId == userId).SingleOrDefault());
        }

        public void LoadUserAuth(IAuthSession session, IAuthTokens tokens)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            var userAuth = GetUserAuth(session, tokens);
            LoadUserAuth(session, userAuth);
        }

        private void LoadUserAuth(IAuthSession session, IUserAuth userAuth)
        {
            session.PopulateSession(userAuth);//, GetUserAuthDetails(session.UserAuthId).ConvertAll(x => (IAuthTokens)x));
        }

        public IUserAuth GetUserAuth(string userAuthId)
        {
            int _userAuthId = int.Parse(userAuthId);
            return UserAuthRepository.GetById(_userAuthId);
        }

        public void SaveUserAuth(IAuthSession authSession)
        {
            if (authSession == null)
                throw new ArgumentNullException(nameof(authSession));

            var userAuth = !authSession.UserAuthId.IsNullOrEmpty()
                ? UserAuthRepository.GetById(int.Parse(authSession.UserAuthId))
                : authSession.ConvertTo<JarsUserAuth>();

            if (userAuth.Id == default(int) && !authSession.UserAuthId.IsNullOrEmpty())
                userAuth.Id = int.Parse(authSession.UserAuthId);

            userAuth.ModifiedDate = DateTime.UtcNow;
            if (userAuth.CreatedDate == default(DateTime))
                userAuth.CreatedDate = userAuth.ModifiedDate;

            UserAuthRepository.CreateUpdate(userAuth, MODIEFIED_BY);
        }

        public void SaveUserAuth(IUserAuth userAuth)
        {
            userAuth.ModifiedDate = DateTime.UtcNow;
            if (userAuth.CreatedDate == default(DateTime))
                userAuth.CreatedDate = userAuth.ModifiedDate;

            userAuth = UserAuthRepository.CreateUpdate(userAuth as JarsUserAuth, MODIEFIED_BY);
        }

        public List<IUserAuthDetails> GetUserAuthDetails(string userAuthId)
        {

            int id = int.Parse(userAuthId);
            var value = UserAuthDetailsRepository.Where(u => u.UserAuthId == id)
                .OrderBy(u => u.ModifiedDate);

            return value.Cast<IUserAuthDetails>().ToList();
        }

        public IUserAuth GetUserAuth(IAuthSession authSession, IAuthTokens tokens)
        {
            if (!string.IsNullOrEmpty(authSession.UserAuthId))
            {
                var userAuth = GetUserAuth(authSession.UserAuthId);
                if (userAuth != null) return userAuth;
            }

            if (!string.IsNullOrEmpty(authSession.UserAuthName))
            {
                var userAuth = GetUserAuthByUserName(authSession.UserAuthName);
                if (userAuth != null) return userAuth;
            }

            if (tokens == null || string.IsNullOrEmpty(tokens.Provider) || string.IsNullOrEmpty(tokens.UserId))
                return null;

            var oAuthProvider = UserAuthDetailsRepository.Where(u => u.Provider == tokens.Provider && u.UserId == tokens.UserId)
                .FirstOrDefault();

            if (oAuthProvider != null)
            {
                return UserAuthRepository.GetById(oAuthProvider.UserAuthId);
            }
            return null;
        }

        public IUserAuthDetails CreateOrMergeAuthSession(IAuthSession authSession, IAuthTokens tokens)
        {
            var userAuth = GetUserAuth(authSession, tokens) ?? new JarsUserAuth();

            var authDetails = UserAuthDetailsRepository.Where(u => u.Provider == tokens.Provider && u.UserId == tokens.UserId)
                .FirstOrDefault();

            if (authDetails == null)
            {
                authDetails = new JarsUserAuthDetails
                {
                    Provider = tokens.Provider,
                    UserId = tokens.UserId,
                };
            }

            authDetails.PopulateMissing(tokens, overwriteReserved: true);
            userAuth.PopulateMissingExtended(authDetails);

            userAuth.ModifiedDate = DateTime.UtcNow;
            if (userAuth.CreatedDate == default(DateTime))
                userAuth.CreatedDate = userAuth.ModifiedDate;

            userAuth = UserAuthRepository.CreateUpdate(userAuth as JarsUserAuth, MODIEFIED_BY);

            authDetails.UserAuthId = userAuth.Id;

            authDetails.ModifiedDate = userAuth.ModifiedDate;
            if (authDetails.CreatedDate == default(DateTime))
                authDetails.CreatedDate = userAuth.ModifiedDate;

            authDetails = UserAuthDetailsRepository.CreateUpdate(authDetails, MODIEFIED_BY);

            return authDetails;
        }

        #endregion

        #region IMangageRoles


        public ICollection<string> GetRoles(string userAuthId)
        {
            var userAuth = GetUserAuth(userAuthId);
            if (userAuth == null)
                return TypeConstants.EmptyStringArray;

            return userAuth.Roles;
        }

        public ICollection<string> GetPermissions(string userAuthId)
        {
            var userAuth = GetUserAuth(userAuthId);
            if (userAuth == null)
                return TypeConstants.EmptyStringArray;

            return userAuth.Permissions;
        }

        public bool HasRole(string userAuthId, string role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            if (userAuthId == null)
                return false;

            var userAuth = GetUserAuth(userAuthId);
            return userAuth.Roles != null && userAuth.Roles.Contains(role);
        }

        public bool HasPermission(string userAuthId, string permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            if (userAuthId == null)
                return false;

            var userAuth = GetUserAuth(userAuthId);
            return userAuth.Permissions != null && userAuth.Permissions.Contains(permission);
        }

        public void AssignRoles(string userAuthId, ICollection<string> roles = null, ICollection<string> permissions = null)
        {
            var userAuth = GetUserAuth(userAuthId);

            if (!roles.IsEmpty())
            {
                foreach (var missingRole in roles.Where(x => userAuth.Roles == null || !userAuth.Roles.Contains(x)))
                {
                    if (userAuth.Roles == null)
                        userAuth.Roles = new List<string>();

                    userAuth.Roles.Add(missingRole);
                }
            }

            if (!permissions.IsEmpty())
            {
                foreach (var missingPermission in permissions.Where(x => userAuth.Permissions == null || !userAuth.Permissions.Contains(x)))
                {
                    if (userAuth.Permissions == null)
                        userAuth.Permissions = new List<string>();

                    userAuth.Permissions.Add(missingPermission);
                }
            }
            SaveUserAuth(userAuth);
        }

        public void UnAssignRoles(string userAuthId, ICollection<string> roles = null, ICollection<string> permissions = null)
        {
            var userAuth = GetUserAuth(userAuthId);
            roles.Each(x => userAuth.Roles.Remove(x));
            permissions.Each(x => userAuth.Permissions.Remove(x));

            if (roles != null || permissions != null)
            {
                SaveUserAuth(userAuth);
            }
        }
        #endregion

        #region IManageApiKeys

        public void InitApiKeySchema()
        {
            //throw new NotImplementedException();
            InitSchema();
        }

        public bool ApiKeyExists(string apiKey)
        {
            JarsApiKey key = ApiKeyRepository.GetById(apiKey);
            return key != null;
        }

        public ApiKey GetApiKey(string apiKey)
        {
            JarsApiKey key = ApiKeyRepository.GetById(apiKey);
            return key.ConvertTo<ApiKey>();
        }

        public List<ApiKey> GetUserApiKeys(string userId)
        {
            List<JarsApiKey> keys = ApiKeyRepository.Where(k => k.UserAuthId == userId
                                    && k.CancelledDate == null
                                    && (k.ExpiryDate == null || k.ExpiryDate >= DateTime.UtcNow))
                                    .OrderByDescending(k => k.CreatedDate).ToList();
            return keys.ConvertAll(k => k.ConvertTo<ApiKey>());
        }

        public void StoreAll(IEnumerable<ApiKey> apiKeys)
        {
            List<JarsApiKey> jarsKeys = new List<JarsApiKey>();
            foreach (var key in apiKeys)
                jarsKeys.Add(key.ConvertTo<JarsApiKey>());

            ApiKeyRepository.CreateUpdateList(jarsKeys, "AUTH");
        }

        #endregion
        public void InitSchema()
        {
            if (hasInitSchema)
                return;
            
            var currentConfig = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(JarsCore.JarsConnectionString).ShowSql)
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<JarsUserAuth>())
            .BuildConfiguration();
            try
            {
                SchemaUpdate upd = new SchemaUpdate(currentConfig);
                upd.Execute(true, true);
                hasInitSchema = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        #region ICustomUserAuth

        public IUserAuth CreateUserAuth()
        {
            return new JarsUserAuth();
        }

        public IUserAuthDetails CreateUserAuthDetails()
        {
            return new JarsUserAuthDetails();
        }

        public void GetRolesAndPermissions(string userAuthId, out ICollection<string> roles, out ICollection<string> permissions)
        {
            //throw new NotImplementedException();
            roles = new[] { "Administrator" };
            permissions = new[] { "Administrator" };
        }

        #endregion
    }
}
