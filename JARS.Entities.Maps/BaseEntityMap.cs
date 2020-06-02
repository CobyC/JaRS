using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.MappingModel;

namespace JARS.Entities.Maps
{
    public abstract class BaseEntityMap : ClassMap<BaseEntity>
    {
        
        public BaseEntityMap()
        {
            // indicates that this class is the base
            // one for the class inheritance strategy and that 
            // the values of its properties should
            // be united with the values of derived classes
            UseUnionSubclassForInheritanceMapping();
            //base properties
            Id(x => x.ID);//.GeneratedBy.HiLo("10"); //<-- add the id to each map individually for the HiLo generation to work correctly. 
            Map(x => x.RecordCreatedOn);
            Map(x => x.RecordCreatedBy);
            Map(x => x.RecordModifiedOn);
            Map(x => x.RecordModifiedBy);
            //other properties
        }

        protected BaseEntityMap(AttributeStore attributes, MappingProviderStore providers) : base(attributes, providers)
        {
        }
    }
}
