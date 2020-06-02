namespace JARS.Core.Data.Interfaces.DataContext
{
    public interface IManageDatabase: IDataContextBase
    {       
        /// <summary>
        /// Used for Populating the database with default data.
        /// </summary>
        void PopulateDefaultData();

        /// <summary>
        /// WARNING!!! This will drop the database and all the data in it.
        /// </summary>
        void DropDatabaseTables();

        /// <summary>
        /// This will update the database with changes made to the mappings.
        /// It will only update tables and columns that can be altered.
        /// </summary>
        void UpdateDatabaseTableSchemas();

        /// <summary>
        /// This schema should be used if the database does not exist and it is a new build.
        /// </summary>
        void CreateDatabaseTableSchemas();
    }
}