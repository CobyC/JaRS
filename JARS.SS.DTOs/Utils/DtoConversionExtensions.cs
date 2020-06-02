using JARS.Core.Interfaces.Entities;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;

namespace JARS.SS.DTOs.Utils
{
    public static class DtoConversionExtensions
    {
        /// <summary>
        /// Convert a List of IEntityBase type to another list of IEntityBase types.
        /// This is just a more convenient version of .ConvertAll(x=>x.ConvertTo<SomeEntity>())
        /// </summary>        
        public static IEnumerable<TOut> ConvertAllTo<TOut>(this IEnumerable<IEntityBase> from) where TOut : IEntityBase
        {
            IEnumerable<TOut> to = from.ToList().ConvertAll(x => x.ConvertTo<TOut>());
            return to;
        }

        /// <summary>
        /// Convert a List of IEntityBase type to another list of IEntityBase types.
        /// This is just a more convenient version of .ConvertAll(x=>x.ConvertTo<SomeEntity>())
        /// </summary>        
        public static IList<TOut> ConvertAllTo<TOut>(this IList<IEntityBase> from) where TOut : IEntityBase
        {
            IList<TOut> to = from.ToList().ConvertAll(x => x.ConvertTo<TOut>());
            return to;
        }

        /// <summary>
        /// Convert a List of IEntityBase type to another list of IEntityBase types.
        /// This is just a more convenient version of .ConvertAll(x=>x.ConvertTo<SomeEntity>())
        /// </summary>        
        public static List<TOut> ConvertAllTo<TOut>(this List<IEntityBase> from) where TOut : IEntityBase
        {
            List<TOut> to = from.ToList().ConvertAll(x => x.ConvertTo<TOut>());
            return to;
        }
    }
}
