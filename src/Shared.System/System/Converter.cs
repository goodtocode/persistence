using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace GoodToCode.Shared.System
{
    public class Convert
    {
        public static Dictionary<string, StringValues> ToDictionary(string key, string value)
        {
            var qs = new Dictionary<string, StringValues>
            {
                { key, value }
            };
            return qs;
        }
    }
}
