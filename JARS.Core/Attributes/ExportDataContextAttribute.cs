using JARS.Core.Interfaces.Attributes;
using System;
using System.ComponentModel.Composition;

namespace JARS.Core.Attributes
{
    /// <summary>
    /// This attribute is used to identify data context plugins using (MEF)
    /// it takes parameters to identify where and how the plugin should be handled.
    /// it implements the <see cref="IExportDataContextMetadata"/> interface which is helpful in getting the correct plugins when using MEF and the Lazy&lt;&gt; function (Use this attribute to export multiple items under the same contract).    
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false), MetadataAttribute]
    public class ExportDataContextAttribute : ExportAttribute, IExportDataContextMetadata
    {
        /// <summary>
        /// Supply a main contract type as the MEF contract and then give it an interface type to 
        /// give the contract a sub type. Useful in DataContext because we can export all contexts under the same contract name,
        /// but have access to the specific context via the InterfaceType property.
        /// </summary>
        /// <param name="contractType">The MEF contract type</param>
        /// <param name="interfaceType">The sub contract type that the export represents.</param>
        public ExportDataContextAttribute(Type contractType, Type interfaceType) : base(interfaceType.Name, contractType)
        {
            InterfaceType = interfaceType;
        }
        public Type InterfaceType { get; private set; }
    }
}
