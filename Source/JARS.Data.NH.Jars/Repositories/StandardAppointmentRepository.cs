using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IStandardAppointmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StandardAppointmentRepository : DataRepositoryNhEagerlyBase<StandardAppointment>, IStandardAppointmentRepository
    {
        [ImportingConstructor()]
        public StandardAppointmentRepository(IDataContextNhJars context) : base(context)
        { }
    }
}