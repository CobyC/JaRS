using JARS.Core.Exceptions;
using JARS.Core.Interfaces.Repositories;
using JARS.Data.NH.Interfaces;

namespace JARS.Data.NH.Repositories
{
    /// <summary>
    /// This is the abstract base repository class.
    /// It has a property DBContext that holds the connection to the database.
    /// </summary>
    public abstract class DataRepositoryNhBase : IDataRepositoryBase
    {
        public DataRepositoryNhBase()
        { }

        public DataRepositoryNhBase(IDataContextBaseNh context) : base()
        {
            _DBContext = context;
        }


        private IDataContextBaseNh _DBContext;
        /// <summary>
        /// The DataContext that will be used for data manipulation.
        /// The type of data context is determined by the TContextInterface that is supplied by the implementing class.
        /// </summary>
        public virtual IDataContextBaseNh DBContext
        {
            get
            {
                if (_DBContext == null)
                    throw new NotFoundException($"The Data Base Context has no value assigned, make sure to pass a value through the constructor.");

                return _DBContext;
            }
            private set { _DBContext = value; }

        }
    }
}
