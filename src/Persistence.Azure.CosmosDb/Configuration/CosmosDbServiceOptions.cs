using Microsoft.Extensions.Options;

namespace GoodToCode.Persistence.Azure.CosmosDb
{
    public class CosmosDbServiceOptions : IOptions<ICosmosDbServiceConfiguration>
    {
        public ICosmosDbServiceConfiguration Value { get; }

        public CosmosDbServiceOptions(string connectionString, string tableName)
        {
            Value = new CosmosDbServiceConfiguration(connectionString, tableName);
        }
    }
}
