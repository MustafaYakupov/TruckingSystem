using TruckingSystem.Web.ViewModels.Dispatch;

namespace TruckingSystem.Services.Data.Contracts
{
	public interface IDispatchService
	{
		Task<IEnumerable<DispatchInProgressViewModel>> GetAllDispatchesInProgressAsync();
	}
}
