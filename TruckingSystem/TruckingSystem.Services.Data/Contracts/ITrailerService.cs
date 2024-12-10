using TruckingSystem.Web.ViewModels;
using TruckingSystem.Web.ViewModels.Driver;
using TruckingSystem.Web.ViewModels.Trailer;

namespace TruckingSystem.Services.Data.Contracts
{
    public interface ITrailerService
    {
        Task<PaginatedList<TrailerAllViewModel>> GetAllTrailersAsync(int page, int pageSize);

        Task CreateTrailerAsync(TrailerAddInputModel model);

        Task<TrailerDeleteViewModel> DeleteTrailerGetAsync(Guid id);

        Task DeleteTrailerAsync(TrailerDeleteViewModel model);

        Task<TrailerEditInputModel> GetEditTrailerByIdAsync(Guid id);

        Task<bool> PostEditTrailerByIdAsync(TrailerEditInputModel model, Guid id);

	}
}
