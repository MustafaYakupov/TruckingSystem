using TruckingSystem.Data;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;

namespace TruckingSystem.Infrastructure.Repositories
{
    public class DriverManagerRepository : Repository<DriverManager>, IDriverManagerRepository
    {
        private readonly TruckingSystemDbContext context;

        public DriverManagerRepository(TruckingSystemDbContext context) 
            : base(context)
        {
            this.context = context;
        }
    }
}
