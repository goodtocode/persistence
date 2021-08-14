using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GoodToCode.Shared.Analytics.Abstractions
{
    public class LinkedMatches
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Matches { get; set; }
    }

    public class LinkedResult : ReadOnlyCollection<LinkedMatches>
    {
        public LinkedResult(IList<LinkedMatches> list) : base(list)
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
