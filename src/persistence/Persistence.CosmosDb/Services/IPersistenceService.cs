using GoodToCode.Shared.Persistence;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public interface IPersistenceService<T> where T : class, IEntity
    {
        Task<T> AddAsync(T newEntity);
    }
}
