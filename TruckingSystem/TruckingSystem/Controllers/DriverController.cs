using Microsoft.AspNetCore.Mvc;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Driver;

namespace TruckingSystem.Web.Controllers
{
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
                throw new ArgumentException("Non-existent driver!");
            } 

            return View(viewModel);
        }
    }
}
