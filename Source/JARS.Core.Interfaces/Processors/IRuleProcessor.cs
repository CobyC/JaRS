using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Rules;
using System.Collections.Generic;

namespace JARS.Core.Interfaces.Processors
{
    public interface IRuleProcessor : IProcessor
    {        
        /// <summary>
        /// This method is used for testing the source entity only
        /// </summary>
        /// <param name="jarsRules">The rules to evaluate</param>
        /// <returns>returns the rule that failed the evaluation</returns>
        IJarsRule EvaluateRules(List<IJarsRule> entityRules, IEntityBase sourceEntity, RuleRunsOn ruleApplicator);

        /// <summary>
        /// This method is used for testing the source and target entity when the rule applicator is present (DragDrop, OnChange, etc..) 
        /// </summary>
        /// <param name="jarsRules">The rules to evaluate against the source and the target</param>
        /// <returns>returns the rule that failed the evaluation</returns>
        IJarsRule EvaluateRules(List<IJarsRule> jarsRules, IEntityBase sourceObject, IEntityBase targetObject, RuleRunsOn ruleApplicator);
    }
}
