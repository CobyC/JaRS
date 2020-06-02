using JARS.Core.WinForms.Interfaces.Plugins;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Interfaces.Controls
{
    /// <summary>
    /// This interface can be used to create a control that can be presented on any windows surface area.
    /// The control will then handle the actions required to execute events or context external apis etc..
    /// </summary>
    public interface IPluginAsUserControl : IPluginWinForms
    {
        Control control { get; set; }
    }
}
