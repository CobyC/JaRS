/// taken from the azureAS article
/// https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/token-cache-serialization
///
using Microsoft.Identity.Client;
using System;
using System.IO;
using System.Security.Cryptography;

namespace JARS.Core.Authentication
{
    /// <summary>
    /// this class is used for saving token cache on a client side application (desktop, console), for web applications and pages the strategy will be different and this class should not be used.
    /// This also relies on MSAL2.x 
    /// (This enables the cache to be saved to a file in the clients install directory)
    /// </summary>
    static class TokenCacheHelper
    {
        public static void EnableSerialization(ITokenCache tokenCache)
        {
            tokenCache.SetBeforeAccess(BeforeAccessNotification);
            tokenCache.SetAfterAccess(AfterAccessNotification);
        }

        /// <summary>
        /// Path to the token cache
        /// </summary>
        public static readonly string CacheFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location + ".msalcache.bin3";

        private static readonly object FileLock = new object();

        private static void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            lock (FileLock)
            {
                args.TokenCache.DeserializeMsalV3(File.Exists(CacheFilePath)
                        ? ProtectedData.Unprotect(File.ReadAllBytes(CacheFilePath), null, DataProtectionScope.CurrentUser) : null);
            }
        }

        private static void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
            if (args.HasStateChanged)
            {
                lock (FileLock)
                {
                    // reflect changes in the persistent store
                    File.WriteAllBytes(CacheFilePath, ProtectedData.Protect(args.TokenCache.SerializeMsalV3(), null, DataProtectionScope.CurrentUser));
                }
            }
        }

        internal static bool CacheFileExists()
        {
            return File.Exists(CacheFilePath);
        }
    }
}
