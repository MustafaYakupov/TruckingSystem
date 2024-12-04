using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Truck;

namespace TruckingSystem.Services.Data
{
    public class TruckService : ITruckService
    {
        private IRepository<Truck> truckRepository;
        private IRepository<Part> partRepository;

        public TruckService(IRepository<Truck> truckRepository, IRepository<Part> partRepository)
        {
            this.truckRepository = truckRepository;
            this.partRepository = partRepository;
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

        public async Task<TruckDeleteViewModel> DeleteTruckGetAsync(Guid id)
        {
            TruckDeleteViewModel? deleteModel = await truckRepository
                .GetAllAttached()
                .Where(t => t.Id == id)
                .Where(t => t.IsDeleted == false)
                .AsNoTracking()
                .Select(t => new TruckDeleteViewModel()
                { 
                    Id = t.Id,
                    TruckNumber = t.TruckNumber
                })
                .FirstOrDefaultAsync();

            return deleteModel;
        }

        public async Task DeleteTruckAsync(TruckDeleteViewModel model)
        {
            Truck? truck = await truckRepository
                .GetAllAttached()
                .Where(t => t.Id == model.Id)
                .Where(t => t.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (truck != null)
            {
                truck.IsDeleted = true;
                await truckRepository.UpdateAsync(truck);
            }
        }

        public async Task LoadPartsList(TruckAddInputModel model)
        {
            model.Parts = await GetParts();
        }

        private async Task<IEnumerable<Part>> GetParts()
        {
            return await partRepository
                .GetAllAttached()
                .ToListAsync();
        }
    }
}
