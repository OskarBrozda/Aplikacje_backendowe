using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Core.Interfaces;
using SchoolSchedule.Infrastructure.Data;

namespace SchoolSchedule.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SchoolScheduleContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(SchoolScheduleContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists((int)entity.GetType().GetProperty("Id").GetValue(entity, null)))
                {
                    throw new Exception("Entity not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        private bool EntityExists(int id)
        {
            return _dbSet.Find(id) != null;
        }
    }
}