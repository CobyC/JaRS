using JARS.Core.Interfaces.Entities;
using System;

namespace JARS.SS.DTOs.Interfaces
{
    public interface INotificationDto
    {
        /// <summary>
        /// Assign the user name sending information over service stack.
        /// This will also be the information used to update the 'RecordModifiedBy' value of entities that contains that value.
        /// </summary>
        string FromUserName { get; set; }
        /// <summary>
        /// This indicates the type of message that is sent (cmd. or store. or find. etc)
        /// </summary>
        string Selector { get; set; }

        /// <summary>
        /// This can be used to identify the client (application) that sent the notification.
        /// If the client instance has a unique identifier (guid), assign it to this property.
        /// </summary>
        Guid ClientGuid { get; set; }

    }

    public interface INotificationBaseDto<T> where T : IEntityBase
    {
        ///// <summary>
        ///// This is where the message will be sent to, this defaults to the name of the entity type linked to the notification [T].
        ///// </summary>
        //string Channel { get; set; }
        ///// <summary>
        ///// The message that is sent by the client or user
        ///// </summary>
        //string DataMessage { get; set; }
        
        /// <summary>
        /// Assign the user name sending information over service stack.
        /// This will also be the information used to update the 'RecordModifiedBy' value of entities that contains that value.
        /// </summary>
        string FromUserName { get; set; }
        /// <summary>
        /// This indicates the type of message that is sent (cmd. or store. or find. etc)
        /// </summary>
        string Selector { get; set; }

        /// <summary>
        /// This can be used to identify the client (application) that sent the notification.
        /// If the client instance has a unique identifier (guid), assign it to this property.
        /// </summary>
        Guid ClientGuid { get; set; }
    }
}
