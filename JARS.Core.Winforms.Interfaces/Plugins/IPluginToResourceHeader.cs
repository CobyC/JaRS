using JARS.Entities;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.Interfaces.Processors;
using System.Drawing;

namespace JARS.Core.WinForms.Interfaces.Plugins
{
    /// <summary>
    /// This plugin is specific for the use in the resource header popup window.
    /// It provides the ability to add plugins that relates to the resource and the scheduler control.
    /// </summary>
    public interface IPluginToResourceHeader : IProcessor, IPluginWinForms, IPluginForEntity<JarsResource>, IPluginWithExecuteAsync, IPluginToSchedulerStorage, IPluginRequiresPermission
    {
        /// <summary>
        /// The path or uri property of the image associated with the plugin.
        /// </summary>
        Image Image { get; }

    }
}
