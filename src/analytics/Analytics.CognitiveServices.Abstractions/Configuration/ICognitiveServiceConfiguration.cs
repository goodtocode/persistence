using System;

namespace GoodToCode.Shared.Analytics.Abstractions
{
    public interface ICognitiveServiceConfiguration
    {
        string KeyCredential { get; }
        Uri Endpoint { get; }
    }
}
