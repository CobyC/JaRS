using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Processors;
using JARS.SS.DTOs.Base;
using ServiceStack;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Interfaces.Plugins
{
    /// <summary>
    /// This interface indicates that the implementing processor has methods that will process commands received from the Events service.
    /// The Delete and Store command comes with additional meta data, alternatively the message received can be processed by the AnyCommand method.
    /// </summary>
    public interface IProcessorForEventServiceCommandReceived : IProcessor
    {
        /// <summary>
        /// This method handles messages received about a delete of a record that took place on another client.
        /// </summary>
        /// <param name="affectsControl">The scheduler control affected by the delete</param>
        /// <param name="syncEvent">The object carrying the information about the delete.</param>
        /// <returns>true to indicate the message was processed successfully</returns>
        bool OnDeleteCommandReceived(Control affectsControl, ServerEventMessage msg);

        /// <summary>
        /// This method handles messages about data being updated or stored that originated on another client.
        /// </summary>
        /// <param name="affectsControl">The scheduler control being affected by the event.</param>
        /// <param name="syncEvent">the object carrying the information about the store event.</param>
        /// <returns>true to indicate the message was processed successfully</returns>
        bool OnStoreCommandReceived(Control affectsControl, ServerEventMessage msg );// JarsSyncStoreEvent<IEntityBase> syncEvent);

        /// <summary>
        /// this handles all other commands sent from other clients.
        /// </summary>
        /// <param name="affectsControl">the scheduler control being affected (if at all) by the event message</param>
        /// <param name="msg">the message (data) sent from the other client</param>
        /// <returns>true to indicate the message was processed successfully</returns>
        bool OnAnyCommandReceived(Control affectsControl, ServerEventMessage msg);

    }
}
