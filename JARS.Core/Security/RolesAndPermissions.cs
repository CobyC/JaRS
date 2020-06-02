using JARS.Core.Exceptions;
using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Security;
using System;
using System.Linq;


namespace JARS.Core.Security
{
    public class RolesAndPermissions : IRolesAndPermissions
    {

        public RolesAndPermissions(IJarsUserBase user)
        {
            User = user;
            //Initialize();
        }
        //protected GlobalContext Context { get { return GlobalContext.Instance; } }

        protected void SetUser(IJarsUserBase user)
        {
            User = user;
        }

        public IJarsUserBase User { get; private set; }

        /// <summary>
        /// Checks the current user roles, if any of the roles are found the check is passed as true (non strict).                
        /// </summary>        
        /// <param name="roles">a string array of roles to check, if null is passed in the role check will fail (false)</param>        
        /// <returns>true if role is found, false if not.</returns>
        public bool CheckMatchAnyRole(string[] roles)
        {
            try
            {
                if (User == null)
                    throw new Exception("No user present, please make sure that the user is assigned.");

                if (roles != null)
                {
                    if (!User.Roles.Intersect(roles).Any())
                        return false;
                    else
                        return true;
                }
                else { return false; }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Checks the current user permissions, if any of the permissions are found the check is passed as true (non strict).                
        /// </summary>                
        /// <param name="permissions">a string array of permission to check, if null is passed in the permission check will be ignored (passed)</param>        
        /// <returns>true if permission is found, false if not.</returns>
        public bool CheckMatchAnyPermission(string[] permissions = null)
        {
            try
            {
                if (User == null)
                    throw new Exception("No user present, please make sure that the user is assigned.");

                if (permissions != null)
                    if (!User.Permissions.Intersect(permissions).Any())
                        return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Checks the current user roles and / or permissions. , if any of the roles or permissions are found the check is passed as true (non strict).                
        /// </summary>        
        /// <param name="roles">a string array of roles to check, if null is passed in the role check will be ignored (passed)</param>
        /// <param name="permissions">a string array of permission to check, if null is passed in the permission check will be ignored (passed)</param>        
        /// <returns>true if role or permission is found, false if not.</returns>
        public bool CheckMatchAny(string[] roles = null, string[] permissions = null)
        {
            try
            {
                if (User == null)
                    throw new Exception("No user present, please make sure that the user is assigned.");

                if (roles != null)
                    return CheckMatchAnyRole(roles);

                if (permissions != null)
                    return CheckMatchAnyPermission(permissions);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Checks the current user roles. All of the roles must be matched for the check to passed as true (strict).                
        /// </summary>        
        /// <param name="roles">a string array of roles to check, if null is passed in the result will be false</param>        
        /// <returns>true if all roles are found, false if not.</returns>
        public bool CheckMatchAllRoles(string[] roles)
        {
            try
            {
                if (User == null)
                    throw new Exception("No user present, please make sure that the user is assigned.");

                if (roles != null)
                {
                    var intersect_R = User.Roles.Intersect(roles);
                    if (roles.Except(intersect_R).Any())//we want/expect nothing here
                        return false;
                    else
                        return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Checks the current user permissions. All of the permissions must be matched for the check to passed as true (strict).                
        /// </summary>        
        /// <param name="permissions">a string array of permissions to check, if null is passed in the result will be false</param>        
        /// <returns>true if all permissions are found, false if not.</returns>
        public bool CheckMatchAllPermissions(string[] permissions)
        {
            try
            {
                if (User == null)
                    throw new Exception("No user present, please make sure that the user is assigned.");

                if (permissions != null)
                {
                    var intersect_P = User.Permissions.Intersect(permissions);
                    if (permissions.Except(intersect_P).Any())//we want/expect nothing here
                        return false;
                    else
                        return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Checks the current user roles and / or permissions. All of the roles or permissions must be matched for the check to passed as true (semi-strict).                
        /// </summary>        
        /// <param name="roles">a string array of roles to check, if null is passed in the role check will be ignored (passed)</param>
        /// <param name="permissions">a string array of permission to check, if null is passed in the permission check will be ignored (passed)</param>        
        /// <returns>true if role or permission is found, false if not.</returns>
        public bool CheckMatchAll(string[] roles = null, string[] permissions = null)
        {
            try
            {
                if (User == null)
                    throw new Exception("No user present, please make sure that the user is assigned.");

                if (roles != null)
                    return CheckMatchAllRoles(roles);

                if (permissions != null)
                    return CheckMatchAllPermissions(permissions);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // <summary>
        /// Checks the current user roles and permissions strictly, all roles and permissions must be present and matched as required, values can not be null. 
        /// </summary>        
        /// <param name="roles">a string array of roles to check, if null is passed the roles check fails</param>
        /// <param name="permissions">a string array of permission to check, if null is passed in the permission the check will fail</param>        
        /// <returns>true if roles and permissions are found, false if not.</returns>
        public bool CheckStrict(string[] roles, string[] permissions)
        {
            try
            {
                if (User == null)
                    throw new Exception("No user present, please make sure that the user is assigned.");

                if (roles == null || permissions == null)
                    return false;

                bool rPass = false;
                var intersectR = User.Roles.Intersect(roles);
                if ((roles.Count() == intersectR.Count()))
                    rPass = true;

                bool pPass = false;
                var intersectP = User.Permissions.Intersect(permissions);
                if ((permissions.Count() == intersectP.Count()))
                    pPass = true;

                if (rPass && pPass)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      

        /// <summary>
        /// This method wraps the code passes to it with a roles and permissions check
        /// It returns whatever is returned from within the encapsulated code
        /// if it fails it will return the default(T) and not execute the code, it will also display the tooltip popup with a reason why.
        /// </summary>
        /// <typeparam name="T">The type or result that will be returned if code was successful</typeparam>
        /// <param name="roles">a string array of roles to check, if null is passed in the role check will be ignored</param>
        /// <param name="permissions">a string array of permission to check, if null is passed in the permission check will be ignored</param>
        /// <param name="codeToExecute">The code to executed (in lambda expression). return (()=>{return code;}); </param>
        /// <returns>The result specified as T (Generic return from within the code), null if failed</returns>
        public T ExecuteFunction<T>(string[] roles, string[] permissions, Func<T> codeToExecute)
        {
            if (User == null)
                throw new Exception("No user present, please make sure that the user is assigned.");

            if (roles != null)
                if (!CheckMatchAnyRole(roles))
                {
                    throw new UserRoleOrPermissionException($"The user requires role(s) {  string.Join(", ", roles) } ");
                }
            if (permissions != null)
                if (!CheckMatchAnyPermission(permissions))
                {
                    throw new UserRoleOrPermissionException($"The user requires permission(s) {  string.Join(", ", permissions) } ");
                }
            return codeToExecute.Invoke();
        }

        /// <summary>
        /// This method wraps the code passes to it with role and permission checks.         
        /// if it fails the code will not execute and a notification will be shown.
        /// </summary>      
        /// <param name="roles">a string array of roles to check, if null is passed in the role check will be ignored</param>
        /// <param name="permissions">a string array of permission to check, if null is passed in the permission check will be ignored</param>
        /// <param name="codeToExecute">The code(or action) to execute, usually in a lambda expression. ()=>{code;}</param>
        public void ExecuteAction(string[] roles, string[] permissions, Action codeToExecute)
        {
            if (User == null)
                throw new Exception("No user present, please make sure that the user is assigned.");

            if (roles != null)
                if (!CheckMatchAnyRole(roles))
                {
                    throw new UserRoleOrPermissionException($"The user requires role(s) {  string.Join(", ", roles) } ");
                }
            if (permissions != null)
                if (!CheckMatchAnyPermission(permissions))
                {
                    throw new UserRoleOrPermissionException($"The user requires permission(s) {  string.Join(", ", permissions) } ");
                }

            //all is good proceed!
            codeToExecute.Invoke();
        }
    }
}
