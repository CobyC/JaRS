using JARS.SS.DTOs.Base;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Collections.Generic;

namespace JARS.SS.DTOs
{


    [Route("/apptlabels/{Id}","GET")]
    [Exclude(Feature.Metadata)]
    public class GetApptLabel : IReturn<ApptLabelResponse>
    {
        public int Id { get; set; }
    }

    [Route("/apptlabels/find", "GET")]
    [Exclude(Feature.Metadata)]
    public class FindApptLabels : IReturn<ApptLabelsResponse>
    {
        public string ViewType { get; set; }
        public string LabelName { get; set; }
        public string InterfaceTypeName { get; set; }
    }

    [Route("/apptlabels/store", "POST")]
    [Exclude(Feature.Metadata)]
    public class StoreApptLabel : StoreRequestBase, IReturn<ApptLabelResponse>
    {
        public StoreApptLabel()
        { }
        public ApptLabelDto Label { get; set; }
    }

    [Route("/apptlabels/storemany", "POST")]
    [Exclude(Feature.Metadata)]
    public class StoreApptLabels : StoreRequestBase, IReturn<ApptLabelsResponse>
    {
        public StoreApptLabels()
        {
            Labels = new List<ApptLabelDto>();
        }

        public List<ApptLabelDto> Labels { get; set; }
    }

    [Route("/apptlabels/{Id}", "DELETE")]
    [Exclude(Feature.Metadata)]
    public class DeleteApptLabel : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Authenticate]
    [Route("/channels/notify/apptlabels", "POST")]
    [Exclude(Feature.Metadata)]
    public class ApptLabelsNotification : NotificationDto, IReturnVoid
    {
        public List<int> Ids { get; set; }
    }
}
