using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Interfaces.Rules
{
    /// <summary>
    /// The Enum help to indicate if a rule must be true or false to pass.
    /// </summary>
    public enum RulePassesWhen
    {
        /// <summary>
        /// The condition must be true, if it is false, the condition fails
        /// </summary>
        IsTrue,

        /// <summary>
        /// The condition must be false, if it is true, the condition fails.
        /// </summary>
        IsFalse,
    }

    /// <summary>
    /// The Enum help to indicate how the rule is evaluated.
    /// </summary>
    public enum RuleEvaluation
    {
        /// <summary>
        /// Evaluate only the source side
        /// </summary>
        SourceOnly,

        /// <summary>
        /// Evaluate only the target side
        /// </summary>
        TargetOnly,

        /// <summary>
        /// Evaluate the source and the target side
        /// </summary>
        Both,
    }

    /// <summary>
    /// This enum helps with identifying when a rule needs to be applied to an action or event.
    /// The enum value roughly indicates when the rule needs to be applied.
    /// </summary>
    public enum RuleRunsOn
    {
        /// <summary>
        /// The drag drop is when an entity is being dragged and dropped on the scheduler
        /// </summary>
        OnDragDrop,       
        /// <summary>
        /// On change can be when an appointment is for example resized, 
        /// </summary>
        OnChange,
    }
}
