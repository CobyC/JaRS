using DevExpress.XtraTab;

namespace JARS.Core.WinForms.Interfaces.Plugins
{
    /// <summary>
    /// This interface allows a plugin to access the external tab control
    /// </summary>
    public interface IPluginToExternalTabControl
    {
        XtraTabControl ExternalTabControl { get; set; }
    }
}
