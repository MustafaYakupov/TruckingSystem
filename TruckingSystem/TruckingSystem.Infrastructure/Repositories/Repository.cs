using Microsoft.EntityFrameworkCore;
using TruckingSystem.Data;
using TruckingSystem.Infrastructure.Repositories.Contracts;

namespace TruckingSystem.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly TruckingSystemDbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(TruckingSystemDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await this.dbSet.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                this.dbSet.Remove(entity);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.dbSet.ToListAsync();
        }

        public IQueryable<T> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public async Task RemoveRange(ICollection<T> entities)
        {
            this.dbSet.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            this.dbSet.Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
        }
    }
}
