using Microsoft.AspNetCore.Mvc;
using TruckingSystem.Services.Data.Contracts;
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
    }
}
