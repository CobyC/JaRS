using JARS.SS.DTOs.Base;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Net;

namespace JARS.BOS.SS.DTOs
{
    /// <summary>
    /// This class represent a request that can be used to get a single job from the service.
    /// </summary>
    [Tag("BOSEntity Requests")]
    [Api("Back Office System Services")]
    [Route("/bosentities/{Id}", "GET")]
    public class GetBOSEntity : IReturn<BOSEntityResponse>
    {
        public virtual int Id { get; set; }
    }

    /// <summary>
    /// This class represent a request that can be used to get a single job from the service.
    /// </summary>
    [Tag("BOSEntity Mobile Requests")]
    [Api("Back Office System Services For Mobiles (smaller data sets)")]
    [Route("/bosentities/mobile/{Id}", "GET")]
    public class GetMobileBOSEntity : IReturn<MobileBOSEntityResponse>
    {
        public virtual int Id { get; set; }
    }

    /// <summary>
    /// This class represent a request that can be used to find jobs.
    /// for statuses use the query syntax ie ?Statuses=NEW or ?Statuses=NEW,OLD,ETC 
    /// </summary>
    [Tag("BOSEntity Requests")]
    [Api("Back Office System Services")]
    [Route("/bosentities/find", "GET")]
    [Route("/bosentities/find/{ResourceId}", "GET")]
    [Route("/bosentities/find/{ResourceId}/{StartDate}", "GET")]
    [Route("/bosentities/find/{ResourceId}/{StartDate}/{EndDate}", "GET")]
    public class FindBOSEntities : IReturn<BOSEntitiesResponse>
    {
        public virtual string ResourceId { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string Statuses { get; set; }
    }

    /// <summary>
    /// This class represent a request that can be used to find jobs.
    /// for statuses use the query syntax ie ?Statuses=NEW or ?Statuses=NEW,OLD,ETC 
    /// Note: the mobile requests are POST instead of GET
    /// </summary>
    [Tag("BOSEntity Mobile Requests")]
    [Api("Back Office System Services For Mobiles (smaller data sets)")]
    [Route("/bosentities/mobile/find", "POST")]
    public class FindMobileBOSEntities : IReturn<MobileBOSEntitiesResponse>
    {
        public virtual string ResourceId { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string Statuses { get; set; }
    }


    /// <summary>
    /// This enables single or multiple jobs to be created or updated.
    /// </summary>
    [Tag("BOSEntity Requests")]
    [Api("Back Office System Services")]
    [ApiResponse(HttpStatusCode.BadRequest, "Your request was not understood")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke.")]
    [Route("/bosentities/store", "POST", Summary = "Store Back Office System Entities", Notes = "Store BOS Entities that has been changed or that are new.")]
    public class StoreBOSEntity : StoreRequestBase, IReturn<BOSEntityResponse>
    {
        public StoreBOSEntity()
        { }
        public virtual BOSEntityDto BOSEntity { get; set; }
    }

    /// <summary>
    /// This enables single or multiple jobs to be created or updated.
    /// </summary>
    [Tag("BOSEntity Requests")]
    [Api("Back Office System Services")]
    [Route("/bosentities/storemany", "POST")]
    public class StoreBOSEntities : StoreRequestBase, IReturn<BOSEntitiesResponse>
    {
        public StoreBOSEntities()
        {
            BOSEntities = new List<BOSEntityDto>();
        }
        public virtual List<BOSEntityDto> BOSEntities { get; set; }
    }

    /// <summary>
    /// This enables single or multiple jobs to be created or updated.
    /// </summary>
    [Tag("BOSEntity Mobile Requests")]
    [Api("Back Office System Services for mobiles (smaller data sets)")]
    [ApiResponse(HttpStatusCode.BadRequest, "Your request was not understood")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke.")]
    [Route("/bosentities/mobile/store", "POST")]
    public class StoreMobileBOSEntity : IReturn<MobileBOSEntityResponse>
    {
        public StoreMobileBOSEntity()
        { }
        public virtual MobileBOSEntityDto BOSEntity { get; set; }
    }

    /// <summary>
    /// This enables single or multiple jobs to be created or updated.
    /// </summary>
    [Tag("BOSEntity Mobile Requests")]
    [Api("Back Office System Services")]
    [Route("/bosentities/mobile/storemany", "POST")]
    public class StoreMobileBOSEntities : IReturn<MobileBOSEntitiesResponse>
    {
        public StoreMobileBOSEntities()
        {
            BOSEntities = new List<MobileBOSEntityDto>();
        }
        public virtual List<MobileBOSEntityDto> BOSEntities { get; set; }
    }

    /// <summary>
    /// This class represent a request that can be used to get a single job from the service.
    /// </summary>
    [Exclude(Feature.Metadata)]
    [Tag("BOSEntity Requests")]
    [Api("Back Office System Services")]
    [Route("/bosentities/{Id}", "DELETE")]
    public class DeleteBOSEntity : DeleteRequestBase, IReturnVoid
    {
        public virtual int Id { get; set; }
    }

    #region Send notifications via chanels regarding the jobs    
    [Exclude(Feature.Metadata)]
    [Tag("BOSEntity Requests")]
    [Api("Back Office System Services")]
    [Route("/channels/notify/bosentities", "POST")]
    public class BOSEntitiesNotification : NotificationDto, IReturnVoid
    {
        public BOSEntitiesNotification()
        {
            Ids = new List<int>();
        }

        public virtual List<int> Ids { get; set; }
    }
    #endregion  
}
