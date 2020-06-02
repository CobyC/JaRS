using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Rules.Attributes;
using JARS.Entities;
using System;

namespace JARS.BOS.Entities
{
    [AllowRuleProcessing]
    [Serializable]
    public class BOSEntity : JarsJobBase, IEntityWithDuration, IEntityWithNotes
    {
        //this will have all the default properties from a jars job.. this could be customized, but for the demo its just the full job..
        public virtual double Duration { get; set; }

        public virtual string AddedNotes { get; set; }
    }
}
