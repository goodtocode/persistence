using GoodToCode.Shared.Persistence.StorageTables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.Tests
{
    [TestClass]
    public class StorageTables_Service_ValidationTests
    {
        private IConfiguration configuration;
        private StorageTablesServiceOptions configPersistence;
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public StorageTables_Service_ValidationTests() { }

        [TestInitialize]
        public void Initialize()
        {
            configuration = new AppConfigurationFactory().Create();
            configPersistence = new StorageTablesServiceOptions(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"UnitTest-{DateTime.UtcNow:yyyy-MM-dd}");
        }

        [TestMethod]
        public void StorageTables_Service_Validate()
        {
            var item = new Dictionary<string, object>();
            item.Add("RowIndex", "1");
            item.Add("Column1", "This is the cell value.");

            var validator = new StorageTablesServiceConfigurationValidation().Validate(null, (StorageTablesServiceConfiguration)configPersistence.Value);
            Assert.IsTrue(validator.Succeeded);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Fails: await SutItem.DeleteTableAsync();
        }
    }
}