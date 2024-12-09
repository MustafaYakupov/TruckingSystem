using TruckingSystem.Web.ViewModels.Dispatch;

namespace TruckingSystem.Services.Data.Contracts
{
	public interface IDispatchService
	{
		Task<IEnumerable<DispatchInProgressViewModel>> GetAllDispatchesInProgressAsync();

        Task<IEnumerable<DispatchCompletedViewModel>> GetAllDispatchesCompletedAsync();

        Task<bool> CompleteDispatchByIdAsync(Guid id);
    }
}
