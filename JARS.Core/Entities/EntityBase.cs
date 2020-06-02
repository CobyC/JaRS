using JARS.Core.Extensions;
using JARS.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace JARS.Core.Entities
{
    /// <summary>
    /// This class is the base entity class that will be inherited by all classes in the system.
    /// it is marked as abstract because we dont need a table created for this class.
    /// </summary>
    /// <typeparam name="TPrimaryKeyType">the type of the ID value, it can be int, long, guid, string anything that can be used as a database key</typeparam>
    [DataContract]
    [Serializable]
    public abstract class EntityBase<TPrimaryKeyType> : IEntityBase<TPrimaryKeyType>, INotifyPropertyChanged, IExtensibleDataObject
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
            set
            {
                _Id = value;
                OnPropertyChanged(() => Id);
            }
        }

        /// <summary>
        /// This implicitly implements the interface, so when the entity is passed through the interface the ID value gets represented by the polymorphic type.
        /// </summary>
        object IEntityBase.Id
        {
            get { return this.Id; }
            set { this.Id = (TPrimaryKeyType)Convert.ChangeType(value, typeof(TPrimaryKeyType)); }
        }

        #region PropertyNotification Code
        /*
         Create a property notification with a backing store(long form) properties, this will help with event wiring on properties.
         Then you add or remove events in a similar fashion as you would so in a property, using 'value'
         */
        private List<PropertyChangedEventHandler> _PropertyChangedSubscribers; //<- this will be used to keep track of subscribed events to the property
        private event PropertyChangedEventHandler _PropertyChanged;
        public virtual event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if (_PropertyChangedSubscribers == null)
                    _PropertyChangedSubscribers = new List<PropertyChangedEventHandler>();
                //check to not add the subscriber twice
                if (!_PropertyChangedSubscribers.Contains(value))
                {
                    _PropertyChanged += value;
                    _PropertyChangedSubscribers.Add(value);
                }
            }
            remove
            {
                _PropertyChanged -= value;
                _PropertyChangedSubscribers.Remove(value);
            }
        }

        /// <summary>
        /// This method is called to subscribe the property to the OnChanged event.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (_PropertyChanged != null)
            {
                _PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// This method is called to subscribe to the PropertyChangedEvent and uses the property itself and not the name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpression">lambda expression to access property</param>
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            string propertyName = PropertyHelperExtensions.GetPropertyName(propertyExpression);
        }

        #endregion


        /// <summary>
        /// This is the to help with entities to become version tolerant.
        /// It helps when serializing data, if they receive data that it can not map, it wont break,
        /// it will pass the data into this property in case it needs to pass it on. 
        /// </summary>
        public virtual ExtensionDataObject ExtensionData { get; set; }

        #region Comparision Code for NHibernate

        public override bool Equals(object obj)
        {
            //if (obj == null)
            //    return false;
            //if (obj.GetType() != this.GetType())
            //    return false;
            //----this line cause issues in the scheduler..
            //if (((IEntityBase<TPrimaryKeyType>)obj).Id.ToString() == ((TPrimaryKeyType)Convert.ChangeType(Id, typeof(TPrimaryKeyType))).ToString())
            //    return true;
            //if (base.Equals(obj))
            //    return true;
            //return false;
            
            //https://codereview.stackexchange.com/questions/27421/implementation-of-equals-and-gethashcode-for-base-class

            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var asThis = obj as IEntityBase<TPrimaryKeyType>;            
            return asThis != null && base.Equals(asThis);
        }

        public override int GetHashCode()
        {
            //int hashCode = base.GetHashCode();
            //return hashCode;
            return Id.GetHashCode();
        }
        #endregion
    }

}

