using JARS.SS.DTOs.Base;
using JARS.SS.DTOs.Requests.Base;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/standardappointments/{Id}", "GET")]
    public class GetStandardAppointment : RequestBase<StandardAppointmentsResponse>
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/standardappointments/find", "GET",
        Notes = "Find all the standard appointments (The InCalenderForResources is a comma seperated string of resource Ids)")]
    public class FindStandardAppointments : RequestBase<StandardAppointmentsResponse>
    {
        public DateTime? FromStartDate { get; set; }
        public DateTime? ToEndDate { get; set; }
        public string WithAppointmentDescription { get; set; }
        public string InCalendarForResources { get; set; }
        public int Id { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/standardappointments/store", "POST")]
    public class StoreStandardAppointment : StoreRequestBase, IReturn<StandardAppointmentResponse>
    {
        public StoreStandardAppointment()
        { }

        public StandardAppointmentDto Appointment { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/standardappointments/storemany", "POST")]
    public class StoreStandardAppointments : StoreRequestBase, IReturn<StandardAppointmentsResponse>
    {
        public StoreStandardAppointments()
        {
            Appointments = new List<StandardAppointmentDto>();
        }

        public IList<StandardAppointmentDto> Appointments { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/standardappointments/{Id}", "DELETE")]
    public class DeleteStandardAppointment : DeleteRequestBase, IReturnVoid
    {
        public int Id { get; set; }
    }


    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/channels/notify/standardappointments", "POST")]
    public class StandardAppointmentsNotification : NotificationDto, IReturnVoid
    {
        /// <summary>
        /// The record Id's affected.
        /// </summary>
        public List<int> Ids { get; set; }
    }

}
