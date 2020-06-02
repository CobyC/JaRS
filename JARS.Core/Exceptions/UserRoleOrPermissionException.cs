using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Exceptions
{
    public class UserRoleOrPermissionException : Exception
    {
        public UserRoleOrPermissionException() : base("User Not In Role")
        { }
        public UserRoleOrPermissionException(string message) : base(message)
        { }

        public UserRoleOrPermissionException(string message, Exception exception) : base(message, exception)
        { }

        public UserRoleOrPermissionException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }

    }
}
