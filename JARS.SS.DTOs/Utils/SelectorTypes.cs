using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.SS.DTOs.Utils
{
    /// <summary>
    /// This class is a helper class used for setting the selector types.
    /// It will help with keeping things consistent.
    /// </summary>
    public struct SelectorTypes
    {
        //private string value { get; set; }

        /// <summary>
        /// Use this selector type for creation or update commands
        /// </summary>
        public const string store = "store.";
        //public static readonly SelectorTypes store = new SelectorTypes("store.");
        /// <summary>
        /// Use this selector when deleting
        /// </summary>
        public const string delete = "delete.";
        //public static readonly SelectorTypes delete = new SelectorTypes("delete.");

        /// <summary>
        /// use this as any other command
        /// </summary>
        public const string cmd ="cmd.";
        //public static readonly SelectorTypes cmd = new SelectorTypes("cmd.");

        //public SelectorTypes(string selector)
        //{
        //    this.value = selector;
        //}

        //public override string ToString()
        //{
        //    return value;
        //}

    }
}
