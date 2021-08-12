using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodToCode.Infrastructure.Covid19
{
    public class SitRepTable
    {
        private readonly ILogger _log;

        public SitRepTable(ILogger log)
        {
            _log = log;
        }

        public async Task<string> CreateHTML()
        {
            return await CreateHTML(DateTime.Now);
        }

        public async Task<string> CreateHTML(DateTime targetDate)
        {
            var returnData = new StringBuilder();
            var covidData = await GetSummaryData(targetDate.AddDays(1)); // Data is delayed by a day, advance 1 day to catch latest data to that target day

            returnData.Append($"<p>SitRep Data as of {DateTime.Now.ToShortDateString()} at 1:00AM PST</p>");
            returnData.Append("<table>");
            // Header
            returnData.Append($"<tr style='background-color:royalblue; color:white; text-align:center;'>" +
                $"<th style='width:16%;'></th><th style='width:16%;'>JHU</th><th style='width:16%;'>US</th><th style='width:16%;'>CA</th><th style='width:16%;'>OC</th><th style='width:16%;'>Date</th>" +
                $"</tr>");
            // Date
            returnData.Append($"<tr><td style='padding-left:5px;padding-right:5px;font-size: 11pt;'>Report Date:</td><td style='font-size: 11pt;'><b>{targetDate.ToShortDateString()}</b></td></tr>");
            // Total
            returnData.Append($"<tr>" +
                $"<td style='padding-left:5px;padding-right:5px;'><b>Total</b></td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetTotal().Worldwide:N0}</td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetTotal().Country:N0}</td>" +
                $"<td style='padding-left:5px;padding-right:5px;'>{ covidData.GetTotal().State:N0}</td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetTotal().County:N0}</td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetDate()}</td>" +
                $"</tr>");
            // Deaths
            returnData.Append($"<tr>" +
                $"<td style='padding-left:5px;padding-right:5px;'><b>Deaths</b></td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetDeaths().Worldwide:N0}</td><td style='padding-left:5px;padding-right:5px;'>{ covidData.GetDeaths().Country:N0}</td>" +
                $"<td style='padding-left:5px;padding-right:5px;'>{ covidData.GetDeaths().State:N0}</td><td style='padding-left:5px;padding-right:5px;'>{ covidData.GetDeaths().County:N0}</td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetDate()}</td>" +
                $"</tr>");
            // Change 24 hour
            returnData.Append($"<tr>" +
                $"<td style='padding-left:5px;padding-right:5px;'><b>Change (24hr)</b></td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetChangedWorldwide(CovidSummary.Period.Previous24hr)}</td><td style='padding-left:5px;padding-right:5px;'>{ covidData.GetChangedCountry(CovidSummary.Period.Previous24hr)}</td>" +
                $"<td style='padding-left:5px;padding-right:5px;'>{ covidData.GetChangedState(CovidSummary.Period.Previous24hr)}</td><td style='padding-left:5px;padding-right:5px;'>{ covidData.GetChangedCounty(CovidSummary.Period.Previous24hr)}</td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetDate(CovidSummary.Period.Previous24hr)}</td>" +
                $"</tr>");
            // Change 48 hour
            returnData.Append($"<tr>" +
                $"<td style='padding-left:5px;padding-right:5px;'><b>Change (48hr)</b></td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetChangedWorldwide(CovidSummary.Period.Previous48hr)}</td><td style='padding-left:5px;padding-right:5px;'>{ covidData.GetChangedCountry(CovidSummary.Period.Previous48hr)}</td>" +
                $"<td style='padding-left:5px;padding-right:5px;'>{ covidData.GetChangedState(CovidSummary.Period.Previous48hr)}</td><td style='padding-left:5px;padding-right:5px;'>{ covidData.GetChangedCounty(CovidSummary.Period.Previous48hr)}</td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetDate(CovidSummary.Period.Previous48hr)}</td>" +
                $"</tr>");
            // Change 72 hour
            returnData.Append($"<tr>" +
                $"<td style='padding-left:5px;padding-right:5px;'><b>Change (72hr)</b></td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetChangedWorldwide(CovidSummary.Period.Previous72hr)}</td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetChangedCountry(CovidSummary.Period.Previous72hr)}</td>" +
                $"<td style='padding-left:5px;padding-right:5px;'>{ covidData.GetChangedState(CovidSummary.Period.Previous72hr)}</td><td style='padding-left:5px;padding-right:5px;'>{ covidData.GetChangedCounty(CovidSummary.Period.Previous72hr)}</td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetDate(CovidSummary.Period.Previous72hr)}</td>" +
                $"</tr>");
            // Recovered
            returnData.Append($"<tr>" +
                $"<td style='padding-left:5px;padding-right:5px;'><b>Recovered</b></td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetRecovered().Worldwide:N0}</td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetRecovered().Country:N0}</td>" +
                $"<td style='padding-left:5px;padding-right:5px;'></td><td style='padding-left:5px;padding-right:5px;'></td><td style='padding-left:5px;padding-right:5px;'>{covidData.GetDate()}</td>" +
                $"</tr>");
            returnData.Append("</table>");

            return returnData.ToString();
        }

        public async Task<string> GenerateHospitalizationHtml()
        {
            return await GenerateHospitalizationHtml(DateTime.Today);  
        }

        public async Task<string> GenerateHospitalizationHtml(DateTime reportDate)
        {
            var returnData = new StringBuilder();
            var hospitalData = await GetHospitalizationData(reportDate);
            var ocLatestTotal = hospitalData.GetOcLatestTotal();
            var topTotals = hospitalData.GetTopHospitalizations().ToList();

            // Date
            returnData.Append($"<div style='padding-top:20px;padding-bottom:10px;'><b>California COVID-19 Hospitalization: {DateTime.Now.ToShortDateString()}</b><br/>");
            returnData.Append("<table>");
            // Header
            returnData.Append($"<tr style='background-color:royalblue; color:white; text-align:center;'>"
                              + $"<th></th><th style='width:13%;'>{ocLatestTotal.AdminRegion2}</th>"
                              + string.Join(string.Empty,topTotals.Select((v,i) => $"<th style='width:12%;'>{v.AdminRegion2}</th>"))
                              + $"</tr>");
            // Hospitalized Confirmed
            returnData.Append($"<tr>" +
                              $"<td style='padding-left:5px;padding-right:5px;'><b>Hospitalized Confirmed</b><br/>(&#120491; 1 Day)<br/>[Ave 7 Day]</td><td style='padding-left:5px;padding-right:5px;'>{ocLatestTotal.ConfirmedPatients:N0}<br/>({ocLatestTotal.ConfirmedDelta1Day:+#;-#;0})<br/>[{Math.Round(ocLatestTotal.Confirmed7DayAverage, 1):N1}]</td>"
                              + string.Join(string.Empty, topTotals.Select((v, i) => $"<td style='padding-left:5px;padding-right:5px;'>{v.ConfirmedPatients:N0}<br/>({v.ConfirmedDelta1Day:+#;-#;0})<br/>[{Math.Round(v.Confirmed7DayAverage, 1):N1}]</td>"))
                              + $"</tr>");
            // Hospitalized Suspected
            returnData.Append($"<tr style='background-color:#eee'>" +
                              $"<td style='padding-left:5px;padding-right:5px;'><b>Hospitalized Suspected</b><br/>(&#120491; 1 Day)<br/>[Ave 7 Day]</td><td style='padding-left:5px;padding-right:5px;'>{ocLatestTotal.SuspectedPatients:N0}<br/>({ocLatestTotal.SuspectedDelta1Day:+#;-#;0})<br/>[{Math.Round(ocLatestTotal.Suspected7DayAverage, 1):N1}]</td>"
                              + string.Join(string.Empty, topTotals.Select((v, i) => $"<td style='padding-left:5px;padding-right:5px;'>{v.SuspectedPatients:N0}<br/>({v.SuspectedDelta1Day:+#;-#;0})<br/>[{Math.Round(v.Suspected7DayAverage, 1):N1}]</td>"))
                              + $"</tr>");
            // Hospitalized Total
            returnData.Append($"<tr>" +
                $"<td style='padding-left:5px;padding-right:5px;'><b>Hospitalized Total</b><br/>(&#120491; 1 Day)<br/>[Ave 7 Day]</td><td style='padding-left:5px;padding-right:5px;'>{ocLatestTotal.HospitalizedPatients:N0}<br/>({ocLatestTotal.HospitalizedDelta1Day:+#;-#;0})<br/>[{Math.Round(ocLatestTotal.Hospitalized7DayAverage, 1):N1}]</td>"
                + string.Join(string.Empty,topTotals.Select((v,i) => $"<td style='padding-left:5px;padding-right:5px;'>{v.HospitalizedPatients:N0}<br/>({v.HospitalizedDelta1Day:+#;-#;0})<br/>[{Math.Round(v.Hospitalized7DayAverage, 1):N1}]</td>"))
                + $"</tr>");
            // ICU Confirmed
            returnData.Append($"<tr style='background-color:#eee'>" +
                              $"<td style='padding-left:5px;padding-right:5px;'><b>ICU Confirmed</b><br/>(&#120491; 1 Day)<br/>[Ave 7 Day]</td><td style='padding-left:5px;padding-right:5px;'>{ocLatestTotal.IcuConfirmedPatients:N0}<br/>({ocLatestTotal.IcuConfirmedDelta1Day:+#;-#;0})<br/>[{Math.Round(ocLatestTotal.IcuConfirmed7DayAverage, 1):N1}]</td>"
                              + string.Join(string.Empty, topTotals.Select((v, i) => $"<td style='padding-left:5px;padding-right:5px;'>{v.IcuConfirmedPatients:N0}<br/>({v.IcuConfirmedDelta1Day:+#;-#;0})<br/>[{Math.Round(v.IcuConfirmed7DayAverage, 1):N1}]</td>"))
                              + $"</tr>");
            // ICU Suspected
            returnData.Append($"<tr>" +
                              $"<td style='padding-left:5px;padding-right:5px;'><b>ICU Suspected</b><br/>(&#120491; 1 Day)<br/>[Ave 7 Day]</td><td style='padding-left:5px;padding-right:5px;'>{ocLatestTotal.IcuSuspectedPatients:N0}<br/>({ocLatestTotal.IcuSuspectedDelta1Day:+#;-#;0})<br/>[{Math.Round(ocLatestTotal.IcuSuspected7DayAverage, 1):N1}]</td>"
                              + string.Join(string.Empty, topTotals.Select((v, i) => $"<td style='padding-left:5px;padding-right:5px;'>{v.IcuSuspectedPatients:N0}<br/>({v.IcuSuspectedDelta1Day:+#;-#;0})<br/>[{Math.Round(v.IcuSuspected7DayAverage, 1):N1}]</td>"))
                              + $"</tr>");
            // ICU Total
            returnData.Append($"<tr style='background-color:#eee'>" +
                              $"<td style='padding-left:5px;padding-right:5px;'><b>ICU Total</b></td><td style='padding-left:5px;padding-right:5px;'>{(ocLatestTotal.IcuSuspectedPatients + ocLatestTotal.IcuConfirmedPatients):N0}</td>"
                              + string.Join(string.Empty, topTotals.Select((v, i) => $"<td style='padding-left:5px;padding-right:5px;'>{(v.IcuSuspectedPatients + v.IcuConfirmedPatients):N0}</td>"))
                              + $"</tr>");
            // ICU Available Beds
            returnData.Append($"<tr>" +
                              $"<td style='padding-left:5px;padding-right:5px;'><b>ICU Available Beds</b></td><td style='padding-left:5px;padding-right:5px;'>{ocLatestTotal.IcuAvailableBeds:N0}</td>"
                              + string.Join(string.Empty, topTotals.Select((v, i) => $"<td style='padding-left:5px;padding-right:5px;'>{v.IcuAvailableBeds:N0}</td>"))
                              + $"</tr>");
            // All Available Beds
            returnData.Append($"<tr style='background-color:#eee'>" +
                              $"<td style='padding-left:5px;padding-right:5px;'><b>All Available Beds</b></td><td style='padding-left:5px;padding-right:5px;'>{ocLatestTotal.AllAvailableBeds:N0}</td>"
                              + string.Join(string.Empty, topTotals.Select((v, i) => $"<td style='padding-left:5px;padding-right:5px;'>{v.AllAvailableBeds:N0}</td>"))
                              + $"</tr>");
            returnData.Append("</table></div>");

            return returnData.ToString();
        }
        private async Task<CovidSummary> GetSummaryData(DateTime targetDate)
        {
            // Pull data by region
            var table = new AzureTable<CovidEntity>("TotalAllRegions", "bing", _log);
            var worldwideAll = await table.GetByCountryAsync("Worldwide");
            var countryAll = await table.GetByCountryAsync("United States");
            var stateAll = await table.GetByStateAsync("California");
            var countyAll = await table.GetByCountyAsync("California", "Orange County");
            // Reduce to latest 3 days
            var worldwide3Day = worldwideAll.Where(y => y.UpdatedDate.Ticks <= targetDate.Ticks && y.ConfirmedChange > -1).OrderByDescending(x => x.UpdatedDate).Take(4);
            var country3Day = countryAll.Where(y => y.UpdatedDate.Ticks <= targetDate.Ticks && y.ConfirmedChange > -1).OrderByDescending(x => x.UpdatedDate).Take(4);
            var state3Day = stateAll.Where(y => y.UpdatedDate.Ticks <= targetDate.Ticks && y.ConfirmedChange > -1).OrderByDescending(x => x.UpdatedDate).Take(4);
            var county3Day = countyAll.Where(y => y.UpdatedDate.Ticks <= targetDate.Ticks && y.ConfirmedChange > -1).OrderByDescending(x => x.UpdatedDate).Take(4);

            return new CovidSummary(worldwide3Day, country3Day, state3Day, county3Day);
        }

        private async Task<HospitalizationSummary> GetHospitalizationData(DateTime reportDate)
        {
            try
            {
                // Pull data by region
                var table = new AzureTable<HospitalizationEntity>("HospitalizationByCounty", "caodp", _log);
                var ocAll = await table.GetByCountyAsync(reportDate,"California", "Orange");
                var top5Hospitalize = (await table.GetByHospitalizedPatientsDesc(reportDate, new string[] {"Orange"})).Take(5);
                if (top5Hospitalize.Count() < 5)
                    throw new ArgumentException($"Patient hospitalization data for {nameof(reportDate)} does not contain 5 or more record.");

                return new HospitalizationSummary(ocAll, top5Hospitalize);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
