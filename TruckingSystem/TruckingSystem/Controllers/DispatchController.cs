using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruckingSystem.Services.Data.Contracts;
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
        public async Task<IActionResult> LoadsInProgress()
		{
			IEnumerable<DispatchInProgressViewModel> dispatchesInProgress =
				await this.dispatchService.GetAllDispatchesInProgressAsync();

			return View(dispatchesInProgress);
		}
	}
}
