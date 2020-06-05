using JARS.Core.WinForms.Plugins;
using System;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Behaviours
{
    /// <summary>
    /// The abstract base class for the Behaviour plugin, it contains the methods for triggering the event handlers and a property to indicate if the behaviour is active or not.
    /// </summary>
    public abstract class BehaviourPluginBase
    {  

        protected bool _IsActive;
        /// <summary>
        /// Indicates if the behaviour is active or not
        /// </summary>
        public bool IsActive
        {
            get
            {
                return _IsActive;
            }
        }

        /// <summary>
        /// an event handler that gets invoked when the plugin is activated.
        /// </summary>
        public event EventHandler OnActivate;
        protected void OnActivated(object sender, EventArgs e)
        {
            OnActivate?.Invoke(sender, e);
            _IsActive = true;
        }

        /// <summary>
        /// an event handler that gets invoked when the plugin is de-activated.
        /// </summary>
        public event EventHandler OnDeactivate;
        protected void OnDeactivated(object sender, EventArgs e)
        {
            OnDeactivate?.Invoke(sender, e);
            _IsActive = false;
        }

        /// <summary>
        /// Find the UserControlPlugin so that we can add events to the control.
        /// </summary>
        /// <param name="parentControl">the top level control where the search will start from</param>
        /// <returns>returns the UserControlBasePlugin if found or null if nothing found</returns>
        protected UserControlBasePlugin FindUserControlBasePlugin(Control parentControl)
        {
            UserControlBasePlugin basePlugin = null;

            if (parentControl is UserControlBasePlugin foundPlugin)
            {
                return basePlugin = foundPlugin;
            }
            else
            {
                if (parentControl.Controls.Count > 0)
                {
                    foreach (Control childControl in parentControl.Controls)
                    {
                        if (childControl is UserControlBasePlugin childPlugin)
                            return basePlugin = childPlugin;
                        else
                            FindUserControlBasePlugin(childControl);
                    }

                }
            }
            return basePlugin;
        }

        /// <summary>
        /// Recurring function to find the main grid control used for interacting with the behaviour 
        /// </summary>
        /// <param name="parentControl">The top level control to start searching from</param>
        /// <returns>will return a GridControl if found, otherwise null</returns>
        protected T FindMainEventControl<T>(Control parentControl)
        {
            T mainGrid = default(T);
            if (parentControl is T mGrid)
                return mainGrid = mGrid;
            else
            {
                if (parentControl.Controls.Count > 0)
                    foreach (Control childControl in parentControl.Controls)
                        if (childControl is T childGrid)
                            return mainGrid = childGrid;
                        else
                            FindMainEventControl<T>(childControl);//keep looking
            }
            return mainGrid;//if this point is reached there is no grid control available.
        }
    }
}
