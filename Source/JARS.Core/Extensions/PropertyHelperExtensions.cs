using JARS.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace JARS.Core.Extensions
{
    /// <summary>
    /// Helper class for working with properties on an object
    /// </summary>
    public static class PropertyHelperExtensions
    {
        /// <summary>
        /// Get the name as string value of a property.
        /// </summary>
        /// <typeparam name="T">this will be inferred from the expression</typeparam>
        /// <param name="propertyExpression">Lambda expression to access the property.</param>
        /// <returns></returns>
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("memberExpression");

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
                throw new ArgumentException("property");

            var getMethod = property.GetGetMethod(true);
            if (getMethod.IsStatic)
                throw new ArgumentException("static method");

            //return the name of the property here
            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Determines if an IEntityBase item has any properties that are generic IList<![CDATA[<T>]]> properties  and extracts the types of the IList.
        /// This is helpfull in the rules processing.
        /// </summary>
        /// <param name="entity">The entity that is being inspected</param>
        /// <returns>The list of Type that contains the types found in generic list properties. Or an empty list if nothing found</returns>
        public static IList<Type> GetGenericListTypes(this IEntityBase entity)
        {
            IList<Type> foundList = new List<Type>();
            IEnumerable<Type> genericProperties = entity.GetType().GetProperties().Select(p => p.PropertyType).Where(pi => pi.IsGenericType && pi.GetGenericTypeDefinition() == typeof(IList<>));
            if (genericProperties.Any())
            {
                //check if there are list proerties and if they are of a certain type, if they are what type
                foundList = genericProperties.Select(p => p.GenericTypeArguments[0]).ToList();
            }
            return foundList;
        }

        /// <summary>
        /// Determines if an entity has any properties that are generic IList<> properties and extract the name and IList<> type of the property.
        /// This is helpfull in rules processing
        /// </summary>
        /// <param name="entity">The entity that might contain the properties of IList<![CDATA[<T>]]> </param>
        /// <returns>return a readonly dictionary with the property name and type</returns>
        public static ReadOnlyDictionary<string, Type> GetGenericListTypesDictionary(this IEntityBase entity)
        {
            IDictionary<string, Type> foundList = new Dictionary<string, Type>();
            IList<Type> foundTypes = new List<Type>();

            var entProperties = entity.GetType().GetProperties().Select(p => new { pType = p.PropertyType, pName = p.Name })
                .Where(pi => pi.pType.IsGenericType && pi.pType.GetGenericTypeDefinition() == typeof(IList<>));
            //IEnumerable<Type> genericProperties = entProperties.Select(t => t.pType);
            if (entProperties.Any())
            {
                //check if there are list proerties and if they are of a certain type, if they are what type
                //foundTypes = entProperties.Select(p => p.pType.GenericTypeArguments[0]).ToList();
                foreach (var pInfo in entProperties)
                {
                    foundList.Add(pInfo.pName, pInfo.pType.GenericTypeArguments[0]);
                }
            }
            return new ReadOnlyDictionary<string, Type>(foundList);
        }
    }

}