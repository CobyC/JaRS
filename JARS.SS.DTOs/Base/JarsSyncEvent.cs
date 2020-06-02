using JARS.Core.Interfaces.Entities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs.Base
{
    /// <summary>
    /// This class is used to notify the channels of a delete event that happened that needs to be synchronized across all subscriptions
    /// </summary>
    /// <typeparam name="T">Specifies the type of entity the delete event relates to</typeparam>
    //[DataContract]
    //public class JarsSyncDeleteEvent<T> where T : IEntityBase
    //{
    //    public JarsSyncDeleteEvent()
    //    {
    //        IDs = new List<object>();
    //    }
    //    [DataMember]
    //    public IList<object> IDs { get; private set; }
    //    /// <summary>
    //    /// The Entity type that the event relates to.
    //    /// (This also links back to the name of the channel used for sending the data)
    //    /// </summary>
    //    [DataMember]
    //    public string EntityTypeName
    //    {
    //        get => typeof(T).Name;         
    //    }

    //}

    /// <summary>
    /// This class is used to notify the channels of a store event that happened that needs to be synchronized across all subscriptions
    /// it enables sending and receiving multiple entities.
    /// </summary>
    /// <typeparam name="T">Specifies the type of entity the create or update event relates to. 
    /// (this also links back to the name of the channel of a specific entity)</typeparam>
    //[DataContract]
    //public class JarsSyncStoreEvent<T> where T : class, IEntityBase
    //{
    //    private string _EntityTypeName;

    //    public JarsSyncStoreEvent()
    //    {
    //        Entities = new List<T>();
    //    }
    //    [DataMember]
    //    public IList<T> Entities { get; private set; }

    //    /// <summary>
    //    /// The Entity type that the event relates to.
    //    /// (This also links back to the name of the channel used for sending the data)
    //    /// </summary>
    //    [DataMember]
    //    public string EntityTypeName { get => _EntityTypeName == null ? typeof(T).Name : _EntityTypeName; set => _EntityTypeName = value; }

    //}
}
