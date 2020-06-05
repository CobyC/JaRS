using JARS.Core.Interfaces.Entities;
using JARS.SS.DTOs.Interfaces;
using System;

namespace JARS.SS.DTOs.Base
{


    public abstract class NotificationDto: INotificationDto
    {
        /// <summary>
        /// Assign the user id sending information over service stack.
        /// This will also be the information used to update the 'RecordModifiedBy' value of entities that contains that value.
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// This indicates the type of message that is sent (cmd. or store. or find. etc)
        /// </summary>
        public string Selector { get; set; }

        /// <summary>
        /// This can be used to identify the client (application) that sent the notification.
        /// If the client instance has a unique identifier (guid), assign it to this property.
        /// </summary>
        public Guid ClientGuid { get; set; }
    }

    /// <summary>
    /// The base class containing the default values for any of the inherited classes.
    /// <para>
    /// <typeparamref name="T"/>: The type name of T will also be the default chanel used for server events. When using Dtos please make sure to use the Dto type.
    /// 
    /// </para>
    /// </summary>
    public abstract class NotificationBaseDto<T> : INotificationBaseDto<T>
where T : IEntityBase
    {
        /// <summary>
        /// Assign the user id sending information over service stack.
        /// This will also be the information used to update the 'RecordModifiedBy' value of entities that contains that value.
        /// </summary>
        public string FromUserName { get; set; }
        ///// <summary>
        ///// Use this property to GetSubscription information from ServiceStack using the id value
        ///// </summary>
        //public string FromSubscriptionId { get; set; }
        ///// <summary>
        ///// This is where the message will be sent to, this defaults to the name of the entity type linked to the notification [T].
        ///// </summary>
        //public string Channel { get; set; } = typeof(T).Name;
        ///// <summary>
        ///// The message that is sent by the client or user
        ///// </summary>
        //public string DataMessage { get; set; }
        /// <summary>
        /// This indicates the type of message that is sent (cmd. or store. or find. etc)
        /// </summary>
        public string Selector { get; set; }

        /// <summary>
        /// This can be used to identify the client (application) that sent the notification.
        /// If the client instance has a unique identifier (guid), assign it to this property.
        /// </summary>
        public Guid ClientGuid { get; set; }

    }
}
