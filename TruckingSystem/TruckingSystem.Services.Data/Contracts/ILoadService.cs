using TruckingSystem.Web.ViewModels.Load;

namespace TruckingSystem.Services.Data.Contracts
{
    public interface ILoadService
    {
        Task<IEnumerable<LoadAllViewModel>> GetAllLoadsAsync();

        Task LoadBrokerCompanies(LoadAddInputModel model);
    }
}
