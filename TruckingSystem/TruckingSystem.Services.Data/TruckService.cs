using Microsoft.EntityFrameworkCore;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Truck;

namespace TruckingSystem.Services.Data
{
	public class TruckService : ITruckService
	{
		private IRepository<Truck> truckRepository;

        public TruckService(IRepository<Truck> truckRepository)
        {
            this.truckRepository = truckRepository;
        }

        public async Task<IEnumerable<TruckAllViewModel>> GetAllTrucksAsync()
        {
            IEnumerable<Truck> trucks = await this.truckRepository
                .GetAllAttached()
                .Include(t => t.TrucksParts)
                .ThenInclude(tp => tp.Part.TruckParts)
                .Where(t => t.IsDeleted == false)
                .ToListAsync();

            IEnumerable<TruckAllViewModel> truckViewModel = trucks
                .Select(t => new TruckAllViewModel()
                {
                    Id = t.Id,
                    TruckNumber = t.TruckNumber,
                    Make = t.Make,
                    Model = t.Model,
                    LicensePlate = t.LicensePlate,
                    ModelYear = t.ModelYear,
                    Color = t.Color,
                    TrucksParts = t.TrucksParts,
                });

            return truckViewModel;
        }
    }
}
