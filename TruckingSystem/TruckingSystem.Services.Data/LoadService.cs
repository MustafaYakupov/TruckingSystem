using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Driver;
using TruckingSystem.Web.ViewModels.Load;
using static TruckingSystem.Common.ValidationConstants.LoadConstants;

namespace TruckingSystem.Services.Data
{
    public class LoadService : ILoadService
    {
        private IRepository<Load> loadRepository;
        private IRepository<BrokerCompany> brokerCompanyRepository;

        public LoadService(IRepository<Load> loadRepository, IRepository<BrokerCompany> brokerCompanyRepository)
        {
            this.loadRepository = loadRepository;
            this.brokerCompanyRepository = brokerCompanyRepository;
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

        public async Task<bool> CreateLoadAsync(LoadAddInputModel model)
        {
            bool isPickupTimeValid = DateTime.TryParseExact(model.PickupTime,
                DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime pickupTime);

            bool isDeliveryTimeValid = DateTime.TryParseExact(model.DeliveryTime,
                DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime deliveryTime);

            if (isPickupTimeValid == false || isDeliveryTimeValid == false)
            {
                return false;
            }

            Load load = new Load()
            {
                PickupLocation = model.PickupLocation,
                DeliveryLocation = model.DeliveryLocation,
                Weight = model.Weight,
                Temperature = model?.Temperature ?? null,
                PickupTime = pickupTime,
                DeliveryTime = deliveryTime,
                Distance = model.Distance,
                BrokerCompanyId = model.BrokerCompanyId
            };

            await loadRepository.AddAsync(load);

            return true;
        }

        public async Task<LoadEditInputModel> GetEditLoadByIdAsync(Guid id)
        {
            LoadEditInputModel? viewModel = await loadRepository
                .GetAllAttached()
                .Where(l => l.Id == id)
                .Where(l => l.IsDeleted == false)
                .AsNoTracking()
                .Select(l => new LoadEditInputModel()
                {
                    PickupLocation = l.PickupLocation,
                    DeliveryLocation = l.DeliveryLocation,
                    Weight = l.Weight,
                    Temperature = l.Temperature ?? null,
                    PickupTime = l.PickupTime.ToString(DateTimeFormat),
                    DeliveryTime = l.DeliveryTime.ToString(DateTimeFormat),
                    Distance = l.Distance,
                    BrokerCompanyId = l.BrokerCompanyId
				})
                .FirstOrDefaultAsync();

            if (viewModel == null)
            {
                return null;
            }

            await LoadBrokerCompanies(viewModel);


            return viewModel;
        }

        public async Task<LoadDeleteViewModel> DeleteLoadGetAsync(Guid id)
        {
            LoadDeleteViewModel? deleteModel = await loadRepository
                .GetAllAttached()
                .Where(l => l.Id == id)
                .Where(l => l.IsDeleted == false)
                .AsNoTracking()
                .Select(l => new LoadDeleteViewModel()
                {
                    Id = l.Id,
                    PickupLocation = l.PickupLocation,
                    DeliveryLocation = l.DeliveryLocation
                })
                .FirstOrDefaultAsync();

            return deleteModel;
        }

        public async Task DeleteLoadAsync(LoadDeleteViewModel model)
        {
            Load? load = await loadRepository
                .GetAllAttached()
                .Where(l => l.Id == model.Id)
                .Where(l => l.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (load != null)
            {
                load.IsDeleted = true;
                await loadRepository.UpdateAsync(load);
            }
        }


        public async Task LoadBrokerCompanies(LoadAddInputModel model)
        {
            model.BrokerCompanies = await GetBrokerCompanies();
        }

        public async Task LoadBrokerCompanies(LoadEditInputModel model)
        {
            model.BrokerCompanies = await GetBrokerCompanies();
        }

        private async Task<IEnumerable<BrokerCompany>> GetBrokerCompanies()
        {
            return await brokerCompanyRepository
                .GetAllAsync();
        }
    }
}
