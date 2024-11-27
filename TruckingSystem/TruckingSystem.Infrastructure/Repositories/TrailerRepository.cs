using TruckingSystem.Data;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;

namespace TruckingSystem.Infrastructure.Repositories
{
    public class TrailerRepository : Repository<Trailer>, ITrailerRepository
    {
        private readonly TruckingSystemDbContext context;

        public TrailerRepository(TruckingSystemDbContext context) 
            : base(context)
        {
            this.context = context;
        }
    }
}
