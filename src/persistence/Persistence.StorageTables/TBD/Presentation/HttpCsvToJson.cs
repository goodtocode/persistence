using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using GoodToCode.Infrastructure.Covid19.Core;

namespace GoodToCode.Infrastructure.Covid19
{
    public static class HttpCsvToJson
    {
        [FunctionName("HttpCsvToJson")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", "patch", Route = null)] HttpRequest req,
            ILogger log)
        {
            IActionResult returnData;
            log.LogInformation("CsvToJson Triggered");
            log.LogInformation($"Http Method: {req.Method}");
            try
            {
                string csv = await req.ReadAsStringAsync();
                var responseMessage = JsonSerializer.Serialize(new CovidCsv(csv));
                returnData = new OkObjectResult(responseMessage);
            }
            catch (Exception ex)
            {
                returnData = new ExceptionResult(ex, true);
            }
            return returnData;
        }
    }
}
