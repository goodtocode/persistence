namespace GoodToCode.Shared.Workflows
{
    public struct AppConfigurationKeys
    {
        public const string SentinelSetting = "Ciac:Shared:Sentinel";
        public const string CognitiveServicesEndpoint = "Ciac:Haas:Ingress:CognitiveServices:Endpoint";
        public const string CognitiveServicesKeyCredential = "Ciac:Haas:Ingress:CognitiveServices:KeyCredential";
        public const string TextAnalyticsEndpoint = "Ciac:Haas:Ingress:TextAnalytics:Endpoint";
        public const string TextAnalyticsKeyCredential = "Ciac:Haas:Ingress:TextAnalytics:KeyCredential";
        public const string StorageTablesConnectionString = "Ciac:Haas:Ingress:StorageTables:ConnectionString";
    }
}
