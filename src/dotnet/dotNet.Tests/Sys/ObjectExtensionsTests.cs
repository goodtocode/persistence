using GoodToCode.Shared.dotNet.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Shared.dotNet.Tests.Sys
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        public ObjectA SutA { get; private set; }
        public ObjectB SutB { get; private set; }

        public ObjectExtensionsTests()
        {
            SutA = new ObjectA();
            SutB = new ObjectB();
        }

        [TestMethod]
        public void Object_Fill()
        {
            SutA.SomeData = "Data from ObjectA";
            Assert.IsTrue(SutA.SomeData.Length > 0);
            SutB.Fill(SutA);
            Assert.IsTrue(SutA.SomeData == SutB.SomeData);
        }

    }
}
