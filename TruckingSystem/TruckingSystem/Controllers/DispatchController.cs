using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruckingSystem.Services.Data;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Dispatch;
using TruckingSystem.Web.ViewModels.Driver;

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
        public async Task<IActionResult> LoadsInProgress(string searchString)
		{
			IEnumerable<DispatchInProgressViewModel> dispatchesInProgress =
				await this.dispatchService.GetAllDispatchesInProgressAsync(searchString);

			return View(dispatchesInProgress);
		}

        [HttpGet]
        public async Task<IActionResult> CompletedLoads(string searchString)
        {
            IEnumerable<DispatchCompletedViewModel> dispatchesCompleted =
                await this.dispatchService.GetAllDispatchesCompletedAsync(searchString);

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
