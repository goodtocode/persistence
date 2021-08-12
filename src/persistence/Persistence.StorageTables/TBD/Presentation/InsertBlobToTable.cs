using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace GoodToCode.Infrastructure.Covid19
{
    public static class InsertBlobToTable
    {
        [FunctionName("BlobCsvToJson")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "patch", Route = null)] HttpRequest req,
            ILogger log)
        {
            IActionResult returnData;
            log.LogInformation("BlobCsvToJson Triggered");
            log.LogInformation($"Http Method: {req.Method}");
            string lastId = string.Empty, currentId = string.Empty;
            try
            {
                var daysToGet = Convert.ToInt32(req?.Query["days"]) == 0 ? 5 : Convert.ToInt32(req?.Query["days"]);
                daysToGet *= -1;
                var rawCsv = await new AzureBlob(req?.Query["blobContainerName"], req?.Query["blobName"]).GetBlobContents();
                var objectCsv = new CovidCsv(rawCsv);
                var table = new AzureTable<CovidEntity>(req?.Query["tableName"], req?.Query["partitionKey"], log);      
                var stats = objectCsv.CovidStats
                    .Where(x => x.UpdatedDate.Ticks >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                        .AddDays(daysToGet).Ticks).ToList();

                foreach (var item in stats)
                {
                    lastId = currentId;
                    currentId = item.ID.ToString();
                    var result = await table.InsertOrReplaceAsync((CovidEntity)item);
                }                
                returnData = new OkObjectResult($"{daysToGet} days requested. {stats.Count()} records processed.");
            }
            catch (Exception ex)
            {
                log.LogError($"{ex.Message} - LastId:{lastId} - Id:{currentId}");
                returnData = new ExceptionResult(ex, true);
            }
            return returnData;
        }
    }
}
