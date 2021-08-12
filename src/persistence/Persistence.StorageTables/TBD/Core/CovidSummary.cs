using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Infrastructure.Covid19
{
    public class CovidSummary
    {
        public enum Period
        {
            Current = 0,
            Previous24hr = 1,
            Previous48hr = 2,
            Previous72hr = 3
        }

        private IEnumerable<ICovidStat> Worldwide { get; set; }
        private IEnumerable<ICovidStat> Country { get; set; }
        private IEnumerable<ICovidStat> State { get; set; }
        private IEnumerable<ICovidStat> County { get; set; }

        public CovidSummary() : base() { }

        public CovidSummary(IEnumerable<ICovidStat> worldwide, IEnumerable<ICovidStat> country, IEnumerable<ICovidStat> state, IEnumerable<ICovidStat> county) : base()
        {
            if (worldwide?.Count() < 3 || country?.Count() < 3 || state?.Count() < 3 || county?.Count() < 3)
                throw new ArgumentException("CovidSummary requires 4 records/days of data.");
            Worldwide = worldwide;
            Country = country;
            State = state;
            County = county;
        }

        public ICovidRegion GetTotal(Period statPeriod = 0)
        {
            return new CovidRegion()
            {
                Name = "Total",
                Worldwide = Worldwide.ElementAt((int)statPeriod).Confirmed,
                Country = Country.ElementAt((int)statPeriod).Confirmed,
                State = State.ElementAt((int)statPeriod).Confirmed,
                County = County.ElementAt((int)statPeriod).Confirmed
            };
        }

        public ICovidRegion GetDeaths(Period statPeriod = 0)
        {
            return new CovidRegion()
            {
                Name = "Deaths",
                Worldwide = Worldwide.ElementAt((int)statPeriod).Deaths,
                Country = Country.ElementAt((int)statPeriod).Deaths,
                State = State.ElementAt((int)statPeriod).Deaths,
                County = County.ElementAt((int)statPeriod).Deaths
            };
        }

        public ICovidRegion GetRecovered(Period statPeriod = 0)
        {
            return new CovidRegion()
            {
                Name = "Recovered",
                Worldwide = Worldwide.ElementAt((int)statPeriod).Recovered,
                Country = Country.ElementAt((int)statPeriod).Recovered,
                State = State.ElementAt((int)statPeriod).Recovered,
                County = County.ElementAt((int)statPeriod).Recovered
            };
        }

        public string GetChangedWorldwide(Period statPeriod = 0)
        {
            var changeTotal = Worldwide.ElementAt(0).Confirmed - Worldwide.ElementAt((int)statPeriod).Confirmed;
            var changeDeaths = Worldwide.ElementAt(0).Deaths - Worldwide.ElementAt((int)statPeriod).Deaths;
            return $"{changeTotal:N0}/{changeDeaths:N0}";
        }

        public string GetChangedCountry(Period statPeriod = 0)
        {
            var changeTotal = Country.ElementAt(0).Confirmed - Country.ElementAt((int)statPeriod).Confirmed;
            var changeDeaths = Country.ElementAt(0).Deaths - Country.ElementAt((int)statPeriod).Deaths;
            return $"{changeTotal:N0}/{changeDeaths:N0}";
        }

        public string GetChangedState(Period statPeriod = 0)
        {
            var changeTotal = State.ElementAt(0).Confirmed - State.ElementAt((int)statPeriod).Confirmed;
            var changeDeaths = State.ElementAt(0).Deaths - State.ElementAt((int)statPeriod).Deaths;
            return $"{changeTotal:N0}/{changeDeaths:N0}";
        }

        public string GetChangedCounty(Period statPeriod = 0)
        {
            var changeTotal = County.ElementAt(0).Confirmed - County.ElementAt((int)statPeriod).Confirmed;
            var changeDeaths = County.ElementAt(0).Deaths - County.ElementAt((int)statPeriod).Deaths;
            return $"{changeTotal:N0}/{changeDeaths:N0}";
        }
        public string GetDate(Period statPeriod = 0)
        {
            return Worldwide.ElementAt((int)statPeriod).UpdatedDate.ToShortDateString();
        }
    }
}
