using System;

namespace GoodToCode.Infrastructure.Covid19
{
    public interface ICovidRegion
    {
        string Name { get; set; }
        int Worldwide { get; set; }
        int Country { get; set; }
        int State { get; set; }
        int County { get; set; }
    }
}
