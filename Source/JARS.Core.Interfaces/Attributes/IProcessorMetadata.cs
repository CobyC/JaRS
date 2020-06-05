using System;

namespace JARS.Core.Interfaces.Attributes
{
    /// <summary>
    /// This Interface identifies the metadata that is used for a processor.
    /// a processor can be used to identify any process or action that is linked to an entity    
    /// This is useful with MEF (Lazy&lt;gt&; method)
    /// </summary>
    public interface IProcessorMetadata
    {
        /// <summary>
        /// This is the type that is linked to the process.
        /// The entity has to derive from IEntityBase.
        /// This value will be set to the contract name value in the MEF export.
        /// </summary>
        Type LinkedEntityType { get; }

        /// <summary>
        /// Indicates that the processor also belongs to a certain type (usually an interface).
        /// This can be used to further restrict how the processor is linked to an entity.
        /// for example the processor links to XYZEntity and only when the plugin also implements ISpecialRuleXYZ interface.
        /// (This property is optional)
        /// </summary>
        Type AdditionalProcessorType { get; }
    }
}
