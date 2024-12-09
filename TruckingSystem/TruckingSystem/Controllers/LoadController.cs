using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TruckingSystem.Services.Data;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Driver;
using TruckingSystem.Web.ViewModels.Load;
using static TruckingSystem.Common.ValidationMessages.LoadValidationMessages;


namespace TruckingSystem.Web.Controllers
{
	[Authorize]
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            LoadAddInputModel model = new LoadAddInputModel();

            await loadService.LoadBrokerCompanies(model);

            return View(model);
        }

		[HttpPost]
		public async Task<IActionResult> Create(LoadAddInputModel model)
		{
			if (ModelState.IsValid == false)
			{
				await loadService.LoadBrokerCompanies(model);
				return View(model);
			}

            bool result = await loadService.CreateLoadAsync(model);

            if (result == false)
            {
                this.ModelState.AddModelError(nameof(model.PickupTime), LoadDateTimeFormatErrorMessage);
                this.ModelState.AddModelError(nameof(model.DeliveryTime), LoadDateTimeFormatErrorMessage);
				await loadService.LoadBrokerCompanies(model);
				return View(model);
			}

			return RedirectToAction(nameof(Index));
		}

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            LoadEditInputModel? viewModel = await loadService
                .GetEditLoadByIdAsync(id);

            if (viewModel == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

		[HttpPost]
		public async Task<IActionResult> Edit(LoadEditInputModel model, Guid id)
		{
			if (ModelState.IsValid == false)
			{
				await loadService.LoadBrokerCompanies(model);
				return View(model);
			}

			bool successfullyEdited = await loadService.PostEditLoadByIdAsync(model, id);

			if (successfullyEdited == false)
			{
				await loadService.LoadBrokerCompanies(model);
				return View(model);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			LoadDeleteViewModel model = await loadService.DeleteLoadGetAsync(id);

			return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(LoadDeleteViewModel model)
        {
            await loadService.DeleteLoadAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AssignDriverToLoad(Guid id)
        {
            LoadAssignInputModel? viewModel = await loadService
                .GetAssignLoadByIdAsync(id);

            if (viewModel == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

		[HttpPost]
		public async Task<IActionResult> AssignDriverToLoadConfirmed(LoadAssignInputModel model)
		{
            if (ModelState.IsValid == false)
            {
                await loadService.LoadAvailableDrivers(model);
				return RedirectToAction(nameof(Index));
			}

            bool successfullyAssigned = await loadService.PostAssignLoadByIdAsync(model, model.LoadId);

            if (successfullyAssigned == false)
            {
				await loadService.LoadAvailableDrivers(model);
				return RedirectToAction(nameof(Index));
			}

            return RedirectToAction("LoadsInProgress", "Dispatch");
		}
	}
}
