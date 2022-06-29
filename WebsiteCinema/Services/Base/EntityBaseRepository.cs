using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace WebsiteCinema.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        protected readonly AppDbContext _context;
        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> AllAsync() =>
            await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> AllAsync(params Expression<Func<T, object>>[] incudeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = incudeProperties.Aggregate(query, (current, incudeProperty) => current.Include(incudeProperty));
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id) =>
            await _context.Set<T>().FirstOrDefaultAsync(actr => actr.Id == id);

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(ent => ent.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        
    }
}
