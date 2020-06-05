using JARS.Core.Entities;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace JARS.SS.Auth.Entities
{
    [Serializable]
    public class JarsApiKey : EntityBase<string>, IMeta
    {
        public virtual string UserAuthId { get; set; }
        public virtual string Environment { get; set; }
        public virtual string KeyType { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? ExpiryDate { get; set; }
        public virtual DateTime? CancelledDate { get; set; }
        public virtual string Notes { get; set; }
        public virtual int? RefId { get; set; }
        public virtual string RefIdStr { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
    }
}
