using JARS.SS.DTOs.Base;
using JARS.SS.DTOs.Requests.Base;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{
    [Authenticate]
    [Route("/standardappointmentexceptions/{Id}")]
    public class GetStandardAppointmentException : RequestBase<StandardAppointmentExceptionsResponse>
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Route("/standardappointmentexceptions/find")]
    public class FindStandardAppointmentExceptions : IReturn<StandardAppointmentExceptionsResponse>
    {
        public string[] InCalendarForResources { get; set; }
        public string[] WithAppointmentDescription { get; set; }
        public int Id { get; set; }
        public DateTime FromStartDate { get; set; }
        public DateTime ToEndDate { get; set; }
    }

    [Authenticate]
    [Route("/standardappointmentexceptions/store")]
    public class StoreStandardAppointmentExceptions : StoreRequestBase, IReturn<StandardAppointmentExceptionsResponse>
    {
        public StoreStandardAppointmentExceptions()
        {
            AppointmentExceptions = new List<StandardAppointmentExceptionDto>();
        }

        public List<StandardAppointmentExceptionDto> AppointmentExceptions { get; set; }
    }

    [Authenticate]
    [Route("/standardappointmentexceptions/{Id}")]
    public class DeleteStandardAppointmentException : DeleteRequestBase, IReturnVoid
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Route("/channels/{channel}/standardappointmentexceptionscrudnotification")]
    public class StandardAppointmentExceptionsCrudNotification : CrudNotificationBaseDto<StandardAppointmentExceptionDto>, IReturnVoid
    {
        public StandardAppointmentExceptionsCrudNotification()
        {
            AppointmentExceptions = new List<StandardAppointmentExceptionDto>();
        }

        public List<StandardAppointmentExceptionDto> AppointmentExceptions { get; set; }

    }

}
