using DevExpress.XtraBars;
using DevExpress.XtraEditors.DXErrorProvider;
using JARS.Core.Authentication;
using JARS.Core.Client;
using JARS.Core.WinForms.Utils;
using JARS.Entities;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System.Linq;
using JARS.Core.Security;
using JARS.SS.DTOs;

namespace JARS.Core.WinForms.Forms
{
    public partial class LoginRegisterForm : DevExpress.XtraEditors.XtraForm
    {
        Authenticator _Authenticator { get; set; }
        public JarsUser JarsUserAccount { get; set; }

        int LoginAttempts = 0;
        //service stack client used for authorization       
        IAppSettings _AppSettings;
        AuthenticateResponse _AuthResponse = null;

        public LoginRegisterForm(IJsonServiceClient authClient)
        {
            //_SSEventClient = sSEventClient;
            //check auth here??
            InitializeComponent();
            tabLogin.PageVisible = false;
            tabRegister.PageVisible = false;
            tabAutoLogin.PageVisible = true;
            tabResetPassword.PageVisible = false;
            ctrl_LayoutUserEmail.ContentVisible = false;
            ctrl_LayoutPassword.ContentVisible = false;
            ctrl_LayoutSwithToResetPassword.ContentVisible = false;
            tabCtrlLoginRegister.SelectedTabPage = tabAutoLogin;
            tabCtrlLoginRegister.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            DialogResult = DialogResult.Abort;
            Text = "JaRS - Sign in";
            _AppSettings = new AppSettings();
            _Authenticator = new Authenticator(authClient);
            cbEditRememberLogin.Checked = _Authenticator.LastRememberMe;

            //add custom validators
            CustomEmailValidationRule emRule = new CustomEmailValidationRule
            {
                ErrorText = "please provide a valid email",
                ErrorType = ErrorType.Default

            };

            CustomPasswordValidationRule pwRule = new CustomPasswordValidationRule
            {
                ErrorText = "Please make sure the password meets the required criteria.",
                ErrorType = ErrorType.Default
            };

            dxRegisterValidationProvider.SetValidationRule(txtRegisterEmail, emRule);
            dxRegisterValidationProvider.SetValidationRule(txtRegisterPassword1, pwRule);
        }

        private async Task StartAuthentication()
        {
            if (!_Authenticator.LastProvider.IsNullOrEmpty())
                _AuthResponse = await AuthenticateUsingProvider(_Authenticator.LastProvider);

            if (_AuthResponse != null || LoginAttempts >= 3)
                Close();

            if (_AuthResponse == null && LoginAttempts < 3)
                ShowAuthenticationMethods();
        }

        private async Task<AuthenticateResponse> AuthenticateUsingProvider(string providerName)
        {
            //authenticate user..
            try
            {
                if (providerName == CredentialsAuthProvider.Name)
                    _AuthResponse = await _Authenticator.CredentialSignOn(txtEmailOrUsername.Text, txtPassword.Text, cbEditRememberLogin.Checked);

                //lots of work to be done here still
                if (providerName == "aad")
                {
                    if (_Authenticator.AzureCacheFileExists())
                    {
                        tabCtrlLoginRegister.SelectedTabPage = tabAutoLogin;
                        tabRegister.PageVisible = false;
                        tabLogin.PageVisible = false;
                        tabAutoLogin.PageVisible = true;
                        _AuthResponse = await _Authenticator.AzureSingleSignOn();
                        //await AuthenticateWithServiceStack();
                    }

                    _AuthResponse = await _Authenticator.AzureSingleSignOn();
                }

            }
            catch (AuthenticationException aex)
            {
                LoginAttempts++;
                MessageBox.Show(aex.Message, $"Failed Login - {LoginAttempts} of 3");
            }
            catch (WebServiceException wse)
            {
                LoginAttempts++;
                MessageBox.Show(wse.ErrorMessage, $"Failed Login - {LoginAttempts} of 3");
            }
            catch (Exception Ex)
            {
                LoginAttempts++;
                MessageBox.Show(Ex.Message, $"Failed Login - {LoginAttempts}");
            }
            if (LoginAttempts >= 3)
                _AuthResponse = new AuthenticateResponse() { BearerToken = null, ResponseStatus = new ResponseStatus("Not Authenticated", "All attempts for authentication failed") };

            return _AuthResponse;
        }

        public AuthenticateResponse LoginOrRegister()
        {
            ShowDialog();
            return _AuthResponse;
        }

        void ShowAuthenticationMethods()
        {
            // #Task 127 change the flow of the login screen.
            tabLogin.PageVisible = true;
            tabRegister.PageVisible = false;
            tabAutoLogin.PageVisible = false;
            tabResetPassword.PageVisible = false;
            tabCtrlLoginRegister.SelectedTabPage = tabLogin;
            tabCtrlLoginRegister.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            ImgComboBoxAuthProvider.Properties.Items.Clear();
            string[] authProviders = _AppSettings.Get<string>("Authproviders").Split(new[] { ',' });
            foreach (var aProv in authProviders)
            {
                string displayName = $"{aProv.ToTitleCase()} Authentication";
                if (aProv == "aad")
                {
                    displayName = "Azure Active Directory";
                }

                ImageComboBoxItem authItem = new ImageComboBoxItem()
                {
                    Description = displayName,
                    ImageIndex = imageListProviders.Images.Keys.IndexOf(aProv),
                    Value = aProv
                };
                ImgComboBoxAuthProvider.Properties.Items.Add(authItem);
            }
        }

