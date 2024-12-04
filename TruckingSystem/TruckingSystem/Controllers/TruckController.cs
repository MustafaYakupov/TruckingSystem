using Microsoft.AspNetCore.Mvc;
using TruckingSystem.Services.Data;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Driver;
using TruckingSystem.Web.ViewModels.Truck;

namespace TruckingSystem.Web.Controllers
{
    public class TruckController : Controller
    {
        private readonly ITruckService truckService;

        public TruckController(ITruckService truckService)
        {
            this.truckService = truckService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TruckAllViewModel> trucks =
                await this.truckService.GetAllTrucksAsync();

            return View(trucks);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TruckAddInputModel model = new TruckAddInputModel();

            await truckService.LoadPartsList(model);

            return View(model);
        }

		[HttpPost]
		public async Task<IActionResult> Create(TruckAddInputModel truckModel)
		{
			if (ModelState.IsValid == false)
			{
				await truckService.LoadPartsList(truckModel);
				return View(truckModel);
			}

			await truckService.CreateTruckAsync(truckModel);

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            TruckDeleteViewModel model = await truckService.DeleteTruckGetAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(TruckDeleteViewModel model)
        {
            await truckService.DeleteTruckAsync(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
