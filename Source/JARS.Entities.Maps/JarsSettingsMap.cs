using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{
    public class JarsSettingsMap : ClassMap<JarsSetting>
    {
        public JarsSettingsMap()
        {
            Table($"{typeof(JarsSetting).Name}s");
            LazyLoad();

            Id(x => x.Id)
                .GeneratedBy
                .Identity();
            
            Map(x => x.Platform);
            Map(x => x.PartName);
            Map(x => x.SettingData).Length(int.MaxValue);
        }

    }
}
