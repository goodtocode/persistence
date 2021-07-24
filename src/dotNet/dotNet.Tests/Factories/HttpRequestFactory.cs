using GoodToCode.Shared.System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;

namespace GoodToCode.Shared.Unit
{
    public class HttpRequestFactory
    {
        private readonly string method = "GET";

        public HttpRequestFactory()
        { }

        public HttpRequestFactory(string httpMethod)
        {
            method = httpMethod;
        }

        public HttpRequest CreateHttpRequest(string queryStringKey, string queryStringValue)
        {
            IQueryFeature queryFeature = new QueryFeature(new QueryCollection(Converter.ToDictionary(queryStringKey, queryStringValue)));
            IFeatureCollection features = new FeatureCollection();
            features.Set(queryFeature);
            var context = new DefaultHttpContext(features);
            return context.Request;
        }

        public HttpRequest CreateHttpRequest(string queryStringKey, string queryStringValue, object content)
        {
            IHttpRequestFeature feature = new HttpRequestFeature
            {
                QueryString = $"?{queryStringKey}={queryStringValue}",
                Method = method,
                Body = JsonConvert.SerializeObject(content).ToStream()
            };
            IFeatureCollection features = new FeatureCollection();
            features.Set(feature);
            var context = new DefaultHttpContext(features);
            return context.Request;
        }

        public HttpRequest CreateHttpRequest(object content)
        {
            IHttpRequestFeature feature = new HttpRequestFeature
            {
                Method = method,
                Body = JsonConvert.SerializeObject(content).ToStream()
            };
            IFeatureCollection features = new FeatureCollection();
            features.Set(feature);
            var context = new DefaultHttpContext(features);
            return context.Request;
        }
    }
}