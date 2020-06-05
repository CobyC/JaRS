using FluentNHibernate.Mapping;
using JARS.Core.Rules;

namespace JARS.Entities.Maps
{
    public class JarsRuleMap : ClassMap<JarsRule>
    {
        public JarsRuleMap()
        {
            Table($"{typeof(JarsRule).Name}s");
            //specifies lazy loading
            //LazyLoad();
            Id(x => x.Id)
                .GeneratedBy
                .Identity();

            //audit fields
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.SourceCriteriaString);
            Map(x => x.SourceTypeName);
            Map(x => x.TargetCriteriaString);
            Map(x => x.TargetTypeName);
            Map(x => x.RulePassesWhen);
            Map(x => x.RuleRunsOn);
            Map(x => x.RuleEvaluation);
        }
    }
}
