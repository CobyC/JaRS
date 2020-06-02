using JARS.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Extensions
{
    public static class SystemTypeExtensions
    {
        public static TypeNameItem ToTypeNameItem(this Type type)
        {
            return new TypeNameItem(type);
        }
    }
}
