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
        private IConfiguration configuration = new AppConfigurationFactory().Create();
        private ILogger<CosmosDbContainerService<EntityA>> logger = LoggerFactory.CreateLogger<CosmosDbContainerService<EntityA>>();
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
            configCosmos = new CosmosDbServiceOptions(
                configuration["Ciac:Haas:Ingress:CosmosDb:ConnectionString"],
                configuration["Ciac:Haas:Ingress:CosmosDb:DatabaseName"],
                $"AutomatedTest-{DateTime.UtcNow.Year}-{DateTime.UtcNow.Month}-{DateTime.UtcNow.Day}",
                "PartitionKey");
            SutContainer = new CosmosDbContainerService<EntityA>(configCosmos, logger);
            SutItem = new CosmosDbItemService<EntityA>(configCosmos);
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
            var item = new EntityA(configCosmos.Value.PartitionKey) { SomeData = "Some content data." };
            await SutContainer.CreateDatabaseAsync();
            await SutItem.AddItemAsync(item);
            var readItem = await SutItem.GetItemAsync(item.id.ToString());
            Assert.IsTrue(readItem.id == item.id);
        }

        [TestMethod]
        public async Task CosmosDb_Write()
        {
            var item = new EntityA(configCosmos.Value.PartitionKey) { SomeData = "Some content data." };
            await SutContainer.CreateDatabaseAsync();
            await SutItem.AddItemAsync(item);
            var readItem = await SutItem.GetItemAsync(item.id.ToString());
            Assert.IsTrue(readItem.id == item.id);
        }
    }
}