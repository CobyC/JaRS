using System;

namespace JARS.Core.Interfaces.Rules.Attributes
{
    /// <summary>
    /// Apply this attribute to an entity class to allow rules to be tested against this entity,
    /// If an entity does not implement this attribute rules will not be tested against the entity.    
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
    public class AllowRuleProcessingAttribute : Attribute
    {
        public AllowRuleProcessingAttribute() : base()
        { }
    }
}
