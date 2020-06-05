using JARS.Core.Entities;
using System;
using System.Collections.Generic;

namespace JARS.Entities
{

    /// <summary>
    /// This class represents a collective group like a trade or department.
    /// it contains a list of workers/people
    /// </summary>
    [Serializable]
    public class JarsResourceGroup : AuditableEntityBase//, IResourceGroup
    {
        public JarsResourceGroup()
        {
            _Resources = new List<JarsResource>();
        }

        private string _Name;
        private string _Code;
        private int? _SortIndex;
        private bool? _IsActive;

        /// <summary>
        /// Get or set the name of the group.
        /// </summary>
         
        public virtual string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged(() => Name);
            }
        }

        /// <summary>
        /// Get or set the code that represents a group.
        /// ie. ELEC = Electrical, PLUMB= Plumbing, DOOR = door fitter. etc..
        /// </summary>
         
        public virtual string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
                OnPropertyChanged(() => Code);
            }
        }
        /// <summary>
        /// Get or set the position of this group when sort is not done by name or by ID
        /// </summary>
         
        public virtual int? SortIndex
        {
            get
            {
                return _SortIndex;
            }
            set
            {
                _SortIndex = value;
                OnPropertyChanged(() => SortIndex);
            }
        }

        /// <summary>
        /// Is used to indicate if the group is still active or not.
        /// </summary>
         
        public virtual bool? IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
                OnPropertyChanged(() => IsActive);
            }
        }

        private IList<JarsResource> _Resources;

        /// <summary>
        /// Get or set the list of workers that belong to the roup
        /// The JarsCalendarManager will use this list to generate a JarsCalendar for each user in this list/ per group
        /// see JarsCalendarManager for more details.
        /// </summary>

         
        public virtual IList<JarsResource> Resources
        {
            get
            {
                if (_Resources == null)
                    _Resources = new List<JarsResource>();
                return _Resources;
            }
            set
            {
                _Resources = value;
                OnPropertyChanged(() => Resources);
            }
        }
    }
}
