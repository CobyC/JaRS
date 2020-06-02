using JARS.Core.Entities;
using ServiceStack.Caching;
using System;

namespace JARS.SS.Auth.Entities
{
    [Serializable]
    public class JarsCacheEntry : EntityBase<string>, ICacheEntry
    {
        public virtual string Data { get; set; }
        public virtual DateTime? ExpiryDate { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
    }
}
