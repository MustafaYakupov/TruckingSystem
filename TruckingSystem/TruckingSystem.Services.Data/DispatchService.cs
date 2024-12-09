﻿using Microsoft.EntityFrameworkCore;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Dispatch;
using static TruckingSystem.Common.ValidationConstants.LoadConstants;

namespace TruckingSystem.Services.Data
{
	public class DispatchService : IDispatchService
	{
		private IRepository<Dispatch> dispatchRepository;
		private IRepository<Load> loadRepository;

		public DispatchService(IRepository<Dispatch> dispatchRepository, IRepository<Load> loadRepository)
		{
			this.dispatchRepository = dispatchRepository;
			this.loadRepository = loadRepository;
		}

		public async Task<IEnumerable<DispatchInProgressViewModel>> GetAllDispatchesInProgressAsync()
		{
			IEnumerable<Load> loadsInProgress = await this.loadRepository
				.GetAllAttached()
				.Where(l => l.IsDeleted == false)
				.Where(l => l.Driver != null)
				.Where(l => l.IsAvailable == false)
				.Include(l => l.BrokerCompany)
				.Include(l => l.Driver)
					.ThenInclude(d => d.Truck)
				.Include(l => l.Driver)
					.ThenInclude(d => d.Trailer)
				.Include(l => l.Driver)
					.ThenInclude(d => d.DriverManager)
				.ToListAsync();

			IEnumerable<DispatchInProgressViewModel> viewModel = loadsInProgress
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
				});

			return viewModel;

		}

	}
}
