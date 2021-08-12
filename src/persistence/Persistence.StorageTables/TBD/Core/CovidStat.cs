using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodToCode.Infrastructure.Covid19
{
    public class CovidStat : ICovidStat
    {
        public int ID { get; set; } = default;
        public DateTime UpdatedDate { get; set; } = default;
        public int Confirmed { get; set; } = default;
        public int ConfirmedChange { get; set; } = default;
        public int Deaths { get; set; } = default;
        public int DeathsChange { get; set; } = default;
        public int Recovered { get; set; } = default;
        public int RecoveredChange { get; set; } = default;
        public decimal Latitude { get; set; } = default;
        public decimal Longitude { get; set; } = default;
        public string ISO2 { get; set; } = string.Empty;
        public string ISO3 { get; set; } = string.Empty;
        public string Country_Region { get; set; } = string.Empty;
        public string AdminRegion1 { get; set; } = string.Empty;
        public string AdminRegion2 { get; set; } = string.Empty;
    }
}
