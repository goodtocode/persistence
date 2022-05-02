using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.Abstractions
{
    public interface IEntityPersistenceService<T> where T : IEntity, new()
    {
        Task<IEnumerable<T>> GetItemsAsync(string query);
        Task<T> GetItemAsync(string id, string partitionKeyPath);
        Task AddItemAsync(T item);
        Task UpdateItemAsync(string id, T item);
        Task DeleteItemAsync(string id, string partitionKeyPath);
    }
}
