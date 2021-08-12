using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GoodToCode.Infrastructure.Covid19.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GoodToCode.Infrastructure.Covid19.Presentation
{
    public static class HospitalCsvToTableRecord
    {
        [FunctionName("HospitalCsvToTableRecord")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "patch", Route = null)] HttpRequest req, ILogger log)
        {
            IActionResult returnData;
            log.LogInformation("HospitalCsvToTableRecord Triggered");
            log.LogInformation($"Http Method: {req.Method}");
            string lastId = string.Empty, currentId = string.Empty;
            try
            {
                var daysToGet = Convert.ToInt32(req?.Query["days"]) < 0 ? 7 : Convert.ToInt32(req?.Query["days"]);
                var azureBlob = await new AzureBlob(req?.Query["blobContainerName"], req?.Query["blobName"]).GetBlobContents();                
                var azureTable = new AzureTable<HospitalizationEntity>(req?.Query["tableName"], req?.Query["partitionKey"], log);
                var objectCsv = new HospitalizationCsv(azureBlob);                
                var stats = objectCsv.Hospitalizations;
                if (daysToGet > 0)
                {
                    stats = stats.Where(x => x.Date.Ticks >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                        .AddDays(daysToGet * -1).Ticks).ToList();
                }

                foreach (var item in stats)
                {
                    lastId = currentId;
                    currentId = item.Id.ToString();
                    var result = await azureTable.InsertOrReplaceAsync((HospitalizationEntity)item);
                }                
                returnData = new OkObjectResult($"{daysToGet} days requested. {stats.Count()} records processed.");
            }
            catch (Exception ex)
            {
                returnData = new ExceptionResult(ex, true);
            }
            return returnData;
        }
    }
}
