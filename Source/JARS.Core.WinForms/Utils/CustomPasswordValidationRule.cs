using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Utils
{
    public class CustomPasswordValidationRule : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            bool isValid = false;
            if (value == null)
                return isValid;

            string password = (string)value;
            try
            {
                isValid = Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{7,15}$", RegexOptions.None, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            { }

            return isValid;
        }
    }

}

/*
  Show();
            //first we try to use the azure authentication if we can, if not fall back on another method.
            //JaRS will make use of the Microsoft.Identity.Client package            
            AuthenticationResult authRes = await AuthenticateOrStateStateAsync();
            if (authRes.Account != null)
            {
                JarsUserAccount = _SSEventClient.ServiceClient.Get(new GetJarsUser() { UserName = authRes.Account.Username }).UserAccount;
            }
            else
            {
                isAuthenticated = TryAuthenticate();


    //go off and authenticate
            AuthenticateResponse response = AuthenticateUsingProvider(typeof(IwaAzAuthProvider));

            if (response.ResponseStatus != null && response.ResponseStatus.Errors == null)
            {
                JarsUserAccount = _SSEventClient.ServiceClient.Get(new GetJarsUser() { UserName = response.UserName }).UserAccount;
                return true;
            }
            else
                return false;
     */
