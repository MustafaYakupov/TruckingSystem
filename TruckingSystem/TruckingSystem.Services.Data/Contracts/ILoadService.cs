﻿using TruckingSystem.Web.ViewModels.Load;

namespace TruckingSystem.Services.Data.Contracts
{
    public interface ILoadService
    {
        Task<IEnumerable<LoadAllViewModel>> GetAllLoadsAsync();

        Task LoadBrokerCompanies(LoadAddInputModel model);

		Task LoadBrokerCompanies(LoadEditInputModel model);

		Task<bool> CreateLoadAsync(LoadAddInputModel model);

        Task<LoadDeleteViewModel> DeleteLoadGetAsync(Guid id);

        Task DeleteLoadAsync(LoadDeleteViewModel model);

        Task<LoadEditInputModel> GetEditLoadByIdAsync(Guid id);

        Task<bool> PostEditLoadByIdAsync(LoadEditInputModel model, Guid id);

        Task<LoadAssignInputModel> GetAssignLoadByIdAsync(Guid id);
    }
}