        private async void LoginRegisterForm_Shown(object sender, EventArgs e)
        {
            if (_Authenticator.LastRememberMe)
                await StartAuthentication();
            else
                ShowAuthenticationMethods();

            if (_AuthResponse != null && _AuthResponse.BearerToken != null)
                Close();
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnLogin_Click(sender, e);
        }

        private void ImgComboBoxAuthProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ImgComboBoxAuthProvider.SelectedItem != null)
                if (((ImageComboBoxItem)ImgComboBoxAuthProvider.SelectedItem).Value.ToString() != CredentialsAuthProvider.Name)
                {
                    ctrl_LayoutUserEmail.ContentVisible = false;
                    ctrl_LayoutPassword.ContentVisible = false;
                    ctrl_LayoutSwithToResetPassword.ContentVisible = false;
                }
                else
                {
                    ctrl_LayoutUserEmail.ContentVisible = true;
                    ctrl_LayoutPassword.ContentVisible = true;
                    ctrl_LayoutSwithToResetPassword.ContentVisible = true;
                }
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            if (ImgComboBoxAuthProvider.SelectedItem is ImageComboBoxItem authItem)
            {
                bool canContinue = true;

                _Authenticator.LastProvider = authItem.Value.ToString();
                _Authenticator.LastRememberMe = cbEditRememberLogin.Checked;
                if (_Authenticator.LastProvider == CredentialsAuthProvider.Name)
                {
                    if (!dxLoginValidationProvider.Validate())
                        canContinue = false;
                }

                if (canContinue)
                    await StartAuthentication();
            }
        }

        private void BtnSwitchToRegister_Click(object sender, EventArgs e)
        {
            Text = "JaRS - Register";
            tabCtrlLoginRegister.SelectedTabPage = tabRegister;
            tabRegister.PageVisible = true;
            tabLogin.PageVisible = false;
            tabAutoLogin.PageVisible = false;
            tabResetPassword.PageVisible = false;
        }

        private void BtnSwitchToLogin_Click(object sender, EventArgs e)
        {
            Text = "JaRS - Sign in";
            tabCtrlLoginRegister.SelectedTabPage = tabLogin;
            tabRegister.PageVisible = false;
            tabLogin.PageVisible = true;
            tabResetPassword.PageVisible = false;
        }

        private void btnSwitchToResetPassword_Click(object sender, EventArgs e)
        {
            Text = "JaRS - Reset Password";
            tabCtrlLoginRegister.SelectedTabPage = tabRegister;
            tabRegister.PageVisible = false;
            tabLogin.PageVisible = false;
            tabAutoLogin.PageVisible = false;
            tabResetPassword.PageVisible = true;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            //!Regex.IsMatch(txtRegisterEmail.Text, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
            if (dxRegisterValidationProvider.Validate())
            {
                RegisterResponse regRes = GlobalContext.Instance.AuthClient.Post(new Register()
                {
                    FirstName = txtRegisterFirstName.Text,
                    LastName = txtRegisterLastName.Text,
                    DisplayName = $"{txtRegisterFirstName.Text} {txtRegisterLastName.Text }",
                    UserName = txtRegisterUserName.Text,
                    Email = txtRegisterEmail.Text,
                    Password = txtRegisterPassword1.Text,
                    ConfirmPassword = txtRegisterPassword2.Text,
                    AutoLogin = true,
                });

                //send of registration and move to login page
                tabCtrlLoginRegister.SelectedTabPage = tabLogin;
                tabRegister.PageVisible = false;
                tabLogin.PageVisible = true;
                //DialogResult = DialogResult.Yes;
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            ResetJarsUserPasswordResponse response = GlobalContext.Instance.AuthClient.Post(new ResetJarsUserPassword()
            {
                EmailOrUserName = txtResetEmail.Text,
                ResetToken = txtResetToken.Text,
                NewPassword = txtResetPasswordConfirm.Text
            });
            if (response.ResetSuccess)
            {
                MessageBox.Show("Password reset success, please log in with new password.");
                BtnSwitchToLogin_Click(null, null);
            }

        }

        private void txtRegisterFirstName_TextChanged(object sender, EventArgs e)
        {
            txtRegisterUserName.Text = $"{txtRegisterFirstName.Text}{txtRegisterLastName.Text}";
        }

        private void txtRegisterLastName_TextChanged(object sender, EventArgs e)
        {
            txtRegisterUserName.Text = $"{txtRegisterFirstName.Text}{txtRegisterLastName.Text}";
        }

        private void txtRegisterPassword2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnRegister_Click(sender, e);
        }


    }

}
