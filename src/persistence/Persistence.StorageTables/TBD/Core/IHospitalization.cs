using System;

namespace GoodToCode.Infrastructure.Covid19
{
    public interface IHospitalization
    {
        int AllAvailableBeds { get; set; }
        int ConfirmedPatients { get; set; }
        string Country { get; set; }
        string AdminRegion1 { get; set; }
        string AdminRegion2 { get; set; }
        int HospitalizedPatients { get; set; }
        int IcuAvailableBeds { get; set; }
        int IcuConfirmedPatients { get; set; }
        int IcuSuspectedPatients { get; set; }
        int Id { get; set; }
        DateTime Date { get; set; }
        int SuspectedPatients { get; set; }
        // Calculated Properties
        int ConfirmedDelta1Day { get; set; }
        int SuspectedDelta1Day { get; set; }
        int HospitalizedDelta1Day { get; set; }
        double Confirmed7DayAverage { get; set; }
        double Suspected7DayAverage { get; set; }
        double Hospitalized7DayAverage { get; set; }
        int IcuConfirmedDelta1Day { get; set; }
        int IcuSuspectedDelta1Day { get; set; }
        double IcuConfirmed7DayAverage { get; set; }
        double IcuSuspected7DayAverage { get; set; }

    }
}