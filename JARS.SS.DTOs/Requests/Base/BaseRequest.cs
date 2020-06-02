using JARS.Core.Interfaces.Entities;
using ServiceStack;

namespace JARS.SS.DTOs.Requests.Base
{
    /// <summary>
    /// This base abstract class inherits from the IReturn&lt;<typeparamref name="T"/>>&gt; interface and includes the FetchEagerly property.
    /// This property is for use to determine if complex objects needs to contain their child properties too.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RequestBase<T> : IReturn<T>
    {
        /// <summary>
        /// indicate if the detail that gets returned should include related child objects as well.
        /// </summary>
        public bool FetchEagerly { get; set; }
    }
}
