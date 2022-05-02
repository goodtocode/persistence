using Microsoft.AspNetCore.Http;

namespace GoodToCode.Shared.dotNet.Identity
{
    public static class HttpRequestExtensions
    {
        public static string Bearer(this HttpRequest item)
        {
            var authorizationHeader = item.Headers?["Authorization"];
            string[] parts = authorizationHeader?.ToString().Split(null) ?? new string[0];
            if (parts.Length == 2 && parts[0].Equals("Bearer"))
                return parts[1];
            return null;
        }
    }
}
