using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace GoodToCode.Shared.Blob.Excel
{
    [Binding]
    public class ExcelBlobReaderTests
    {
        public string SutA { get; private set; }
        public string SutB { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public ExcelBlobReaderTests()
        {
        }

        [Given(@"I have two strings")]
        public void GivenIHaveTwoStrings()
        {
            SutA = "MyKeyString";
            SutB = "MyValueString";
        }

        [When(@"Converter ToDictionary is called")]
        public void WhenConverterToDictionaryIsCalled()
        {
            //SutReturn = Converter.ToDictionary(SutA, SutB);
        }

        [Then(@"A dictionary object of those strings is returned")]
        public void ThenADictionaryObjectOfThoseStringsIsReturned()
        {
            Assert.IsTrue(SutReturn.ContainsKey(SutA));
            Assert.IsTrue(SutReturn.ContainsValue(SutB));
            Assert.IsTrue(SutReturn[SutA] == SutB);
        }
    }
}
