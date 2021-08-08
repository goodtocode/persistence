using GoodToCode.Shared.Persistence.CosmosDb;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GoodToCode.Shared.Persistence.Tests
{
    [TestClass]
    public class CosmosDbServiceTests
    {
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
            SutService = new CosmosDbService<EntityA>();
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