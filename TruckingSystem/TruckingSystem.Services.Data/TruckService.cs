using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;

namespace TruckingSystem.Services.Data
{
	public class TruckService : ITruckService
	{
		private IRepository<Truck> truckRepository;

        public TruckService(IRepository<Truck> truckRepository)
        {
            this.truckRepository = truckRepository;
        }


    }
}
