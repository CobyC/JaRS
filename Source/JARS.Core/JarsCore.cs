using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core
{
    /// <summary>
    /// The core class contain all the properties, values and functions that is required throughout the system.
    /// </summary>
    public class JarsCore
    {
        /// <summary>
        /// This is the global container for the entities loaded for DI (dependency injection)
        /// </summary>
        public static CompositionContainer Container { get; set; }

        // public static CompositionContainer PluginContainer { get; set; }

        // public static CompositionContainer RulesContainer { get; set; }
        static string _ConnectionString;
        public static string JarsConnectionString
        {
            get
            {
                _ConnectionString = ConfigurationManager.ConnectionStrings["JaRSDatabase"].ConnectionString;
                return _ConnectionString;
            }
        }     
       
        public static Color defaultBackColour = Color.WhiteSmoke;//Color.FromArgb(0, 150, 190, 255);
        public static Color defaultForeColour = Color.Black;//Color.FromArgb(0, 150, 190, 255);
        
    }
}
