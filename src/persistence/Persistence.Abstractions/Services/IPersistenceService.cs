using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.Abstractions
{
    public interface IPersistenceService<T> where T : class, IEntity
    {
        Task<T> AddItemAsync(T newEntity);
    }
}
