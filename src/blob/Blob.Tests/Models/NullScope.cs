using System;

namespace GoodToCode.Shared.Blob.Unit.Tests
{
    public class NullScope : IDisposable
    {
        public static NullScope Instance { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
