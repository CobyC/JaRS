using FluentNHibernate.Mapping;

namespace JARS.Entities.Maps
{
    public class JarsResourceSkillMap : ClassMap<JarsResourceSkill>
    {
        public JarsResourceSkillMap()
        {
            Table($"{typeof(JarsResourceSkill).Name}s");
            LazyLoad();
            Id(x => x.Id)
                .GeneratedBy
                .Identity();
            //audit mapping
            //Map(x => x.RecordCreatedOn);
            //Map(x => x.RecordCreatedBy);
            //Map(x => x.RecordModifiedOn);
            //Map(x => x.RecordModifiedBy);
            //class property mapping
            Map(x => x.MaxLevel);
            Map(x => x.CurrentLevel);
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            Map(x => x.Description);
            Map(x => x.DocumentCode);
            //the reference to the owning operative/resource
            References(x => x.Resource).ForeignKey();//the skill belongs to one operative/resource
        }
    }
}
