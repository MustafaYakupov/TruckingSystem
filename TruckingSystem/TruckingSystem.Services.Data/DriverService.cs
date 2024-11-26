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

        public DriverService(IRepository<Driver> driverRepository)
        {
                this.driverRepository = driverRepository;
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
                    TruckNumber = d.Truck.TruckNumber.ToString() ?? string.Empty,
                    TrailerNumber = d.Trailer.TrailerNumber.ToString() ?? string.Empty,
                    DriverManager = d.DriverManager.FirstName + " " + d.DriverManager.LastName ?? string.Empty
                })
                .FirstOrDefaultAsync();

            if (viewModel == null)
            {
                return null;
            }
            
            return viewModel;
        }

        //public async Task<bool> PostEditDriverByIdAsync(DriverEditViewModel model, Guid id)
        //{
        //    Driver? driver = await driverRepository
        //        .GetByIdAsync(id);

        //    if (driver == null || driver.IsDeleted)
        //    {
        //        return false;
        //    }

        //    driver.FirstName = model.FirstName;
        //    driver.LastName = model.LastName;
        //    driver.LicenseNumber = model.LicenseNumber;
        //    driver.Truck.TruckNumber = model.TruckNumber;


        //    return true;
        //}
    }
}
