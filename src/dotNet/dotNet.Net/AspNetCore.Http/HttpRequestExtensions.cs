using Microsoft.AspNetCore.Http;
using System;

namespace GoodToCode.Shared.Net
{
    public static class HttpRequestExtensions
    {
        public static Uri ToUri(this HttpRequest request)
        {
            Uri returnValue;

            if (string.IsNullOrWhiteSpace(request.Scheme) || string.IsNullOrWhiteSpace(request.Host.ToString()))
                returnValue = new Uri($"http://localhost{request.QueryString}");
            else
            {
                var uriBuilder = new UriBuilder
                {
                    Scheme = request.Scheme,
                    Host = request.Host.Host,
                    Port = request.Host.Port.GetValueOrDefault(80),
                    Path = request.Path.ToString(),
                    Query = request.QueryString.ToString()
                };
                returnValue = uriBuilder.Uri;
            }
            return returnValue;
        }
    }
}
