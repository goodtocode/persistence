using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using GoodToCode.Infrastructure.Covid19.Core;
using System.Net.Http;

namespace GoodToCode.Infrastructure.Covid19
{
    public static class DownloadCsvToBlob
    {
        [FunctionName("DownloadCsvToBlob")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "patch", Route = null)] HttpRequest req,
            ILogger log)
        {
            IActionResult returnData;
            log.LogInformation("CsvToBlob Triggered");
            log.LogInformation($"Http Method: {req.Method}");
            try
            {
                // Get data
                var client = new HttpClient();
                var response = await client.GetAsync("https://media.githubusercontent.com/media/microsoft/Bing-COVID-19-Data/master/data/Bing-COVID19-Data.csv");
                var bytes = await response.Content.ReadAsByteArrayAsync();

                // Commit data
                await new AzureBlob(req?.Query["blobContainerName"], req?.Query["blobName"]).SaveBlob(bytes);
                returnData = new OkResult();
            }
            catch (Exception ex)
            {
                log.LogError($"{ex.Message}");
                returnData = new ExceptionResult(ex, true);
            }
            return returnData;
        }
    }
}
