using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Driver;

namespace TruckingSystem.Services.Data
{
    public class DriverService : IDriverService
    {
        private readonly IRepository<Driver> driverRepository;
        private readonly IRepository<DriverManager> driverManagerRepository;
        private readonly IRepository<Truck> truckRepository;
        private readonly IRepository<Trailer> trailerRepository;

        public DriverService(
            IRepository<Driver> driverRepository,
            IRepository<DriverManager> driverManagerRepository,
            IRepository<Truck> truckRepository,
            IRepository<Trailer> trailerRepository)
        {
            this.driverRepository = driverRepository;
            this.driverManagerRepository = driverManagerRepository;
            this.truckRepository = truckRepository;
            this.trailerRepository = trailerRepository;
        }

        public async Task<IEnumerable<DriverAllViewModel>> GetAllDriversAsync()
        {
            IEnumerable<Driver> drivers = await this.driverRepository
                .GetAllAttached()
                .Include(d => d.Truck)
                .Include(d => d.Trailer)
                .Include(d => d.DriverManager)
                .Where(d => d.IsDeleted == false)
                .ToListAsync();

            IEnumerable<DriverAllViewModel> driverViewModel = drivers
                .Select(d => new DriverAllViewModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    LicenseNumber = d.LicenseNumber,
                    TruckNumber = d.Truck?.TruckNumber ?? string.Empty,
                    TrailerNumber = d.Trailer?.TrailerNumber ?? string.Empty,
                    DriverManager = d.DriverManager?.FirstName + " " + d.DriverManager?.LastName ?? string.Empty,
                });

            return driverViewModel;
        }

		public async Task CreateDriverAsync(DriverAddInputModel model)
		{
            Driver driver = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                LicenseNumber = model.LicenseNumber,
                TruckId = model.TruckId,
                TrailerId = model.TrailerId,
                DriverManagerId = model.DriverManagerId,
            };

