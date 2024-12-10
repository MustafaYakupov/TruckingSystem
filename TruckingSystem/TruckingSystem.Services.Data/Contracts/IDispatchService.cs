using TruckingSystem.Web.ViewModels;
using TruckingSystem.Web.ViewModels.Dispatch;

namespace TruckingSystem.Services.Data.Contracts
{
	public interface IDispatchService
	{
        Task<PaginatedList<DispatchInProgressViewModel>> GetAllDispatchesInProgressAsync(string searchString, int page, int pageSize);

        Task<PaginatedList<DispatchCompletedViewModel>> GetAllDispatchesCompletedAsync(string searchString, int page, int pageSize);

        Task<bool> CompleteDispatchByIdAsync(Guid id);
    }
}
