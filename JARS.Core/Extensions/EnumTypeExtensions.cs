using JARS.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Extensions
{
    public static class EnumTypeExtensions
    {
        public static IList<CheckInfoItem> GetNamesAsCheckItems<T>(this T e) where T : IConvertible//enum
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            IList<CheckInfoItem> checkItems = new List<CheckInfoItem>();
            foreach (var name in Enum.GetNames(typeof(T)))
            {
                checkItems.Add(new CheckInfoItem() { IsChecked = false, Name = name });
            }

            return checkItems;
        }
    }
}
