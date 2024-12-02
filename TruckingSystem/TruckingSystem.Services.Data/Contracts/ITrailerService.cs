using TruckingSystem.Web.ViewModels.Trailer;

namespace TruckingSystem.Services.Data.Contracts
{
    public interface ITrailerService
    {
        Task<IEnumerable<TrailerAllViewModel>> GetAllTrailersAsync();

        Task CreateTrailerAsync(TrailerAddInputModel model);
    }
}
