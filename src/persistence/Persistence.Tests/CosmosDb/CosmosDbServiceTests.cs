using GoodToCode.Shared.Persistence.CosmosDb;
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
    public class CosmosDbServiceTests
    {
        private IConfiguration configuration;
        private ILogger<CosmosDbContainerService<EntityA>> logContainer;
        private ILogger<CosmosDbItemService<EntityA>> logItem;
        private CosmosDbServiceOptions configCosmos;
        public CosmosDbContainerService<EntityA> SutContainer { get; private set; }
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
            logContainer = LoggerFactory.CreateLogger<CosmosDbContainerService<EntityA>>();
            logItem = LoggerFactory.CreateLogger<CosmosDbItemService<EntityA>>();
            configuration = new AppConfigurationFactory().Create();
            configCosmos = new CosmosDbServiceOptions(
                configuration["Ciac:Haas:Ingress:CosmosDb:ConnectionString"],
                configuration["Ciac:Haas:Ingress:CosmosDb:DatabaseName"],
                $"AutomatedTest-{DateTime.UtcNow.Year}-{DateTime.UtcNow.Month}-{DateTime.UtcNow.Day}",
                "/PartitionKey");
            SutContainer = new CosmosDbContainerService<EntityA>(configCosmos, logContainer);
            SutItem = new CosmosDbItemService<EntityA>(configCosmos, logItem);
        }

        [TestMethod]
        public async Task CosmosDb_Create()
        {
            var db = await SutContainer.CreateDatabaseAsync();
            Assert.IsTrue(db != null);
        }

        [TestMethod]
        public async Task CosmosDb_Read()
        {
            var item = new EntityA("PartRead") { SomeData = "Some read data." };
            await SutContainer.CreateDatabaseAsync();
            await SutItem.AddItemAsync(item);
            var readItem = await SutItem.GetItemAsync(item.id.ToString(), item.PartitionKey);
            Assert.IsTrue(readItem.id == item.id);
        }

        [TestMethod]
        public async Task CosmosDb_Write()
        {
            var item = new EntityA("PartWrite") { SomeData = "Some write data." };
            await SutContainer.CreateDatabaseAsync();
            await SutItem.AddItemAsync(item);
            var writeItem = await SutItem.GetItemAsync(item.id.ToString(), item.PartitionKey);
            Assert.IsTrue(writeItem.id == item.id);
            await SutItem.DeleteItemAsync(writeItem.id.ToString(), writeItem.PartitionKey);
            writeItem = await SutItem.GetItemAsync(item.id.ToString(), item.PartitionKey);
            Assert.IsTrue(writeItem.id != item.id);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await SutContainer.DeleteContainerAsync();
        }
    }
}