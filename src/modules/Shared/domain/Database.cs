
namespace Shared.Domain
{
    public interface IDatabase<T>
    {
        Task<List<T>> GetAllAsync();
        Task GetByIdAsync(string id);
        Task InsertAsync(T entity);
        Task UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);
    }
}