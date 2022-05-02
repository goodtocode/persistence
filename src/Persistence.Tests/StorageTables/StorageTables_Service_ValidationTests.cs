using GoodToCode.Shared.Persistence.StorageTables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Tests
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
        public void StorageTables_Configuration_Green()
        {
            var validator = new StorageTablesServiceConfigurationValidation().Validate(null, (StorageTablesServiceConfiguration)configPersistence.Value);
            Assert.IsTrue(validator.Succeeded);
        }

        [TestMethod]
        public void StorageTables_Configuration_Red_Length()
        {
            // TableName cannot be over 63 chars
            var invalidTableName = new StorageTablesServiceOptions(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"TooLong1234567890123456789012345678901234567890123456789012345678901234567890");
            var validator = new StorageTablesServiceConfigurationValidation().Validate(null, (StorageTablesServiceConfiguration)invalidTableName.Value);
            Assert.IsTrue(validator.Succeeded);
            Assert.IsFalse(validator.Failed);
        }

        [TestMethod]
        public void StorageTables_Configuration_Red_FirstChar()
        {
            // TableName cannot be over 63 chars
            var invalidTableName = new StorageTablesServiceOptions(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"1NumberAsFirstChar");
            var validator = new StorageTablesServiceConfigurationValidation().Validate(null, (StorageTablesServiceConfiguration)invalidTableName.Value);
            Assert.IsFalse(validator.Succeeded);
            Assert.IsTrue(validator.Failed);
            Assert.IsTrue(validator.FailureMessage.Length > 0);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Fails: await SutItem.DeleteTableAsync();
        }
    }
}