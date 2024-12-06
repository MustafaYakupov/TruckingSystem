using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;

namespace TruckingSystem.Services.Data
{
	public class DispatchService : IDispatchService
	{
        private IRepository<Dispatch> dispatchRepository;
        public DispatchService(IRepository<Dispatch> dispatchRepository)
        {
            this.dispatchRepository = dispatchRepository;
        }
    }
}
