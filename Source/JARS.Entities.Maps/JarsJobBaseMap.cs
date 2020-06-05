using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{
    /// <summary>
    /// This class will maps to the 'lazy' entity of the jars job, it will get the information without any collections of 
    /// child linked entities.
    /// </summary>
    public class JarsJobBaseMap : SubclassMap<JarsJobBase>
    {
        public JarsJobBaseMap()
        {
            Extends<JobBaseMap>();
            DiscriminatorValue("JARS");
            DynamicUpdate();

            //mapping for additional properties.
           
        }
    }
}
