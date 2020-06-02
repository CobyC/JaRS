using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Exceptions
{
    /// <summary>
    /// This exception is for use with Services when a client could not be found.
    /// </summary>
    public class ClientNotFoundException: ApplicationException
    {
        public ClientNotFoundException(string message)
           : base(message)
        {
        }

        public ClientNotFoundException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
