namespace GoodToCode.Shared.Persistence.Abstractions
{
    public interface IPersistenceServiceConfiguration
    {
        string ConnectionString { get; }
        string DatabaseName { get;  }
    }
}
