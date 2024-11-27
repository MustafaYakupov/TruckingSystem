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
                .Where(d => d.IsDeleted == false)
                .AsNoTracking()
                .ToListAsync();

            IEnumerable<DriverAllViewModel> driverViewModel = drivers
                .Select(d => new DriverAllViewModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    LicenseNumber = d.LicenseNumber,
                    TruckNumber = d.Truck?.TruckNumber.ToString() ?? string.Empty,
                    TrailerNumber = d.Trailer?.TrailerNumber.ToString() ?? string.Empty,
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

            viewModel.AvailableTrucks = await GetTrucks();
            viewModel.AvailableTrailers = await GetTrailers();
            viewModel.DriverManagers = await GetDriverManagers();


            return viewModel;
        }

        //public async Task<bool> PostEditDriverByIdAsync(DriverEditViewModel model, Guid id)
        //{
        //    //Driver? driver = await driverRepository
        //    //    .GetByIdAsync(id);

        //    //if (driver == null || driver.IsDeleted)
        //    //{
        //    //    return false;
        //    //}

        //    //DriverManager? driverManager = await driverManagerRepository
        //    //    .GetAllAttached()
        //    //    .Where(m => m.FirstName == model.DriverManager)
        //    //    .Where(m => m.IsDeleted == false)
        //    //    .AsNoTracking()
        //    //    .FirstOrDefaultAsync();

        //    //Truck? truck = await truckRepository
        //    //    .GetAllAttached()
        //    //    .Where(t => t.TruckNumber == model.TruckNumber)
        //    //    .Where(t => t.IsDeleted == false)
        //    //    .AsNoTracking()
        //    //    .FirstOrDefaultAsync();

        //    //Trailer? trailer = await trailerRepository
        //    //    .GetAllAttached()
        //    //    .Where(t => t.TrailerNumber == model.TrailerNumber)
        //    //    .Where(t => t.IsDeleted == false)
        //    //    .AsNoTracking()
        //    //    .FirstOrDefaultAsync();

        //    //driver.FirstName = model.FirstName;
        //    //driver.LastName = model.LastName;
        //    //driver.LicenseNumber = model.LicenseNumber;
        //    //driver.Truck = truck ?? null;
        //    //driver.Trailer = trailer ?? null;
        //    //driver.DriverManager = driverManager ?? null;

        //    //await driverRepository.UpdateAsync(driver);

        //    return true;
        //}

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
    }
}
