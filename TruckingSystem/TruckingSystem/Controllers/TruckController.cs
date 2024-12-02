using Microsoft.AspNetCore.Mvc;

namespace TruckingSystem.Web.Controllers
{
    public class TruckController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
