using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Exceptions
{
    /// <summary>
    /// This Exception is used when something (Entity, Record, Plugin) could not be found after expecting it to load or be available.
    /// </summary>
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
