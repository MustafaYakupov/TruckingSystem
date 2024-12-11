using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels;
using TruckingSystem.Web.ViewModels.Dispatch;

namespace TruckingSystem.Web.Controllers
{
	[Authorize]
	public class DispatchController : Controller
	{
		private readonly IDispatchService dispatchService;

        public DispatchController(IDispatchService dispatchService)
        {
            this.dispatchService = dispatchService;
        }

		[HttpGet]
        public async Task<IActionResult> LoadsInProgress(string searchString, int page = 1, int pageSize = 5)
		{
            PaginatedList<DispatchInProgressViewModel> dispatchesInProgress =
				await this.dispatchService.GetAllDispatchesInProgressAsync(searchString, page, pageSize);

			return View(dispatchesInProgress);
		}

        [HttpGet]
        public async Task<IActionResult> CompletedLoads(string searchString, int page = 1, int pageSize = 5)
        {
            PaginatedList<DispatchCompletedViewModel> dispatchesCompleted =
                await this.dispatchService.GetAllDispatchesCompletedAsync(searchString, page, pageSize);

            return View(dispatchesCompleted);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteLoad(Guid id)
        {
            bool result = await this.dispatchService.CompleteDispatchByIdAsync(id);

            if (result == false)
            {
                return RedirectToAction(nameof(LoadsInProgress));
            }

            return RedirectToAction(nameof(CompletedLoads));
        }
    }
}
