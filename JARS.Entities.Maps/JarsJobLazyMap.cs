using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{
    /// <summary>
    /// This mapping represents the mapping for the lazy class, it also extends (needs to extend) from the JarsJobBase mapping, the same way the JarsJob (full entity) does.
    /// This mapping and entity is created to help with the polymorphic behaviour of NH where NH casts the base class into the super class of it has an inherited structure.    
    /// </summary>
    public class JarsJobLazyMap : SubclassMap<JarsJobLazy>
    {
        //the mapping implementation will be the sme as the JarsJob mapping, but without the collection mappings (as they are not available in the lazy entity)
        public JarsJobLazyMap()
        {
            Extends<JarsJobBaseMap>();
            //we need to make sure all the discriminator values are the same for related entities (ie base, full, lazy classes).
            DiscriminatorValue("JARS");//<-- very important, if not in class, or differs from base, the entity will return a null entity from the database.
        }
    }
}
