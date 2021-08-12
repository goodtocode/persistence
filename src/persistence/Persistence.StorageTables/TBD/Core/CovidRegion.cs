using System;

namespace GoodToCode.Infrastructure.Covid19
{
    public class CovidRegion : ICovidRegion
    {
        public string Name { get; set; } = string.Empty;
        public int Worldwide { get; set; } = default;
        public int Country { get; set; } = default;
        public int State { get; set; } = default;
        public int County { get; set; } = default;
        public int SortOrder { get; set; } = default;

        public CovidRegion() : base() { }        
    }
}
