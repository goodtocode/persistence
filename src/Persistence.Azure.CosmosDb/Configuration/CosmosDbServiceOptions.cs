using Microsoft.Extensions.Options;

namespace GoodToCode.Shared.Persistence.CosmosDb
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
