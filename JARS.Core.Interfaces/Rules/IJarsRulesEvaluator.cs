using JARS.Core.Interfaces.Entities;
using System.Collections.Generic;
using System.Data;


namespace JARS.Core.Interfaces.Rules
{
    public interface IJarsRulesEvaluator
    {
        /// <summary>
        /// Evaluates the rules that are linked to the entities involved in the operation being carried out.
        /// If it doesn't return a rule it means all rules have passed the test.
        /// </summary>
        /// <param name="EntityRules">The list of rules available for evaluation, this list can be the full list of rules, as it will be filtered according to the passed in target and source entities.</param>
        /// <param name="sourceEntity">The entity that acts as the source for the rule</param>
        /// <param name="targetEntity">The entity that acts as the target for the rule.</param>
        /// <param name="ruleApplicator">The event or process the event is applied to.</param>
        /// <returns>If a rule gets returned it will be the first rule the process failed on, if nothing gets returned all rules passed.</returns>
        IJarsRule EvaluateEntityRules(List<IJarsRule> EntityRules, IEntityBase sourceEntity, IEntityBase targetEntity, RuleRunsOn ruleApplicator);

        /// <summary>
        /// Validates the entity against rules set against its own Type.
        /// </summary>
        /// <param name="EntityRules">This can be a list of all the rules or an already filtered list, the rules will be checked again.</param>
        /// <param name="theEntity">The entity that is being used to look up rules set against its type</param> 
        /// <param name="ruleApplicator">The event or process the event is applied to.</param>
        /// <returns>Returns the rule that was matched or null if nothing matched</returns>
        IJarsRule EvaluateEntityOwnTypeRules(List<IJarsRule> EntityRules, IEntityBase theEntity, RuleRunsOn ruleApplicator);

        /// <summary>
        /// Validates the entity against rules set for the specific entity against itself.
        /// </summary>
        /// <param name="EntityRules">This can be a list of all the rules or an already filtered list, the rules will be checked again.</param>
        /// <param name="theEntity">The entity that is being used to look up rules against its own type and its specific record</param>
        /// <param name="ruleApplicator">The event or process the event is applied to.</param>
        /// <returns>Returns the rule that was matched or null if nothing matched</returns>
        IJarsRule EvaluateEntityOwnRules(List<IJarsRule> EntityRules, IEntityBase theEntity, RuleRunsOn ruleApplicator);

        /// <summary>
        /// Evaluate The source entity against the target entity only.
        /// This does not include self evaluating.
        /// swapping the source and target will do the reverse check.
        /// </summary>
        /// <param name="EntityRules">This can be a list of all the rules or an already filtered list, the rules will be checked again.</param>
        /// <param name="sourceEntity">The entity that acts as the source for the rule</param>
        /// <param name="targetEntity">The entity that acts as the target for the rule.</param>
        /// <param name="ruleApplicator">The event or process the event is applied to.</param>
        /// <returns>Returns the rule that was matched or null if nothing matched</returns>
        IJarsRule EvaluateSourceAgainstTarget(List<IJarsRule> EntityRules, IEntityBase sourceEntity, IEntityBase targetEntity, RuleRunsOn ruleApplicator);

        /// <summary>
        /// Process the rule criteria on the data provided.
        /// If this method return true the rule was met.
        /// </summary>
        /// <param name="sourceDataToTest">The source data that will be used when testing the criteria string</param>
        /// /// <param name="targetDataToTest">The target data that will be used when testing the criteria string</param>
        /// <param name="entityRule">the criteria that will be used to determine if the condition is true or false</param>
        /// <returns>If true then the rule was met and the condition passed, if false the rule failed and the condition was not met.</returns>
        bool EvaluateRule(DataTable sourceDataToTest, DataTable targetDataToTest, IJarsRule entityRule);

    }
}

