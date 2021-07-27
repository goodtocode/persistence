using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace MySundial.Reflections.Services.Functions
{
    public class SwaggerJson
    {
        public const string FunctionName = "Swagger";

        public SwaggerJson()
        {
        }

        [SwaggerIgnore]
        [FunctionName(FunctionName)]
        public static Task<HttpResponseMessage> Run(
                [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Swagger/json")] HttpRequestMessage req,
                [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
        {
            if (req is null)
                throw new System.ArgumentNullException(nameof(req));

            return Task.FromResult(swashBuckleClient.CreateSwaggerDocumentResponse(req));
        }
    }
}
