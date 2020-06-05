using JARS.SS.DTOs.Base;
using JARS.SS.DTOs.Requests.Base;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{

    /// <summary>
    /// This class represent a request that can be used to get a single job from the service.
    /// </summary>
    [Exclude(Feature.Metadata)]
    [Route("/jobs/{Id}", "GET")]    
    [Authenticate]
    public class GetJarsJob : RequestBase<JarsJobsResponse>
    {
        /// <summary>
        /// The Id of the record in the database. this will look for the Entity.ID = Id value.
        /// </summary>
        public int Id { get; set; }     
    }

    /// <summary>
    /// This class represent a request that can be used to find jobs.
    /// </summary>
    [Exclude(Feature.Metadata)]
    [Route("/jobs/find", "GET")]
    public class FindJarsJobs : RequestBase<JarsJobsResponse>
    {
        public int Id { get; set; }
        public string ExtRefID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime ActualEndDate { get; set; }
        public string Location { get; set; }
        public int ResourceId { get; set; }
        public string ProgressStatus { get; set; }
        public DateTime CompletionDate { get; set; }

    }

    /// <summary>
    /// This enables single or multiple jobs to be created or updated.
    /// </summary>
    [Exclude(Feature.Metadata)]
    [Route("/jobs/store", "GET")]
    public class StoreJobs : StoreRequestBase, IReturn<JarsJobsResponse>
    {
        public StoreJobs()
        {
            Jobs = new List<JarsJobDto>();
        }

        public virtual List<JarsJobDto> Jobs { get; set; }
    }

    [Exclude(Feature.Metadata)]
    [Route("/jobs/{Id}", "DELETE")]
    [Authenticate]
    public class DeleteJarsJob : IReturnVoid
    {
        /// <summary>
        /// The Id of the record in the database. this will look for the Entity.ID = Id value.
        /// </summary>
    public int Id { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/channels/notify/jarsjobs", "POST")]
    public class JarsJobsNotification : NotificationBaseDto<JarsJobDto>, IReturnVoid
    {
        public JarsJobsNotification()
        {
            Jobs = new List<JarsJobDto>();
        }

        /// <summary>
        /// This list represents the list of jobs.
        /// Use this when possible (or if not all the data is required) to save on the amount of information being sent over the wire.
        /// </summary>
        public virtual List<JarsJobDto> Jobs { get; set; }
    }


}
