using System;

namespace JARS.Core.Attributes
{
    /// <summary>
    /// Attribute that if applied to the property will protect it for data protection
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DataProtectionAttribute : Attribute
    {
    }
}
