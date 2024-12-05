using TruckingSystem.Data.Models;
using TruckingSystem.Web.ViewModels.Truck;

namespace TruckingSystem.Services.Data.Contracts
{
	public interface ITruckService
	{
        Task<IEnumerable<TruckAllViewModel>> GetAllTrucksAsync();

        Task<TruckDeleteViewModel> DeleteTruckGetAsync(Guid id);

        Task DeleteTruckAsync(TruckDeleteViewModel model);

        Task LoadPartsListAsync(TruckAddInputModel model);

        Task LoadPartsListAsync(TruckEditInputModel model);

		Task CreateTruckAsync(TruckAddInputModel model);

        Task<TruckEditInputModel> GetEditTruckByIdAsync(Guid id);

        Task<bool> PostEditTruckByIdAsync(TruckEditInputModel model, Guid id);

    }
}
