using GoodToCode.Shared.Persistence.StorageTables;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;

namespace GoodToCode.Infrastructure.Covid19
{
    public class CovidEntity : TableEntity, ICovidStat
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

        public static explicit operator CovidEntity(CovidStat s) => new CovidEntity(s);

        public CovidEntity() : base() { }

        public CovidEntity(CovidStat stat) : base()
        {
            this.Fill(stat);
            RowKey = ToAlphaNumeric($"{Country_Region}{AdminRegion1}{AdminRegion2}{UpdatedDate.Year}{UpdatedDate.Month:00}{UpdatedDate.Day:00}");
            Timestamp = DateTime.Now;
        }

        private string ToAlphaNumeric(string rawString)
        {
            string returnData = new string(rawString.Where(c => char.IsLetterOrDigit(c)).ToArray());
            return returnData;
        }
    }
}
