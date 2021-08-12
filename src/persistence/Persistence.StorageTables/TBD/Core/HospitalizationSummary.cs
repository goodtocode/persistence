using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Infrastructure.Covid19
{
    public class HospitalizationSummary
    {
        public enum Period
        {
            Current = 0,
            Previous24hr = 1,
            Previous48hr = 2,
            Previous72hr = 3
        }

        private IEnumerable<IHospitalization> OrangeCounty { get; set; }
        private IEnumerable<IHospitalization> TopHospitalizations { get; set; }
        //private IEnumerable<IHospitalization> TopTwoCounty { get; set; }
        //private IEnumerable<IHospitalization> TopThreeCounty { get; set; }
        //private IEnumerable<IHospitalization> TopFourCounty { get; set; }
        //private IEnumerable<IHospitalization> TopFiveCounty { get; set; }

        public HospitalizationSummary() : base() { }

        public HospitalizationSummary(IEnumerable<IHospitalization> oc, IEnumerable<IHospitalization> topHospitalized) : base()
        {
            if (oc?.Count() < 1)
                throw new ArgumentException("Orange Count Hospitalization records is empty.");

            if (topHospitalized?.Count() < 5)
                throw new ArgumentException("Patient hospitalization requires a minimum of 5 or more records.");

            OrangeCounty = oc;
            TopHospitalizations = topHospitalized;
        }

        public IHospitalization GetOcLatestTotal(Period statPeriod = 0)
        {
            var latest = OrangeCounty.OrderByDescending(x => x.Date).First();
            return latest;
        }

        public IEnumerable<IHospitalization> GetOcHospitalizations()
        {
            return OrangeCounty;
        }

        public IEnumerable<IHospitalization> GetTopHospitalizations()
        {
            return TopHospitalizations;
        }

        //public ICovidRegion GetDeaths(Period statPeriod = 0)
        //{
        //    return new CovidRegion()
        //    {
        //        Name = "Deaths",
        //        Worldwide = Worldwide.ElementAt((int)statPeriod).Deaths,
        //        Country = Country.ElementAt((int)statPeriod).Deaths,
        //        State = State.ElementAt((int)statPeriod).Deaths,
        //        County = County.ElementAt((int)statPeriod).Deaths
        //    };
        //}

        //public ICovidRegion GetRecovered(Period statPeriod = 0)
        //{
        //    return new CovidRegion()
        //    {
        //        Name = "Recovered",
        //        Worldwide = Worldwide.ElementAt((int)statPeriod).Recovered,
        //        Country = Country.ElementAt((int)statPeriod).Recovered,
        //        State = State.ElementAt((int)statPeriod).Recovered,
        //        County = County.ElementAt((int)statPeriod).Recovered
        //    };
        //}

        //public string GetChangedWorldwide(Period statPeriod = 0)
        //{
        //    var changeTotal = Worldwide.ElementAt(0).Confirmed - Worldwide.ElementAt((int)statPeriod).Confirmed;
        //    var changeDeaths = Worldwide.ElementAt(0).Deaths - Worldwide.ElementAt((int)statPeriod).Deaths;
        //    return $"{changeTotal:N0}/{changeDeaths:N0}";
        //}

        //public string GetChangedCountry(Period statPeriod = 0)
        //{
        //    var changeTotal = Country.ElementAt(0).Confirmed - Country.ElementAt((int)statPeriod).Confirmed;
        //    var changeDeaths = Country.ElementAt(0).Deaths - Country.ElementAt((int)statPeriod).Deaths;
        //    return $"{changeTotal:N0}/{changeDeaths:N0}";
        //}

        //public string GetChangedState(Period statPeriod = 0)
        //{
        //    var changeTotal = State.ElementAt(0).Confirmed - State.ElementAt((int)statPeriod).Confirmed;
        //    var changeDeaths = State.ElementAt(0).Deaths - State.ElementAt((int)statPeriod).Deaths;
        //    return $"{changeTotal:N0}/{changeDeaths:N0}";
        //}

        //public string GetChangedCounty(Period statPeriod = 0)
        //{
        //    var changeTotal = County.ElementAt(0).Confirmed - County.ElementAt((int)statPeriod).Confirmed;
        //    var changeDeaths = County.ElementAt(0).Deaths - County.ElementAt((int)statPeriod).Deaths;
        //    return $"{changeTotal:N0}/{changeDeaths:N0}";
        //}
    }
}
