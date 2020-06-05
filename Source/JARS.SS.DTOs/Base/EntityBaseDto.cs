using JARS.Core.Interfaces.Entities;
using JARS.SS.DTOs.Interfaces;
using System;
using System.Runtime.Serialization;

namespace JARS.Core.Entities
{
    /// <summary>
    /// This class is the base entity class that will be inherited by all classes in the system.
    /// it is marked as abstract because we dont need a table created for this class.
    /// </summary>
    /// <typeparam name="TPrimaryKeyType">the type of the ID value, it can be int, long, guid, string anything that can be used as a database key</typeparam>
    [DataContract]
    public abstract class EntityBaseDto<TPrimaryKeyType> : IEntityBase<TPrimaryKeyType>, IExtensibleDataObject
    // where TPrimaryKeyType : struct
    {

        TPrimaryKeyType _Id;
        /// <summary>
        /// this will represent the ID column (key column) in the database.
        /// it takes the type of the T supplied in the polymorphic value indicator.
        /// </summary>
        [DataMember]
        public virtual TPrimaryKeyType Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// This implicitly implements the interface, so when the entity is passed through the interface the ID value gets represented by the polymorphic type.
        /// </summary>
        object IEntityBase.Id
        {
            get { return this.Id; }
            set { this.Id = (TPrimaryKeyType)Convert.ChangeType(value, typeof(TPrimaryKeyType)); }
        }
        
        /// <summary>
        /// This is the to help with entities to become version tolerant.
        /// It helps when serializing data, if they receive data that it can not map, it wont break,
        /// it will pass the data into this property in case it needs to pass it on. 
        /// </summary>
        public virtual ExtensionDataObject ExtensionData { get; set; }
        
    }

}

