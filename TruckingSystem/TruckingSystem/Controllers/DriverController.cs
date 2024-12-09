using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Driver;

namespace TruckingSystem.Web.Controllers
{
    [Authorize]
    public class DriverController : Controller
    {
        private readonly IDriverService driverService;

        public DriverController(IDriverService driverService)
        {
            this.driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<DriverAllViewModel> drivers =
                await this.driverService.GetAllDriversAsync();

            return View(drivers);
        }

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			DriverAddInputModel model = new DriverAddInputModel();
             
			await this.driverService.LoadSelectLists(model);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(DriverAddInputModel model)
		{
			if (ModelState.IsValid == false)
			{
				await this.driverService.LoadSelectLists(model);
				return View(model);
			}

			await driverService.CreateDriverAsync(model);

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            DriverEditInputModel? viewModel = await this.driverService
                .GetEditDriverByIdAsync(id);

            if (viewModel == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DriverEditInputModel model, Guid id)
        {
            if (ModelState.IsValid == false)
            {
                await this.driverService.LoadSelectLists(model);
                return View(model);
            }

            bool successfullyEdited = await this.driverService.PostEditDriverByIdAsync(model, id);

            if (successfullyEdited == false)
            {
                await this.driverService.LoadSelectLists(model);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			DriverDeleteViewModel model = await this.driverService.DeleteDriverGetAsync(id);

			return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(DriverDeleteViewModel model)
        {
            await this.driverService.DeleteDriverAsync(model);

            return RedirectToAction(nameof(Index));
        }
	}
}
