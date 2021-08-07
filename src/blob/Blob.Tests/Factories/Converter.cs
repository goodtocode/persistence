using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Tests
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
