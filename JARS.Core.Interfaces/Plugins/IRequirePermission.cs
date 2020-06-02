namespace JARS.Core.Interfaces.Plugins
{
    /// <summary>
    /// This interface indicates that the implementing class can implement require roles and or permissions.
    /// The Roles and permissions are held in the RequiredRoles and RequiredPermissions properties.
    /// </summary>
    public interface IPluginRequiresPermission
    {
        /// <summary>
        /// Indicate what role or roles the plugin expects for it to be accessible
        /// </summary>
        string[] RequiredRoles { get; }

        /// <summary>
        /// Indicate what permission or permissions the plugin expects for it to be accessible
        /// </summary>
        string[] RequiredPermissions { get; }
    }
}
