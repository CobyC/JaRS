using JARS.SS.DTOs.Base;
using JARS.SS.DTOs.Requests.Base;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{

    [Exclude(Feature.Metadata)]
    [Route("/resourcegroups/{Id}", "GET")]
    public class GetResourceGroup : RequestBase<ResourceGroupResponse>
    {
        public int Id { get; set; }
        /// <summary>
        /// This indicates if only active records will be returned.
        /// The default is True, change to false to include records set to not active.
        /// </summary>
        public bool IsActive { get; set; } = true;

    }

    [Exclude(Feature.Metadata)]
    [Route("/resourcegroups/find", "GET")]
    public class FindResourceGroups : RequestBase<ResourceGroupsResponse>
    {
        public string GroupName { get; set; }
        /// <summary>
        /// This indicates if only active records will be returned.
        /// The default is True, change to false to include records set to not active.
        /// </summary>
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// This class represents a request for a single resource group.
    /// </summary>
    [Exclude(Feature.Metadata)]
    [Route("/resourcegroups/store", "POST")]
    public class StoreResourceGroup : StoreRequestBase, IReturn<ResourceGroupResponse>
    {
        public StoreResourceGroup()
        { }
        public ResourceGroupDto Group { get; set; }
    }

    /// <summary>
    /// This class represents a request for multiple resource groups.
    /// </summary>
    [Exclude(Feature.Metadata)]
    [Route("/resourcegroups/storemany", "POST")]
    public class StoreResourceGroups : StoreRequestBase, IReturn<ResourceGroupsResponse>
    {
        public StoreResourceGroups()
        {
            Groups = new List<ResourceGroupDto>();
        }
        public List<ResourceGroupDto> Groups { get; set; }
    }

    [Exclude(Feature.Metadata)]
    [Route("/resourcegroups/{Id}", "DELETE")]
    public class DeleteResourceGroup : IReturnVoid
    {
        public int Id { get; set; }

    }

    [Authenticate]
    [Exclude(Feature.Metadata)]
    [Route("/channels/notify/resourcegroups", "POST")]
    public class ResourceGroupsNotification : NotificationDto, IReturnVoid
    {
        public ResourceGroupsNotification()
        { }

        public List<int> Ids { get; set; }

    }
}
