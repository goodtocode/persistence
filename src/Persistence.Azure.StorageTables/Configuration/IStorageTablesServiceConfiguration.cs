using GoodToCode.Persistence.Abstractions;

namespace GoodToCode.Shared.Persistence.StorageTables
{
    public interface IStorageTablesServiceConfiguration : IPersistenceServiceConfiguration
    {
        string TableName { get; }
    }
}
