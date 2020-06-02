using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IJobAttachmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JobAttachementRepository : DataRepositoryNhCrudBase<JarsJobAttachment>, IJobAttachmentRepository
    {
        [ImportingConstructor()]
        public JobAttachementRepository(IDataContextNhJars DbContext) : base(DbContext)
        {
        }
    }
}
