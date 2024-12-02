using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruckingSystem.Services.Data;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Driver;
using TruckingSystem.Web.ViewModels.Trailer;

namespace TruckingSystem.Web.Controllers
{
    [Authorize]
    public class TrailerController : Controller
    {
        private readonly ITrailerService trailerService;

        public TrailerController(ITrailerService trailerService)
        {
            this.trailerService = trailerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TrailerAllViewModel> trailers =
                await this.trailerService.GetAllTrailersAsync();

            return View(trailers);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TrailerAddInputModel model = new TrailerAddInputModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrailerAddInputModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await trailerService.CreateTrailerAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            TrailerDeleteViewModel model = await trailerService.DeleteTrailerGetAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(TrailerDeleteViewModel model)
        {
            await trailerService.DeleteTrailerAsync(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
