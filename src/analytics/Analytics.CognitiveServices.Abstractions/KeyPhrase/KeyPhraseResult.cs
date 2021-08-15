using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GoodToCode.Shared.Analytics.Abstractions
{
    public class KeyPhraseResult : ReadOnlyCollection<string>, IKeyPhrases
    {
        public KeyPhraseResult(IList<string> list) : base(list)
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
