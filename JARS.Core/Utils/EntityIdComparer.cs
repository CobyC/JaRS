using JARS.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Utils
{
    /// <summary>
    /// This comparer class is used to compare the Ids of 2 Entities or objects.
    /// It takes the 2 objects and applies .ToString() to them.
    /// If the strings match it is assumed the Ids are the same.
    /// </summary>
    public class EntityIdComparer : IEqualityComparer<object>
    {
        public new bool Equals(object x, object y)
        {
            if (x.ToString() == y.ToString())
                return true;
            else
                return false;
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }

    /// <summary>
    /// This comparer compares the types and the Id of the object.
    /// If the type and the Ids match, then the comparer will return true.
    /// </summary>
    public class EntityTypeAndIdComparer : IEqualityComparer<object>
    {
        public new bool Equals(object x, object y)
        {
            if (x is IEntityBase xEnt && y is IEntityBase yEnt)
            {
                if (xEnt.GetType() == yEnt.GetType())
                {
                    if ($"{xEnt.Id}" == $"{yEnt.Id}")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
            {

                if (x.ToString() == y.ToString())
                    return true;
                else
                    return false;
            }
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }

    /// <summary>
    /// This comparer compares the types and the external ExtRefId of the objects.
    /// If the type and the ExtRefIds match, then the comparer will return true.
    /// </summary>
    public class EntityTypeAndExternalExtRefIdComparer : IEqualityComparer<object>
    {
        public new bool Equals(object x, object y)
        {
            if (x is IEntityWithExternalReference xEnt && y is IEntityWithExternalReference yEnt)
            {
                if (xEnt.GetType() == yEnt.GetType())
                {
                    if ($"{xEnt.ExtRefId}" == $"{yEnt.ExtRefId}")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
            {

                if (x.ToString() == y.ToString())
                    return true;
                else
                    return false;
            }
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }

    /// <summary>
    /// This comparer compares the types and the external ExtRefId of the objects.
    /// If the type and the ExtRefIds match, then the comparer will return true.
    /// </summary>
    public class EntityTypeAndGuidComparer : IEqualityComparer<object>
    {
        public new bool Equals(object x, object y)
        {
            if (x is IEntityWithAppointing xEnt && y is IEntityWithAppointing yEnt)
            {
                if (xEnt.GetType() == yEnt.GetType())
                {
                    if ($"{xEnt.GuidValue}" == $"{yEnt.GuidValue}")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
            {                
                if (x.ToString() == y.ToString())
                    return true;
                else
                    return false;
            }
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }
}
