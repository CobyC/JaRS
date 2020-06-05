using DevExpress.XtraEditors.Controls;
using JARS.Core.Security;
using JARS.Core.WinForms.Forms;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using JARS.Core.WinForms.Utils;
using DevExpress.XtraEditors.DXErrorProvider;
using JARS.Entities;

namespace JARS.Win.Plugins
{
    //[PrincipalPermission(SecurityAction.Demand, Role = JarsCoreSecurity.Administrators)]
    //[PrincipalPermission(SecurityAction.Demand, Role = JarsCoreSecurity.JarsAdministrators)]
    //[PrincipalPermission(SecurityAction.Demand, Role = JarsCoreSecurity.JarsPowerUsers)]
    public partial class JarsUsersForm : RibbonFormCrudBase
    {
        public JarsUsersForm()
        {
            InitializeComponent();
            CustomEmailValidationRule emRule = new CustomEmailValidationRule
            {
                ErrorText = "please provide a valid email",
                ErrorType = ErrorType.Default

            };
            dxValidator.SetValidationRule(ctrl_txteMail, emRule);
        }

        private void JarsUsersForm_Load(object sender, EventArgs e)
        {
            if (RolesAndOrPermissions.CheckMatchAny(new[] { JarsRoles.Admin, JarsRoles.PowerUser }, new[] { JarsPermissions.Full }))
            {
                SetGridControl(gridControlJarsUserAccounts);
                OnRefreshDataAsync();
                if (Context.LoggedInUser.Roles.Intersect(new[] { JarsRoles.Admin }).Any())
                    layoutControlAPIKey.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                else
                    layoutControlAPIKey.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                MessageBox.Show("You do not have permission to view this form.");
                Close();
            }
        }

        public override async void OnRefreshDataAsync()
        {
            base.OnRefreshDataAsync();
            JarsUsersResponse jarsUserResponse = await ServiceClient.GetAsync(new FindJarsUsers());
            defaultBindingSource.DataSource = jarsUserResponse.UserAccounts.ConvertAllTo<JarsUser>();
            UpdateLinkedBindingSources();
            FormEditState = FormEditStates.Browsing;
        }

        public override void OnAddData()
        {
            base.OnAddData();
            JarsUser newOp = defaultBindingSource.AddNew() as JarsUser;
            defaultBindingSource.Position = defaultBindingSource.IndexOf(newOp);
            ctr_txtUserName.Enabled = true;
        }

        public override void OnEditData()
        {
            base.OnEditData();
            //start code here
        }

        public override void OnSaveData()
        {
            if (dxValidator.Validate())
            {
                JarsUser saveObject = defaultBindingSource.Current as JarsUser;
                ApplyPermissionsToUser(saveObject);
                StoreJarsUser store = new StoreJarsUser()
                {
                    IsAppointment = false,
                    UserAccount = saveObject.ConvertTo<JarsUserDto>()
                };

                JarsUserResponse resp = ServiceClient.Post(store);
                saveObject = resp.UserAccount.ConvertTo<JarsUser>();
                base.OnSaveData();
                ctr_txtUserName.Enabled = false;
            }
        }

        public override void OnCancelData()
        {
            base.OnCancelData();
        }

        public override bool OnDeleteData()
        {
            //add code 
            if (base.OnDeleteData(true))
            {
                JarsUser delOp = defaultBindingSource.Current as JarsUser;

                DeleteJarsUser delUser = new DeleteJarsUser()
                {
                    Id = delOp.Id
                };
                ServiceClient.Delete(delUser);
                defaultBindingSource.RemoveCurrent();
                defaultBindingSource.ResetBindings(false);
            }

            //call this after the record removal was successful.
            return base.OnDeleteData();
        }

        public override void OnMessageEvent(ServiceStack.ServerEventMessage msg)
        {
            if (msg.Channel != typeof(JarsUser).Name)
                return;

            //var thread = new Thread(() =>
            //{
            //    switch (msg.Selector)
            //    {
            //        case SelectorTypes.delete:
            //            MessageBox.Show($"DELETE - Op:{msg.Op} Selector:{msg.Selector} Target:{msg.Target} Channel:{msg.Channel} EventId:{msg.EventId}");
            //            break;
            //        case SelectorTypes.store:
            //            MessageBox.Show($"SAVE - Op:{msg.Op} Selector:{msg.Selector} Target:{msg.Target} Channel:{msg.Channel} EventId:{msg.EventId}");
            //            break;
            //    }
            //});
            //thread.Start();
            base.OnMessageEvent(msg);
        }

        private void defaultBindingSource_PositionChanged(object sender, EventArgs e)
        {
            UpdateLinkedBindingSources();
        }

        private void UpdateLinkedBindingSources()
        {
            ctrl_cbListRoles.Items.Clear();
            ctrl_cbListPermissions.Items.Clear();

            //get the jars roles list
            List<string> AllRoles = typeof(JarsRoles).Fields().Where(p=> p.IsLiteral).Select(p => p.Name).ToList();
            //get the jars permissions list
            List<string> AllPermissions = typeof(JarsPermissions).Fields().Where(p => p.IsLiteral).Select(p => p.Name).ToList();

            if (defaultBindingSource.Current is JarsUser jUser)
            {
                foreach (var role in AllRoles)
                {
                    if (jUser.Roles.Contains(role))
                        ctrl_cbListRoles.Items.Add(role, true);
                    else
                        ctrl_cbListRoles.Items.Add(role, false);
                }

                foreach (var permission in AllPermissions)
                {
                    if (jUser.Permissions.Contains(permission))
                        ctrl_cbListPermissions.Items.Add(permission, true);
                    else
                        ctrl_cbListPermissions.Items.Add(permission, false);
                }
            }
        }
        void ApplyPermissionsToUser(JarsUser jarsUser)
        {
            foreach (CheckedListBoxItem item in ctrl_cbListRoles.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    if (!jarsUser.Roles.Contains($"{item.Value}"))
                        jarsUser.Roles.Add($"{item.Value}");
                }
                else
                {
                    if (jarsUser.Roles.Contains($"{item.Value}"))
                        jarsUser.Roles.Remove($"{item.Value}");
                }
            }

            foreach (CheckedListBoxItem item in ctrl_cbListPermissions.CheckedItems)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    if (!jarsUser.Permissions.Contains($"{item.Value}"))
                        jarsUser.Permissions.Add($"{item.Value}");
                }
                else
                {
                    if (jarsUser.Permissions.Contains($"{item.Value}"))
                        jarsUser.Permissions.Remove($"{item.Value}");
                }
            }
        }

        private void ctrl_txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if (FormEditState == FormEditStates.Adding)
            {
                if (defaultBindingSource.Current is JarsUser curU)
                {
                    curU.DisplayName = $"{ctrl_txtFirstName.Text} {ctrl_txtLastname.Text}";
                    curU.UserName = $"{ctrl_txtFirstName.Text.ToLower().Trim()}{ctrl_txtLastname.Text.ToLower().Trim()}";
                }
            }
        }

        private void ctrl_txtLastname_TextChanged(object sender, EventArgs e)
        {
            if (FormEditState == FormEditStates.Adding)
            {
                if (defaultBindingSource.Current is JarsUser curU)
                {
                    curU.DisplayName = $"{ctrl_txtFirstName.Text} {ctrl_txtLastname.Text}";
                    curU.UserName = $"{ctrl_txtFirstName.Text.ToLower().Trim()}{ctrl_txtLastname.Text.ToLower().Trim()}";
                }
            }
        }

        private void ctrl_txtFirstName_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
