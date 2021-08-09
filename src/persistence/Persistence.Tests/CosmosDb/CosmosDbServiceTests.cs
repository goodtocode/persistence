using GoodToCode.Shared.Persistence.CosmosDb;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GoodToCode.Shared.Persistence.Tests
{
    [TestClass]
    public class CosmosDbServiceTests
    {
        private IConfiguration configuration = new AppConfigurationFactory().Create();
        private ILogger<CosmosDbService<EntityA>> logger = LoggerFactory.CreateLogger<CosmosDbService<EntityA>>();
        private readonly string cosmosDbSetting = "Ciac:Haas:Persistence";

        public CosmosDbService<EntityA> SutService { get; private set; }
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
            var config = new CosmosDbServiceConfiguration(
                cosmosDbSection["ConnectionString"],
                cosmosDbSection["DatabaseName"],
                $"{DateTime.UtcNow:o}",
                "PartitionKey");
            SutService = new CosmosDbService<EntityA>(config, logger);
        }

        [TestMethod]
        public void CosmosDb_Read()
        {
        }


        public void CosmosDb_Write()
        {

        }
    }
}