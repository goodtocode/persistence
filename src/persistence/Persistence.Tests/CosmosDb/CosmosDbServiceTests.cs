using GoodToCode.Shared.Persistence.CosmosDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.Tests
{
    [TestClass]
    public class CosmosDbServiceTests
    {
        private IConfiguration configuration;
        private ILogger<CosmosDbItemService<EntityA>> logItem;
        private CosmosDbServiceOptions configCosmos;
        public CosmosDbItemService<EntityA> SutItem { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public CosmosDbServiceTests()
        {
            ///     private readonly IDataService<IEntity> _dataService;
            ///     var myFileContent = GetFileContents(myFile);
            ///     if (myFileContent != null)
            ///     {
            ///         await _dataService.AddAsync(myFileContent);
            ///     }
        }

        [TestInitialize]
        public void Initialize()
        {
            logItem = LoggerFactory.CreateLogger<CosmosDbItemService<EntityA>>();
            configuration = new AppConfigurationFactory().Create();
            configCosmos = new CosmosDbServiceOptions(
                configuration["Gtc:Shared:Persistence:CosmosDb:ConnectionString"],
                $"AutomatedTest-{DateTime.UtcNow:O}"); 
            SutItem = new CosmosDbItemService<EntityA>(configCosmos, logItem);
        }

        [TestMethod]
        public async Task CosmosDb_Create()
        {
            var table = await SutItem.CreateOrGetTableAsync();
            Assert.IsTrue(table != null);
        }

        [TestMethod]
        public async Task CosmosDb_Read()
        {
            var item = new EntityA("PartRead") { SomeData = "Some read data." };
            await SutItem.AddItemAsync(item);
            var readItem = SutItem.GetItem(item.RowKey);
            Assert.IsTrue(readItem.RowKey == item.RowKey);
        }

        [TestMethod]
        public async Task CosmosDb_Write()
        {
            var item = new EntityA("PartWrite") { SomeData = "Some write data." };
            await SutItem.AddItemAsync(item);
            var writeItem = SutItem.GetItem(item.RowKey.ToString());
            Assert.IsTrue(writeItem.RowKey == item.RowKey);
            await SutItem.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
            writeItem = SutItem.GetItem(item.RowKey);
            Assert.IsTrue(writeItem == null);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await SutItem.DeleteTableAsync();
        }
    }
}