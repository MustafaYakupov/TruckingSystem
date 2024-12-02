using Microsoft.EntityFrameworkCore;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Trailer;

namespace TruckingSystem.Services.Data
{
    public class TrailerService : ITrailerService
    {
        private IRepository<Trailer> trailerRepository;

        public TrailerService(IRepository<Trailer> trailerRepository)
        {
            this.trailerRepository = trailerRepository;
        }

        public async Task<IEnumerable<TrailerAllViewModel>> GetAllTrailersAsync()
        {
            IEnumerable<Trailer> trailers = await this.trailerRepository
                .GetAllAttached()
                .Where(t => t.IsDeleted == false)
                .ToListAsync();

            IEnumerable<TrailerAllViewModel> trailerViewModel = trailers
                .Select(t => new TrailerAllViewModel
                {
                    Id = t.Id,
                    TrailerNumber = t.TrailerNumber,
                    Make = t.Make,
                    Type = t.Type,
                    ModelYear = t.ModelYear,
                });

            return trailerViewModel;
        }

        public async Task CreateTrailerAsync(TrailerAddInputModel model)
        {
            Trailer trailer = new Trailer()
            {
                TrailerNumber = model.TrailerNumber,
                Make = model.Make,
                Type = model.Type,
                ModelYear = model.ModelYear,
            };

            await trailerRepository.AddAsync(trailer);
        }
    }
}
