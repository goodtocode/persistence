using GoodToCode.Shared.dotNet.System;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GoodToCode.Shared.dotNet.Tests.Sys
{
    [TestClass]
    public class ConverterTests
    {
        public string SutA { get; private set; }
        public string SutB { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public ConverterTests()
        {
        }

        [TestMethod]
        public void Converter_ToDictionary()
        {
            SutA = "MyKeyString";
            SutB = "MyValueString";
            SutReturn = Converter.ToDictionary(SutA, SutB);
            Assert.IsTrue(SutReturn.ContainsKey(SutA));
            Assert.IsTrue(SutReturn.ContainsValue(SutB));
            Assert.IsTrue(SutReturn[SutA] == SutB);
        }
    }
}
