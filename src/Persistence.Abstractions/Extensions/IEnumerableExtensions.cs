using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Persistence.Abstractions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> ToBatch<T>(this IEnumerable<T> items, int batchSize)
        {
            return items.Select((item, index) => new { item, inx = index })
                        .GroupBy(x => x.inx / batchSize)
                        .Select(g => g.Select(x => x.item));
        }
    }
}
