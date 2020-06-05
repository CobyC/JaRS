using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Utils
{
    /// <summary>
    /// This class is a small helper class that takes a type and wraps it 
    /// in a class that shows the short name and hold the type.
    /// This is helpful in dropdown menus where you only want display the name, but still want the type as well.
    /// </summary>
    public class TypeNameItem
    {
        public TypeNameItem(Type type)
        {
            this.Name = type.Name;
            this.Type = type;
        }
        public string Name { get; private set; }
        public Type Type { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
