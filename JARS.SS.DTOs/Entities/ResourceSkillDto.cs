using JARS.Core.Entities;
using System;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    /// <summary>
    /// This class represents a skill that can belong to a operative/resource.
    /// </summary>
    [DataContract]
    public class ResourceSkillDto : EntityBase<int>
    {
        public ResourceSkillDto()
        { }     

        /// <summary>
        /// Get or Set the maximum level of this skill.
        /// </summary>
        [DataMember]
        public virtual double? MaxLevel { get; set; }

        /// <summary>
        /// Get or set the current level of the skill, this usually links to a operative/resource.
        /// </summary>
        [DataMember]
        public virtual double? CurrentLevel { get; set; }

        /// <summary>
        /// Get or set the efficiency of this skill.
        /// </summary>
        [DataMember]
        public virtual double Efficiency { get; set; }

        /// <summary>
        /// Get or set when this skill level starts
        /// </summary>
        [DataMember]
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// Get or set when this skill level ends.
        /// </summary>
        [DataMember]
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// Get or set a code or reference number of a document that links to this skill level.
        /// </summary>
        [DataMember]
        public virtual string DocumentCode { get; set; }

        /// <summary>
        /// Get or set a description for this skill.
        /// </summary>
        [DataMember]
        public virtual string Description { get; set; }

        /// <summary>
        /// Get or set whether it matters if the skill expires or not.
        /// </summary>
        [DataMember]
        public virtual bool? ExpiryMatters { get; set; }

        /// <summary>
        /// get or set the operative/resource this skill belongs to.
        /// NOTE!! that this class is the -basic- skill class (to prevent circular references when serializing.)
        /// </summary>      
        [DataMember]
        public virtual BasicResourceDto Resource { get; set; }
    }
}
