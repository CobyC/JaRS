using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{
    public class JarsJobLineMap : SubclassMap<JarsJobLine>
    {
        public JarsJobLineMap()
        {
            DiscriminatorValue("JARS");
            Extends<JobLineBaseMap>();
        }
    }
}
