﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels;
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
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            PaginatedList<TrailerAllViewModel> trailers =
                await this.trailerService.GetAllTrailersAsync(page, pageSize);

            return View(trailers);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            TrailerAddInputModel model = new();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(TrailerAddInputModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await this.trailerService.CreateTrailerAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            TrailerDeleteViewModel model = await this.trailerService.DeleteTrailerGetAsync(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(TrailerDeleteViewModel model)
        {
            await this.trailerService.DeleteTrailerAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            TrailerEditInputModel? viewModel = await this.trailerService
                .GetEditTrailerByIdAsync(id);

            if (viewModel == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

		[HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(TrailerEditInputModel model, Guid id)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			bool successfullyEdited = await this.trailerService.PostEditTrailerByIdAsync(model, id);

			if (successfullyEdited == false)
			{
				return View(model);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
