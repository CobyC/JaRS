using JARS.SS.DTOs.Base;
using JARS.SS.DTOs.Requests.Base;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{
    [Api("JaRS Resources Services")]
    [Tag("JaRS Resources Requests")]
    [Route("/resources/{Id}", "GET POST")]
    public class GetResource : RequestBase<ResourceResponse>
    {
        public int Id { get; set; }
        /// <summary>
        /// This indicates if only active records will be returned.
        /// The default is True, change to false to include records set to not active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }

    [Api("JaRS Resources Services")]
    [Tag("JaRS Resources Requests")]
    [Route("/resources/find", "GET POST")]
    public class FindResources : RequestBase<ResourcesResponse>
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string ExternalRef { get; set; }
        public string ExternalRef1 { get; set; }
        public string ExternalRef2 { get; set; }

        /// <summary>
        /// This indicates if only active records will be returned.
        /// The default is True, change to false to include records set to not active.
        /// </summary>
        public bool? IsActive { get; set; }// = true;

    }

    [Tag("JaRS Resources Mobile Requests")]
    [Api("JaRS Resources used on mobile devices")]
    [Route("/resources/mobile/find", "POST")]
    public class FindMobileResources : IReturn<MobileResourcesResponse>
    {
        public int Id { get; set; }
        public string ExternalRef { get; set; }
    }

    [Exclude(Feature.Metadata)]
    [Api("JaRS Resources Services")]
    [Tag("JaRS Resources Requests")]
    [Route("/resources/store", "POST")]
    public class StoreResource : StoreRequestBase, IReturn<ResourceResponse>
    {
        public StoreResource()
        { }
        public ResourceDto Resource { get; set; }
    }

    [Exclude(Feature.Metadata)]
    [Api("JaRS Resources Services")]
    [Tag("JaRS Resources Requests")]
    [Route("/resources/storemany", "POST")]
    public class StoreResources : StoreRequestBase, IReturn<ResourcesResponse>
    {
        public StoreResources()
        {
            Resources = new List<ResourceDto>();
        }
        public List<ResourceDto> Resources { get; set; }
    }

    [Exclude(Feature.Metadata)]
    [Api("JaRS Resources Services")]
    [Tag("JaRS Resources Requests")]
    [Route("/resources/{Id}", "DELETE")]
    public class DeleteResource : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Api("JaRS Resources Services")]
    [Tag("JaRS Resources Requests")]
    [Route("/channels/notify/resources", "POST")]
    public class ResourcesNotification : NotificationDto, IReturnVoid
    {
        public ResourcesNotification()
        { }

        public List<int> Ids { get; set; }
    }

}
