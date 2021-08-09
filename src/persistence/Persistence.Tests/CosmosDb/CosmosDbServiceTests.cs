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
        private readonly string cosmosDbSetting = "Ciac:Haas:Persistence";

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
            var cosmosDbSection = configuration.GetSection(cosmosDbSetting);
            var config = new CosmosDbServiceOptions(
                cosmosDbSection["ConnectionString"],
                cosmosDbSection["DatabaseName"],
                $"{DateTime.UtcNow:o}",
                "PartitionKey");
            SutContainer = new CosmosDbContainerService<EntityA>(config, logger);
            SutItem = new CosmosDbItemService<EntityA>(config);
        }

        [TestMethod]
        public async Task CosmosDb_Create()
        {

        }

        [TestMethod]
        public async Task CosmosDb_Read()
        {
        }

        [TestMethod]
        public async Task CosmosDb_Write()
        {

        }
    }
}