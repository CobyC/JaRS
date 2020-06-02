using Microsoft.Identity.Client;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Authentication
{
    public class Authenticator
    {
        IPublicClientApplication app = null;
        IAccount account = null;
        IJsonServiceClient AuthClient { get; set; }
        JarsUserClientSettings UserClientSettings { get; set; }
        string userSetPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\client_settings.jrs";

        public string LastProvider
        {
            get
            {
                if (UserClientSettings == null)
                    LoadUserClientSettings();
                return UserClientSettings.LastProvider;
            }
            set
            {
                if (UserClientSettings == null)
                    LoadUserClientSettings();
                UserClientSettings.LastProvider = value;
                SaveUserClientSettings();
            }
        }

        public bool LastRememberMe
        {
            get
            {
                if (UserClientSettings == null)
                    LoadUserClientSettings();
                return UserClientSettings.LastRememberMe;
            }
            set
            {
                if (UserClientSettings == null)
                    LoadUserClientSettings();
                UserClientSettings.LastRememberMe = value;
                SaveUserClientSettings();
            }
        }

        /// <summary>
        /// Encodes the Credentials passes through the authenticator.
        /// </summary>
        /// <param name="credentials"></param>
        private void EncodeUserCredentials(InnerCredentials credentials)
        {
            if (UserClientSettings == null)
                LoadUserClientSettings();

            // Generate additional entropy (will be used as the Initialization vector)                       
            byte[] entropy = new byte[24];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            { rng.GetBytes(entropy); }

            //set the settings salt value            
            UserClientSettings.Salt = Encoding.Default.GetString(entropy);

            // Data to protect. Convert a to string and encrypt.
            string credJson = credentials.ToSafeJsv();
            byte[] credBytes = Encoding.Default.GetBytes(credJson);

            //encrypt
            byte[] cipherCred = ProtectedData.Protect(credBytes, entropy, DataProtectionScope.CurrentUser);

            //string the cipher to save in file
            UserClientSettings.Credentials = Encoding.Default.GetString(cipherCred);

            SaveUserClientSettings();

            //clear the bytes so they are empty in memory
            Array.Clear(entropy, 0, entropy.Length);
            entropy = null;
            Array.Clear(credBytes, 0, credBytes.Length);
            credBytes = null;
            Array.Clear(cipherCred, 0, cipherCred.Length);
            cipherCred = null;
        }

        /// <summary>
        /// Decrypts the credentials saved in the settings of the client user.
        /// </summary>
        /// <returns>it returns the decrypted credentials if they were found</returns>
        private InnerCredentials DecodeUserCredentials()
        {
            InnerCredentials resCredentials = new InnerCredentials();
            //read the file as bytes
            try
            {
                if (UserClientSettings == null || UserClientSettings.Salt.IsNullOrEmpty())
                    return resCredentials;

                byte[] entropy = Encoding.Default.GetBytes(UserClientSettings.Salt);

                //utf8 breaks things as the check is done in bytes not utf bytes
                byte[] ciphBytes = Encoding.Default.GetBytes(UserClientSettings.Credentials);//.ToUtf8Bytes(); 

                byte[] credBytes = ProtectedData.Unprotect(ciphBytes, entropy, DataProtectionScope.CurrentUser);

                string credString = Encoding.Default.GetString(credBytes);//.FromUtf8Bytes();    
                resCredentials = credString.FromJsv<InnerCredentials>();

                //clear the bytes so they are empty in memory
                Array.Clear(entropy, 0, entropy.Length);
                entropy = null;
                Array.Clear(credBytes, 0, credBytes.Length);
                credBytes = null;
                Array.Clear(ciphBytes, 0, ciphBytes.Length);
                ciphBytes = null;

                return resCredentials;
            }
            catch (Exception ex)
            {
                Logger.Error("Login credentials not valid.", ex);
                return new InnerCredentials();
            }
        }

        public Authenticator(IJsonServiceClient authClient)
        {
            AuthClient = authClient;
            LoadUserClientSettings();
        }

        private void LoadUserClientSettings()
        {
            if (File.Exists(userSetPath))
            {
                byte[] fileBytes = File.ReadAllBytes(userSetPath);
                //convert bytes to string 
                string jSonfile = Encoding.Default.GetString(fileBytes);
                //serialize text to object
                UserClientSettings = jSonfile.FromJsv<JarsUserClientSettings>();

                //clear the bytes.
                Array.Clear(fileBytes, 0, fileBytes.Length);
                fileBytes = null;
            }
            else
            {
                UserClientSettings = new JarsUserClientSettings();
                SaveUserClientSettings();
            }
        }

        private void SaveUserClientSettings()
        {
            //encode the file 
            byte[] fileBytes = Encoding.Default.GetBytes(UserClientSettings.ToSafeJsv());
            //save the file as byte..
            File.WriteAllBytes(userSetPath, fileBytes);
            //clear array
            Array.Clear(fileBytes, 0, fileBytes.Length);
            fileBytes = null;
        }

        public async Task<AuthenticateResponse> AzureSingleSignOn()
        {
            string clientId = ConfigurationManager.AppSettings["aad:clientId"];
            string tenantId = ConfigurationManager.AppSettings["aad:tenantId"];
            string aadInstance = ConfigurationManager.AppSettings["aad:aadInstance"];
            string[] scopes = ConfigurationManager.AppSettings["aad:scopes"].Split(new[] { ',' }); //"user.read"

            string authority = String.Format(CultureInfo.InvariantCulture, "{0}{1}", aadInstance, tenantId);
            app = PublicClientApplicationBuilder
                    .Create(clientId)
                    .WithDefaultRedirectUri()
                    .WithAuthority(authority)
                    .Build();
            //this enables the cache to be saved to a file in the clients install directory
            TokenCacheHelper.EnableSerialization(app.UserTokenCache);
            //AuthenticationResult authenticationResult = new AuthenticationResult("00", false, "00", DateTimeOffset.MinValue, DateTimeOffset.MinValue, "00", null, "", new[] { "" }, Guid.Empty);
            AuthenticateResponse response = null;
            AuthenticationResult authRes = null;
            try
            {
                var accounts = await app.GetAccountsAsync();
                if (accounts.Any())
                {
                    account = accounts.FirstOrDefault();
                    authRes = await app.AcquireTokenSilent(scopes, account).ExecuteAsync();
                }
                else
                {
                    authRes = await app.AcquireTokenSilent(scopes, System.Security.Principal.WindowsIdentity.GetCurrent().Name).ExecuteAsync();
                }

                if (authRes.Account != null)
                {
                    response = SetAuthenticationResponse(authRes);
                    response.Meta = new System.Collections.Generic.Dictionary<string, string>();
                    response.Meta.Add("token", authRes.IdToken);
                    response.Meta.Add("secret", authRes.AccessToken);
                    return response;
                }
            }
            catch (MsalUiRequiredException ax)
            {
                try
                {
                    if (ax.ErrorCode == MsalError.FailedToAcquireTokenSilentlyFromBroker || ax.ErrorCode == MsalError.NoAccountForLoginHint || ax.ErrorCode == MsalError.CodeExpired)
                    {
                        authRes = await app.AcquireTokenInteractive(scopes).ExecuteAsync();
                        response = SetAuthenticationResponse(authRes);
                        return response;
                    }
                    else
                    { }
                }
                catch (MsalClientException icx)
                {
                    Logger.Info("Login:", icx);
                    response = SetAuthenticationResponse(authRes);
                    return response;
                }
                catch (MsalServiceException imsx)
                {
                    response = SetAuthenticationResponse(authRes);
                    if (imsx.ErrorCode == MsalError.AccessDenied || imsx.ErrorCode == MsalError.CodeExpired)
                    {
                        response.ResponseStatus = new ResponseStatus(imsx.ErrorCode, imsx.Message);
                    }
                    return response;
                }
            }
            catch (MsalClientException cx)
            {
                Logger.Info("Login:", cx);
                response = SetAuthenticationResponse(authRes);
                response.ResponseStatus = new ResponseStatus(cx.ErrorCode, cx.Message);
                return response;
            }
            catch (MsalServiceException msalEx)
            {
                response = SetAuthenticationResponse(authRes);
                if (msalEx.ErrorCode == MsalError.AccessDenied || msalEx.ErrorCode == MsalError.CodeExpired)
                {
                    response.ResponseStatus = new ResponseStatus(msalEx.ErrorCode, msalEx.Message);
                }
                return response;
            }
            catch (Exception ex)
            {
                Logger.Info("Login:", ex);
                if (account != null)
                    await app.RemoveAsync(account);
            }
            return null;
        }

        private AuthenticateResponse SetAuthenticationResponse(AuthenticationResult authRes)
        {
            AuthenticateResponse response;

            if (authRes == null)
            {
                response = new AuthenticateResponse()
                {
                    BearerToken = null,
                    RefreshToken = null,
                };
            }
            else
            {
                response = new AuthenticateResponse()
                {
                    BearerToken = authRes.IdToken,
                    RefreshToken = authRes.AccessToken,
                };
                if (authRes.Account != null)
                {
                    response.DisplayName = authRes.Account.Username;
                    response.UserName = authRes.Account.Username;
                }
            }
            return response;
        }
        /// <summary>
        /// This removes the user token from the cache, so if you need to keep the cache dont call this method.
        /// </summary>
        public async void LogOutAzureAsync()
        {
            AuthenticationResult authenticationResult = new AuthenticationResult("00", false, "00", DateTimeOffset.MinValue, DateTimeOffset.MinValue, "00", null, "", new[] { "" }, Guid.Empty);
            if (authenticationResult.Account != null)
                Logger.Info($"{authenticationResult.Account.Username} logged out at {DateTime.Now}");
            else
                Logger.Info($"User logged out.");

            await app.RemoveAsync(account);
        }

        public bool AzureCacheFileExists()
        {
            return TokenCacheHelper.CacheFileExists();
        }


        public async Task<AuthenticateResponse> CredentialSignOn(string UserName, string password, bool rememberLogin = true)
        {
            InnerCredentials credentials = new InnerCredentials() { Username = UserName, Password = password };
            if (!credentials.IsValid)
                credentials = DecodeUserCredentials();

            AuthenticateResponse authRes = null;
            if (credentials.IsValid)
                authRes = await AuthClient.PostAsync(new Authenticate { provider = "credentials", UserName = credentials.Username, Password = credentials.Password, RememberMe = rememberLogin });

            if (authRes != null)
            {
                EncodeUserCredentials(credentials);
            }
            return authRes;
        }

        public async Task<AuthenticateResponse> TryAuthenticateWithLastSavedProvider()
        {
            if (LastProvider == "credentials")
                return await CredentialSignOn("", "");
            if (LastProvider == "aad")
                return await AzureSingleSignOn();

            //else just return null
            return null;
        }

        public void ForgetUser()
        {
            UserClientSettings = new JarsUserClientSettings() { LastProvider = "", Credentials = "", Salt = "" };
            SaveUserClientSettings();
        }
    }

    class InnerCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid
        {
            get => !Username.IsNullOrEmpty() && !Password.IsNullOrEmpty();
        }
    }
}
