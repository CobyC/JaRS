using System;

namespace JARS.Core.Interfaces.Attributes
{
    /// <summary>
    /// This Interface describes the metadata required for a DataContext plugin.
    /// It takes a type property that should be the Interface Type of the DataContext.
    /// </summary>
    public interface IExportDataContextMetadata
    {
        Type InterfaceType { get; }
    }
}
