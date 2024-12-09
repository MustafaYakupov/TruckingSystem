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

            await this.loadService.LoadBrokerCompanies(model);

            return View(model);
        }

		[HttpPost]
		public async Task<IActionResult> Create(LoadAddInputModel model)
		{
			if (ModelState.IsValid == false)
			{
				await this.loadService.LoadBrokerCompanies(model);
				return View(model);
			}

            bool result = await this.loadService.CreateLoadAsync(model);

            if (result == false)
            {
                this.ModelState.AddModelError(nameof(model.PickupTime), LoadDateTimeFormatErrorMessage);
                this.ModelState.AddModelError(nameof(model.DeliveryTime), LoadDateTimeFormatErrorMessage);
				await this.loadService.LoadBrokerCompanies(model);
				return View(model);
			}

			return RedirectToAction(nameof(Index));
		}

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            LoadEditInputModel? viewModel = await this.loadService
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
				await this.loadService.LoadBrokerCompanies(model);
				return View(model);
			}

			bool successfullyEdited = await this.loadService.PostEditLoadByIdAsync(model, id);

			if (successfullyEdited == false)
			{
				await this.loadService.LoadBrokerCompanies(model);
				return View(model);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			LoadDeleteViewModel model = await this.loadService.DeleteLoadGetAsync(id);

			return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(LoadDeleteViewModel model)
        {
            await this.loadService.DeleteLoadAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AssignDriverToLoad(Guid id)
        {
            LoadAssignInputModel? viewModel = await this.loadService
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
                await this.loadService.LoadAvailableDrivers(model);
				return RedirectToAction(nameof(Index));
			}

            bool successfullyAssigned = await this.loadService.PostAssignLoadByIdAsync(model, model.LoadId);

            if (successfullyAssigned == false)
            {
				await this.loadService.LoadAvailableDrivers(model);
				return RedirectToAction(nameof(Index));
			}

            return RedirectToAction("LoadsInProgress", "Dispatch");
		}
	}
}
