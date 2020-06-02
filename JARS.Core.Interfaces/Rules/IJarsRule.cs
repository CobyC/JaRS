using JARS.Core.Interfaces.Entities;
using System.Runtime.Serialization;

namespace JARS.Core.Interfaces.Rules
{
    //blank interface so that anything can pass as a rule
    public interface IJarsRule : IEntityBase<int>
    {
        /// <summary>
        /// The name for this rule
        /// </summary>
        [DataMember]
        string Name { get; set; }

        /// <summary>
        /// a short description of the purpose of this rule
        /// </summary>
        [DataMember]
        string Description { get; set; }

        /// <summary>
        /// The criteria string used to execute on the source type.
        /// This is build up using the DevExpress filter editor control
        /// </summary>
        [DataMember]
        string SourceCriteriaString { get; set; }

        /// <summary>
        /// The name of the type that is the source (or trigger) for this rule. The value received from [sourceEnt.GetType().Name]       
        /// </summary>
        [DataMember]
        string SourceTypeName { get; set; }

        /// <summary>
        /// Indicates how the criteria should be processed.
        /// </summary>
        [DataMember]
        RulePassesWhen RulePassesWhen { get; set; }

        /// <summary>
        /// The criteria string used to execute the target condition.
        /// This is build up using the DevExpress filter editor control
        /// </summary>
        [DataMember]
        string TargetCriteriaString { get; set; }
        /// <summary>
        /// The name of the entity type linked to this rule. The value received from [targetEnt.GetType().Name]
        // This needs to be in the format JARS.Full.Namespace.Class.Name, JARS.Name.Space, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // The information can be retrieved using the type. ie typeof(class/interface).AssemblyQualifiedName
        /// </summary>
        [DataMember]
        string TargetTypeName { get; set; }

        /// <summary>
        /// The comma separated string of actions that indicate when a rule gets triggered.
        /// i.e. if the 'OnDragDrop' action is in the list the rule will be triggered on drag and drop events in the system.
        /// </summary>
        [DataMember]
        string RuleRunsOn { get; set; }

        /// <summary>
        /// This indicates which part,or all side, of the rule are to be tested for the whole rule to pass.        
        /// </summary>
        /// 
        [DataMember]
        RuleEvaluation RuleEvaluation { get; set; }
    }
}
