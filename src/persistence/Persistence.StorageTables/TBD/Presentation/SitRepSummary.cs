using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GoodToCode.Infrastructure.Covid19
{
    public static class SitRepSummary
    {
        [FunctionName("SitRepSummary")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "patch", Route = null)] HttpRequest req,
            ILogger log)
        {
            string covidStats;
            string covidHospitalization;
            var summary = new SitRepTable(log);
            // Data is delayed by a day and updated at 5:00 PM PST, and report generation is triggered at 7:00 AM PST so subtract 2 days
            DateTime targetDateTime = DateTime.Today.AddDays(-2);

            log.LogInformation("SitRepSummary Triggered");
            log.LogInformation($"Http Method: {req.Method}");

            try
            {                
                covidStats = await summary.CreateHTML();
            }
            catch (Exception ex)
            {
                covidStats = $"Covid Stats failed. Error: {ex.Message}";
            }

            try
            {
                covidHospitalization = await summary.GenerateHospitalizationHtml(targetDateTime);
            }
            catch (Exception ex)
            {
                covidHospitalization = $"Covid Stats failed. Error: {ex.Message}";
            }

            return new OkObjectResult($"{covidStats}<br />{covidHospitalization}");
        }
    }
}
