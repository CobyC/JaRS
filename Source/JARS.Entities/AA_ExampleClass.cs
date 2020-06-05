using JARS.Core.Entities;
using System;

namespace JARS.Entities
{
    [Serializable] //<- this will only be used for data side, but we are using one set of entities for both sides so the class will contain things like this.
    //[DocumentTypeFilter("AA_ExampleClass")]//<- this is used by couchbase, this helps to define the document type (couchbase removed 18-04-2018)
    public class AA_ExampleClass : EntityBase<int> //<- inherit common class
    {
        /*  For the use in nHibernate the class needs  a constructor
        public AA_ExampleClass()
        { }
        */
        private string _AnotherPropertyNotEverywhere;
        private string _SomeText;
        private int _SomeNumber;

          //<- for correct serialization this is required and not optional (especially for couchbase - json)
        public int SomeNumber
        {
            get
            {
                return _SomeNumber;
            }
            set
            {
                _SomeNumber = value;
                OnPropertyChanged(() => SomeNumber);
                //OnPropertyChanged("SomeNumber");
            }
        }

         
        public string SomeText
        {
            get
            {
                return _SomeText;
            }
            set
            {
                _SomeText = value;
                OnPropertyChanged(() => SomeText);
            }
        }

       
        public string AnotherPropertyNotEverywhere
        {
            get
            {
                return _AnotherPropertyNotEverywhere;
            }
            set
            {
                _AnotherPropertyNotEverywhere = value;
                OnPropertyChanged(() => AnotherPropertyNotEverywhere);
            }
        }


    }
}
