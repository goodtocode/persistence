using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace GoodToCode.Infrastructure.Covid19
{
    public class AzureFunction
    {
        public async Task<string> CallAzureFunction(string url, HttpContent content, HttpMethod method)
        {
            var result = string.Empty;
            CancellationToken cancellationToken;
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(method, url))
            using (var httpContent = content)
            {
                request.Content = httpContent;
                using var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        public async Task<string> CallAzureFunction(string url, HttpMethod method)
        {
            var result = string.Empty;
            CancellationToken cancellationToken;
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(method, url))
            {
                using var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                result = await response.Content.ReadAsStringAsync();
            }

            return result;
        }

        public HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }

        public void SerializeJsonIntoStream(object value, Stream stream)
        {
            using var sw = new StreamWriter(stream, new System.Text.UTF8Encoding(false), 1024, true);
            using var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None };
            var js = new JsonSerializer();
            js.Serialize(jtw, value);
            jtw.Flush();
        }
    }
}
