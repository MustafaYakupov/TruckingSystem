using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data.Contracts;
using TruckingSystem.Web.ViewModels.Truck;

namespace TruckingSystem.Services.Data
{
	public class TruckService : ITruckService
	{
		private IRepository<Truck> truckRepository;
		private IRepository<Part> partRepository;

		public TruckService(IRepository<Truck> truckRepository, IRepository<Part> partRepository)
		{
			this.truckRepository = truckRepository;
			this.partRepository = partRepository;
		}

		public async Task<IEnumerable<TruckAllViewModel>> GetAllTrucksAsync()
		{
			IEnumerable<Truck> trucks = await this.truckRepository
				.GetAllAttached()
				.Include(t => t.TrucksParts)
				.ThenInclude(tp => tp.Part.TruckParts)
				.Where(t => t.IsDeleted == false)
				.ToListAsync();


			IEnumerable<TruckAllViewModel> truckViewModel = trucks
				.Select(t => new TruckAllViewModel()
				{
					Id = t.Id,
					TruckNumber = t.TruckNumber,
					Make = t.Make,
					Model = t.Model,
					LicensePlate = t.LicensePlate,
					ModelYear = t.ModelYear,
					Color = t.Color,
					Parts = t.TrucksParts.Select(tp => tp.Part)
				});

			return truckViewModel;
		}

		public async Task CreateTruckAsync(TruckAddInputModel model)
		{
			Truck truck = new Truck()
			{
				TruckNumber = model.TruckNumber,
				Make = model.Make,
				Model = model.Model,
				LicensePlate = model.LicensePlate,
				ModelYear = model.ModelYear,
				Color = model.Color,
			};

			if (model.Parts.Any(p => p.IsSelected == true))
			{
				foreach (var part in model.Parts)
				{
					TruckPart truckPart = new TruckPart()
					{
						TruckId = truck.Id,
						PartId = part.PartId,
					};

					truck.TrucksParts.Add(truckPart);
				}
			}

			await truckRepository.AddAsync(truck);
		}

		public async Task<TruckEditInputModel> GetEditTruckByIdAsync(Guid id)
		{
			TruckEditInputModel? viewModel = await truckRepository
				.GetAllAttached()
				.Where(t => t.Id == id)
				.Where(t => t.IsDeleted == false)
				.Include(t => t.TrucksParts)
				.ThenInclude(tp => tp.Part.TruckParts)
				.AsNoTracking()
				.Select(t => new TruckEditInputModel()
				{
					TruckNumber = t.TruckNumber,
					Make = t.Make,
					Model = t.Model,
					LicensePlate = t.LicensePlate,
					ModelYear = t.ModelYear,
					Color = t.Color
				})
				.FirstOrDefaultAsync();

			if (viewModel == null)
			{
				return null;
			}

			await LoadPartsListAsync(viewModel);

			return viewModel;
		}

		public async Task<bool> PostEditTruckByIdAsync(TruckEditInputModel model, Guid id)
		{
			Truck? truck = await truckRepository
				.GetAllAttached()
				.Where(t => t.Id == id)
				.Where(t => t.IsDeleted == false)
				.Include(t => t.TrucksParts)
				.ThenInclude(tp => tp.Part.TruckParts)
				.FirstOrDefaultAsync();

			if (truck == null || truck.IsDeleted)
			{
				return false;
			}

			truck.TruckNumber = model.TruckNumber;
			truck.Make = model.Make;
			truck.Model = model.Model;
			truck.LicensePlate = model.LicensePlate;
			truck.ModelYear = model.ModelYear;
			truck.Color = model.Color;

			if (model.Parts.Any(p => p.IsSelected == true))
			{
				foreach (var part in model.Parts.Where(p => p.IsSelected == true))
				{
					TruckPart truckPart = new TruckPart()
					{
						TruckId = truck.Id,
						PartId = part.PartId,
					};

					if (truck.TrucksParts.Any(tp => tp.PartId == truckPart.PartId) == false)
					{
						truck.TrucksParts.Add(truckPart);
					}
				}
			}
			else
			{
				truck.TrucksParts = new HashSet<TruckPart>();
			}

			await truckRepository.UpdateAsync(truck);
			return true;
		}

		public async Task<TruckDeleteViewModel> DeleteTruckGetAsync(Guid id)
		{
			TruckDeleteViewModel? deleteModel = await truckRepository
				.GetAllAttached()
				.Where(t => t.Id == id)
				.Where(t => t.IsDeleted == false)
				.AsNoTracking()
				.Select(t => new TruckDeleteViewModel()
				{
					Id = t.Id,
					TruckNumber = t.TruckNumber
				})
				.FirstOrDefaultAsync();

			return deleteModel;
		}

		public async Task DeleteTruckAsync(TruckDeleteViewModel model)
		{
			Truck? truck = await truckRepository
				.GetAllAttached()
				.Where(t => t.Id == model.Id)
				.Where(t => t.IsDeleted == false)
				.FirstOrDefaultAsync();

			if (truck != null)
			{
				truck.IsDeleted = true;
				await truckRepository.UpdateAsync(truck);
			}
		}

		public async Task LoadPartsListAsync(TruckAddInputModel model)
		{
			model.Parts = await GetPartsAsync();
		}

		public async Task LoadPartsListAsync(TruckEditInputModel model)
		{
			model.Parts = await GetPartsAsync();
		}

		private async Task<IList<PartSelectionViewModel>> GetPartsAsync()
		{
			return await partRepository
				.GetAllAttached()
				.AsNoTracking()
				.Select(p => new PartSelectionViewModel()
				{
					PartId = p.Id,
					PartType = p.Type,
					PartMake = p.Make,
					IsSelected = false,
				})
				.ToListAsync();
		}
	}
}
