using Microsoft.AspNetCore.Mvc;

namespace TruckingSystem.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("404"); 
            }

            if (statusCode == 500)
            {
                return View("500");
            }

            return View("Error");
        }
    }
}
