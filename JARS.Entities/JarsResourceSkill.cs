using JARS.Core.Entities;
using System;

namespace JARS.Entities
{
    /// <summary>
    /// This class represents a skill that can belong to a operative/resource.
    /// </summary>
    [Serializable]
    public class JarsResourceSkill : EntityBase<int>
    {
        public JarsResourceSkill()
        { }

        private double? _MaxLevel;
        private double? _CurrentLevel;
        private DateTime? _StartDate;
        private DateTime? _EndDate;
        private string _DocumentCode;
        private String _Description;
        private bool? _ExpiryMatters;


        /// <summary>
        /// Get or Set the maximum level of this skill.
        /// </summary>
         
        public virtual double? MaxLevel
        {
            get => _MaxLevel;
            set
            {
                _MaxLevel = value;
                OnPropertyChanged(() => MaxLevel);
            }
        }

        /// <summary>
        /// Get or set the current level of the skill, this usually links to a operative/resource.
        /// </summary>
         
        public virtual double? CurrentLevel
        {
            get => _CurrentLevel;
            set
            {
                _CurrentLevel = value;
                OnPropertyChanged(() => CurrentLevel);
            }
        }

        /// <summary>
        /// Get or set the efficiency of this skill.
        /// </summary>
         
        public virtual double Efficiency
        {
            get
            {
                if (CurrentLevel.HasValue && MaxLevel.HasValue)
                    return Math.Round((CurrentLevel.Value / MaxLevel.Value), 0);
                else
                    return 100;
            }
        }

        /// <summary>
        /// Get or set when this skill level starts
        /// </summary>
         
        public virtual DateTime? StartDate
        {
            get => _StartDate;
            set
            {
                _StartDate = value;
                OnPropertyChanged(() => StartDate);
            }
        }

        /// <summary>
        /// Get or set when this skill level ends.
        /// </summary>
         
        public virtual DateTime? EndDate
        {
            get => _EndDate;
            set
            {
                _EndDate = value;
                OnPropertyChanged(() => EndDate);
            }
        }

        /// <summary>
        /// Get or set a code or reference number of a document that links to this skill level.
        /// </summary>
         
        public virtual string DocumentCode
        {
            get => _DocumentCode;
            set
            {
                _DocumentCode = value;
                OnPropertyChanged(() => DocumentCode);
            }
        }

        /// <summary>
        /// Get or set a description for this skill.
        /// </summary>
         
        public virtual string Description
        {
            get => _Description;
            set
            {
                _Description = value;
                OnPropertyChanged(() => Description);
            }
        }

        /// <summary>
        /// Get or set whether it matters if the skill expires or not.
        /// </summary>
         
        public virtual bool? ExpiryMatters
        {
            get => _ExpiryMatters;
            set
            {
                _ExpiryMatters = value;
                OnPropertyChanged(() => ExpiryMatters);
            }
        }

        /// <summary>
        /// get or set the operative/resource this skill belongs to.
        /// </summary>
        private JarsResource _Resource;
         
        public virtual JarsResource Resource
        {
            get => _Resource;
            set
            {
                _Resource = value;
                OnPropertyChanged(() => Resource);
            }
        }
    }
}
