using FluentNHibernate.Mapping;

namespace JARS.Entities.Maps
{
    public class JobAttachmentMap : ClassMap<JarsJobAttachment>
    {
        public JobAttachmentMap()
        {
            Table($"{typeof(JarsJobAttachment).Name}s");
            Id(x => x.Id)
                .GeneratedBy
                .Identity();

            Map(x => x.Name);
            Map(x => x.AttachmentData);
            Map(x => x.TimeAttached);
            //Map(x => x.JobId);
            //audit records
            //Map(x => x.RecordCreatedOn);
            //Map(x => x.RecordCreatedBy);
            //Map(x => x.RecordModifiedOn);
            //Map(x => x.RecordModifiedBy);

            //add the reference to the job the one to many side of the relationship
            //References(x => x.JobID).Column("JobID")
            //    .ForeignKey();
        }
    }
}
