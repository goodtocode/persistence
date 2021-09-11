using System;
using System.Collections.Generic;
using System.Linq;

namespace dotNet.Linq
{
    public class Class1
    {
        public static IEnumerable<IEnumerable<T>> ToBatch<T>(this IEnumerable<T> items,
                                                   int maxItems)
        {
            return items.Select((item, inx) => new { item, inx })
                        .GroupBy(x => x.inx / maxItems)
                        .Select(g => g.Select(x => x.item));
        }
    }
}
