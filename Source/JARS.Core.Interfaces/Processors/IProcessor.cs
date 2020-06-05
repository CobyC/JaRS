namespace JARS.Core.Interfaces.Processors
{
    /// <summary>
    /// This interface is to help with identifying the type of processor that is being implemented, it is an empty base interface used for identifying processors.
    /// a Processor can represent anything that will be referenced in regards with processing information or data, it is usually used in conjunction with other Interfaces.
    /// It is especially helpful with using MEF and plugins.
    /// </summary>
    public interface IProcessor
    { }

}
