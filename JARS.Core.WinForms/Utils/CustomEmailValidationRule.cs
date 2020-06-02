using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Utils
{
    public class CustomEmailValidationRule : ValidationRule
    {
        public override bool Validate(Control control, object value)
        {
            bool isValid = false;
            if (value == null)
                return isValid;
            if (string.IsNullOrEmpty(value.ToString()))
                return isValid;

            string email = (string)value;
            try
            {
                isValid = Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            { }
            return isValid;
        }
    }

}
