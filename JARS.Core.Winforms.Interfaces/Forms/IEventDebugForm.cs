using JARS.Core.WinForms.Interfaces.Plugins;

namespace JARS.Core.WinForms.Interfaces.Forms
{
    public interface IEventDebugForm : IPluginWinForms
    {
        /// <summary>
        /// Add a debug item to the list of events
        /// </summary>
        /// <param name="formName">What form the event took place on</param>
        /// <param name="cmdType">The typeof event command (ie store. or delete. etc) </param>
        /// <param name="infoString">additional string info</param>
        void AddEventItemToList(string formName, string cmdType, string infoString);

        /// <summary>
        /// Clears the list on the form
        /// </summary>
        void ClearEventItems();
    }
}
