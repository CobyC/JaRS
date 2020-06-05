using JARS.Core.Interfaces.Attributes;
using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Processors;
using System;
using System.ComponentModel.Composition;

namespace JARS.Core.Attributes
{
    /// <summary>
    /// Its purpose is to help with the export of processor plugins. (using MEF)
    /// This attribute is used to identify plugins with additional metadata using/inheriting from Export attribute (MEF attribute) 
    /// it implements the <see cref="IProcessorMetadata"/> interface which is helpful in identifying the plugins for further use.
    /// (Use this attribute to export multiple items under the same contract using the Lazy&lt;&gt; function).    
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false), MetadataAttribute]
    public class ExportProcessorAttribute : ExportAttribute, IProcessorMetadata
    {

        ExportProcessorAttribute() : base(typeof(IProcessor))
        {
            _LinkedEntityType = typeof(IEntityBase);
        }

        /// <summary>
        /// Exports as <see cref="IProcessor"/> linked to a specific entity.
        /// </summary>
        /// <param name="linkedEntityType">The entity type linked to this processor</param>
        public ExportProcessorAttribute(Type linkedEntityType) : base(typeof(IProcessor))
        {
            _LinkedEntityType = linkedEntityType;
        }

        /// <summary>
        /// Exports as <see cref="IProcessor"/> linked to a specific entity and with additional link to a type (Interface).
        /// Use this is the link has to be more specific, ie if the process is linked to an Entity but only when the process should only be used for ISpecialPurpose interface.
        /// </summary>
        /// <param name="linkedEntityType">the entity this processor is linked to</param>
        /// <param name="additionalProcessorType">an additional interface or type to describe this processor.</param>
        public ExportProcessorAttribute(Type linkedEntityType, Type additionalProcessorType) : base(typeof(IProcessor))
        {
            _LinkedEntityType = linkedEntityType;
            _AdditionalProcessorType = additionalProcessorType;
        }

       

        Type _LinkedEntityType;
        public Type LinkedEntityType
        {
            get
            {
                return _LinkedEntityType;
            }
        }

        Type _AdditionalProcessorType;
        public Type AdditionalProcessorType
        {
            get
            {
                return _AdditionalProcessorType;
            }
        }
    }
}
