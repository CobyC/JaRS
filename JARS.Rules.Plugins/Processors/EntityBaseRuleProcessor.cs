using DevExpress.Data.Filtering;
using JARS.Core;
using JARS.Core.Attributes;
using JARS.Core.Extensions;
using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Processors;
using JARS.Core.Interfaces.Rules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JARS.Rules.Plugins.Processors
{
    /// <summary>
    /// This plugin shows how to implement a rule processor.
    /// The processor gets called and executed from the main application, the interfaces helps with 
    /// linking the processes within this processor to the various parts of the main system.
    /// </summary>
    [ExportProcessor(typeof(IEntityBase))]
    //[PartCreationPolicy(CreationPolicy.NonShared)] //<-- Only want one instance so not creating a new instance for every call
    public class EntityBaseRuleProcessor : IRuleProcessor
    {
        public IJarsRule EvaluateRules(List<IJarsRule> jarsRules, IEntityBase sourceEntity, RuleRunsOn ruleRunsOn)
        {
            //does the source meet the its own conditions?
            var failedRule = EvaluateEntityOwnRules(jarsRules, sourceEntity, ruleRunsOn, RuleEvaluation.SourceOnly);
            if (failedRule != null)
                return failedRule;

            return null;
        }

        public IJarsRule EvaluateRules(List<IJarsRule> jarsRules, IEntityBase sourceObject, IEntityBase targetObject, RuleRunsOn ruleRunsOn)
        {
            return EvaluateEntityRules(jarsRules, sourceObject, targetObject, ruleRunsOn);
        }


        /// <summary>
        /// Evaluates the rules that are linked to the entities involved in the operation being carried out.
        /// If it doesn't return a rule it means all rules have passed the test.
        /// </summary>
        /// <param name="entityRules">The list of rules available for evaluation, this list can be the full list of rules, as it will be filtered according to the passed in target and source entities.</param>
        /// <param name="sourceEntity">The entity that acts as the source for the rule</param>
        /// <param name="targetEntity">The entity that acts as the target for the rule.</param>
        /// <param name="ruleApplicator">The event or process the event is applied to.</param>
        /// <returns>If a rule gets returned it will be the first rule the process failed on, if nothing gets returned all rules passed.</returns>
        public IJarsRule EvaluateEntityRules(IList<IJarsRule> entityRules, IEntityBase sourceEntity, IEntityBase targetEntity, RuleRunsOn ruleRunsOn)
        {
            IJarsRule failedRule = null;
            List<IJarsRule> filteredByRuleRunsOnList = entityRules.Where(r => r.RuleRunsOn.Contains(ruleRunsOn.ToString())).ToList();

            //first check the rule for source only rules
            var fullSourceOnlyFilterList = filteredByRuleRunsOnList.FindAll(rules => rules.RuleEvaluation == RuleEvaluation.SourceOnly && rules.SourceTypeName == sourceEntity.GetType().Name);
            //does the source meet the its own conditions?
            if (fullSourceOnlyFilterList.Any())
                failedRule = EvaluateEntityOwnRules(fullSourceOnlyFilterList, sourceEntity, ruleRunsOn, RuleEvaluation.SourceOnly);
            if (failedRule != null)
                return failedRule;

            //first check the rule for source only rules
            var fullTargeteOnlyFilterList = filteredByRuleRunsOnList.FindAll(rules => rules.RuleEvaluation == RuleEvaluation.TargetOnly && rules.TargetTypeName == targetEntity.GetType().Name);
            //does the source meet the its own conditions?
            if (fullTargeteOnlyFilterList.Any())
                failedRule = EvaluateEntityOwnRules(fullTargeteOnlyFilterList, targetEntity, ruleRunsOn, RuleEvaluation.TargetOnly);
            if (failedRule != null)
                return failedRule;

            //---- from here we test both sides

            //need to test rules of target type on target type 
            //and source on target type
            //get all rules linked to the target
            var fullSourceFilterList = filteredByRuleRunsOnList.FindAll(rules => rules.RuleEvaluation == RuleEvaluation.Both && rules.SourceTypeName == sourceEntity.GetType().Name);

            //does the source meet the its own conditions?
            if (fullSourceFilterList.Any())
            {
                failedRule = EvaluateEntityOwnRules(fullSourceFilterList, sourceEntity, ruleRunsOn);
                if (failedRule != null)
                    return failedRule;
                //does the target meet the source conditions?
                failedRule = EvaluateSourceAgainstTarget(fullSourceFilterList, targetEntity, sourceEntity, ruleRunsOn);
                if (failedRule != null)
                    return failedRule;
            }


            var fullTargetFilterList = filteredByRuleRunsOnList.FindAll(rules => rules.TargetTypeName == targetEntity.GetType().Name);
            //does the target meet the its own conditions?
            if (fullTargetFilterList.Any())
            {
                failedRule = EvaluateEntityOwnRules(fullTargetFilterList, targetEntity, ruleRunsOn);
                if (failedRule != null)
                    return failedRule;

                //does the source meet the target conditions?
                failedRule = EvaluateSourceAgainstTarget(fullTargetFilterList, sourceEntity, targetEntity, ruleRunsOn);
                if (failedRule != null)
                    return failedRule;

                failedRule = EvaluateSourceAgainstTargetType(fullTargetFilterList, sourceEntity, targetEntity, ruleRunsOn);
                if (failedRule != null)
                    return failedRule;

                failedRule = EvaluateSourceChildListsAgainstTarget(fullTargetFilterList, sourceEntity, targetEntity, ruleRunsOn);
                if (failedRule != null)
                    return failedRule;
            }

            return null;
        }


        /// <summary>
        /// Validates the entity against rules set for the specific entity against itself.
        /// </summary>
        /// <param name="entityRules">This can be a list of all the rules or an already filtered list, the rules will be checked again.</param>
        /// <param name="theEntity">The entity that is being used to look up rules against its own type and its specific record</param>
        /// <param name="ruleApplicator">The event or process the event is applied to.</param>
        /// <returns>Returns the rule that was matched or null if nothing matched</returns>
        public IJarsRule EvaluateEntityOwnRules(List<IJarsRule> entityRules, IEntityBase theEntity, RuleRunsOn ruleApplicator, RuleEvaluation ruleEvaluation = RuleEvaluation.Both)
        {
            var fullFilterList = new List<IJarsRule>();
            if (ruleEvaluation == RuleEvaluation.Both)
                fullFilterList = entityRules.FindAll(r =>
                r.SourceTypeName == theEntity.GetType().Name &&
                r.TargetTypeName == theEntity.GetType().Name &&
                r.RuleEvaluation == ruleEvaluation &&
                r.RuleRunsOn.Contains(ruleApplicator.ToString()));

            if (ruleEvaluation == RuleEvaluation.SourceOnly)
                fullFilterList = entityRules.FindAll(r =>
                r.SourceTypeName == theEntity.GetType().Name &&
                r.RuleEvaluation == ruleEvaluation &&
                r.RuleRunsOn.Contains(ruleApplicator.ToString()));

            if (ruleEvaluation == RuleEvaluation.TargetOnly)
                fullFilterList = entityRules.FindAll(r =>
                r.TargetTypeName == theEntity.GetType().Name &&
                r.RuleEvaluation == ruleEvaluation &&
                r.RuleRunsOn.Contains(ruleApplicator.ToString()));
            //does the entity type meet the its own conditions?
            //var entityFilterList = fullFilterList.FindAll(rules => rules.TargetEntityId == $"{theEntity.Id}");
            if (!fullFilterList.Any())
                return null;

            DataTable entityTable = theEntity.ConvertToDataTable();
            foreach (var rule in fullFilterList)
            {
                if (ruleEvaluation == RuleEvaluation.Both)
                {
                    if (!EvaluateRule(entityTable, null, rule))
                        return rule;
                    if (!EvaluateRule(null, entityTable, rule))
                        return rule;
                }
                else
                {
                    if (!EvaluateRule(entityTable, null, rule))
                        return rule;
                }

            }
            return null;
        }

        /// <summary>
        /// Evaluate The source entity against the target entity only.
        /// This does not include self evaluating.
        /// swapping the source and target will do the reverse check.
        /// </summary>
        /// <param name="entityRules">This can be a list of all the rules or an already filtered list, the rules will be checked again.</param>
        /// <param name="sourceEntity">The entity that acts as the source for the rule</param>
        /// <param name="targetEntity">The entity that acts as the target for the rule.</param>
        /// <param name="ruleApplicator">The event or process the event is applied to.</param>
        /// <returns>Returns the rule that was matched or null if nothing matched</returns>
        public IJarsRule EvaluateSourceAgainstTarget(List<IJarsRule> entityRules, IEntityBase sourceEntity, IEntityBase targetEntity, RuleRunsOn ruleApplicator)
        {
            var fullTargetFilterList = entityRules.FindAll(r =>
            r.SourceTypeName == sourceEntity.GetType().Name &&
            r.TargetTypeName == targetEntity.GetType().Name &&
            r.TargetCriteriaString.Contains($"[{nameof(targetEntity.Id)}] = {targetEntity.Id}") &&
            r.RuleRunsOn.Contains(ruleApplicator.ToString()));

            if (!fullTargetFilterList.Any())
                return null;

            //get the first matching rule
            DataTable sourceTable = sourceEntity.ConvertToDataTable();
            DataTable targetTable = targetEntity.ConvertToDataTable();
            foreach (var rule in fullTargetFilterList)
            {
                //test the source table
                if (!EvaluateRule(sourceTable, targetTable, rule))
                {
                    return rule;
                }
            }
            return null;
        }

        /// <summary>
        /// Evaluate The source entity against the target entity type.
        /// This is for rules that are set against an entity type rather than a specific entity.
        /// This does not include self evaluating.
        /// swapping the source and target will do the reverse check.
        /// </summary>
        /// <param name="entityRules">This can be a list of all the rules or an already filtered list, the rules will be checked again.</param>
        /// <param name="sourceEntity">The entity that acts as the source for the rule</param>
        /// <param name="targetEntity">The entity that acts as the target for the rule.</param>
        /// <param name="ruleApplicator">The event or process the event is applied to.</param>
        /// <returns>Returns the rule that was matched or null if nothing matched</returns>
        public IJarsRule EvaluateSourceAgainstTargetType(List<IJarsRule> entityRules, IEntityBase sourceEntity, IEntityBase targetEntity, RuleRunsOn ruleApplicator)
        {
            var fullTargetFilterList = entityRules.FindAll(r =>
            r.TargetTypeName == targetEntity.GetType().Name &&
            r.SourceTypeName == sourceEntity.GetType().Name &&
            r.RuleRunsOn.Contains(ruleApplicator.ToString()) &&
            !r.TargetCriteriaString.Contains($"[{nameof(targetEntity.Id)}] = {targetEntity.Id}"));
            //does the source rules meet the target conditions?
            var specificFilterList = fullTargetFilterList.FindAll(rules => string.IsNullOrEmpty(rules.TargetCriteriaString));
            if (!specificFilterList.Any())
                return null;

            DataTable sourceTable = sourceEntity.ConvertToDataTable();
            DataTable targetTable = sourceEntity.ConvertToDataTable();
            foreach (var rule in specificFilterList)
            {
                if (!EvaluateRule(sourceTable, targetTable, rule))
                {
                    return rule;
                }
            }
            return null;
        }

        /// <summary>
        /// This rule check if the source entity has any list properties that contain types that has rules set against the target entity.
        /// ie. a Job with a sub job line that has restrictions on the target.
        /// </summary>
        /// <param name="entityRules">This can be a list of all the rules or an already filtered list, the rules will be checked again.</param>
        /// <param name="sourceEntity">The entity that acts as the source for the rule</param>
        /// <param name="targetEntity">The entity that acts as the target for the rule.</param>
        /// <param name="ruleApplicator">The event or process the event is applied to.</param>
        /// <returns></returns>
        public IJarsRule EvaluateSourceChildListsAgainstTarget(List<IJarsRule> entityRules, IEntityBase sourceEntity, IEntityBase targetEntity, RuleRunsOn ruleApplicator)
        {
            var sourceDictionaryListTypes = sourceEntity.GetGenericListTypesDictionary();

            //get the list of possible rules where the target type is the type of the key value.
            //https://dotnetable.wordpress.com/2015/06/20/find-all-items-in-list-which-exist-in-another-list-using-linq/
            //this can be seen as retrieve all the values from entityRules which also exist in dictionary values.
            var filteredRules = (from er in entityRules
                                 join kVals in sourceDictionaryListTypes.Values
                                 on er.SourceTypeName equals kVals.Name
                                 select er).Where(r => r.RuleRunsOn.Contains(ruleApplicator.ToString())).ToList();

            if (!filteredRules.Any())
                return null;
            /*
             
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(t);
            var instance = Activator.CreateInstance(constructedListType);
             
             */
            IJarsRule failRule = null;
            //test each collection
            foreach (var dictionaryKey in sourceDictionaryListTypes)
            {
                //var itemsListType = typeof(IList<>);
                //var defaultList = itemsListType.MakeGenericType(dictionaryKey.Value);

                var sourceItems = (IList)sourceEntity.GetType()
                    .GetProperty(dictionaryKey.Key)
                    .GetValue(sourceEntity);
                //test the property type first                
                foreach (var subItem in sourceItems)
                {
                    //test broad
                    failRule = EvaluateSourceAgainstTargetType(filteredRules, subItem as IEntityBase, targetEntity, ruleApplicator);
                    if (failRule != null)
                        return failRule;

                    //test narrow
                    failRule = EvaluateSourceAgainstTarget(filteredRules, subItem as IEntityBase, targetEntity, ruleApplicator);
                    if (failRule != null)
                        return failRule;
                }
            }
            return null;
        }

        /// <summary>
        /// Process the rule criteria on the data provided.
        /// If this method return true the rule was met.
        /// </summary>
        /// <param name="sourceDataToTest">The data that will be used when testing the criteria string</param>
        /// <param name="entityRule">the criteria that will be used to determine if the condition is true or false</param>
        /// <returns>If true then the rule was met and the condition passed, if false the rule failed and the condition was not met.</returns>
        public bool EvaluateRule(DataTable sourceDataToTest = null, DataTable targetDataToTest = null, IJarsRule entityRule = null)
        {
            bool result = true;
            int sourceCount = 0, targetCount = 0, lowValue = 0;

            if (entityRule == null)
                return result;

            try
            {
                if ((sourceDataToTest != null && targetDataToTest != null) && entityRule.RuleEvaluation == RuleEvaluation.Both)
                {
                    sourceDataToTest.DefaultView.RowFilter = CriteriaToWhereClauseHelper.GetDataSetWhere(CriteriaOperator.Parse(entityRule.SourceCriteriaString));
                    targetDataToTest.DefaultView.RowFilter = CriteriaToWhereClauseHelper.GetDataSetWhere(CriteriaOperator.Parse(entityRule.TargetCriteriaString));

                    if (sourceDataToTest.DefaultView.Count > 0)
                        sourceCount = 1;
                    if (targetDataToTest.DefaultView.Count > 0)
                        targetCount = 1;

                    lowValue = 1; //starts at 1 because both conditions needs to be met.
                }

                if (sourceDataToTest != null && targetDataToTest == null && entityRule.RuleEvaluation == RuleEvaluation.SourceOnly)
                {
                    sourceDataToTest.DefaultView.RowFilter = CriteriaToWhereClauseHelper.GetDataSetWhere(CriteriaOperator.Parse(entityRule.SourceCriteriaString));
                    sourceCount = sourceDataToTest.DefaultView.Count;
                }

                if (targetDataToTest != null && sourceDataToTest == null && entityRule.RuleEvaluation == RuleEvaluation.TargetOnly)
                {
                    targetDataToTest.DefaultView.RowFilter = CriteriaToWhereClauseHelper.GetDataSetWhere(CriteriaOperator.Parse(entityRule.TargetCriteriaString));
                    targetCount = targetDataToTest.DefaultView.Count;
                }

                if ((sourceCount + targetCount) > lowValue)//there was a match found, so the entity matches the rule (pass)
                {
                    //unless the rule needs to fail to pass                
                    if (entityRule.RulePassesWhen == RulePassesWhen.IsFalse)
                        result = true;
                }
                else//there are no matches, so the rule fails..
                {
                    result = false;
                    //if the rule needs to be false to pass we swap the values.
                    if (entityRule.RulePassesWhen == RulePassesWhen.IsFalse)
                        result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                result = false;
#if DEBUG
                throw ex;
#endif
            }
            return result;
        }

    }
}
