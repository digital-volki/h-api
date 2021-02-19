using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Leifez.Common.Mapping
{
    public static class MappingExtensions
    {
        public static List<TResult> MapToList<TSource, TResult>(this IEnumerable source, IMapper mapper) where TSource : class
        {
            if (source == null)
            {
                return new List<TResult>();
            }

            var enumerable = source as IList<object> ?? source.Cast<object>().ToList();

            IEnumerable<TResult> ienumerableDest = mapper.Map<IEnumerable<TSource>, IEnumerable<TResult>>(enumerable.OfType<TSource>());

            return ienumerableDest.ToList();
        }

        public static TResult Map<TResult>(this object source, IMapper mapper) where TResult : class
        {
            return source == null ? null : mapper.Map<TResult>(source);
        }

        public static TResult Map<TSource, TResult>(this TSource source, IMapper mapper) where TSource : class where TResult : class
        {
            return source == null ? null : mapper.Map<TResult>(source);
        }
    }
}
