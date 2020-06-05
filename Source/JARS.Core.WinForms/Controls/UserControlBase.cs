using JARS.Core.Client;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.Interfaces.Security;
using JARS.Core.Security;
using System.ComponentModel.Composition;

namespace JARS.Core.WinForms.Controls
{
    /// <summary>
    /// This is the base user control that is used within jars plugins, use this for anything other than forms
    /// THis provides access to global context, Roles And Permissions and the PluginFactory
    /// </summary>    
    public partial class UserControlBase : DevExpress.XtraEditors.XtraUserControl
    {

        protected GlobalContext Context { get { return GlobalContext.Instance; } }

        IRolesAndPermissions _RolesAndOrPermissions;
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

        [Import]
        protected IPluginFactory _pluginFactory;

        //[Import]
        //internal IJarsRulesFactory _rulesFactory;

        public UserControlBase()
        {
            if (JarsCore.Container != null)
                JarsCore.Container.SatisfyImportsOnce(this);

            InitializeComponent();           
        }
    }
}
