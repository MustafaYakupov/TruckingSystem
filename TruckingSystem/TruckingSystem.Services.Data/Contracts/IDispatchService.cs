using TruckingSystem.Web.ViewModels.Dispatch;

namespace TruckingSystem.Services.Data.Contracts
{
	public interface IDispatchService
	{
		Task<IEnumerable<DispatchInProgressViewModel>> GetAllDispatchesInProgressAsync(string searchString);

        Task<IEnumerable<DispatchCompletedViewModel>> GetAllDispatchesCompletedAsync(string searchString);

        Task<bool> CompleteDispatchByIdAsync(Guid id);
    }
}
