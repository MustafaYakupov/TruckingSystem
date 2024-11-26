using TruckingSystem.Data;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;

namespace TruckingSystem.Infrastructure.Repositories
{
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        private readonly TruckingSystemDbContext context;

        public DriverRepository(TruckingSystemDbContext context)
            : base(context)
        {
            this.context = context; 
        }
    }
}
