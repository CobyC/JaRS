using JARS.SS.DTOs.Base;
using System.Runtime.Serialization;

namespace JARS.BOS.SS.DTOs
{
    [DataContract]
    public class BOSEntityDto: JarsJobBaseDto
    {
        [DataMember]
        public virtual string AddedNotes { get; set; }
    }
}
