using JARS.Core.Entities;
using System;
using System.Drawing;
using System.IO;

namespace JARS.Entities
{
    /// <summary>
    /// This class represents standard codes that can be set up in the system.
    /// ie Title of a person.
    /// Might belong to a category of TITLE, a value of Dr, a display text of Doctor
    /// All categories will belong to a category where the CategoryCode is set to 'DEF_CAT'
    /// Each Value under the DEF_CAT code needs to be unique.
    /// The display text will be a friendly name for the category
    /// </summary>
    [Serializable]
    public class LookupValue : EntityBase<int>
    {

        /// <summary>
        /// The default category will look like this
        ///   CategoryCode = "LOOKUP_CATEGORY"
        ///   Value = "TITLE"
        ///   DisplayText = "Person Title"
        ///    
        /// And a sub values will look like this
        ///   CategoryCode = "TITLE"
        ///   Value = "DR"
        ///   DisplayText = "Doctor"
        /// 
        ///   CategoryCode = "TITLE"
        ///   Value = "MIRS"
        ///   DisplayText = "Missis"
        /// 
        ///   CategoryCode = "TITLE"
        ///   Value = "SGT"
        ///   DisplayText = "Sergeant"
        ///  
        /// </summary>

        public LookupValue()
        { }

        private string _CategoryCode;
        private string _Value;
        private string _DisplayText;
        Image _Image;
        byte[] _ImageData;

        /// <summary>
        /// The category the standard lookup value belongs to.
        ///ie LOOKUP_CATEGORY or TITLE or FLOOR or WHEELS.
        /// </summary>
         
        public virtual string CategoryCode
        {
            get => _CategoryCode;
            set
            {
                _CategoryCode = value;
                OnPropertyChanged(() => CategoryCode);
            }
        }

        /// <summary>
        /// The Value of the standard value.
        /// ie LOOKUP_CATEGORY or DR or SIR or MR or 1 or 2
        /// </summary>
         
        public virtual string Value
        {
            get => _Value;
            set
            {
                _Value = value;
                OnPropertyChanged(() => Value);
            }
        }

        /// <summary>
        /// The display text of the standard value
        /// ie Doctor or Sir or Mister or Level 1 or Rank 2
        /// </summary>
         
        public virtual string DisplayText
        {
            get => _DisplayText;
            set
            {
                _DisplayText = value;
                OnPropertyChanged(() => DisplayText);
            }
        }

        /// <summary>
        /// The byte array that represents the image linked to this lookup        
        /// </summary>
         
        public virtual byte[] ImageData
        {
            get => _ImageData;
            set
            {
                _ImageData = value;
                OnPropertyChanged(() => ImageData);
            }
        }

        /// <summary>
        /// The image object if the image data was not null
        /// </summary>        
        public virtual Image Image
        {
            get
            {
                if (_ImageData != null)
                {
                    using (MemoryStream mems = new MemoryStream(_ImageData))
                    {
                        _Image = Image.FromStream(mems);
                    }
                }
                return _Image;
            }
            set
            {
                _Image = value;
                OnPropertyChanged(() => Image);
            }
        }
    }
}
