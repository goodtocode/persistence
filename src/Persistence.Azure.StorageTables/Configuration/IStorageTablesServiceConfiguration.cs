using GoodToCode.Persistence.Abstractions;

namespace GoodToCode.Persistence.Azure.StorageTables
{
    public interface IStorageTablesServiceConfiguration : IPersistenceServiceConfiguration
    {
        string TableName { get; }
    }
}
