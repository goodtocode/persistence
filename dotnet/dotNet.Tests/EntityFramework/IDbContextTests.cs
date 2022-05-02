using GoodToCode.Shared.dotNet.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GoodToCode.Shared.dotNet.Tests.Identity
{
    [TestClass]
    public class IDbContextTests
    {
        private readonly IConfiguration configuration = new AppConfigurationFactory().Create();
        public Type SutIDbContext { get; private set; } = typeof(IDbContext);

        public IDbContextTests()
        { }

        [TestMethod]
        public void IDBContext_Methods()
        {
            Assert.IsTrue(SutIDbContext.Name == "IDbContext");
            Assert.IsTrue(SutIDbContext.GetMethods().Length > 0);
            Assert.IsTrue(SutIDbContext.GetMethod("SaveChangesAsync") != null);
        }
    }
}
