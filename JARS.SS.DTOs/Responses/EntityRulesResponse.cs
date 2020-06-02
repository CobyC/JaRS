using JARS.Core.Rules;
using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JARS.SS.DTOs
{
    /// <summary>
    /// Store single record
    /// </summary>
    [DataContract]
    public class JarsRuleResponse
    {
        public JarsRuleResponse()
        {}

        [DataMember]
        public JarsRule Rule { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }

    /// <summary>
    /// Store multiple Records
    /// </summary>
    [DataContract]
    public class JarsRulesResponse
    {
        public JarsRulesResponse()
        {
            Rules = new List<JarsRule>();
        }
        [DataMember]
        public List<JarsRule> Rules { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } // inject structured errors
    }
}
