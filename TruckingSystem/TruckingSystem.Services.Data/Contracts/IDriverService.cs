﻿using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Web.ViewModels.Driver;

namespace TruckingSystem.Services.Data.Contracts
{
    public interface IDriverService
    {
        Task<IEnumerable<DriverAllViewModel>> GetAllDriversAsync();

        Task<DriverEditInputModel> GetEditDriverByIdAsync(Guid id);

        Task<bool> PostEditDriverByIdAsync(DriverEditInputModel model, Guid id);

        Task LoadSelectLists(DriverEditInputModel model);

        Task LoadSelectLists(DriverAddInputModel model);

        Task CreateDriverAsync(DriverAddInputModel model);

        Task<DriverDeleteViewModel> DeleteDriverGetAsync(Guid id);

        Task DeleteDriverAsync(DriverDeleteViewModel model);

    }
}
