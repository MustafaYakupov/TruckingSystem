using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Web.ViewModels.Driver;

namespace TruckingSystem.Services.Data.Contracts
{
    public interface IDriverService
    {
        Task<IEnumerable<DriverAllViewModel>> GetAllDriversAsync();

        Task<DriverEditInputModel> GetEditDriverByIdAsync(Guid id);

        Task<bool> PostEditDriverByIdAsync(DriverEditInputModel model, Guid id);

        Task<IEnumerable<Truck>> GetTrucks();

        Task<IEnumerable<Trailer>> GetTrailers();

        Task<IEnumerable<DriverManager>> GetDriverManagers();

        Task LoadSelectLists(DriverEditInputModel model);

        Task LoadSelectLists(DriverAddInputModel model);
	}
}
