using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{
    public class JarsJobMap : SubclassMap<JarsJob>//JobBaseMap<JarsJob>//
    {
        public JarsJobMap()
        {            
            string simpleClassName = "Job";
            //Polymorphism.Explicit();
            
            Extends<JobBaseMap>();

            Map(x => x.AssignedBy);

            DiscriminatorValue("JARS");//<-- very important, if not in class, or differs from base, the object will return as null from database.

            HasMany(x => x.Attachments)
                .KeyColumn($"{simpleClassName}Id")                
                .Cascade
                .All();
                //.Not.LazyLoad()

            HasMany(x => x.JobLines)
                .KeyColumn($"{simpleClassName}Id")                
                .Cascade
                .SaveUpdate();
                //.Not.LazyLoad()
        }
    }
}
