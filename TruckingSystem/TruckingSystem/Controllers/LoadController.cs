using Microsoft.AspNetCore.Mvc;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Load;

namespace TruckingSystem.Web.Controllers
{
    public class LoadController : Controller
    {
        private readonly ILoadService loadService;

        public LoadController(ILoadService loadService)
        {
                this.loadService = loadService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<LoadAllViewModel> loads =
                await this.loadService.GetAllLoadsAsync();

            return View(loads);
        }
    }
}
