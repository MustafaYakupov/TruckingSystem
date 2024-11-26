using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Web.ViewModels.Driver;

namespace TruckingSystem.Services.Data.Contracts
{
    public interface IDriverService
    {
        Task<IEnumerable<DriverAllViewModel>> GetAllDriversAsync();
    }
}
