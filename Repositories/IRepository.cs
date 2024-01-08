using Play.Common.Entities;

namespace Play.Common.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid Id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(Guid id, T entity);

        Task<bool> DeleteAsync(Guid id);
    }
}