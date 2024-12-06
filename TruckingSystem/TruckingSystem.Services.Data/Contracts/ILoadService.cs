using TruckingSystem.Web.ViewModels.Load;

namespace TruckingSystem.Services.Data.Contracts
{
    public interface ILoadService
    {
        Task<IEnumerable<LoadAllViewModel>> GetAllLoadsAsync();

        Task LoadBrokerCompanies(LoadAddInputModel model);

        Task<bool> CreateLoadAsync(LoadAddInputModel model);

        Task<LoadDeleteViewModel> DeleteLoadGetAsync(Guid id);
	}
}
