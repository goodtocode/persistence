using Microsoft.Extensions.Options;
using System;

namespace GoodToCode.Shared.Analytics.Abstractions
{
    public class CognitiveServiceOptions : IOptions<ICognitiveServiceConfiguration>
    {
        public ICognitiveServiceConfiguration Value { get; }

        public CognitiveServiceOptions(string keyCredential, Uri endpoint)
        {
            Value = new CognitiveServiceConfiguration(keyCredential, endpoint);
        }
    }
}
