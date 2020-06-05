using System;

namespace JARS.Entities
{
    /// <summary>
    /// This class is the base jars job line entity that represents a standard Jars Job line.
    /// It inherits from the abstract class JobLineBase and should contain any additional properties added, that does not form part of the core structure.
    /// Any jobs that inherit from this needs to extend it's NH mapping from this class' mapping.
    /// </summary>
    [Serializable]
    public class JarsJobLine : JarsJobLineBase
    {
    }
}
