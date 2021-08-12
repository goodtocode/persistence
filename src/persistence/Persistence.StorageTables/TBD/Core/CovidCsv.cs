using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Infrastructure.Covid19
{
    public class CovidCsv
    {
        public List<CovidStat> CovidStats { get; } = new List<CovidStat>();
        public string CsvData { get; } = string.Empty;

        public CovidCsv(string csvData)
        {
            CsvData = csvData;
            CovidStats = CsvToCovidStat(csvData);
        }

        private List<CovidStat> CsvToCovidStat(string data)
        {
            var returnData = new List<CovidStat>();
            string rawRow = string.Empty;

            try
            {
                var cleansed = data.Replace('\r', ' ');
                var raw = cleansed.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                var rows = raw.Skip(1).Where(x => x.Length > 1);
                foreach (var row in rows)
                {
                    rawRow = row;
                    var currentRow = row.Split(new char[] { ',' }, StringSplitOptions.None);

                    returnData.Add(new CovidStat()
                    {
                        ID = string.IsNullOrWhiteSpace(currentRow[0].Trim()) ? default : Convert.ToInt32(currentRow[0]),
                        UpdatedDate = string.IsNullOrWhiteSpace(currentRow[1]) ? default : Convert.ToDateTime(currentRow[1]),
                        Confirmed = string.IsNullOrWhiteSpace(currentRow[2]) ? default : Convert.ToInt32(currentRow[2]),
                        ConfirmedChange = string.IsNullOrWhiteSpace(currentRow[3]) ? default : Convert.ToInt32(currentRow[3]),
                        Deaths = string.IsNullOrWhiteSpace(currentRow[4]) ? default : Convert.ToInt32(currentRow[4]),
                        DeathsChange = string.IsNullOrWhiteSpace(currentRow[5]) ? default : Convert.ToInt32(currentRow[5]),
                        Recovered = string.IsNullOrWhiteSpace(currentRow[6]) ? default : Convert.ToInt32(currentRow[6]),
                        RecoveredChange = string.IsNullOrWhiteSpace(currentRow[7]) ? default : Convert.ToInt32(currentRow[7]),
                        Latitude = string.IsNullOrWhiteSpace(currentRow[8]) ? default : Convert.ToDecimal(currentRow[8]),
                        Longitude = string.IsNullOrWhiteSpace(currentRow[9]) ? default : Convert.ToDecimal(currentRow[9]),
                        ISO2 = (currentRow[10] ?? string.Empty).Trim(),
                        ISO3 = (currentRow[11] ?? string.Empty).Trim(),
                        Country_Region = (currentRow[12] ?? string.Empty).Trim(),
                        AdminRegion1 = (currentRow[13] ?? string.Empty).Trim(),
                        AdminRegion2 = (currentRow[14] ?? string.Empty).Trim()
                    });
                }
            }
            catch
            {
                throw new Exception($"Failed to transform CSV to Objects on: {rawRow}");
            }
            return returnData;
        }
    }
}
