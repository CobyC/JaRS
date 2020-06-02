using JARS.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JARS.Core.Utils
{
    /// <summary>
    /// This class is helpfull in use with visual (UI) elements like check boxes.
    /// </summary>
    public class SearchEntity<TKeyType> 
        where TKeyType : struct
    {
        /// <summary>
        /// The display test that represents the record
        /// </summary>
        public string DisplayText { get; set; }
        /// <summary>
        /// Indicates whether this item is selected or not
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// the value contains the actual object that is represented by the class.
        /// The value can be another class or any other value
        /// </summary>
        public TKeyType ValueId { get; set; }

    }


    public static class GenerateSearchEntitiesExtensions
    {
        /// <summary>
        /// Create a list of SearchEntity object that can be represented in UI as Check items.
        /// </summary>
        /// <typeparam name="T">The Type of entity the list contains.</typeparam>
        /// <param name="list">Will infer to the list calling this method.</param>
        /// <param name="displayTextProperty">The value that will be set as the DisplayText value in the SearchEntity (needs to be of type string)</param>
        /// <param name="valueIdProperty">The value that will be used to identify the original entity. (needs to be Int64)</param>
        /// <param name="isSelectedValue">The value that indicates if the value is selected or not.</param>
        /// <returns>Returns a list of SearchEntity objects</returns>
        public static List<SearchEntity<TKeyType>> GenerateSearchEntities<T, TKeyType>(this IEnumerable<T> list, Expression<Func<T, string>> displayTextProperty, Expression<Func<T, TKeyType>> valueIdProperty, bool isSelectedValue)
            where T: IEntityBase<TKeyType>
            where TKeyType : struct
        {
            List<SearchEntity<TKeyType>> returnList = new List<SearchEntity<TKeyType>>();
            foreach (var item in list)
            {
                string display = item.GetType().GetProperty(((MemberExpression)displayTextProperty.Body).Member.Name).GetValue(item) as string;
                TKeyType valueID = (TKeyType)item.GetType().GetProperty(((MemberExpression)valueIdProperty.Body).Member.Name).GetValue(item);
                returnList.Add(new SearchEntity<TKeyType>
                {
                    DisplayText = display,
                    ValueId = (TKeyType)Convert.ChangeType(valueID, typeof(TKeyType)),
                    IsSelected = isSelectedValue
                });
            }
            return returnList;
        }
    }
}
