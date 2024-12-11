using Microsoft.EntityFrameworkCore;
using System.Linq;
using TruckingSystem.Data.Models;
using TruckingSystem.Data.Models.Enums;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels;
using TruckingSystem.Web.ViewModels.Dispatch;
using TruckingSystem.Web.ViewModels.Driver;
using static TruckingSystem.Common.ValidationConstants.LoadConstants;

namespace TruckingSystem.Services.Data
{
	public class DispatchService : IDispatchService
	{
		private readonly IRepository<Dispatch> dispatchRepository;
		private readonly IRepository<Load> loadRepository;

		public DispatchService(IRepository<Dispatch> dispatchRepository, IRepository<Load> loadRepository)
		{
			this.dispatchRepository = dispatchRepository;
			this.loadRepository = loadRepository;
		}

		public async Task<PaginatedList<DispatchInProgressViewModel>> GetAllDispatchesInProgressAsync(string searchString, int page, int pageSize)
		{
			int dispatchesInProgressCoount = await	this.dispatchRepository
				.GetAllAttached()
                .Where(l => l.IsDeleted == false)
                .Where(l => l.Driver != null)
                .Where(l => l.Status == DispatchStatus.InProgress)
                .CountAsync();

            IEnumerable<Load> loadsInProgress = await this.loadRepository
				.GetAllAttached()
				.Where(l => l.IsDeleted == false)
				.Where(l => l.Driver != null)
				.Where(l => l.Status == DispatchStatus.InProgress)
				.Include(l => l.BrokerCompany)
				.Include(l => l.Driver)
					.ThenInclude(d => d.Truck)
				.Include(l => l.Driver)
					.ThenInclude(d => d.Trailer)
				.Include(l => l.Driver)
					.ThenInclude(d => d.DriverManager)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                loadsInProgress = loadsInProgress
                    .Where(l => l.Driver.LastName.Contains(searchString)
                                       || l.Driver.FirstName.Contains(searchString));
            }

            List<DispatchInProgressViewModel> viewModel = loadsInProgress
				.Select(l => new DispatchInProgressViewModel()
				{
					Id = l.Id,
					Driver = l.Driver.FirstName + " " + l.Driver.LastName,
					Truck = l.Driver.Truck.TruckNumber,
					Trailer = l.Driver.Trailer.TrailerNumber,
					DriverManager = l.Driver.DriverManager.FirstName + " " + l.Driver.DriverManager.LastName,
					PickupAddress = l.PickupLocation,
					DeliveryAddress = l.DeliveryLocation,
					BrokerCompany = l.BrokerCompany.CompanyName,
					PickupTime = l.PickupTime.ToString(DateTimeFormat),
					DeliveryTime = l.DeliveryTime.ToString(DateTimeFormat),
					Distance = l.Distance,
					Weight = l.Weight,
					Temperature = l.Temperature?.ToString() ?? String.Empty
				}).ToList();

            PaginatedList<DispatchInProgressViewModel> paginatedList = new PaginatedList<DispatchInProgressViewModel>(viewModel, dispatchesInProgressCoount, page, pageSize);

            return paginatedList;
		}

        public async Task<PaginatedList<DispatchCompletedViewModel>> GetAllDispatchesCompletedAsync(string searchString, int page, int pageSize)
        {
			int dispatchesCompletedCount =
				await this.dispatchRepository
				.GetAllAttached()
				.Where(d => d.IsDeleted == false)
				.Where(d => d.Status == DispatchStatus.Completed)
				.CountAsync();

            IEnumerable<Dispatch> dispatchesCompleted = await this.dispatchRepository
                .GetAllAttached()
                .Where(d => d.IsDeleted == false)
                .Where(d => d.Status == DispatchStatus.Completed)
                .Include(d => d.Driver)
                .Include(d => d.Truck)
                .Include(d => d.Trailer)
                .Include(d => d.DriverManager)
                .Include(d => d.Load)
					.ThenInclude(l => l.BrokerCompany)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                dispatchesCompleted = dispatchesCompleted
					.Where(d => d.Driver.LastName.Contains(searchString)
                                       || d.Driver.FirstName.Contains(searchString));
            }

            List<DispatchCompletedViewModel> viewModel = dispatchesCompleted
                .Select(d => new DispatchCompletedViewModel()
                {
                    Id = d.Id,
                    Driver = d.Driver.FirstName + " " + d.Driver.LastName,
                    Truck = d.Driver.Truck.TruckNumber,
                    Trailer = d.Driver.Trailer.TrailerNumber,
                    DriverManager = d.Driver.DriverManager.FirstName + " " + d.Driver.DriverManager.LastName,
                    PickupAddress = d.Load.PickupLocation,
                    DeliveryAddress = d.Load.DeliveryLocation,
                    BrokerCompany = d.Load.BrokerCompany.CompanyName,
                    PickupTime = d.Load.PickupTime.ToString(DateTimeFormat),
                    DeliveryTime = d.Load.DeliveryTime.ToString(DateTimeFormat),
                    Distance = d.Load.Distance,
                    Weight = d.Load.Weight,
                    Temperature = d.Load.Temperature?.ToString() ?? String.Empty
                }).ToList();

            PaginatedList<DispatchCompletedViewModel> paginatedList = new PaginatedList<DispatchCompletedViewModel>(viewModel, dispatchesCompletedCount, page, pageSize);

            return paginatedList;

        }

        public async Task<bool> CompleteDispatchByIdAsync(Guid loadId)
		{
			Dispatch? dispatch = await this.dispatchRepository
				.GetAllAttached()
				.Where(d => d.LoadId == loadId)
				.Where(d => d.IsDeleted == false)
				.FirstOrDefaultAsync();

			if (dispatch == null || dispatch.IsDeleted)
			{
				return false;
			}

			Load? load = await this.loadRepository
				.GetAllAttached()
				.Where(l => l.Id == loadId)
				.FirstOrDefaultAsync();

			load.Status = DispatchStatus.Completed;
            dispatch.Status = DispatchStatus.Completed;

			await this.dispatchRepository.UpdateAsync(dispatch);

			return true;
		}
    }
}
