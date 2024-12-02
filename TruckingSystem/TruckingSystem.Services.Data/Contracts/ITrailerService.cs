using TruckingSystem.Web.ViewModels.Driver;
using TruckingSystem.Web.ViewModels.Trailer;

namespace TruckingSystem.Services.Data.Contracts
{
    public interface ITrailerService
    {
        Task<IEnumerable<TrailerAllViewModel>> GetAllTrailersAsync();

        Task CreateTrailerAsync(TrailerAddInputModel model);

        Task<TrailerDeleteViewModel> DeleteTrailerGetAsync(Guid id);

        Task DeleteTrailerAsync(TrailerDeleteViewModel model);
    }
}
