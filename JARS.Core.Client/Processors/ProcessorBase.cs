using JARS.Core.Interfaces.Processors;
using JARS.Core.Interfaces.Security;
using JARS.Core.Security;
using System.ComponentModel.Composition;

namespace JARS.Core.Client.Processors
{
    /// <summary>
    /// The base class that can be inherited from when using a plugin.
    /// This provides access to the Global context and RolesAndPermissions
    /// </summary>
    public class ProcessorBase : IProcessor
    {
        [ImportingConstructor]
        public ProcessorBase()
        { }

        public ProcessorBase(GlobalContext context)
        {
            _Context = context;
        }

        private GlobalContext _Context;
        /// <summary>
        /// The global context of the application
        /// </summary>
        protected GlobalContext Context
        {
            get
            {
                if (_Context == null)
                    _Context = GlobalContext.Instance;
                return _Context;
            }
            private set { }
        }

        IRolesAndPermissions _RolesAndOrPermissions;
        /// <summary>
        /// Access to roles and permissions for the current user.
        /// </summary>
        protected IRolesAndPermissions RolesAndOrPermissions
        {
            get
            {
                if (_RolesAndOrPermissions == null)
                {
                    _RolesAndOrPermissions = new RolesAndPermissions(Context.LoggedInUser);
                }
                return _RolesAndOrPermissions;
            }
            private set
            {
                _RolesAndOrPermissions = value;
            }
        }
    }
}
