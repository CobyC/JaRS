using JARS.Core.Interfaces.Entities;
using System;

namespace JARS.Entities.Base
{
    public class AppointableEntityBase : AppointableBase<int>,
        IEntityWithIsActive, // Enforces the IsActive property, useful for indicating if an entity is still active or not.
        IEntityWithAppointing, //Enforces the properties used for by an appointment (start, End, Description, ResourceId)
        IEntityWithAudit, //Enforces the properties for keeping track of auditing, when a record was created, who created it and when it was last modified.
        IEntityWithExternalReference, //Enforces the ExtRefID property - should be the reference to the external system ID.
        IEntityWithIsEmergency, // indicates that the entity is an emergency - 
        IEntityWithIntegration, // Enforces the properties used for integrating into other systems, integration status, date, message
        IEntityWithLineOfWork, // Enforces the LineOfWork property, useful in view options and processors
        IEntityWithLocation, // Enforces the Location property, will be used as appointment location and useful in view options)
        IEntityWithProgressStatus, // Enforces the progress status property, this indicates the progress on an entity (NEW, COMP, WAITING) and useful in view options.
        IEntityWithPriority, // Enforces the Priority property, useful in processors, view options and general priority indication.
        IEntityWithStatusLabels, // Enforces the properties for holding the status and label key values (useful in view options)
        IEntityWithShowOnMobile, // Enforces a property that indicates if the appointment will be sent to a mobile device.
        IEntityWithTargetDate // Enforces the TargetDate Property, useful for holding a target date property        
    {
        string labelKey;
        public string LabelKey
        {
            get
            {
                return labelKey;
            }
            set
            {
                labelKey = value;
            }
        }
        public string StatusKey { get; set; }
        public string Location { get; set; }
        public string LocationCode { get; private set; }
        public string LineOfWork { get; set; }
        public string ProgressStatus { get; set; }
        public string Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public int IntegrationStatus { get; set; }
        public DateTime? IntegrationDate { get; set; }
        public string IntegrationMessage { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmergency { get; set; }
        public bool ShowOnMobile { get; set; }
        public string ExtRefId { get; set; }
        public DateTime? TargetDate { get; set; }
    }
}
