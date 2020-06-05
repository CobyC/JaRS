using JARS.Core.Exceptions;
using JARS.Core.Interfaces.Security;
using JARS.Core.Security;
using System;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Extensions
{
    public static class WinFormsRolesAndOrPermissionsExtensions
    {
        private static DevExpress.Utils.ToolTipController _rapToolTip;
        internal static DevExpress.Utils.ToolTipController RapToolTip
        {
            get
            {
                if (_rapToolTip == null)
                {
                    _rapToolTip = new DevExpress.Utils.ToolTipController();
                    _rapToolTip.AllowHtmlText = true;
                    _rapToolTip.AutoPopDelay = 2000;
                    _rapToolTip.InitialDelay = 200;
                    _rapToolTip.KeepWhileHovered = true;
                }
                return _rapToolTip;
            }
        }

        /// <summary>
        /// This extension method traps the <see cref="UserRoleOrPermissionException"/> and provides UI feedback in a tooltip.
        /// It wraps the default <see cref="RolesAndPermissions.ExecuteAction(string[], string[],Action)"/> method.
        /// </summary>       
        /// <param name="roles">The roles to check for</param>
        /// <param name="permissions">The permissions to check for</param>
        /// <param name="action">The action to execute</param>
        public static void ExecuteActionUIOnException(this IRolesAndPermissions rap, string[] roles, string[] permissions, Action action)
        {
            try
            {
                rap.ExecuteAction(roles: roles, permissions: permissions, codeToExecute: action);
            }
            catch (UserRoleOrPermissionException rapEx)
            {
                RapToolTip.ShowHint($"<color=255,0,0>Not Allowed</br></color><color=55,55,55>{rapEx.Message}</color>", Cursor.Position);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This extension method traps the <see cref="UserRoleOrPermissionException"/> and provides UI feedback in a tooltip.
        /// It wraps the default <see cref="RolesAndPermissions.ExecuteFunction{T}(string[], string[],Func{T})"/> method.
        /// </summary> 
        /// <param name="roles">The roles to check for</param>
        /// <param name="permissions">The permissions to check for</param>
        /// <param name="function">The action to execute</param>
        public static T ExecuteFunctionUIOnException<T>(this IRolesAndPermissions rap, string[] roles, string[] permissions, Func<T> function)
        {
            try
            {
                return rap.ExecuteFunction(roles: roles, permissions: permissions, codeToExecute: function);
            }
            catch (UserRoleOrPermissionException rapEx)
            {
                RapToolTip.ShowHint($"<color=255,0,0>Not Allowed</br></color><color=55,55,55>{rapEx.Message}</color>", Cursor.Position);
                return default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
