using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace GoodToCode.Shared.dotNet.System
{
    public class Converter
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
