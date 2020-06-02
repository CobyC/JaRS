using JARS.Core.Interfaces.Rules.Attributes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace JARS.Core.Utils
{
    /// <summary>
    /// This class helps load the additional assemblies that contain the extendable/additional parts of the system.
    /// </summary>
    public static class AssemblyLoaderUtil
    {
        public static string _executingDirectory
        {
            get
            {
                if (ConfigurationManager.AppSettings["UseWebPath"].ToLower() == "true")
                    return HttpRuntime.BinDirectory;
                else
                    return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }
       
        static List<Assembly> _ServicesAssemblies;
        /// <summary>
        /// Get the additional service stack assemblies.
        /// The assemblies are loaded from the main executing folder and should end in .services.*.dll
        /// </summary>
        public static List<Assembly> ServicesAssemblies
        {
            get
            {
                if (_ServicesAssemblies == null)
                {
                    List<Assembly> assemList = new List<Assembly>();

                    string[] files = Directory.GetFiles(_executingDirectory, "*.services*.dll");
                    foreach (string filename in files)
                    {
                        Assembly a = Assembly.LoadFile(filename);
                        assemList.Add(a);
                    }

                    _ServicesAssemblies = assemList;
                }
                return _ServicesAssemblies;
            }
        }
        static List<Assembly> _ServiceStackAssemblies;
        public static List<Assembly> ServiceStackAssemblies
        {
            get
            {
                if (_ServiceStackAssemblies == null)
                {
                    _ServiceStackAssemblies = new List<Assembly>();
                    string[] files = Directory.GetFiles(_executingDirectory, "*.SS.Auth*.dll");
                    foreach (string filename in files)
                    {
                        Assembly a = Assembly.LoadFile(filename);
                        _ServiceStackAssemblies.Add(a);
                    }
                }
                return _ServiceStackAssemblies;
            }
        }
       

        static List<Assembly> _DataContextAssemblies;
        /// <summary>
        /// Get the additional data context assemblies.
        /// The assemblies are loaded from the main executing folder and should match "*.Data.*.dll" search filer
        /// </summary>
        public static List<Assembly> DataAssemblies
        {
            get
            {
                if (_DataContextAssemblies == null)
                {
                    List<Assembly> assemList = new List<Assembly>();
                    string filters = "*.data.dll|*.data.*.dll";                    
                    string[] files = filters.Split('|').SelectMany(filter => Directory.GetFiles(_executingDirectory, filter)).ToArray();

                    foreach (string filename in files)
                    {
                        Assembly a = Assembly.LoadFile(filename);
                        assemList.Add(a);
                    }

                    _DataContextAssemblies = assemList;
                }
                return _DataContextAssemblies;
            }
        }

        static List<Assembly> _WinFormsPlugins;
        public static List<Assembly> WinFormsPlugins
        {
            get
            {

                if (_WinFormsPlugins == null)
                {
                    List<Assembly> assemList = new List<Assembly>();

                    string[] files = Directory.GetFiles(_executingDirectory, "*.plugins*.dll");
                    foreach (string filename in files)
                    {
                        Assembly a = Assembly.LoadFile(filename);
                        assemList.Add(a);
                    }

                    _WinFormsPlugins = assemList;
                }
                return _WinFormsPlugins;
            }
            set
            {
                _WinFormsPlugins = value;
            }
        }

        public static async Task<IList<Type>> FindAllEntityTypesThatAllowRules()
        {

            return await Task.Run(() =>
            {
                IList<Type> foundTypes = new List<Type>();
                List<Assembly> assemList = new List<Assembly>();

                string[] entFiles = Directory.GetFiles(_executingDirectory, "*.Entities*.dll");
                string[] dtoFiles = Directory.GetFiles(_executingDirectory, "*.DTO*.dll");
                string[] allFiles = entFiles.Concat(dtoFiles).ToArray();
                foreach (string filename in allFiles)
                {
                    Assembly a = Assembly.LoadFile(filename);
                    assemList.Add(a);
                }

                if (assemList.Any())
                {
                    foreach (var assem in assemList)
                    {
                        Type[] types = assem.GetTypes();
                        foreach (var t in types)
                        {
                            var attribs = t.GetCustomAttribute<AllowRuleProcessingAttribute>(true);
                            if (attribs != null && !t.IsAbstract)
                                foundTypes.Add(t);
                        }
                    }
                }
                return foundTypes;
            });
        }
    }
}
