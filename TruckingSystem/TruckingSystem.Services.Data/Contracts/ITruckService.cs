using TruckingSystem.Web.ViewModels.Truck;

namespace TruckingSystem.Services.Data.Contracts
{
	public interface ITruckService
	{
        Task<IEnumerable<TruckAllViewModel>> GetAllTrucksAsync();
    }
}
