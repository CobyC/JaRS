using JARS.Core.Attributes;
using JARS.Core.Interfaces.Entities;
using System;
using System.Runtime.Serialization;

namespace JARS.Core.Entities
{
    /// <summary>
    /// When an entity derives from this class, it will gain the integration properties, IntegrationStatus, IntegrationDate, IntegrationMessage
    /// The main purpose is to enable the implementer to record information about integrating the data into another system.
    /// </summary>
    [DataContract]
    public abstract class IntegratableEntityBase : AuditableEntityBase, IEntityWithIntegration
    {
        int _IntegrationStatus;
        DateTime? _IntegrationDate;
        string _IntegrationMessage;

        /// <summary>
        /// this column is intended to be used by QX(if used), Get or set this to check the status or trigger QX (or whatever process). 
        /// </summary>
        [DataMember]
        [LookupValue(DefaultCategoryCode = "INTEG_STATUS", DefaultFirstValue = "99")]
        public virtual int IntegrationStatus
        {
            get => _IntegrationStatus;
            set
            {
                _IntegrationStatus = value;
                OnPropertyChanged(() => IntegrationStatus);
            }
        }
        /// <summary>
        /// This column is intended for use by SX , Get or Set when the job was last run through a script or process.
        /// </summary>
        [DataMember]
        public virtual DateTime? IntegrationDate
        {
            get => _IntegrationDate;
            set
            {
                _IntegrationDate = value;
                OnPropertyChanged(() => IntegrationDate);
            }
        }
        /// <summary>
        /// Get or Set the Message indicating if the integration was successful or failed with messages.
        /// </summary>
        [DataMember]
        public virtual string IntegrationMessage
        {
            get => _IntegrationMessage;
            set
            {
                _IntegrationMessage = value;
                OnPropertyChanged(() => IntegrationMessage);
            }
        }


    }
}
