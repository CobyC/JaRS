using JARS.Core.Entities;
using JARS.Core.Interfaces.Rules;
using System;
using System.Runtime.Serialization;

namespace JARS.Core.Rules
{

    /// <summary>
    /// This class is used to hold the rule conditions (criteria) that needs to be validated for a specific entity.
    /// for example, if an entity can not be assigned to a specific date, this class will hold the criteria string and a flag to
    /// determine what needs to be validated and if it needs to pass or fail to meet the restriction.
    /// </summary>
    [Serializable]
    //[DataContract]
    public class JarsRule : EntityBase<int>, IJarsRule
    {
        private string _Name;
        /// <summary>
        /// a short description of the purpose of this condition
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

        private string _Description;
        /// <summary>
        /// a short description of the purpose of this condition
        /// </summary>
        public virtual string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                OnPropertyChanged(() => Description);
            }
        }

        private string _SourceCriteriaString;
        /// <summary>
        /// The criteria string used to execute the source rule.
        /// This is build up using the DevExpress filter editor control
        /// </summary>
        public virtual string SourceCriteriaString
        {
            get
            {
                return _SourceCriteriaString;
            }
            set
            {
                _SourceCriteriaString = value;
                OnPropertyChanged(() => SourceCriteriaString);
            }
        }

        private string _SourceTypeName;
        /// <summary>
        /// The name of the type that is the source (or trigger) for this rule. The value received from [sourceEnt.GetType().Name]
        /// The source type name can be left blank if the rule applies globally to a target type, 
        /// but for rules where the target and source are the same, this property must be filled in for accurate testing of the rule
        /// </summary>
        public virtual string SourceTypeName
        {
            get
            {
                return _SourceTypeName;
            }
            set
            {
                _SourceTypeName = value;
                OnPropertyChanged(() => SourceTypeName);
            }
        }

        RulePassesWhen _RulePassesWhen;
        /// <summary>
        /// Set this to indicate how the rule gets evaluated.
        /// </summary>
        public virtual RulePassesWhen RulePassesWhen
        {
            get
            {
                return _RulePassesWhen;
            }
            set
            {
                _RulePassesWhen = value;
                OnPropertyChanged(() => RulePassesWhen);
            }
        }

        private string _TargetCriteriaString;
        /// <summary>
        /// The criteria string used to execute the target side rule.
        /// This is build up using the DevExpress filter editor control
        /// </summary>
        public virtual string TargetCriteriaString
        {
            get
            {
                return _TargetCriteriaString;
            }
            set
            {
                _TargetCriteriaString = value;
                OnPropertyChanged(() => TargetCriteriaString);
            }
        }

        private string _TargetTypeName;
        /// <summary>
        /// The fully qualified type name of the entity type linked to this rule.
        /// This needs to be in the format JARS.Full.Namespace.Class.Name, JARS.Name.Space, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        /// The information can be retrieved using the type. ie typeof(class/interface).AssemblyQualifiedName
        /// </summary>
        public virtual string TargetTypeName
        {
            get
            {
                return _TargetTypeName;
            }
            set
            {
                _TargetTypeName = value;
                OnPropertyChanged(() => TargetTypeName);
            }
        }

        private string _RuleRunsOn;
        /// <summary>
        /// Rule applicators are indicators that help identify when a rule gets checked.
        /// i.e. if the OnDragDrop applicator is found, the rule will be triggered on a drag and drop event.
        /// The values presented in a comma separated string of values.
        /// </summary>
        public virtual string RuleRunsOn
        {
            get { return _RuleRunsOn; }
            set
            {
                _RuleRunsOn = value;
                OnPropertyChanged(() => RuleRunsOn);
            }
        }


        RuleEvaluation _RuleEvaluation;
        /// <summary>
        /// This indicates which parts of the rule will be evaluated for the rule to pass.
        /// </summary>
        public virtual RuleEvaluation RuleEvaluation
        {
            get
            {
                return _RuleEvaluation;
            }
            set
            {
                _RuleEvaluation = value;
            }
        }
    }
}
