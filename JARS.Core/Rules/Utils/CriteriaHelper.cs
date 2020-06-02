using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using System.Linq;

namespace JARS.Core.Rules.Utils
{
    /// <summary>
    /// Helper class for validating filter criteria expected by DevExpress Filter Controls
    /// </summary>
    public class CriteriaHelper
    {
        /// <summary>
        /// Validates if a string is a valid DevExpress Criteria, that can be used with rules or other filter controls
        /// </summary>
        /// <param name="filterString">The string that will be tested as a valid criteria</param>
        /// <param name="IgnoreNullOrEmpty">Indicates if null or empty strings are ignored (default is false)</param>
        /// <returns>True or false depending if the string is valid</returns>
        public static bool ValidateStringAsFilterCriteria(string filterString, bool IgnoreNullOrEmpty = false)
        {
            if (string.IsNullOrEmpty(filterString) && !IgnoreNullOrEmpty)
                return false;

            CriteriaParser.Parse(filterString, out OperandValue[] opps);
            if (opps.Any(o => o.Value == null))
            {
                return false;
            }
            return true;
        }
    }
}
