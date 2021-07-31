using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence
{
    public interface IPersistenceService<T> where T : class, IEntity
    {
        Task<T> AddAsync(T newEntity);
    }
}
