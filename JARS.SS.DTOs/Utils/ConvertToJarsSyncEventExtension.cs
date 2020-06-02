using JARS.Core.Interfaces.Entities;
using JARS.SS.DTOs.Base;
using ServiceStack;
using System.Collections.Generic;

namespace JARS.SS.DTOs.Utils
{
    /// <summary>
    /// This extension is helpful in the event of sending notifications that only contain the object type and the ID or IDs affected.
    /// </summary>
    public static class ConvertToJarsSyncEventExtension
    {
        /// <summary>
        /// The Dto transmitted to all subscribed channels. it contains the deleted entity ids in a list of ids.
        /// in the event of a store/update the JarsSyncEventStore is transmitted.
        /// </summary>        
        /// <typeparam name="T">The type of the entity that derives from IEntityBase</typeparam>
        /// <param name="entity">The entity that implements IEntityBase</param>
        /// <returns>Returns the JarsSyncEvent containing the ID and the type.</returns>
        //public static JarsSyncDeleteEvent<T> ConvertToJarsSyncDeleteEvent<T>(this T entity) where T : IEntityBase
        //{
        //    JarsSyncDeleteEvent<T> to = entity.ConvertTo<JarsSyncDeleteEvent<T>>();
        //    to.IDs.Add(entity.Id);
        //    return to;
        //}

        ///// <summary>       
        ///// The Dto transmitted to all subscribed channels. it contains the deleted entity ids in a list of ids.
        ///// in the event of a store/update the JarsSyncEventStore is transmitted.
        ///// </summary>        
        ///// <typeparam name="T">The type of the entity that derives from IEntityBase</typeparam>
        ///// <param name="entity">The entity that implements IEntityBase</param>
        ///// <returns>Returns the JarsSyncEvent containing the ID and the type.</returns>
        //public static JarsSyncDeleteEvent<T> ConvertToJarsSyncDeleteEvent<T>(this List<T> entityList) where T : IEntityBase
        //{
        //    JarsSyncDeleteEvent<T> to = entityList[0].ConvertTo<JarsSyncDeleteEvent<T>>();
        //    foreach (IEntityBase item in entityList)
        //    {
        //        to.IDs.Add(item.Id);
        //    }
        //    return to;
        //}


        ///// <summary>
        ///// The Dto transmitted to all subscribed channels. it contains the stored/changed entities in a list.
        ///// in the event of a store/update the JarsSyncEventDelete is transmitted.
        ///// </summary>
        ///// <typeparam name="T">The generic type that has been changed or updated</typeparam>
        ///// <param name="entity">The entity that has been changed or updated</param>
        ///// <returns>The Dto object transmitted to the subscribed channels</returns>
        //public static JarsSyncStoreEvent<T> ConvertToJarsSyncStoreEvent<T>(this T entity) where T : class, IEntityBase
        //{
        //    JarsSyncStoreEvent<T> to = entity.ConvertTo<JarsSyncStoreEvent<T>>();
        //    to.Entities.Add(entity);
        //    return to;
        //}

        ///// <summary>
        ///// The Dto transmitted to all subscribed channels. it contains the stored/changed entities in a list.
        ///// in the event of a store/update the JarsSyncEventDelete is transmitted.
        ///// </summary>
        ///// <typeparam name="T">The generic type that has been changed or updated</typeparam>
        ///// <param name="entity">The entity that has been changed or updated</param>
        ///// <returns>The Dto object transmitted to the subscribed channels</returns>
        //public static JarsSyncStoreEvent<T> ConvertToJarsSyncStoreEvent<T>(this List<T> entityList) where T : class, IEntityBase
        //{
        //    JarsSyncStoreEvent<T> to = entityList[0].ConvertTo<JarsSyncStoreEvent<T>>();
        //    foreach (T entity in entityList)
        //        to.Entities.Add(entity);
        //    return to;
        //}
    }
}
