using JARS.SS.DTOs.Base;
using JARS.SS.DTOs.Requests.Base;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{


    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/jarsusers/{EmailOrUserName}", "GET")]
    public class GetJarsUser : RequestBase<JarsUserResponse>
    {
        public string EmailOrUserName { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [RequiredRole("Admin")]
    [Route("/jarsusers/find", "GET")]
    public class FindJarsUsers : RequestBase<JarsUsersResponse>
    {
        public bool? IsActive { get; set; }
        // public string[] InRoles { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    //[RequiredRole("Admin")]
    [Route("/jarsusers/store", "POST")]
    public class StoreJarsUser : StoreRequestBase, IReturn<JarsUserResponse>
    {
        public JarsUserDto UserAccount { get; set; }
    }

    [Exclude(Feature.Metadata)]
    [Route("/jarsusers/{Id}", "DELETE")]
    [RequiredRole("Admin")]
    public class DeleteJarsUser : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Exclude(Feature.Metadata)]
    [Route("/jarsusers/reset/{EmailOrUserName}", "POST")]
    [RequiredRole("Admin")]
    public class ResetJarsUserPassword : RequestBase<ResetJarsUserPasswordResponse>
    {
        public string EmailOrUserName { get; set; }

        public string ResetToken { get; set; }

        public string NewPassword { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [RequiredRole("Admin")]
    [Route("/channels/notify/jarsusers", "POST")]
    public class JarsUserNotification : NotificationDto, IReturnVoid
    {
        public JarsUserNotification()
        { }

        public List<int> Ids { get; set; }

    }

}
