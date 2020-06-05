using JARS.SS.DTOs.Base;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{
    class AnyJarsDefaultAppointments
    { } //This is just to name the file and has no purpose
      

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/jarsdefaultappointments/{Id}", "GET")]
    public class GetJarsDefaultAppointments : IReturn<JarsDefaultAppointmentResponse>
    {
        public string Id { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/jarsdefaultappointments/find", "GET")]
    public class FindJarsDefaultAppointments : IReturn<JarsDefaultAppointmentsResponse>
    {
        public string DescriptionLike { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/jarsdefaultappointments/store", "POST")]
    public class StoreJarsDefaultAppointment : StoreRequestBase, IReturn<JarsDefaultAppointmentResponse>
    {
        public StoreJarsDefaultAppointment()
        {}
        public JarsDefaultAppointmentDto Appointment { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/jarsdefaultappointments/storemany", "POST")]
    public class StoreJarsDefaultAppointments : StoreRequestBase, IReturn<JarsDefaultAppointmentsResponse>
    {
        public StoreJarsDefaultAppointments()
        {
            Appointments = new List<JarsDefaultAppointmentDto>();
        }
        public List<JarsDefaultAppointmentDto> Appointments { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/jarsdefaultappointments/{Id}", "DELETE")]
    public class DeleteJarsDefaultAppointment : DeleteRequestBase, IReturnVoid
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/channels/notify/jarsdefaultappointment", "POST")]
    public class JarsDefaultAppointmentNotification : NotificationDto, IReturnVoid
    {
        //public JarsDefaultAppointmentCrudNotification()
        //{
        //    Appointments = new List<JarsDefaultAppointmentDto>();
        //}

        //public List<JarsDefaultAppointmentDto> Appointments { get; set; }
       
        /// <summary>
        /// The list of Ids affected by the crud action
        /// </summary>
        public List<int> Ids { get; set; }
    }
}
