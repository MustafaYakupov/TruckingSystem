using Microsoft.EntityFrameworkCore;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Driver;

namespace TruckingSystem.Services.Data
{
    public class DriverService : IDriverService
    {
        private IRepository<Driver> driverRepository;
        private IRepository<DriverManager> driverManagerRepository;
        private IRepository<Truck> truckRepository;
        private IRepository<Trailer> trailerRepository;

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

        public async Task<DriverEditViewModel> GetEditDriverByIdAsync(Guid id)
        {
            DriverEditViewModel? viewModel = await driverRepository
                .GetAllAttached()
                .Where(d => d.Id == id)
                .Where(d => d.IsDeleted == false)
                .AsNoTracking()
                .Select(d => new DriverEditViewModel()
                {
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    LicenseNumber = d.LicenseNumber,
                    TruckId = d.TruckId,
                    TrailerId = d.TrailerId,
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

        public async Task<bool> PostEditDriverByIdAsync(DriverEditViewModel model, Guid id)
        {
            Driver? driver = await driverRepository
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
                DriverManager? manager = await driverManagerRepository.GetAllAttached()
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
                    var oldTruck = await truckRepository.GetAllAttached()
                        .Where(t => t.Id == driver.TruckId)
                        .FirstOrDefaultAsync();

                    var newTruck = await truckRepository.GetAllAttached()
                        .Where(t => t.Id == model.TruckId)
                        .FirstOrDefaultAsync();

                    if (oldTruck != newTruck)
                    {
                        if (oldTruck != null)
                        {
                            oldTruck.IsAvailable = true;
                            await truckRepository.UpdateAsync(oldTruck);
                        }

                        // Assign new truck and set its availability to false
                        if (model.TruckId.HasValue)
                        {
                            if (newTruck != null)
                            {
                                newTruck.IsAvailable = false;
                                driver.TruckId = newTruck.Id;
                                await truckRepository.UpdateAsync(newTruck);
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
                    var newTruck = await truckRepository.GetAllAttached()
                        .Where(t => t.Id == model.TruckId)
                        .FirstOrDefaultAsync();

                    if (newTruck != null)
                    {
                        newTruck.IsAvailable = false;
                        driver.TruckId = newTruck.Id;
                        await truckRepository.UpdateAsync(newTruck);
                    }
                }
            }

            if (model.TrailerId != driver.TrailerId)
            {
                // Set previous trailer's availability to true
                if (driver.TrailerId.HasValue)
                {
                    var oldTrailer = await trailerRepository.GetAllAttached()
                        .Where(t => t.Id == driver.TrailerId)
                        .FirstOrDefaultAsync();

                    var newTrailer = await trailerRepository.GetAllAttached()
                        .Where(t => t.Id == model.TrailerId)
                        .FirstOrDefaultAsync();

                    if (oldTrailer != newTrailer)
                    {
                        if (oldTrailer != null)
                        {
                            oldTrailer.IsAvailable = true;
                            await trailerRepository.UpdateAsync(oldTrailer);
                        }

                        // Assign new trailer and set its availability to false
                        if (model.TrailerId.HasValue)
                        {
                            if (newTrailer != null)
                            {
                                newTrailer.IsAvailable = false;
                                driver.TrailerId = newTrailer.Id;
                                await trailerRepository.UpdateAsync(newTrailer);
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
                    var newTrailer = await trailerRepository.GetAllAttached()
                        .Where(t => t.Id == model.TrailerId)
                        .FirstOrDefaultAsync();

                    if (newTrailer != null)
                    {
                        newTrailer.IsAvailable = false;
                        driver.TrailerId = newTrailer.Id;
                        await trailerRepository.UpdateAsync(newTrailer);
                    }
                }
            }

            await driverRepository.UpdateAsync(driver);

            return true;
        }

        public async Task<IEnumerable<Trailer>> GetTrailers()
        {
            return await trailerRepository
                .GetAllAttached()
                .Where(t => t.IsDeleted == false)
                .Where(t => t.IsAvailable == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Truck>> GetTrucks()
        {
            return await truckRepository
                .GetAllAttached()
                .Where(t => t.IsDeleted == false)
                .Where(t => t.IsAvailable == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<DriverManager>> GetDriverManagers()
        {
            return await driverManagerRepository
                .GetAllAttached()
                .Where(t => t.IsDeleted == false)
                .ToListAsync();
        }

        public async Task LoadSelectLists(DriverEditViewModel model)
        {
            model.AvailableTrucks = await GetTrucks();
            model.AvailableTrailers = await GetTrailers();
            model.DriverManagers = await GetDriverManagers();
        }
    }
}
