using GoodToCode.Shared.Persistence.Abstractions;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public interface ICosmosDbItemService<T> : IEntityPersistenceService<T> where T : IEntity, new()
    {
    }
}
