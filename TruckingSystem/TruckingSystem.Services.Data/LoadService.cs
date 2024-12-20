﻿using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TruckingSystem.Data.Models;
using TruckingSystem.Data.Models.Enums;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels;
using TruckingSystem.Web.ViewModels.Load;
using static TruckingSystem.Common.ValidationConstants.LoadConstants;

namespace TruckingSystem.Services.Data
{
    public class LoadService : ILoadService
    {
        private readonly IRepository<Load> loadRepository;
        private readonly IRepository<BrokerCompany> brokerCompanyRepository;
        private readonly IRepository<Driver> driverRepository;
        private readonly IRepository<Dispatch> dispatchRepository;

		public LoadService(
            IRepository<Load> loadRepository, 
            IRepository<BrokerCompany> brokerCompanyRepository, 
            IRepository<Driver> driverRepository,
			IRepository<Dispatch> dispatchRepository)
        {
            this.loadRepository = loadRepository;
            this.brokerCompanyRepository = brokerCompanyRepository;
            this.driverRepository = driverRepository;
            this.dispatchRepository = dispatchRepository;
        }

        public async Task<PaginatedList<LoadAllViewModel>> GetAllLoadsAsync(int page, int pageSize)
        {
            int totalLoads = await this.loadRepository
                .GetAllAttached()
                .Where(l => l.IsDeleted == false)
                .Where(l => l.Status == DispatchStatus.Available)
                .CountAsync();

            IEnumerable<Load> loads = await this.loadRepository
                .GetAllAttached()
                .Where(l => l.IsDeleted == false)
                .Where(l => l.Status == DispatchStatus.Available)
                .Include(l => l.BrokerCompany)
                .OrderBy(l => l.PickupTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<LoadAllViewModel> loadViewModel = loads
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
                }).ToList();

            PaginatedList<LoadAllViewModel> paginatedList = new PaginatedList<LoadAllViewModel>(loadViewModel, totalLoads, page, pageSize);

            return paginatedList;
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

            Load load = new()
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

            await this.loadRepository.AddAsync(load);

            return true;
        }

        public async Task<LoadEditInputModel> GetEditLoadByIdAsync(Guid id)
        {
            LoadEditInputModel? viewModel = await this.loadRepository
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

        public async Task<bool> PostEditLoadByIdAsync(LoadEditInputModel model, Guid id)
        {
			Load? load = await this.loadRepository
			   .GetAllAttached()
			   .Where(l => l.Id == id)
			   .Where(l => l.IsDeleted == false)
               .Include(l => l.BrokerCompany)
			   .FirstOrDefaultAsync();

            if (load == null || load.IsDeleted)
            {
                return false;
            }

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

			load.PickupLocation = model.PickupLocation;
            load.DeliveryLocation = model.DeliveryLocation;
            load.Weight = model.Weight;
            load.Temperature = model.Temperature ?? null;
            load.Distance = model.Distance;
            load.BrokerCompanyId = model.BrokerCompanyId;
            load.PickupTime = pickupTime;
            load.DeliveryTime = deliveryTime;

            await this.loadRepository.UpdateAsync(load);

            return true;
		}

		public async Task<LoadDeleteViewModel> DeleteLoadGetAsync(Guid id)
        {
            LoadDeleteViewModel? deleteModel = await this.loadRepository
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
            Load? load = await this.loadRepository
                .GetAllAttached()
                .Where(l => l.Id == model.Id)
                .Where(l => l.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (load != null)
            {
                load.IsDeleted = true;
                await this.loadRepository.UpdateAsync(load);
            }
        }

        public async Task<LoadAssignInputModel> GetAssignLoadByIdAsync(Guid id)
        {
            LoadAssignInputModel? viewModel = await this.loadRepository
                .GetAllAttached()
                .Where(l => l.Id == id)
                .Where(l => l.IsDeleted == false)
                .Include(l => l.Driver)
                .AsNoTracking()
                .Select(l => new LoadAssignInputModel()
                {
                    LoadId = l.Id,
                    DriverId = l.DriverId ?? Guid.Empty,
                })
                .FirstOrDefaultAsync();

            if (viewModel == null)
            {
                return null;
            }

            await LoadAvailableDrivers(viewModel);


            return viewModel;
        }

		public async Task<bool> PostAssignLoadByIdAsync(LoadAssignInputModel model, Guid id)
		{
			Load? load = await this.loadRepository
			   .GetAllAttached()
			   .Where(l => l.Id == id)
			   .Where(l => l.IsDeleted == false)
			   .Include(l => l.BrokerCompany)
               .Include(l => l.Driver)
			   .FirstOrDefaultAsync();

			if (load == null || load.IsDeleted)
			{
				return false;
			}

            Driver? driver = await this.driverRepository
                .GetAllAttached().
                Where(d => d.Id == model.DriverId).
                Where(d => d.IsDeleted == false)
				.FirstOrDefaultAsync();

            if (driver == null || driver.IsDeleted)
            {
				return false;
			}

			load.DriverId = model.DriverId;
            load.Status = DispatchStatus.InProgress;

            driver.IsAvailable = false;

			await this.loadRepository.UpdateAsync(load);
			await this.driverRepository.UpdateAsync(driver);

			Dispatch dispatch = new()
            {
                DriverId = driver.Id,
                DriverManagerId = load.Driver?.DriverManagerId ?? Guid.Empty,
                LoadId = load.Id,
                TruckId = driver.TruckId ?? Guid.Empty,
                TrailerId = driver.TrailerId ?? Guid.Empty,
                Status = DispatchStatus.InProgress,
            };

            await this.dispatchRepository.AddAsync(dispatch);

			return true;
		}

		public async Task LoadAvailableDrivers(LoadAssignInputModel model)
        {
            model.Drivers = await this.driverRepository
                .GetAllAttached()
                .Include(d => d.Truck)
                .Include(d => d.Trailer)
                .Include(d => d.DriverManager)
                .Where(d => d.IsDeleted == false)
                .Where(d => d.Truck != null)
                .Where(d => d.Trailer != null)
                .Where(d => d.DriverManager != null)
                .ToListAsync();
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
            return await this.brokerCompanyRepository
                .GetAllAsync();
        }
    }
}
