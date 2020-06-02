using JARS.Core.Entities;
using System;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    /// <summary>
    /// The basic classes does not contain any of the child relations.
    /// i.e. this basic resource skill class does not contain any reference to the resource it might be linked to.
    /// to get the class with the resource, the <see cref="ResourceSkillDto"/> can be used.
    /// </summary>
    [DataContract]
    public class BasicResourceSkillDto : EntityBase<int>
    {
        public BasicResourceSkillDto()
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
              
    }
}
