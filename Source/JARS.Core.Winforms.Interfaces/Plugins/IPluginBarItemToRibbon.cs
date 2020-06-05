using DevExpress.XtraBars;

namespace JARS.Core.WinForms.Interfaces.Plugins
{
    /// <summary>
    /// Use this interface when creating a plugin that will add a bar item to the ribbon on the main form.
    /// </summary>
    public interface IPluginBarItemToRibbon: IPluginWinForms
    {        
        /// <summary>
        /// Get the bar item that will be added to the ribbon control,
        /// This can be any control that inherits from the DevXpress bar item. for example the BarCheckItem or BarButtonItem etc..
        /// </summary>
        BarItem BarItem { get; }
       
    }
}
