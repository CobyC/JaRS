using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Extensions
{
    public static class StringManipulationExtensions
    {
        /// <summary>
        /// Converts a string by inserting a space in front of each capital letter within the string.
        /// </summary>
        /// <param name="inputString">The string that will be converted</param>
        /// <returns></returns>
        public static string SpaceOnCapitalLetters(this string inputString)
        {
            var result = new StringBuilder();

            foreach (var ch in inputString)
            {
                if (char.IsUpper(ch) && result.Length > 0)
                {
                    result.Append(' ');
                }
                result.Append(ch);
            }
            return result.ToString();
        }
    }
}
