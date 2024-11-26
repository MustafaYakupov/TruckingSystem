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
                .GetAllAsync();

            IEnumerable<DriverAllViewModel> driverViewModel = drivers
                .Select(d => new DriverAllViewModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    LicenseNumber = d.LicenseNumber,
                    TruckNumber = d.Truck?.TruckNumber.ToString() ?? string.Empty,
                    TrailerNumber = d.Truck?.Trailer?.TrailerNumber.ToString() ?? string.Empty,
                    DriverManager = d.DriverManager?.FirstName + " " + d.DriverManager?.LastName ?? string.Empty,
                });

            return driverViewModel;
        }
    }
}
