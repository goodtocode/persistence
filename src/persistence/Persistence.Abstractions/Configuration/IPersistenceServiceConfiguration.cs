namespace GoodToCode.Shared.Persistence
{
    public interface IPersistenceServiceConfiguration
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
