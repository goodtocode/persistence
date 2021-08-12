using System;

namespace GoodToCode.Infrastructure.Covid19.Core
{
    public class Hospitalization : IHospitalization
    {
        public int Id { get; set; } = default;
        public DateTime Date { get; set; } = default;
        public int ConfirmedPatients { get; set; } = default;
        public int SuspectedPatients { get; set; } = default;
        public int HospitalizedPatients { get; set; } = default;
        public int AllAvailableBeds { get; set; } = default;
        public int IcuConfirmedPatients { get; set; } = default;
        public int IcuSuspectedPatients { get; set; } = default;
        public int IcuAvailableBeds { get; set; } = default;
        public string Country { get; set; }
        public string AdminRegion1 { get; set; }
        public string AdminRegion2 { get; set; }
        // Calculated Properties
        public int ConfirmedDelta1Day { get; set; } = default;
        public int SuspectedDelta1Day { get; set; } = default;
        public int HospitalizedDelta1Day { get; set; } = default;
        public double Confirmed7DayAverage { get; set; } = default;
        public double Suspected7DayAverage { get; set; } = default;
        public double Hospitalized7DayAverage { get; set; } = default;
        public int IcuConfirmedDelta1Day { get; set; } = default;
        public int IcuSuspectedDelta1Day { get; set; } = default;
        public double IcuConfirmed7DayAverage { get; set; } = default;
        public double IcuSuspected7DayAverage { get; set; } = default;
    }
}
