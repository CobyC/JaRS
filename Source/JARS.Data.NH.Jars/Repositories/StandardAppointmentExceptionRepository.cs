using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IStandardAppointmentExceptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StandardAppointmentExceptionRepository : DataRepositoryNhCrudBase<StandardAppointmentException>, IStandardAppointmentExceptionRepository
    {
        [ImportingConstructor()]
        public StandardAppointmentExceptionRepository(IDataContextNhJars DbContext) : base(DbContext)
        {
        }
    }
}
