using GoodToCode.Shared.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace GoodToCode.Shared.Unit
{
    [Binding]
    public class ObjectExtensionsTests
    {
        public ObjectA SutA { get; private set; }
        public ObjectB SutB { get; private set; }

        public ObjectExtensionsTests()
        {
            SutA = new ObjectA();
            SutB = new ObjectB();
        }

        [Given(@"I have object A to Fill to object B by property name")]
        public void GivenIHaveObjectAToFillToObjectBByPropertyName()
        {
            SutA.SomeData = "Data from ObjectA";
            Assert.IsTrue(SutA.SomeData.Length > 0);
        }

        [When(@"Fill is used to cast Object A to Object B")]
        public void WhenFillIsUsedToCastObjectAToObjectB()
        {
            SutB.Fill(SutA);
        }

        [Then(@"Object B is Filled with the same data from Object A")]
        public void ThenObjectBIsFilledWithTheSameDataFromObjectA()
        {
            Assert.IsTrue(SutA.SomeData == SutB.SomeData);
        }

    }
}
