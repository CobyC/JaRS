using JARS.Core.Interfaces.Rules;
using JARS.Core.Rules;
using JARS.Data.FakeData;
using JARS.Entities;
using JARS.Rules.Plugins.Processors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace JARS.Test.RulesLogic
{
    [TestClass]
    public class TestEntityRules
    {
        private EntityBaseRuleProcessor _validator;
        public EntityBaseRuleProcessor Validator
        {
            get
            {
                if (_validator == null)
                {
                    _validator = new EntityBaseRuleProcessor();
                }
                return _validator;
            }
            set => _validator = value;
        }

        JarsJob SourceEntity = FakeDataHelper.FakeJarsJobs[0];
        JarsResource TargetEntity = FakeDataHelper.FakeResources[0];


        List<IJarsRule> EntityRules = new List<IJarsRule>();

        [TestInitialize]
        public void SetupDefaultRules()
        {

            JarsRule jobToResSingleRule = new JarsRule
            {
                TargetCriteriaString = "StartsWith([Location], '10') And [LineOfWorkCode] = 'PRO'",
                Description = "source 2 target single",
                Id = 1,
                RulePassesWhen = RulePassesWhen.IsTrue,                
                SourceTypeName = "JarsJob",
                SourceCriteriaString = "",
                TargetTypeName = "Resource",
                
            };

            JarsRule resToResGlobalRule = new JarsRule
            {
                TargetCriteriaString = "[IsMobileResource] = True And [IsActive] = True",
                Description = "target 2 target global",
                Id = 2,
                RulePassesWhen = RulePassesWhen.IsTrue,               
                SourceTypeName = "Resource",
                TargetTypeName = "Resource",
                SourceCriteriaString = null
            };

            JarsRule resToRes2GlobalRule = new JarsRule
            {
                TargetCriteriaString = "[DayEndTime] > #12:00:00#",
                Description = "target 2 target global",
                Id = 2,
                RulePassesWhen = RulePassesWhen.IsTrue,                
                SourceTypeName = "Resource",
                TargetTypeName = "Resource",
                SourceCriteriaString = ""
            };

            JarsRule jobToResGlobal = new JarsRule
            {
                TargetCriteriaString = "[TargetDate] > #2019-10-24#",
                Description = "source 2 target global",
                Id = 3,
                RulePassesWhen = RulePassesWhen.IsTrue,                
                SourceTypeName = "Resource",
                TargetTypeName = "JarsJob",
                SourceCriteriaString = null
            };

            JarsRule jobOnJobGlobal = new JarsRule
            {
                TargetCriteriaString = "[ProgressStatus] = 'COMP'",
                Description = "source 2 source global",
                Id = 4,
                RulePassesWhen = RulePassesWhen.IsFalse,                
                SourceTypeName = "JarsJob",
                TargetTypeName = "JarsJob",
                SourceCriteriaString = null
            };

            //also add rules of entities that are not directly related i.e. lineson a job, or skills on a resource.

            JarsRule jobLineOnResourceGlobal = new JarsRule
            {
                TargetCriteriaString = "[LineCode] = 'CJARS1'",
                Description = "job line on resource",
                Id = 4,
                RulePassesWhen = RulePassesWhen.IsFalse,                
                SourceTypeName = "JarsJobLine",
                TargetTypeName = "Resource",
                SourceCriteriaString = null
            };


            EntityRules.AddRange(new[] { jobToResSingleRule,
                resToResGlobalRule,
                resToRes2GlobalRule,
                jobToResGlobal,
                jobOnJobGlobal,
            jobLineOnResourceGlobal});
        }


        [TestMethod]
        public void Test_Evaluate_Source_On_Source_Rule()
        {
            IJarsRule rule = null;
            //is there a rule specifically linked to the entity passed in
            rule = Validator.EvaluateEntityOwnRules(EntityRules, SourceEntity,RuleRunsOn.OnDragDrop);
            Assert.IsNull(rule);
            //are there any rules that are not specific but does not prevent the continuation of the work
            rule = Validator.EvaluateEntityOwnRules(EntityRules, SourceEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNull(rule);

            //set the value so the rule is met
            SourceEntity.LineOfWork = "ANY";
            rule = Validator.EvaluateEntityOwnRules(EntityRules, SourceEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNotNull(rule);

            //does the source meet a global rule?
            SourceEntity.ProgressStatus = "COMP";
            rule = Validator.EvaluateEntityOwnRules(EntityRules, SourceEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNotNull(rule);
        }

        [TestMethod]
        public void Test_Evaluate_Target_On_Target_Rule()
        {
            IJarsRule rule = null;
            //evaluate target against itself 
            rule = Validator.EvaluateEntityOwnRules(EntityRules, TargetEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNull(rule);

            //evaluate target agains a global rule set agains the same type
            rule = Validator.EvaluateEntityOwnRules(EntityRules, TargetEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNull(rule);

            //make sure the rule fails because it meets a criteria
            TargetEntity.DayEndTime = TimeSpan.FromHours(11.00);
            rule = Validator.EvaluateEntityOwnRules(EntityRules, TargetEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNotNull(rule);

            //make sure the target fails because of a global criteria
            TargetEntity.IsMobileResource = false;
            rule = Validator.EvaluateEntityOwnRules(EntityRules, TargetEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNotNull(rule);
        }

        [TestMethod]
        public void Test_Evaluate_Source_On_Target_And_Target_on_Source_Only_Rule()
        {
            IJarsRule rule = null;
            rule = Validator.EvaluateSourceAgainstTarget(EntityRules, SourceEntity, TargetEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNull(rule);

            //swop the source and target to match the other way round
            rule = Validator.EvaluateSourceAgainstTarget(EntityRules, TargetEntity, SourceEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNull(rule);

            //make sure it fails
            SourceEntity.LineOfWork = "XYZ";
            rule = Validator.EvaluateSourceAgainstTarget(EntityRules, SourceEntity, TargetEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNotNull(rule);

            //there is no rule the other way around so this should pass because it tests specific rules
            TargetEntity.DayEndTime = TimeSpan.FromHours(11.59);
            rule = Validator.EvaluateSourceAgainstTarget(EntityRules, TargetEntity, SourceEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNull(rule);
        }

        [TestMethod]
        public void Test_Evaluate_Source_Sub_ListType_On_Target_Rule()
        {
            IJarsRule rule = null;
            //original rule can not pass if jobline code is LINE1
            rule = Validator.EvaluateSourceChildListsAgainstTarget(EntityRules, SourceEntity, TargetEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNotNull(rule);

            //use a job where line is not LINE1
            SourceEntity.JobLines[0].LineCode = "Line0";
            rule = Validator.EvaluateSourceChildListsAgainstTarget(EntityRules, SourceEntity, TargetEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNull(rule);
        }

        [TestMethod]
        public void Test_Evaluate_All_Rule()
        {
            IJarsRule rule = null;
            rule = Validator.EvaluateEntityRules(EntityRules, SourceEntity, TargetEntity, RuleRunsOn.OnDragDrop);
            Assert.IsNull(rule);
        }

        [TestMethod]
        public void TestEntityRulesAgainstRules()
        {

            ////fails on job not being WRK
            //IEntityRule f1Rule = Validator.EvaluateEntityAllRules(EntityRules, SourceEntity, TargetEntity);
            //Assert.IsNotNull(f1Rule);

            ////fails on resource not mobile
            //IEntityRule f2Rule = Validator.EvaluateEntityAllRules(EntityRules, SourceEntity, TargetEntity);
            //Assert.IsNotNull(f2Rule);

            ////fails on target date being in the past
            //IEntityRule f3Rule = Validator.EvaluateEntityAllRules(EntityRules, SourceEntity, TargetEntity);
            //Assert.IsNotNull(f3Rule);

            //sourceEntity.LineOfWorkCode = "WRK";
            //targetEntity.IsMobileResource = true;
            //sourceEntity.TargetDate = DateTime.Now.AddDays(2);
            ////passes
            //IEntityRule p1Rule = Validator.EvaluateEntityAllRules(EntityRules, sourceEntity, targetEntity);
            //Assert.IsNull(p1Rule);

        }
    }
}
