using GoodToCode.Shared.Persistence.Abstractions;

namespace GoodToCode.Shared.Persistence.StorageTables
{
    public interface IStorageTablesItemService<T> : IEntityPersistenceService<T> where T : IEntity, new()
    {
    }
}
