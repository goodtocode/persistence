using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace MySundial.Reflections.Services.Functions
{
    public class SwaggerUi
    {
        public const string FunctionName = "SwaggerUi";
        public SwaggerUi()
        {
        }

        [SwaggerIgnore]
        [FunctionName(FunctionName)]
        public static Task<HttpResponseMessage> Run2(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Swagger/ui")] HttpRequestMessage req,
            [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
        {
            if (req is null)
                throw new System.ArgumentNullException(nameof(req));

            return Task.FromResult(swashBuckleClient.CreateSwaggerUIResponse(req, "swagger/json"));
        }
    }
}
