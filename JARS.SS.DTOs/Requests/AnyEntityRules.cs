using JARS.Core.Rules;
using JARS.SS.DTOs.Base;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{

    [Exclude(Feature.Metadata)]
    [Route("/jarsrules/{Id}","GET")]
    public class GetJarsRules : IReturn<JarsRulesResponse>
    {
        public int Id { get; set; }
    }

    [Exclude(Feature.Metadata)]
    [Route("/jarsrules/find", "GET")]
    public class FindJarsRules : IReturn<JarsRulesResponse>
    {
        public string TargetEntityTypeName { get; set; }

        public string SourceEntityTypeName { get; set; }
    }

    [Exclude(Feature.Metadata)]
    [Route("/jarsrules/store", "POST")]
    public class StoreJarsRule : StoreRequestBase, IReturn<JarsRuleResponse>
    {
        public StoreJarsRule()
        { }

        public JarsRule Rule { get; set; }
    }

    [Exclude(Feature.Metadata)]
    [Route("/jarsrules/storelist", "POST")]
    public class StoreJarsRules : StoreRequestBase, IReturn<JarsRulesResponse>
    {
        public StoreJarsRules()
        {
            Rules = new List<JarsRule>();
        }

        public List<JarsRule> Rules { get; set; }
    }

    [Exclude(Feature.Metadata)]
    [Route("/jarsrules/{Id}", "DELETE")]
    public class DeleteJarsRules : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/channels/notify/jarsrules", "POST")]
    public class JarsRulesNotification : NotificationDto, IReturnVoid
    {
        /// <summary>
        /// The record Id's affected.
        /// </summary>
        public List<int> Ids { get; set; }
    }

}
