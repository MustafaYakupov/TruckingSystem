using TruckingSystem.Data;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;

namespace TruckingSystem.Infrastructure.Repositories
{
	public class DispatchRepository : Repository<Dispatch>, IDispatchRepository
	{
		private readonly TruckingSystemDbContext context;

		public DispatchRepository(TruckingSystemDbContext context) 
			: base(context)
		{
			this.context = context;
		}
	}
}
