//using GoodToCode.Shared.System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using TechTalk.SpecFlow;

//namespace GoodToCode.Shared.dotNet.Tests.Sys
//{
//    [Binding]
//    public class CasterTests
//    {
//        public ObjectA SutA { get; private set; }
//        public ObjectB SutB { get; private set; }

//        public CasterTests()
//        {
//            SutA = new ObjectA();
//            SutB = new ObjectB();
//        }

//        [Given(@"I have object A to cast to object B by property name")]
//        public void GivenIHaveObjectAToCastToObjectBByPropertyName()
//        {
//            SutA.SomeData = "This is a test";
//            SutB.SomeData = string.Empty;
//            Assert.IsTrue(SutA.SomeData.Length > 0);
//            Assert.IsTrue(SutA.SomeData != SutB.SomeData);
//        }

//        [When(@"Caster is used to cast Object A to Object B")]
//        public void WhenCasterIsUsedToCastObjectAToObjectB()
//        {
//            SutB = new Caster<ObjectB>().Cast(SutA);
//        }

//        [Then(@"Object B contains the same data from Object A")]
//        public void ThenObjectBContainsTheSameDataFromObjectA()
//        {
//            Assert.IsTrue(SutA.SomeData == SutB.SomeData);
//        }
//    }
//}
