using Microsoft.EntityFrameworkCore;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Load;
using static TruckingSystem.Common.ValidationConstants.LoadConstants;

namespace TruckingSystem.Services.Data
{
    public class LoadService : ILoadService
    {
        private IRepository<Load> loadRepository;

        public LoadService(IRepository<Load> loadRepository)
        {
            this.loadRepository = loadRepository;
        }

        public async Task<IEnumerable<LoadAllViewModel>> GetAllLoadsAsync()
        {
            IEnumerable<Load> loads = await this.loadRepository
                .GetAllAttached()
                .Where(l => l.IsDeleted == false)
                .Where(l => l.IsAvailable == true)
                .Include(l => l.BrokerCompany)
                .ToListAsync();

            IEnumerable<LoadAllViewModel> loadViewModel = loads
                .Select(l => new LoadAllViewModel()
                {
                    Id = l.Id,
                    PickupLocation = l.PickupLocation,
                    DeliveryLocation = l.DeliveryLocation,
                    Weight = l.Weight,
                    Temperature = l.Temperature?.ToString() ?? String.Empty,
                    PickupTime = l.PickupTime.ToString(DateTimeFormat),
                    DeliveryTime = l.DeliveryTime.ToString(DateTimeFormat),
                    Distance = l.Distance,
                    BrokerCompany = l.BrokerCompany.CompanyName,
                });

            return loadViewModel;
        }
    }
}
