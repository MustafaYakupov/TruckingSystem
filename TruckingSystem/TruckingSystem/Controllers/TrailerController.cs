using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruckingSystem.Services.Data.Contracts;
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
    }
}
