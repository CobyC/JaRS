using JARS.Core.Security;
using JARS.SS.DTOs.Base;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{
    [Exclude(Feature.Metadata)]
    [Api("JaRS Activity Log Services")]
    [Tag("JaRS Activity Log Requests")]
    [Route("/activitylogs/{Id}", "GET")]
    public class GetActivityLog : IReturn<ActivityLogsResponse>
    {
        public int Id { get; set; }
    }

    [Api("JaRS Activity Log Services")]
    [Tag("JaRS Activity Log Requests")]
    [Route("/activitylogs/find", "GET")]
    public class FindActivityLogs : IReturn<ActivityLogsResponse>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string LinkedId { get; set; }
        public int ResourceId { get; set; }
    }

    [Api("JaRS Activity Log Services")]
    [Tag("JaRS Activity Log Requests")]
    [Route("/activitylogs/store", "POST",
        Summary = "Store activities performed by a resource",
        Notes = "Store activities like when a job was accepted, or a resource started traveling or when a job was completed.")]
    public class StoreActivityLogs : StoreRequestBase, IReturn<ActivityLogsResponse>
    {
        public StoreActivityLogs()
        {
            Logs = new List<ActivityLogDto>();
        }
        public List<ActivityLogDto> Logs { get; set; }
    }

    [Exclude(Feature.Metadata)]
    [Api("JaRS Activity Log Services")]
    [Tag("JaRS Activity Log Requests")]
    [Route("/activitylogs/{Id}", "DELETE",
        Summary = "Delete an activity log",
        Notes = "Remove an activity log from the repository, this will permanently remove the record")]
    [RequiredRole(new string[] { JarsRoles.Admin, JarsRoles.Manager })]
    public class DeleteActivityLog : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Exclude(Feature.Metadata)]
    [Authenticate]
    [Api("JaRS Activity Log Services")]
    [Tag("JaRS Activity Log Requests")]
    [Route("/channels/notify/activitylogs", "POST")]
    public class ActivityLogsNotification : NotificationBaseDto<ActivityLogDto>, IReturnVoid
    {
        public ActivityLogsNotification()
        {
            Logs = new List<ActivityLogDto>();
        }
        public List<ActivityLogDto> Logs { get; set; }
    }
}
