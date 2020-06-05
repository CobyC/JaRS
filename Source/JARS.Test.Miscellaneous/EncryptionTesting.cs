using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JARS.Core;
using JARS.Core.Authentication;
using JARS.Core.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;

namespace JARS.Test.Miscellaneous
{
    [TestClass]
    public class EncryptionTesting
    {
        JarsUserClientSettings UserClientSettings { get; set; }
        string userSetPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\token.jrs";

        [TestMethod]
        public void TestMethod1()
        {
            //used this article as reference
            //https://stackoverflow.com/questions/12657792/how-to-securely-save-username-password-local

            LoadUserClientSettings();

            InnerCredentials credentials = new InnerCredentials() { Username = "name@test.com", Password = "password123" };
            EncodeUserCredentials(credentials);

            SaveUserClientSettings();

            credentials = DecodeUserCredentials();

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

                //utf8 breaks things as the check is done in bytes not UtfBytes
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


    }
}