            await this.driverRepository.AddAsync(driver);
		}

		public async Task<DriverEditInputModel> GetEditDriverByIdAsync(Guid id)
        {
			DriverEditInputModel? viewModel = await this.driverRepository
                .GetAllAttached()
                .Where(d => d.Id == id)
                .Where(d => d.IsDeleted == false)
                .AsNoTracking()
                .Select(d => new DriverEditInputModel()
                {
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    LicenseNumber = d.LicenseNumber,
                    TruckId = d.TruckId,
                    TrailerId = d.TrailerId,
                    TrailerNumber = d.Trailer.TrailerNumber ?? null,
                    TruckNumber = d.Truck.TruckNumber ?? null,
                    DriverManagerId = d.DriverManagerId,
                })
                .FirstOrDefaultAsync();

            if (viewModel == null)
            {
                return null;
            }

            await LoadSelectLists(viewModel);


            return viewModel;
        }

        public async Task<bool> PostEditDriverByIdAsync(DriverEditInputModel model, Guid id)
        {
            Driver? driver = await this.driverRepository
                .GetAllAttached()
                .Where(d => d.Id == id)
                .Where(d => d.IsDeleted == false)
                .Include(d => d.Truck)
                .Include(d => d.Trailer)
                .FirstOrDefaultAsync();

            if (driver == null || driver.IsDeleted)
            {
                return false;
            }

            driver.FirstName = model.FirstName;
            driver.LastName = model.LastName;
            driver.LicenseNumber = model.LicenseNumber;
            driver.DriverManagerId = model.DriverManagerId;

            if (model.DriverManagerId != null)
            {
                DriverManager? manager = await this.driverManagerRepository.GetAllAttached()
                    .Where(m => m.Id == model.DriverManagerId)
                    .FirstOrDefaultAsync();

                if (manager != null)
                {
                    manager.Drivers.Add(driver);
                }
            }

            if (model.TruckId != driver.TruckId)
            {
                // Set previous truck's availability to true
                if (driver.TruckId.HasValue)
                {
                    Truck? oldTruck = await this.truckRepository.GetAllAttached()
                        .Where(t => t.Id == driver.TruckId)
                        .FirstOrDefaultAsync();

                    Truck? newTruck = await this.truckRepository.GetAllAttached()
                        .Where(t => t.Id == model.TruckId)
                        .FirstOrDefaultAsync();

                    if (oldTruck != newTruck)
                    {
                        if (oldTruck != null)
                        {
                            oldTruck.IsAvailable = true;
                            await this.truckRepository.UpdateAsync(oldTruck);
                        }

                        // Assign new truck and set its availability to false
                        if (model.TruckId.HasValue)
                        {
                            if (newTruck != null)
                            {
                                newTruck.IsAvailable = false;
                                driver.TruckId = newTruck.Id;
                                await this.truckRepository.UpdateAsync(newTruck);
                            }
                        }
                        else
                        {
                            driver.TruckId = null;
                        }
                    }
                }
                else
                {
                    Truck? newTruck = await this.truckRepository.GetAllAttached()
                        .Where(t => t.Id == model.TruckId)
                        .FirstOrDefaultAsync();

                    if (newTruck != null)
                    {
                        newTruck.IsAvailable = false;
                        driver.TruckId = newTruck.Id;
                        await this.truckRepository.UpdateAsync(newTruck);
                    }
                }
            }

            if (model.TrailerId != driver.TrailerId)
            {
                // Set previous trailer's availability to true
                if (driver.TrailerId.HasValue)
                {
                    Trailer? oldTrailer = await this.trailerRepository.GetAllAttached()
                        .Where(t => t.Id == driver.TrailerId)
                        .FirstOrDefaultAsync();

                    Trailer? newTrailer = await this.trailerRepository.GetAllAttached()
                        .Where(t => t.Id == model.TrailerId)
                        .FirstOrDefaultAsync();

                    if (oldTrailer != newTrailer)
                    {
                        if (oldTrailer != null)
                        {
                            oldTrailer.IsAvailable = true;
                            await this.trailerRepository.UpdateAsync(oldTrailer);
                        }

                        // Assign new trailer and set its availability to false
                        if (model.TrailerId.HasValue)
                        {
                            if (newTrailer != null)
                            {
                                newTrailer.IsAvailable = false;
                                driver.TrailerId = newTrailer.Id;
                                await this.trailerRepository.UpdateAsync(newTrailer);
                            }
                        }
                        else
                        {
                            driver.TrailerId = null;
                        }
                    }
                }
                else
                {
                    Trailer? newTrailer = await this.trailerRepository.GetAllAttached()
                        .Where(t => t.Id == model.TrailerId)
                        .FirstOrDefaultAsync();

                    if (newTrailer != null)
                    {
                        newTrailer.IsAvailable = false;
                        driver.TrailerId = newTrailer.Id;
                        await this.trailerRepository.UpdateAsync(newTrailer);
                    }
                }
            }

            await this.driverRepository.UpdateAsync(driver);

            return true;
        }

		public async Task<DriverDeleteViewModel> DeleteDriverGetAsync(Guid id)
		{
			DriverDeleteViewModel? deleteModel  = await this.driverRepository
                .GetAllAttached()
				.Where(d => d.Id == id)
				.Where(d => d.IsDeleted == false)
				.AsNoTracking()
				.Select(d => new DriverDeleteViewModel()
				{
					Id = d.Id,
					FirstName = d.FirstName,
					LastName = d.LastName
				})
				.FirstOrDefaultAsync();

            return deleteModel;
		}

        public async Task DeleteDriverAsync(DriverDeleteViewModel model)
        {
            Driver? driver = await this.driverRepository
                .GetAllAttached()
                .Where(d => d.Id == model.Id)
                .Where(d => d.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (driver != null)
            {
                driver.IsDeleted = true;
                await this.driverRepository.UpdateAsync(driver);
            }
        }

        public async Task LoadSelectLists(DriverEditInputModel model)
        {
            model.AvailableTrucks = await GetTrucks();
            model.AvailableTrailers = await GetTrailers();
            model.DriverManagers = await GetDriverManagers();
        }

        public async Task LoadSelectLists(DriverAddInputModel model)
        {
            model.AvailableTrucks = await GetTrucks();
            model.AvailableTrailers = await GetTrailers();
            model.DriverManagers = await GetDriverManagers();
        }

        private async Task<IEnumerable<Trailer>> GetTrailers()
        {
            return await this.trailerRepository
                .GetAllAttached()
                .Where(t => t.IsDeleted == false)
                .Where(t => t.IsAvailable == true)
                .ToListAsync();
        }

        private async Task<IEnumerable<Truck>> GetTrucks()
        {
            return await this.truckRepository
                .GetAllAttached()
                .Where(t => t.IsDeleted == false)
                .Where(t => t.IsAvailable == true)
                .ToListAsync();
        }

        private async Task<IEnumerable<DriverManager>> GetDriverManagers()
        {
            return await this.driverManagerRepository
                .GetAllAttached()
                .Where(t => t.IsDeleted == false)
                .ToListAsync();
        }
	}
}
