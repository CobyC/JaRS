using JARS.SS.DTOs.Base;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{

    [Route("/apptstatuses/{Id}", "GET")]
    [Exclude(Feature.Metadata)]
    public class GetApptStatus : IReturn<ApptStatusResponse>
    {
        public int Id { get; set; }
    }

    [Route("/apptstatuses/find", "GET")]
    [Exclude(Feature.Metadata)]
    public class FindApptStatuses : IReturn<ApptStatusesResponse>
    {
        public string ViewType { get; set; }
        public string StatusName { get; set; }
        public string InterfaceTypeName { get; set; }
    }

    [Route("/apptstatuses/store", "POST")]
    [Exclude(Feature.Metadata)]
    public class StoreApptStatus : StoreRequestBase, IReturn<ApptStatusResponse>
    {
        public StoreApptStatus()
        { }

        public ApptStatusDto Status { get; set; }
    }

    [Route("/apptstatuses/storemany", "POST")]
    [Exclude(Feature.Metadata)]
    public class StoreApptStatuses : StoreRequestBase, IReturn<ApptStatusesResponse>
    {
        public StoreApptStatuses()
        {
            Statuses = new List<ApptStatusDto>();
        }

        public List<ApptStatusDto> Statuses { get; set; }
    }

    [Route("/apptstatuses/{Id}", "DELETE")]
    [Exclude(Feature.Metadata)]
    public class DeleteApptStatus : IReturnVoid
    {
        public int Id { get; set; }
    }
    
    [Route("/channels/notify/apptstatuses", "POST")]
    [Exclude(Feature.Metadata)]
    public class ApptStatusesNotification : NotificationBaseDto<ApptStatusDto>, IReturnVoid
    {
        public ApptStatusesNotification()
        { }

        public List<int> Ids { get; set; }
    }
}
