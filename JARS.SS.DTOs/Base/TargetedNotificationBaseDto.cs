using JARS.Core.Interfaces.Entities;

namespace JARS.SS.DTOs.Base
{
    public abstract class TargetedNotificationBaseDto<T> : NotificationBaseDto<T> where T : IEntityBase
    {
        /// <summary>
        /// This can be used in case a message is for a specific user
        /// </summary>
        public string ToUserId { get; set; }

        /// <summary>
        /// By default notify the subscribed channels. 
        /// This can be set to false to not send a notification.
        /// </summary>
        public bool NotifyChanel { get; set; } = true;

    }
}
