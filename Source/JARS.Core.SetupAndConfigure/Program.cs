using JARS.Business.Bootstrap;
using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Interfaces.Repositories;
using JARS.Data.NH.Interfaces;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;

namespace JARS.Core.SetupAndConfigure
{
    class Program
    {
        static IDataContextBaseNh context;
        static IDataRepositoryFactory repositoryFactrory;
        static void Main(string[] args)
        {
            JarsCore.Container = MEFBusinessLoader.Init();
            repositoryFactrory = JarsCore.Container.GetExport<IDataRepositoryFactory>().Value;

            ChoseDataContext();

            bool exit = false;
            while (!exit)
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("");
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("-----------  Setup   ----------");
                sb.AppendLine($" Using Context:{context.GetType().Name}");
                sb.AppendLine(" Please select an option.    ");
                sb.AppendLine("                             ");
                sb.AppendLine(" Change Data Context (C)     ");
                sb.AppendLine(" New Clean Setup (N)         ");
                sb.AppendLine(" Update Table Structures (U) ");
                sb.AppendLine(" Delete All Tables (D)       ");
                sb.AppendLine(" Populate Default Data (P)   ");
                sb.AppendLine(" Close Window (X)            ");
                sb.AppendLine(" ");
                sb.AppendLine("Type Letter and Press Enter:");

                Console.Write(sb.ToString());
                string choice = Console.ReadLine();
                Console.Clear();
                try
                {

                    switch (choice.ToLower())
                    {
                        case "x":
                            exit = true;
                            break;
                        case "c":
                            ChoseDataContext();
                            exit = false;
                            break;
                        case "n":
                            BuildDatabase(IsNewBuild: true);
                            exit = false;
                            break;
                        case "u":
                            BuildDatabase(IsUpdate: true);
                            exit = false;
                            break;
                        case "d":
                            DeleteDatabase();
                            exit = false;
                            break;
                        case "p":
                            PopulateDatabase();
                            exit = false;
                            break;
                        default:
                            exit = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine("");
                    }
                }
            }
        }

        private static void ChoseDataContext()
        {

            IList<ComposablePartDefinition> contexts = new List<ComposablePartDefinition>();
            IList<ExportDefinition> copParts = new List<ExportDefinition>();

            foreach (var part in JarsCore.Container.Catalog.Parts)
            {
                foreach (var exDef in part.ExportDefinitions)
                {
                    if (exDef.ContractName.Contains("DataContext"))
                    {
                        contexts.Add(part);
                        copParts.Add(exDef);
                    }
                }
            }
            StringBuilder sbm = new StringBuilder();
            sbm.AppendLine(" Please select a DB Context. ");
            sbm.AppendLine("                             ");
            for (int i = 0; i < copParts.Count(); i++)
            {
                sbm.AppendLine($"({i}) - {copParts[i].ContractName} -");
            }
            sbm.AppendLine(" ");
            sbm.AppendLine("Type the number and press 'Enter':");

            Console.Write(sbm.ToString());
            int cidx = int.Parse(Console.ReadLine());
            context = (IDataContextBaseNh)(contexts[cidx].CreatePart().GetExportedValue(copParts[cidx]));

            Console.Clear();
        }

        static void BuildDatabase(bool IsNewBuild = false, bool IsUpdate = false)
        {
            if (IsNewBuild)
            {
                Console.WriteLine("Building New Database..");
                context.CreateDatabaseTableSchemas();
                Console.WriteLine("....Done!");
            }

            if (IsUpdate)
            {
                Console.WriteLine("Updating Database Table Schemas..");
                context.UpdateDatabaseTableSchemas();
                Console.WriteLine("....Done!");
            }
        }

        static void DeleteDatabase()
        {
            Console.WriteLine("This will DELETE ALL tables and data in the database, are you sure? (Y/n)");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "y")
            {
                Console.WriteLine("Deleting....");
                context.DropDatabaseTables();
                Console.WriteLine("All tables and data deleted");
            }
            else
            {
                Console.WriteLine("no action taken.");
            }
        }

        static void PopulateDatabase()
        {

            Console.WriteLine("Please wait....");
            context.PopulateDefaultData();

            var repoResourceGroup = repositoryFactrory.GetDataRepository<IGenericEntityRepositoryBase<JarsResourceGroup, IDataContextNhJars>>();// ResourceGroupRepository>();
            var repoResources = repositoryFactrory.GetDataRepository<IGenericEntityRepositoryBase<JarsResource, IDataContextNhJars>>();//<IResourceRepository>();
            var repoLabels = repositoryFactrory.GetDataRepository<IGenericEntityRepositoryBase<ApptLabel, IDataContextNhJars>>();//<IApptLabelRepository>();
            var repoStatus = repositoryFactrory.GetDataRepository<IGenericEntityRepositoryBase<ApptStatus, IDataContextNhJars>>();//<IApptStatusRepository>();
            var repoDefAppts = repositoryFactrory.GetDataRepository<IGenericEntityRepositoryBase<JarsDefaultAppointment, IDataContextNhJars>>();//<IApptStatusRepository>();

            repoResourceGroup.CreateUpdateList(SetupDataUtil.GetDefaultResourceGroup(), "SYSTEM");
            repoResources.CreateUpdateList(SetupDataUtil.GetDefaultResources(), "SYSTEM");
            repoLabels.CreateUpdateList(SetupDataUtil.GetDefaultAppointmentLabels(), "SYSTEM");
            repoStatus.CreateUpdateList(SetupDataUtil.GetDefaultAppointmentStatuses(), "SYSTEM");
            repoDefAppts.CreateUpdateList(SetupDataUtil.GetDefaultAppointments(), "SYSTEM");
            //repoLookups.CreateUpdateList(SetupDataUtil.GetDefaultLookupValues(), "SYSTEM");

            Console.WriteLine("....Done!");
        }
    }
}
