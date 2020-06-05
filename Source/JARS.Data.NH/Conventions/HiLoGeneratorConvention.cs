using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using JARS.Core;
using NHibernate.Dialect;
using NHibernate.Id;
using NHibernate.Mapping;
using NHibernate.SqlTypes;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace JARS.Data.NH.Conventions
{
    /// <summary>
    /// This class is used for generating HiLo values for the ID fields within the entities.
    /// It generates the HiLo values automatically as soon as the mapping calls the Id(x=> x.id) method.
    /// The result is that if the Id(x=>x.id) has additional fluent properties added to it ie. Id(x=> x.id).GeneratedBy.xxxxxx those 
    /// properties will be used and this HiLo generator will be ignored. 
    /// </summary>
    public class HiLoGeneratorConvention : IIdConvention, IIdConventionAcceptance
    {
        public const string EntityTableColumnName = "entity_table"; //the name of the column the entity name will be stored in the HiLo table
        public const string MaxLo = "100"; // the default max_lo value used for Id generation
        public const string HiLoTableName = "HiLoIDs";

        /// <summary>
        /// This checks if the Identity property is mapped and if it is a int type (HiLo only works for int types)
        /// </summary>
        /// <param name="criteria"></param>
        public void Accept(IAcceptanceCriteria<IIdentityInspector> criteria)
        {
            try
            {
                criteria
                .Expect(x => x.Type == typeof(int) || x.Type == typeof(uint) || x.Type == typeof(long) || x.Type == typeof(ulong)) // HiLo only works with integral types
                .Expect(x => x.Generator.EntityType == null); // Specific generator has not been mapped
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
#if DEBUG
                throw ex;
#endif
            }
        }

        /// <summary>
        /// This method applies the HiLo id generation automatically to any identity fields that does not have the .GeneratedBy.xxxx set
        /// it also adds the criteria on the sql builder to generate ids per entity.
        /// </summary>
        /// <param name="instance"></param>
        public void Apply(IIdentityInstance instance)
        {
            try
            {
                instance.GeneratedBy.HiLo(HiLoTableName, TableGenerator.DefaultColumnName, MaxLo,
                                      builder => builder.AddParam(TableGenerator.Where, $" {EntityTableColumnName} = '{instance.EntityType.FullName}'"));//.EntityType.FullName)));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
#if DEBUG
                throw ex;
#endif
            }
        }

        /// <summary>
        /// this method is used generate the sql for adding entity column and entity values to the nhibernate_identity_table 
        /// </summary>
        /// <param name="config"></param>
        public static void CreateHighLowScript(NHibernate.Cfg.Configuration config)
        {
            try
            {
                var dialect = Activator.CreateInstance(Type.GetType(config.GetProperty(NHibernate.Cfg.Environment.Dialect))) as Dialect;
                var createScript = new StringBuilder();

                createScript.AppendFormat("DELETE FROM {0};", HiLoTableName);
                createScript.AppendLine();
                createScript.AppendFormat("ALTER TABLE {0} {1} {2} {3} NOT NULL;", HiLoTableName, dialect.AddColumnString, EntityTableColumnName, dialect.GetTypeName(SqlTypeFactory.GetAnsiString(128)));
                createScript.AppendLine();
                createScript.AppendFormat("CREATE NONCLUSTERED INDEX IX_{0}_{1} ON {0} ({1} ASC);", HiLoTableName, EntityTableColumnName);
                createScript.AppendLine();
                if (dialect.SupportsSqlBatches)
                {
                    createScript.AppendLine("GO");
                    createScript.AppendLine();
                }
                foreach (var entityName in config.ClassMappings.Select(m => m.EntityName).Distinct())
                {
                    createScript.AppendFormat("INSERT INTO [{0}] ({1}, {2}) VALUES ('{3}',1);", HiLoTableName, EntityTableColumnName, TableGenerator.DefaultColumnName, entityName);
                    createScript.AppendLine();
                }
                if (dialect.SupportsSqlBatches)
                {
                    createScript.AppendLine("GO");
                    createScript.AppendLine();
                }

                config.AddAuxiliaryDatabaseObject(new SimpleAuxiliaryDatabaseObject(createScript.ToString(), null));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
#if DEBUG
                throw ex;
#endif
            }
        }
    }
}
