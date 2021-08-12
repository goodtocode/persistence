using GoodToCode.Shared.Persistence.StorageTables;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;
using GoodToCode.Infrastructure.Covid19.Core;

namespace GoodToCode.Infrastructure.Covid19
{
    public class HospitalizationEntity : TableEntity, IHospitalization
    {
        public int AllAvailableBeds { get; set; }
        public int ConfirmedPatients { get; set; }
        public string Country { get; set; }
        public string AdminRegion1 { get; set; }
        public string AdminRegion2 { get; set; }
        public int HospitalizedPatients { get; set; }
        public int IcuAvailableBeds { get; set; }
        public int IcuConfirmedPatients { get; set; }
        public int IcuSuspectedPatients { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int SuspectedPatients { get; set; }
        public int ConfirmedDelta1Day { get; set; }
        public int SuspectedDelta1Day { get; set; }
        public int HospitalizedDelta1Day { get; set; }
        public double Confirmed7DayAverage { get; set; }
        public double Suspected7DayAverage { get; set; }
        public double Hospitalized7DayAverage { get; set; }
        public int IcuConfirmedDelta1Day { get; set; }
        public int IcuSuspectedDelta1Day { get; set; }
        public double IcuConfirmed7DayAverage { get; set; }
        public double IcuSuspected7DayAverage { get; set; }

        public static explicit operator HospitalizationEntity(Hospitalization s) => new HospitalizationEntity(s);

        public HospitalizationEntity() : base() { }

        public HospitalizationEntity(Hospitalization stat) : base()
        {
            this.Fill(stat);
            RowKey = $"{ToAlphaNumeric(Country)}|{ToAlphaNumeric(AdminRegion1)}|{ToAlphaNumeric(AdminRegion2)}|{Date:MMddyyy}";
            Timestamp = DateTime.Now;
        }

        private string ToAlphaNumeric(string rawString)
        {
            if (rawString == null)
                return string.Empty;

            return new string(rawString.Where(c => char.IsLetterOrDigit(c)).ToArray());
        }
    }
}
