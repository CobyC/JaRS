﻿namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// The interface describes what the basic requirements are for every entity within the system.
    /// These properties are the minimum requirement for an entity and are generated by the system and should generally not be changeable by 
    /// user input. (automatically assigned).
    /// </summary>   
    public interface IEntityBase
    {
        /// <summary>
        /// The ID property that will be common in all persistent (entity) classes, this is the Key field
        /// </summary>
        object Id { get; set; }
    }

    /// <summary>
    /// This interface sets the type of the key field, from the polymorphic T type. it can be anything that can be used as the primary key type in a database.
    /// Any entity that derives from this interface will have the ID field set as a type of <typeparamref name="TKeyType"/>
    /// </summary>
    /// <typeparam name="TKeyType">the polymorphic type, can be int, long, string, guid</typeparam>
    public interface IEntityBase<TKeyType> : IEntityBase
        //where TKeyType : struct
    {
        /// <summary>
        /// The ID key field that represents the primary key from the datasource.
        /// </summary>
        new TKeyType Id { get; set; }

    }
}