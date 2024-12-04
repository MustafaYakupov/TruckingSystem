using TruckingSystem.Data.Models;
using TruckingSystem.Web.ViewModels.Truck;

namespace TruckingSystem.Services.Data.Contracts
{
	public interface ITruckService
	{
        Task<IEnumerable<TruckAllViewModel>> GetAllTrucksAsync();

        Task<TruckDeleteViewModel> DeleteTruckGetAsync(Guid id);

        Task DeleteTruckAsync(TruckDeleteViewModel model);

        Task LoadPartsList(TruckAddInputModel model);

        Task CreateTruckAsync(TruckAddInputModel model);

	}
}
