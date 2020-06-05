using JARS.Core;
using JARS.Core.Interfaces.Repositories;
using JARS.Data.NH.Jars.Interfaces;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.DirectoryServices.AccountManagement;

namespace JARS.SS.Auth
{
    public class CustomCredentialsAuthProvider : CredentialsAuthProvider
    {

        [Import]
        internal IDataRepositoryFactory _DataRepositoryFactory;

        public CustomCredentialsAuthProvider()
        {
            if (JarsCore.Container != null)
                JarsCore.Container.SatisfyImportsOnce(this);
        }

        public override bool TryAuthenticate(IServiceBase authService, string adUserName, string password)
        {

            //#if DEBUG
            return VerifyWithActiveDirecory(authService, adUserName, true);
            //#else
            //            return VerifyWithActiveDirecory(authService, adUserName, true);            
            //#endif


        }

        bool VerifyWithActiveDirecory(IServiceBase authService, string adUserName, bool isLive)
        {
            //for AD we will set up the user by compiling the relevant criteria.
            //we will only ever use the name part of AD so we need to add the domain and other details to do the AD query
            //we also need to check if a user with that name does exist in the database

            IJarsUserAccountRepository userRepo = _DataRepositoryFactory.GetDataRepository<IJarsUserAccountRepository>();

            //log that a request was made that failed for the user.. this might help with identifying attacks on the system.
            IErrorLogRepository errRepo = _DataRepositoryFactory.GetDataRepository<IErrorLogRepository>();
            string domain = Environment.UserDomainName;
            //string userName = $"{domain}\\{adUserName}";

            if (isLive)
            {
                JarsUserAccount acc = userRepo.GetByUserNameEagerly(adUserName);
                if (acc != null)
                {

                    //while in dev we will just say the current user is authorized
                    authService.Request.Items.Add("account", acc);

                    //the user was found, but now we need to make sure that the user exists in AD.
                    if (acc.IsActive.HasValue && !acc.IsActive.Value)
                    {
                        authService.Request.Items.Add("IsActive", acc.IsActive.Value);
                        return acc.IsActive.Value;
                    }
                    else
                    {

                        //now check if AD can be accessed
                        return true;//as this computer does not sit on an AD domain
                        try
                        {
                            using (var domainContext = new PrincipalContext(ContextType.Domain, domain))
                            {
                                using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, adUserName))
                                {
                                    //we could update the user settings here if they were assigned to another group or principal.
                                    authService.Request.Items.Add("account", acc);
                                    return foundUser != null;
                                }
                            }
                        }
                        catch (PrincipalServerDownException pex)
                        {
                            errRepo.CreateUpdate(new ErrorLog
                            {
                                EnvironmentUserName = Environment.UserName,
                                ErrorText = pex.Message,
                                ErrorTime = DateTime.Now,
                                ErrorType = "LoginFailed"
                            }, "CustomAuthProvider");
                            return false;
                        }
                        catch (Exception ex)
                        {
                            errRepo.CreateUpdate(new ErrorLog
                            {
                                EnvironmentUserName = Environment.UserName,
                                ErrorText = ex.Message,
                                ErrorTime = DateTime.Now,
                                ErrorType = "LoginFailed"
                            }, "CustomAuthProvider");
                            throw ex;
                        }
                    }
                }
                else
                {
                    JarsUserAccount nacc = new JarsUserAccount { AccountName = adUserName, IsActive = false, UserPermissions = "NONE" };// userRepo.GetByUserName(adUserName);                    
                    acc = userRepo.CreateUpdate(nacc, "AUTOREGISTER");
                    errRepo.CreateUpdate(new ErrorLog
                    {
                        EnvironmentUserName = Environment.UserName,
                        ErrorText = "Failed login attempt.",
                        ErrorTime = DateTime.Now,
                        ErrorType = "LoginFailed"
                    }, "CustomAuthProvider");

                    //!This needs changing, but for testing purposes this has been made so any new sign in will just create a user and continue
                    nacc.IsActive = true;
                    acc = userRepo.CreateUpdate(nacc, "AUTOREGISTER");
                    acc = userRepo.GetByUserNameEagerly(adUserName);
                    authService.Request.Items.Add("account", acc);

                    //the user was found, but now we need to make sure that the user exists in AD.
                    if (acc.IsActive.HasValue && !acc.IsActive.Value)
                    {
                        authService.Request.Items.Add("IsActive", acc.IsActive.Value);
                        return acc.IsActive.Value;
                    }
                    else
                        return false;//userName == "Dev" && password == "Pass";
                }
            }
            else
            {
                if ("TestAccount" == adUserName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override IHttpResult OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            //Fill IAuthSession with data you want to retrieve in the app eg:
            if (authService.Request.Items.ContainsKey("account"))
            {
                JarsUserAccount account = authService.Request.Items["account"] as JarsUserAccount;
                session.FirstName = account.AccountName;
                session.DisplayName = account.AccountName;
                session.UserName = account.AccountName;
                session.UserAuthId = account.Id.ToString();
                session.UserAuthName = account.AccountName;
                //if (authService.Request.Items.ContainsKey(""))
            }
            else
            {
                session.FirstName = "Dev";
                session.DisplayName = "TestAccount";
                session.UserName = "TestAccount";
                session.UserAuthId = 1.ToString();
            }

            //...

            //Call base method to Save Session and fire Auth/Session callbacks:
            return base.OnAuthenticated(authService, session, tokens, authInfo);

            //Alternatively avoid built-in behavior and explicitly save session with
            //authService.SaveSession(session, SessionExpiry);
            //return null;
        }
    }

}
