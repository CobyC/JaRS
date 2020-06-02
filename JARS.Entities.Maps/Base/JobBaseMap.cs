using FluentNHibernate.Mapping;
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
    public class JobBaseMap : ClassMap<JarsJobBase>//<T> : ClassMap<T> where T : JobBase
    {
        public string TableName { get { return $"{typeof(JarsJobBase).Name}s".Replace("Base", ""); } }
        public JobBaseMap()
        {
            Table(TableName);
            //the column used to identify extended class key
            DiscriminateSubClassesOnColumn("JTF"); //Job Type Filter                        
            //specifies lazy loading
            LazyLoad();
            //set polymorph
            Polymorphism.Explicit();

            //persistent fields
            Id(x => x.Id)
                .GeneratedBy
                .Identity();

            //audit fields
            Map(x => x.CreatedDate);
            Map(x => x.CreatedBy);
            Map(x => x.ModifiedDate);
            Map(x => x.ModifiedBy);

            //default properties needed to create an appointment
            Map(x => x.ExtRefId);
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            Map(x => x.StatusKey);
            Map(x => x.LabelKey);
            Map(x => x.LineOfWork);
            Map(x => x.Description);
            Map(x => x.Location);
            Map(x => x.TargetDate);
            Map(x => x.Priority);

            //integration properties
            Map(x => x.ProgressStatus);
            Map(x => x.CompletionDate);
            Map(x => x.IntegrationStatus);
            Map(x => x.IntegrationDate);
            Map(x => x.IntegrationMessage);

            //reference to the operative/resource that owns this job.
            //References(x => x.Resource).Cascade.SaveUpdate();
        }

    }
}
