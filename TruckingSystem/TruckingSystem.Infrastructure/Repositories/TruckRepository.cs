using TruckingSystem.Data;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;

namespace TruckingSystem.Infrastructure.Repositories
{
    public class TruckRepository : Repository<Truck>, ITruckRepository
    {
        private readonly TruckingSystemDbContext context;

        public TruckRepository(TruckingSystemDbContext context)
            : base(context) 
        {
            this.context = context;
        }
    }
}
