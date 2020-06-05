using System;

namespace JARS.Core.Attributes
{
    /// <summary>
    /// This attribute helps to determine what lookup value needs to be used for a property that takes this attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LookupValueAttribute : Attribute
    {
        public string DefaultCategoryCode { get; set; }
        public string DefaultFirstValue { get; set; }
    }
}