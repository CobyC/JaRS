using JARS.SS.DTOs.Base;
using ServiceStack;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{


    [Route("/errorlogs/{Id}", "GET")]
    public class GetErrorLog : IReturn<ErrorLogsResponse>
    {
        public int Id { get; set; }
    }

    [Route("/errorlogs/find", "GET")]
    public class FindErrorLogs : IReturn<ErrorLogsResponse>
    {
        public string ViewType { get; set; }
        public string ErrorLogName { get; set; }
        public int ColourRGB { get; set; }
    }

    [Route("/errorlogs/store", "POST")]
    public class StoreErrorLogs : StoreRequestBase, IReturn<ErrorLogsResponse>
    {
        public StoreErrorLogs()
        {
            ErrorLogs = new List<ErrorLogDto>();
        }

        public List<ErrorLogDto> ErrorLogs { get; set; }
    }

    [Route("/errorlogs/{Id}", "DELETE")]
    public class DeleteErrorLog : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Route("/channels/notify/errorlogs", "POST")]
    public class ErrorLogsNotification : NotificationBaseDto<ErrorLogDto>, IReturnVoid
    {
        public ErrorLogsNotification()
        {
            ErrorLogs = new List<ErrorLogDto>();
        }
        public List<ErrorLogDto> ErrorLogs { get; set; }

    }

}
