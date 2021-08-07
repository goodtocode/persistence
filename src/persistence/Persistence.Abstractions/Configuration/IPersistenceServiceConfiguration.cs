namespace GoodToCode.Shared.Persistence.Abstractions
{
    public interface IPersistenceServiceConfiguration
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
