using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TruckingSystem.Data;
using TruckingSystem.Web.ViewModels;

namespace TruckingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger, TruckingSystemDbContext context)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
