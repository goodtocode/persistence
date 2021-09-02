namespace GoodToCode.Shared.Persistence.Tests
{
    public struct AppConfigurationKeys
    {
        public const string SentinelSetting = "Shared:Sentinel";
        public const string StorageTablesConnectionString = "Gtc:Shared:Persistence:StorageTables:ConnectionString";
        public const string CosmosDbConnectionString = "Gtc:Shared:Persistence:CosmosDb:ConnectionString";        
    }
}
