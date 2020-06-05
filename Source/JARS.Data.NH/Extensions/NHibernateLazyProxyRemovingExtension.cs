using JARS.Core.Entities;
using JARS.Core.Interfaces.Entities;
using JARS.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//the below link article contains most of the code used in this extension method.
//https://stackoverflow.com/questions/15138872/servicestack-text-serialize-circular-references

namespace JARS.Data.NH.Extensions
{
    /// <summary>
    /// This helps with the service stack serialization of objects that contain circular references between child and parent.
    /// It also removed the sub parent to child reference for NHibernate LazyInitialization.
    /// </summary>
    public static class NHibernateLazyProxyRemovingExtension
    {

        public static void DisableNHibernateLazyProxyReferences(this object input, object parent = null)
        {
            Dictionary<int, object> hashDictionary = new Dictionary<int, object>();
            if (input is IEnumerable enumerable)
                foreach (var subItem in enumerable)
                {
                    DisableNHibernateLazyProxyReferences(subItem, input);
                }

            hashDictionary.Add(input.GetHashCode(), input);

            var props = input.GetType().GetProperties();
            foreach (var propertyInfo in props)
            {
                if (propertyInfo.PropertyType.GetInterfaces()
                    .Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                {

                    var instanceTypesInList = propertyInfo.PropertyType.GetGenericArguments();
                    if (instanceTypesInList.Length != 1)
                        continue;

                    if (instanceTypesInList[0].GetInterface(typeof(IEntityBase<>).Name) != null)
                    {
                        //try
                        //{
                        var list = (IList)propertyInfo.GetValue(input, null);
                        if (list != null)
                        {
                            if (list.GetType().GetGenericTypeDefinition() == typeof(NHibernate.Collection.Generic.PersistentGenericBag<>))
                                propertyInfo.SetValue(input, null);
                            else
                                foreach (object t in list)
                                {
                                    UpdateReferenceToParent(input, t);
                                    DisableNHibernateLazyProxyReferences(t, input);
                                }
                        }
                        //                        }
                        //                        catch (Exception ex)
                        //                        {
                        //                            //service stack serialization will walk all the way down the object tree.
                        //                            //beacuse Nhibernate populates the references to other object collections (as lazy) there is always an abstract collection in child classes
                        //                            //we can prevent this eternal loop by setting the lower level child objects to null.
                        //                            //the chances are slim that it will be used as the parent of the child has already been collected.
                        //                            if (ex.GetType().FullName == "NHibernate.LazyInitializationException")
                        //                                propertyInfo.SetValue(input, null);
                        //                            else //for any other errors we throw the error.
                        //                            {
                        //                                Trace.Write(ex);
                        //#if DEBUG
                        //                                throw ex;
                        //#endif
                        //                            }
                        //                        }

                    }
                }
            }
            //hashDictionary.Clear();
        }

        private static void UpdateReferenceToParent(object parent, object item)
        {
            var props = item.GetType().GetProperties();
            var result = props.FirstOrDefault(x => x.PropertyType == parent.GetType());

            if (result != null)
                result.SetValue(item, parent, null);

        }
    }
}
