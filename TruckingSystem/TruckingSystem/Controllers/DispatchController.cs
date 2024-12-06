using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruckingSystem.Services.Data.Contracts;

namespace TruckingSystem.Web.Controllers
{
	[Authorize]
	public class DispatchController : Controller
	{
		private readonly IDispatchService dispatchService;

        public DispatchController(IDispatchService dispatchService)
        {
            this.dispatchService = dispatchService;
        }

        public IActionResult Index()
		{
			return View();
		}
	}
}
