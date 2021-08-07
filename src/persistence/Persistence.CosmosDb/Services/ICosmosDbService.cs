using GoodToCode.Shared.Persistence.Abstractions;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public interface ICosmosDbService<T> : IPersistenceService<T> where T : class, IEntity
    {
    }
}
