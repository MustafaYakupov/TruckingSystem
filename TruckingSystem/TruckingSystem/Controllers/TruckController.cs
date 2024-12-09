﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruckingSystem.Services.Data;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Driver;
using TruckingSystem.Web.ViewModels.Truck;

namespace TruckingSystem.Web.Controllers
{
	[Authorize]
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TruckAddInputModel model = new TruckAddInputModel();

            await this.truckService.LoadPartsListAsync(model);

            return View(model);
        }

		[HttpPost]
		public async Task<IActionResult> Create(TruckAddInputModel truckModel)
		{
			if (ModelState.IsValid == false)
			{
				await this.truckService.LoadPartsListAsync(truckModel);
				return View(truckModel);
			}

			await this.truckService.CreateTruckAsync(truckModel);

			return RedirectToAction(nameof(Index));
		}

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            TruckEditInputModel? viewModel = await this.truckService
                .GetEditTruckByIdAsync(id);

            if (viewModel == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

		[HttpPost]
		public async Task<IActionResult> Edit(TruckEditInputModel truckModel, Guid id)
		{
			if (ModelState.IsValid == false)
			{
				await this.truckService.LoadPartsListAsync(truckModel);

				return View(truckModel);
			}

			bool successfullyEdited = await this.truckService.PostEditTruckByIdAsync(truckModel, id);

			if (successfullyEdited == false)
			{
				await this.truckService.LoadPartsListAsync(truckModel);
				return View(truckModel);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            TruckDeleteViewModel model = await this.truckService.DeleteTruckGetAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(TruckDeleteViewModel model)
        {
            await this.truckService.DeleteTruckAsync(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
