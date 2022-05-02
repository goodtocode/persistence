using GoodToCode.Shared.dotNet.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Shared.dotNet.Tests.Sys
{
    [TestClass]
    public class CasterTests
    {
        public ObjectA SutA { get; private set; }
        public ObjectB SutB { get; private set; }

        public CasterTests()
        {
            SutA = new ObjectA();
            SutB = new ObjectB();
        }

        [TestMethod]
        public void Caster_CastObject()
        {
            SutA.SomeData = "This is a test";
            SutB.SomeData = string.Empty;
            Assert.IsTrue(SutA.SomeData.Length > 0);
            Assert.IsTrue(SutA.SomeData != SutB.SomeData);
            SutB = new Caster<ObjectB>().Cast(SutA);
            Assert.IsTrue(SutA.SomeData == SutB.SomeData);
        }
    }
}
