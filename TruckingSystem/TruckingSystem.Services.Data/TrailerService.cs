using Microsoft.EntityFrameworkCore;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels;
using TruckingSystem.Web.ViewModels.Driver;
using TruckingSystem.Web.ViewModels.Trailer;

namespace TruckingSystem.Services.Data
{
    public class TrailerService : ITrailerService
    {
        private readonly IRepository<Trailer> trailerRepository;

        public TrailerService(IRepository<Trailer> trailerRepository)
        {
            this.trailerRepository = trailerRepository;
        }

        public async Task<PaginatedList<TrailerAllViewModel>> GetAllTrailersAsync(int page, int pageSize)
        {
            int totalTrailers = await this.trailerRepository
                .GetAllAttached()
                .Where(t => t.IsDeleted == false)
                .CountAsync();

            IEnumerable<Trailer> trailers = await this.trailerRepository
                .GetAllAttached()
                .Where(t => t.IsDeleted == false)
                .OrderBy(t => t.TrailerNumber)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<TrailerAllViewModel> trailerViewModel = trailers
                .Select(t => new TrailerAllViewModel
                {
                    Id = t.Id,
                    TrailerNumber = t.TrailerNumber,
                    Make = t.Make,
                    Type = t.Type,
                    ModelYear = t.ModelYear,
                }).ToList();

            PaginatedList<TrailerAllViewModel> paginatedList = new PaginatedList<TrailerAllViewModel>(trailerViewModel, totalTrailers, page, pageSize);

            return paginatedList;
        }

        public async Task CreateTrailerAsync(TrailerAddInputModel model)
        {
            Trailer trailer = new()
            {
                TrailerNumber = model.TrailerNumber,
                Make = model.Make,
                Type = model.Type,
                ModelYear = model.ModelYear,
            };

            await this.trailerRepository.AddAsync(trailer);
        }

        public async Task<TrailerDeleteViewModel> DeleteTrailerGetAsync(Guid id)
        {
            TrailerDeleteViewModel? deleteModel = await this.trailerRepository
                .GetAllAttached()
                .Where(t => t.Id == id)
                .Where(t => t.IsDeleted == false)
                .AsNoTracking()
                .Select(t => new TrailerDeleteViewModel()
                {
                    Id = t.Id,
                    TrailerNumber = t.TrailerNumber
                })
                .FirstOrDefaultAsync();

            return deleteModel;
        }

        public async Task DeleteTrailerAsync(TrailerDeleteViewModel model)
        {
            Trailer? trailer = await this.trailerRepository
                .GetAllAttached()
                .Where(t => t.Id == model.Id)
                .Where(t => t.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (trailer != null)
            {
                trailer.IsDeleted = true;
                await this.trailerRepository.UpdateAsync(trailer);
            }
        }

        public async Task<TrailerEditInputModel> GetEditTrailerByIdAsync(Guid id)
        {
            TrailerEditInputModel? viewModel = await this.trailerRepository
                .GetAllAttached()
                .Where(t => t.Id == id)
                .Where(t => t.IsDeleted == false)
                .AsNoTracking()
                .Select(t =>  new TrailerEditInputModel()
                {
                    TrailerNumber = t.TrailerNumber,
                    Make = t.Make,
                    Type = t.Type,
                    ModelYear = t.ModelYear,
                })
                .FirstOrDefaultAsync();

            if (viewModel == null)
            {
                return null;
            }

            return viewModel;
        }

		public async Task<bool> PostEditTrailerByIdAsync(TrailerEditInputModel model, Guid id)
		{
			Trailer? trailer = await this.trailerRepository
				.GetAllAttached()
				.Where(t => t.Id == id)
				.Where(t => t.IsDeleted == false)
				.FirstOrDefaultAsync();

            if (trailer == null || trailer.IsDeleted)
            {
                return false;
            }

            trailer.TrailerNumber = model.TrailerNumber;
            trailer.Make = model.Make;
            trailer.Type = model.Type;
            trailer.ModelYear = model.ModelYear;

            await this.trailerRepository.UpdateAsync(trailer);

            return true;
		}
	}
}
