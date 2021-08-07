using GoodToCode.Shared.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace GoodToCode.Shared.dotNet.Tests.Identity
{
    [Binding]
    public class IDbContextTests
    {
        private readonly IConfiguration configuration = new AppConfigurationFactory().Create();
        public Type SutIDbContext { get; private set; } = typeof(IDbContext);

        public IDbContextTests()
        { }

        [Given(@"I have a IDbContext abstraction to DbContext")]
        public void GivenIHaveAIDbContextAbstractionToDbContext()
        {
            Assert.IsTrue(SutIDbContext.Name == "IDbContext");
        }

        [When(@"a IDbContext is interrogated for functionality")]
        public void WhenAIDbContextIsInterrogatedForFunctionality()
        {
            Assert.IsTrue(SutIDbContext.GetMethods().Length > 0);
        }

        [Then(@"the IDbContext contains abstracted DbContext functionality")]
        public void ThenTheIDbContextContainsAbstractedDbContextFunctionality()
        {
            Assert.IsTrue(SutIDbContext.GetMethod("SaveChangesAsync") != null);
        }
    }
}
