using System;
using System.Collections.Generic;

namespace JARS.Entities
{
    /// <summary>
    /// This class extends the JarsJobBase class and has the additional properties for collections of JobLines and attachments.
    /// NB!: When retrieving this class from the database, make sure to use EagerLoading options to get the linked collection objects.
    /// </summary>
    [Serializable]
    public class JarsJob : JarsJobBase
    {
        public JarsJob()
        {
            Attachments = new List<JarsJobAttachment>();
            JobLines = new List<JarsJobLine>();
        }

        private string _AssignedBy;
        /// <summary>
        /// This is an additional property to show that any non base properties need to be added to the particular base class.
        /// Other Job classes might not need this property and will not be available in the other entities derived from JobBase.
        /// </summary>
        public virtual string AssignedBy
        {
            get => _AssignedBy;
            set
            {
                _AssignedBy = value;
                OnPropertyChanged(() => AssignedBy);
            }
        }

        private IList<JarsJobAttachment> _Attachments;
        /// <summary>
        /// This is a list of attachments that are linked to this job, this could be images and documents etc..
        /// </summary>
         
        public virtual IList<JarsJobAttachment> Attachments
        {
            get { return _Attachments; }
            set
            {
                _Attachments = value;
                OnPropertyChanged(() => IntegrationMessage);
            }
        }

        private IList<JarsJobLine> _JobLines;
        /// <summary>
        /// Job lines (also known as SOR lines) that are linked to this job.
        /// </summary>
         
        public virtual IList<JarsJobLine> JobLines
        {
            get => _JobLines; set
            {
                _JobLines = value;
                OnPropertyChanged(() => JobLines);
            }
        }        

    }
}
