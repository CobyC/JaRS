using JARS.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace JARS.Core.Extensions
{
    public static class ObjectToDataTableExtension
    {
        /// <summary>
        /// convert an IList to a DataTable (this is particularly useful for building filter criteria using the DevExpress filter utility.
        /// </summary>
        /// <typeparam name="T">The entity type that ill be represented in the table</typeparam>
        /// <param name="list">the list of items that will fill the rows</param>
        /// <returns>the data table containing the records.</returns>
        public static DataTable ConvertToDataTable<T>(this IList<T> list) where T : IEntityBase
        {
            DataTable table = new DataTable();
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

                foreach (PropertyDescriptor prop in properties)
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

                foreach (T item in list)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                    table.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
#if DEBUG
                throw ex;
#endif
            }

            return table;
        }

        /// <summary>
        /// This method turns the entity into a DataTable structure.
        /// Each property represents a column and the row contains the values.
        /// This then provides a way to apply a criteria to the entity via a filter (DevExpress Criteria).
        /// The DevExpress criteria turns the time span into decimals (of seconds) this conversion will also do the same.
        /// To disable the conversion set the ConvertTimeSpan to false.
        /// </summary>
        /// <param name="entity">The entity that will be turned into a table</param>
        /// <param name="convertTimeSpan">This value is true by default</param>
        /// <returns>A data table that represent the entity</returns>
        public static DataTable ConvertToDataTable(this object entity, bool convertTimeSpan = true)
        {
            DataTable table = new DataTable();
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entity.GetType());
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    var originalType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    Type columnType = originalType;

                    //convert time spans to decimals (DevEx error)
                    if (convertTimeSpan)
                        if (originalType == typeof(TimeSpan))
                            columnType = typeof(double);

                    var tableColumn = new DataColumn(prop.Name, columnType);
                    table.Columns.Add(tableColumn);

                    var originalVal = prop.GetValue(entity) ?? DBNull.Value;
                    if (convertTimeSpan)
                    {
                        if (originalType == typeof(TimeSpan) && originalVal != DBNull.Value)
                        {
                            double doubleVal = ((TimeSpan)originalVal).TotalSeconds;
                            row[prop.Name] = doubleVal;
                        }
                        else
                            row[prop.Name] = originalVal;
                    }
                    else
                        row[prop.Name] = originalVal;
                }
                
                table.Rows.Add(row);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
#if DEBUG
                throw ex;
#endif
            }

            return table;
        }

        /// <summary>
        /// Create an empty DataTable instance from the type passed as parameter.
        /// The properties of the type passed in will be used as column headers.
        /// The DataTable will always have only one row with null values.
        /// The purpose of the DataTable is to assist in setting up criteria for the processing of rules.
        /// </summary>
        /// <param name="type">the type that will be represented as DataTable object</param>
        /// <returns>The data table object containing the colum headers matching the properties in the type.</returns>
        public static DataTable CreatePropertiesDataTableFromType(this Type type)
        {
            //crete a data table from the interface
            DataTable table = new DataTable();
            try
            {
                List<PropertyDescriptor> propList = new List<PropertyDescriptor>();
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
                if (properties.Count == 0)
                {
                    //try and get the inherited type properties
                    Type[] subInterfaces = type.GetInterfaces();
                    if (subInterfaces.Length > 0)
                        for (int i = 0; i < subInterfaces.Length; i++)
                        {
                            PropertyDescriptorCollection subProperties = TypeDescriptor.GetProperties(subInterfaces[i]);
                            foreach (PropertyDescriptor prop in subProperties)
                                propList.Add(prop);
                        }
                }
                else
                    foreach (PropertyDescriptor pDesc in properties)
                        propList.Add(pDesc);

                foreach (PropertyDescriptor prop in propList)
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = DBNull.Value;

                table.Rows.Add(row);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
#if DEBUG
                throw ex;
#endif
            }
            return table;
        }

    }
}
