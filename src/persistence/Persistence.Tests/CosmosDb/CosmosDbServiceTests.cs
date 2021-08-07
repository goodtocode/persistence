using GoodToCode.Shared.Persistence.CosmosDb;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace GoodToCode.Shared.Persistence.Tests
{
    [Binding]
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

        [Given(@"I have an CosmosDbService for reading")]
        public void GivenIHaveAnCosmosDbServiceForReading()
        {
            
        }

        [When(@"read a record via CosmosDbService")]
        public void WhenReadARecordViaCosmosDbService()
        {
            
        }

        [Then(@"all the CosmosDbService record contains the expected data")]
        public void ThenAllTheCosmosDbServiceRecordContainsTheExpectedData()
        {
            
        }

        [Given(@"I have an CosmosDbService for writing")]
        public void GivenIHaveAnCosmosDbServiceForWriting()
        {
            
        }

        [When(@"write a record via CosmosDbService")]
        public void WhenWriteARecordViaCosmosDbService()
        {
            
        }

        [Then(@"the record can be read back from CosmosDbService")]
        public void ThenTheRecordCanBeReadBackFromCosmosDbService()
        {
            
        }

    }
}