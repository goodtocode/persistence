using System;

namespace GoodToCode.Infrastructure.Covid19
{
    public interface ICovidStat
    {
        public int ID { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Confirmed { get; set; }
        public int ConfirmedChange { get; set; }
        public int Deaths { get; set; }
        public int DeathsChange { get; set; }
        public int Recovered { get; set; }
        public int RecoveredChange { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ISO2 { get; set; }
        public string ISO3 { get; set; }
        public string Country_Region { get; set; }
        public string AdminRegion1 { get; set; }
        public string AdminRegion2 { get; set; }
    }
}
