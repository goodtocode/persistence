using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Blob.Excel
{
    public class Healthcheck
    {
        private IConfiguration configuration { set; get; }
        private ILogger log { get; set; }
        public const string FunctionName = "Healthcheck";

        public Healthcheck(ILogger<Healthcheck> logger, IConfiguration config)
        {
            log = logger;
            configuration = config;
        }

        [FunctionName(FunctionName)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FunctionHealth))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            log.LogInformation($"Profile.Healthcheck()");
            await Task.Delay(1); // Remove when call becomes truly async
            var health = new FunctionHealth() { Connected = true };

            return new OkObjectResult(health);
        }
    }
}
