using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GoodToCode.Infrastructure.Covid19.Core
{
    public class HospitalizationCsv
    {
        //private readonly string Country = "United States"; 
        //private readonly string State = "California";
        public string Country { get; private set; }
        public string State { get; private set; }
        public List<Hospitalization> Hospitalizations { get; } = new List<Hospitalization>();
        public string CsvData { get; }

        public HospitalizationCsv(string csvData) : this(csvData, "United States", "California")
        {
        }

        public HospitalizationCsv(string csvData, string country, string state)
        {
            CsvData = csvData;
            Country = country;
            State = state;
            Hospitalizations = CsvToHospitalizationStat(csvData);
        }

        private List<Hospitalization> CsvToHospitalizationStat(string data)
        {
            var returnData = new List<Hospitalization>();
            var raw = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(r => r.Trim(new char[] {'\r','\n',' '})).ToList();
            var rows = raw.Skip(1);
            returnData.AddRange(from row in rows
                                let rowData = row.Split(new char[] { ',' }, StringSplitOptions.None)
                                let confirmedPatients = string.IsNullOrWhiteSpace(rowData[2]) ? default : Convert.ToInt32(Math.Floor(Convert.ToDecimal(rowData[2])))
                                let suspectedPatients = string.IsNullOrWhiteSpace(rowData[3]) ? default : Convert.ToInt32(Math.Floor(Convert.ToDecimal(rowData[3])))
                                let hospitalizedPatients =  string.IsNullOrWhiteSpace(rowData[4]) ? default : Convert.ToInt32(Math.Floor(Convert.ToDecimal(rowData[4])))
                                let icuConfirmed = string.IsNullOrWhiteSpace(rowData[6]) ? default : Convert.ToInt32(Math.Floor(Convert.ToDecimal(rowData[6])))
                                let icuSuspected = string.IsNullOrWhiteSpace(rowData[7]) ? default : Convert.ToInt32(Math.Floor(Convert.ToDecimal(rowData[7])))
                                select new Hospitalization()
                                {
                                    Country = Country,
                                    AdminRegion1 = State,
                                    AdminRegion2 = string.IsNullOrWhiteSpace(rowData[0]) ? default : Convert.ToString(rowData[0]),
                                    Date = string.IsNullOrWhiteSpace(rowData[1]) ? default : Convert.ToDateTime(rowData[1]),
                                    ConfirmedPatients = confirmedPatients,
                                    SuspectedPatients = suspectedPatients,
                                    HospitalizedPatients = hospitalizedPatients,
                                    AllAvailableBeds = string.IsNullOrWhiteSpace(rowData[5]) ? default : Convert.ToInt32(Math.Floor(Convert.ToDecimal(rowData[5]))),
                                    IcuConfirmedPatients = icuConfirmed,
                                    IcuSuspectedPatients = icuSuspected,
                                    IcuAvailableBeds = string.IsNullOrWhiteSpace(rowData[8]) ? default : Convert.ToInt32(Math.Floor(Convert.ToDecimal(rowData[8])))
                                });
            foreach (var hospitalization in returnData)
            {
                var sevenDays = returnData.Where(h => h.Country == hospitalization.Country &&
                                                       h.AdminRegion1 == hospitalization.AdminRegion1 &&
                                                       h.AdminRegion2 == hospitalization.AdminRegion2 &&
                                                       h.Date.Date <= hospitalization.Date.Date &&
                                                       h.Date.Date > hospitalization.Date.Date.AddDays(-7)).OrderByDescending(h => h.Date).ToList();
                var yesterdayHosp = sevenDays.SingleOrDefault(h => h.Date.Date == hospitalization.Date.Date.AddDays(-1));
                if (yesterdayHosp != null)
                {
                    hospitalization.ConfirmedDelta1Day =
                        hospitalization.ConfirmedPatients - yesterdayHosp.ConfirmedPatients;
                    hospitalization.SuspectedDelta1Day =
                        hospitalization.SuspectedPatients - yesterdayHosp.SuspectedPatients;
                    hospitalization.HospitalizedDelta1Day =
                        hospitalization.HospitalizedPatients - yesterdayHosp.HospitalizedPatients;
                    hospitalization.IcuConfirmedDelta1Day =
                        hospitalization.IcuConfirmedPatients - yesterdayHosp.IcuConfirmedPatients;
                    hospitalization.IcuSuspectedDelta1Day =
                        hospitalization.IcuSuspectedPatients - yesterdayHosp.IcuSuspectedPatients;
                }

                hospitalization.Confirmed7DayAverage = sevenDays.Average(h => h.ConfirmedPatients);
                    hospitalization.Suspected7DayAverage = sevenDays.Average(h => h.SuspectedPatients);
                    hospitalization.Hospitalized7DayAverage = sevenDays.Average(h => h.HospitalizedPatients);
                    hospitalization.IcuConfirmed7DayAverage = sevenDays.Average(h => h.IcuConfirmedPatients);
                    hospitalization.IcuSuspected7DayAverage = sevenDays.Average(h => h.IcuSuspectedPatients);

            }
            return returnData;
        }
    }
}
