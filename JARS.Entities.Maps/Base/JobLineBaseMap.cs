using FluentNHibernate.Mapping;
using JARS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Entities.Maps
{
    /// <summary>
    /// This class maps the base properties for jobs in the database. </br>
    /// !!! It does not contain the list of items linked to it ie. SOR items!!!</br>
    /// Any class that inherits from this class will have to implement its own map.    
    /// </summary>
    public class JobLineBaseMap : ClassMap<JarsJobLineBase>
    {
        public JobLineBaseMap()
        {
            string simpleClassName = $"{nameof(JarsJobLineBase)}".Replace("Base", "");
            Table($"{simpleClassName}s");
            //the column used to identify extended class key
            DiscriminateSubClassesOnColumn("JTF"); // Job Type Filter
            //specifies lazy loading
            LazyLoad();

            //persistent fields
            Id(x => x.Id)
                .GeneratedBy
                .Identity();

            //audit mapping
            Map(x => x.CreatedDate);
            Map(x => x.CreatedBy);
            Map(x => x.ModifiedDate);
            Map(x => x.ModifiedBy);
            
            //other properties
            Map(x => x.ExternalJobRef);
            
            Map(x => x.LineNum);
            Map(x => x.LineCode);
            Map(x => x.ShortDescription);
            Map(x => x.FullDescription);
            Map(x => x.OriginalQty);
            Map(x => x.AllocatedQty);
            Map(x => x.RevisedQty);
            Map(x => x.CompletedQty);
            Map(x => x.RemainingQty);
            Map(x => x.TotalQtyCompleted);
            Map(x => x.IsShared);
            Map(x => x.ProcessStatus);
            Map(x => x.IntegrationStatus);
            Map(x => x.Uom);
            Map(x => x.PayRate);

            References(x => x.Resource)
                .Not.LazyLoad()
                .ForeignKey();
                //.Cascade.SaveUpdate()//<-- this should not be required as we don't want to create a new op if one has not been assigned.
                //this assignment will be done and checked somewhere else.

            HasMany(x => x.Splits)
                .KeyColumn($"{simpleClassName}Id")
                .Not.LazyLoad()
                .Cascade.All();//<-- we want this as lines might not exist and we want the parent (the line) to manage the splits.

            //extended values discriminator
           
            /*
             * need to revisit composite keys to see how they work and if they will work in this scenario
               CompositeId()
               .KeyProperty(x => x.Job.ExtID)
               .KeyReference(x => x.Operative.ID);
            */
        }
    }
}
