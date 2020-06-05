using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = true)]
    public class AuthorizationAttribute : Attribute
    {
        public string Role { get; }
        //public int PermissionType { get; set; }

        public AuthorizationAttribute(string role)
        {
            Role = role;
        }

    }
}
