namespace TruckingSystem.Infrastructure.Repositories.Contracts
{
    public interface IRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        IQueryable<T> GetAllAttached();

        Task<T> GetByIdAsync(Guid id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(Guid id);

        Task RemoveRange(ICollection<T> entities);
    }
}
