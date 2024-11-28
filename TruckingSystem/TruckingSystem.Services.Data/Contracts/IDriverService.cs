using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Web.ViewModels.Driver;

namespace TruckingSystem.Services.Data.Contracts
{
    public interface IDriverService
    {
        Task<IEnumerable<DriverAllViewModel>> GetAllDriversAsync();

        Task<DriverEditViewModel> GetEditDriverByIdAsync(Guid id);

        Task<bool> PostEditDriverByIdAsync(DriverEditViewModel model, Guid id);

        Task<IEnumerable<Truck>> GetTrucks();

        Task<IEnumerable<Trailer>> GetTrailers();

        Task<IEnumerable<DriverManager>> GetDriverManagers();

        Task LoadSelectLists(DriverEditViewModel model);
    }
}
