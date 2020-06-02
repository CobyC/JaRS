using JARS.Core.Interfaces.Entities;
using System;

namespace JARS.Core.Interfaces.Security
{
    public interface IRolesAndPermissions
    {
        IJarsUserBase User { get; }

        /// <summary>
        /// Checks the current user roles, if any of the roles are found the check is passed as true (non strict).                
        /// </summary>        
        /// <param name="roles">a string array of roles to check, if null is passed in the role check will fail (false)</param>        
        /// <returns>true if role is found, false if not.</returns>
        bool CheckMatchAnyRole(string[] roles);

        /// <summary>
        /// Checks the current user permissions, if any of the permissions are found the check is passed as true (non strict).                
        /// </summary>                
        /// <param name="permissions">a string array of permission to check, if null is passed in the permission check will be ignored (passed)</param>        
        /// <returns>true if permission is found, false if not.</returns>
        bool CheckMatchAnyPermission(string[] permissions);

        /// <summary>
        /// Unlike the 'ExecuteRolesAndOrPermissionsMethod' functions, this just checks the current user roles and permissions,
        /// will return true for any of the matches.
        /// </summary>        
        /// <param name="roles">a string array of roles to check, if null is passed in the role check will be ignored</param>
        /// <param name="permissions">a string array of permission to check, if null is passed in the permission check will be ignored</param>        
        /// <returns>true if role or permission is found, false if not.</returns>
        bool CheckMatchAny(string[] roles = null, string[] permissions = null);

        /// <summary>
        /// Checks the current user roles. All of the roles must be matched for the check to passed as true (strict).                
        /// </summary>        
        /// <param name="roles">a string array of roles to check, if null is passed in the result will be false</param>        
        /// <returns>true if all roles are found, false if not.</returns>
        bool CheckMatchAllRoles(string[] roles);

        /// <summary>
        /// Checks the current user permissions. All of the permissions must be matched for the check to passed as true (strict).                
        /// </summary>        
        /// <param name="permissions">a string array of permissions to check, if null is passed in the result will be false</param>        
        /// <returns>true if all permissions are found, false if not.</returns>
        bool CheckMatchAllPermissions(string[] permissions);

        /// <summary>
        /// Unlike the 'ExecuteRolesAndOrPermissionsMethod' functions, this just checks the current user roles and permissions,
        /// will return true only if all roles and/or permissions are matched.
        /// </summary>        
        /// <param name="roles">a string array of roles to check, if null is passed in the role check will be ignored</param>
        /// <param name="permissions">a string array of permission to check, if null is passed in the permission check will be ignored</param>        
        /// <returns>true if role or permission is found, false if not.</returns>
        bool CheckMatchAll(string[] roles = null, string[] permissions = null);

        /// <summary>
        /// Checks the current user roles and permissions strictly, all roles and permissions must be matched, values can not be null. 
        /// </summary>        
        /// <param name="roles">a string array of roles to check, if null is passed the roles check fails</param>
        /// <param name="permissions">a string array of permission to check, if null is passed in the permission the check will fail</param>        
        /// <returns>true if roles and permissions are found, false if not.</returns>
        bool CheckStrict(string[] roles, string[] permissions);


        /// <summary>
        /// This method wraps the code passes to it with role and permission checks.         
        /// if it fails the code will not execute and an exception will be raised (UserRoleOrPermissionException).
        /// </summary>      
        /// <param name="roles">a string array of roles to check, if null is passed in the role check will be ignored</param>
        /// <param name="permissions">a string array of permission to check, if null is passed in the permission check will be ignored</param>
        /// <param name="codeToExecute">The code(or action) to execute, usually in a lambda expression. ()=>{code;}</param>
        void ExecuteAction(string[] roles, string[] permissions, Action codeToExecute);

        /// <summary>
        /// This method wraps the code passes to it with a roles and permissions check
        /// It returns whatever is returned from within the encapsulated code
        /// if it fails the code will not execute and an exception will be raised (UserRoleOrPermissionException).
        /// </summary>
        /// <typeparam name="T">The type or result that will be returned if code was successful</typeparam>
        /// <param name="roles">a string array of roles to check, if null is passed in the role check will be ignored</param>
        /// <param name="permissions">a string array of permission to check, if null is passed in the permission check will be ignored</param>
        /// <param name="codeToExecute">The code to executed (in lambda expression). return (()=>{return code;}); </param>
        /// <returns>The result specified as T (Generic return from within the code), null if failed</returns>
        T ExecuteFunction<T>(string[] roles, string[] permissions, Func<T> codeToExecute);
    }
}
