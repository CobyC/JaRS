using JARS.Core.Entities;
using System;

namespace JARS.Entities
{
    /// <summary>
    /// This class represents an attachment item. ie a document, image, certificate etc..
    /// </summary>
    [Serializable]
    public class JarsJobAttachment : EntityBase<int>
    {
        public JarsJobAttachment()
        { }

        private string _Name;
        private byte[] _attachmentData;
        private DateTime _timeAttached;
        //private int _JobId;

        /// <summary>
        /// The name of the attached item.
        /// </summary>
         
        public virtual string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged(() => Name);
            }
        }

        /// <summary>
        /// The byte array that needs to be serialized or deserialized into a stream to get the representing information.
        /// </summary>
         
        public virtual byte[] AttachmentData
        {
            get => _attachmentData;
            set
            {
                _attachmentData = value;
                OnPropertyChanged(() => AttachmentData);
            }
        }

        /// <summary>
        /// The timestamp generated when the attachment was created.
        /// </summary>
         
        public virtual DateTime TimeAttached
        {
            get => _timeAttached;
            set
            {
                _timeAttached = value;
                OnPropertyChanged(() => TimeAttached);
            }
        }

        ///// <summary>
        ///// The job that this attachment belongs to.
        ///// </summary>
        // 
        //public virtual int JobId
        //{
        //    get => _JobId;
        //    set
        //    {
        //        _JobId = value;
        //        OnPropertyChanged(() => JobId);
        //    }
        //}


    }
}
