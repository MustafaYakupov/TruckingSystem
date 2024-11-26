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
        public async Task<IActionResult> Edit(Guid id)
        {
            DriverEditViewModel? viewModel = await driverService
                .GetEditDriverByIdAsync(id);

            if (viewModel == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DriverEditViewModel model, Guid id)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }



            return RedirectToAction(nameof(Index));
        }
    }
}
