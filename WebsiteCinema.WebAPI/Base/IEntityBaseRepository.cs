using System.Linq.Expressions;

namespace WebsiteCinema.WebAPI.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    { 
        Task<IEnumerable<T>> AllAsync();
        Task<IEnumerable<T>> AllAsync(params Expression<Func<T, object>>[] incudeProperties);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
} 
