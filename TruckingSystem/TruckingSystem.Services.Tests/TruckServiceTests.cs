using EntityFrameworkCore.Testing.Moq;
using Moq;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data;
using MockQueryable;
using TruckingSystem.Web.ViewModels.Truck;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IO;
using MockQueryable.Moq;

namespace TruckingSystem.Services.Tests
{
    [TestFixture]
    public class TruckServiceTests
    {
        private Mock<IRepository<Truck>> mockTruckRepository;
        private Mock<IRepository<Part>> mockPartRepository;
        private TruckService truckService;

        [SetUp]
        public void SetUp()
        {
            this.mockTruckRepository = new Mock<IRepository<Truck>>();
            this.mockPartRepository = new Mock<IRepository<Part>>();
            this.truckService = new TruckService(this.mockTruckRepository.Object, this.mockPartRepository.Object);
        }

        [Test]
        public async Task GetAllTrucksAsync_ReturnsPaginatedList()
        {
            // Arrange
            int page = 1;
            int pageSize = 2;

            var trucks = new List<Truck>
        {
            new Truck
            {
                Id = Guid.NewGuid(),
                TruckNumber = "1234",
                Make = "Make1",
                Model = "Model1",
                LicensePlate = "ABC123",
                ModelYear = "2020",
                Color = "Red",
                IsDeleted = false,
                TrucksParts = new List<TruckPart>
                {
                    new TruckPart { Part = new Part { Id = Guid.NewGuid(), Type = "Engine" } }
                }
            },
            new Truck
            {
                Id = Guid.NewGuid(),
                TruckNumber = "5678",
                Make = "Make2",
                Model = "Model2",
                LicensePlate = "DEF456",
                ModelYear = "2021",
                Color = "Blue",
                IsDeleted = false,
                TrucksParts = new List<TruckPart>
                {
                    new TruckPart { Part = new Part { Id = Guid.NewGuid(), Type = "Tire" } }
                }
            }
        };

            var mockQueryable = trucks.AsQueryable().BuildMock();

            this.mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(mockQueryable);

            // Act
            var result = await this.truckService.GetAllTrucksAsync(page, pageSize);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(2));
            Assert.That(result.TotalCount, Is.EqualTo(2));
            Assert.That(result.Items.First().TruckNumber, Is.EqualTo("1234"));
        }

        [Test]
        public async Task GetAllTrucksAsync_WhenNoTrucksExist_ReturnsEmptyPaginatedList()
        {
            // Arrange
            var trucks = new List<Truck>();
            var mockQueryable = trucks.AsQueryable().BuildMock();

            this.mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(mockQueryable);

            int page = 1;
            int pageSize = 2;

            // Act
            var result = await this.truckService.GetAllTrucksAsync(page, pageSize);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(0));
            Assert.That(result.TotalCount, Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllTrucksAsync_WhenPageExceedsData_ReturnsEmptyList()
        {
            // Arrange
            var trucks = new List<Truck>
        {
            new Truck
            {
                Id = Guid.NewGuid(),
                TruckNumber = "1234",
                Make = "Make1",
                Model = "Model1",
                LicensePlate = "ABC123",
                ModelYear = "2020",
                Color = "Red",
                IsDeleted = false,
                TrucksParts = new List<TruckPart>
                {
                    new TruckPart { Part = new Part { Id = Guid.NewGuid(), Type = "Engine" } }
                }
            }
        };

            var mockQueryable = trucks.AsQueryable().BuildMock();

            this.mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(mockQueryable);

            int page = 10;
            int pageSize = 2;

            // Act
            var result = await this.truckService.GetAllTrucksAsync(page, pageSize);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(0));
            Assert.That(result.TotalCount, Is.EqualTo(1)); // Total trucks count remains 1
        }

        [Test]
        public async Task CreateTruckAsync_AddsTruckWithoutParts()
        {
            // Arrange
            var model = new TruckAddInputModel
            {
                TruckNumber = "1234",
                Make = "Make1",
                Model = "Model1",
                LicensePlate = "ABC123",
                ModelYear = "2020",
                Color = "Red",
                Parts = new List<PartSelectionViewModel>()
            };

            Truck? addedTruck = null;
            this.mockTruckRepository.Setup(repo => repo.AddAsync(It.IsAny<Truck>()))
                .Callback<Truck>(truck => addedTruck = truck)
                .Returns(Task.CompletedTask);

            // Act
            await this.truckService.CreateTruckAsync(model);

            // Assert
            Assert.That(addedTruck, Is.Not.Null);
            Assert.That(addedTruck.TruckNumber, Is.EqualTo(model.TruckNumber));
            Assert.That(addedTruck.Make, Is.EqualTo(model.Make));
            Assert.That(addedTruck.Model, Is.EqualTo(model.Model));
            Assert.That(addedTruck.LicensePlate, Is.EqualTo(model.LicensePlate));
            Assert.That(addedTruck.ModelYear, Is.EqualTo(model.ModelYear));
            Assert.That(addedTruck.Color, Is.EqualTo(model.Color));
            Assert.That(addedTruck.TrucksParts, Is.Empty);

            this.mockTruckRepository.Verify(repo => repo.AddAsync(It.IsAny<Truck>()), Times.Once);
        }

        [Test]
        public void CreateTruckAsync_CreatesTruck_WhenValidModelIsPassed()
        {
            // Arrange
            var model = new TruckAddInputModel
            {
                TruckNumber = "1234",
                Make = "Ford",
                Model = "F-150",
                LicensePlate = "XYZ123",
                ModelYear = "2020",
                Color = "Red",
                Parts = new List<PartSelectionViewModel>
            {
                new PartSelectionViewModel { PartId = Guid.NewGuid(), PartMake = "Cummins", PartType = "Engine", IsSelected = true},
                new PartSelectionViewModel { PartId = Guid.NewGuid(), PartMake = "Transmission", PartType = "Gearing", IsSelected = true},
            }
            };

            var truck = new Truck
            {
                TruckNumber = model.TruckNumber,
                Make = model.Make,
                Model = model.Model,
                LicensePlate = model.LicensePlate,
                ModelYear = model.ModelYear,
                Color = model.Color
            };

            foreach (var part in model.Parts)
            {
                TruckPart truckPart = new()
                {
                    TruckId = truck.Id,
                    PartId = part.PartId,
                };

                truck.TrucksParts.Add(truckPart);
            }

            // Mock the repository to handle AddAsync method call
            mockTruckRepository.Setup(r => r.AddAsync(It.IsAny<Truck>()))
                .Callback<Truck>(t => truck = t);

            // Act
            this.truckService.CreateTruckAsync(model);

            // Assert
            Assert.That(truck, Is.Not.Null);  // Ensure the truck is created
            Assert.That(truck.TruckNumber, Is.EqualTo(model.TruckNumber));  // Verify truck number
            Assert.That(truck.Make, Is.EqualTo(model.Make));  // Verify truck make
            Assert.That(truck.Model, Is.EqualTo(model.Model));  // Verify truck model
            Assert.That(truck.LicensePlate, Is.EqualTo(model.LicensePlate));  // Verify license plate
            Assert.That(truck.ModelYear, Is.EqualTo(model.ModelYear));  // Verify model year
            Assert.That(truck.Color, Is.EqualTo(model.Color));  // Verify truck color
            Assert.That(truck.TrucksParts.Count, Is.EqualTo(2));  // Ensure two truck parts are added
        }

        [Test]
        public void CreateTruckAsync_AddsNoParts_WhenNoPartsSelected()
        {
            // Arrange
            var model = new TruckAddInputModel
            {
                TruckNumber = "1234",
                Make = "Ford",
                Model = "F-150",
                LicensePlate = "XYZ123",
                ModelYear = "2020",
                Color = "Red",
                Parts = new List<PartSelectionViewModel>()  // No parts selected
            };

            var truck = new Truck
            {
                TruckNumber = model.TruckNumber,
                Make = model.Make,
                Model = model.Model,
                LicensePlate = model.LicensePlate,
                ModelYear = model.ModelYear,
                Color = model.Color,
                TrucksParts = new HashSet<TruckPart>()
            };

            // Mock the repository to handle AddAsync method call
            mockTruckRepository.Setup(r => r.AddAsync(It.IsAny<Truck>()))
                .Callback<Truck>(t => truck = t);  // Capture the truck passed to AddAsync

            // Act
            this.truckService.CreateTruckAsync(model);  // Call the method (note that it's not async here)

            // Assert
            Assert.That(truck, Is.Not.Null);  // Ensure the truck is created
            Assert.That(truck.TrucksParts.Count, Is.EqualTo(0));  // Ensure no parts are added
        }

        [Test]
        public void CreateTruckAsync_CallsAddAsyncOnce()
        {
            // Arrange
            var model = new TruckAddInputModel
            {
                TruckNumber = "1234",
                Make = "Ford",
                Model = "F-150",
                LicensePlate = "XYZ123",
                ModelYear = "2020",
                Color = "Red",
                Parts = new List<PartSelectionViewModel>
            {
                new PartSelectionViewModel { PartId = Guid.NewGuid(), PartMake = "Cummins", PartType = "Engine"}
            }
            };

            // Act
            this.truckService.CreateTruckAsync(model);

            // Assert
            mockTruckRepository.Verify(r => r.AddAsync(It.IsAny<Truck>()), Times.Once);
        }

        [Test]
        public void GetPartsAsync_ReturnsPartsList_WhenPartsExist()
        {
            // Arrange
            var parts = new List<Part>
        {
            new Part { Id = Guid.NewGuid(), Type = "Engine", Make = "Ford" },
            new Part { Id = Guid.NewGuid(), Type = "Wheel", Make = "Goodyear" }
        };

            // Mock the repository to return a list of parts
            mockPartRepository.Setup(r => r.GetAllAttached())
                .Returns(parts.AsQueryable());

            // Act
            var result = mockPartRepository.Object.GetAllAttached().ToList();

            // Assert
            Assert.That(result, Is.Not.Null);  // Check if the result is not null
            Assert.That(result.Count, Is.EqualTo(2));  // Ensure there are two parts in the list
            Assert.That(result[0].Id, Is.EqualTo(parts[0].Id));  // Assert the first part's Id
            Assert.That(result[0].Type, Is.EqualTo(parts[0].Type));  // Assert the first part's Type
            Assert.That(result[0].Make, Is.EqualTo(parts[0].Make));  // Assert the first part's Make
        }

        [Test]
        public void GetPartsAsync_ReturnsEmptyList_WhenNoPartsExist()
        {
            // Arrange
            var parts = new List<Part>();  // No parts in the repository

            // Mock the repository to return an empty list of parts
            mockPartRepository.Setup(r => r.GetAllAttached())
                .Returns(parts.AsQueryable());

            // Act
            var result = mockPartRepository.Object.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);  // Check if the result is not null
        }

        [Test]
        public void GetPartsAsync_CallsRepositoryOnce()
        {
            // Arrange
            var parts = new List<Part>
        {
            new Part { Id = Guid.NewGuid(), Type = "Engine", Make = "Ford" }
        };

            // Mock the repository to return a list of parts
            mockPartRepository.Setup(r => r.GetAllAttached())
                .Returns(parts.AsQueryable());

            // Act
            var result = mockPartRepository.Object.GetAllAttached().ToList();

            // Assert
            mockPartRepository.Verify(r => r.GetAllAttached(), Times.Once);  // Ensure that GetAllAttached was called exactly once
        }

        [Test]
        public void GetPartsAsync_MapsCorrectlyToViewModel()
        {
            // Arrange
            var parts = new List<Part>
        {
            new Part { Id = Guid.NewGuid(), Type = "Engine", Make = "Ford" },
            new Part { Id = Guid.NewGuid(), Type = "Wheel", Make = "Goodyear" }
        };

            // Mock the repository to return a list of parts
            mockPartRepository.Setup(r => r.GetAllAttached())
                .Returns(parts.AsQueryable());

            // Act
            var result = mockPartRepository.Object.GetAllAttached().ToList();

            // Assert
            Assert.That(result[0].Type, Is.EqualTo(parts[0].Type));  // Ensure the partId matches
            Assert.That(result[0].Make, Is.EqualTo(parts[0].Make));  // Ensure the partType matches
        }

        [Test]
        public void GetEditTruckByIdAsync_ReturnsTruckViewModel_WhenTruckExists()
        {
            // Arrange
            var truckId = Guid.NewGuid();  // Existing truck ID
            var truck = new Truck
            {
                Id = truckId,
                TruckNumber = "1234",
                Make = "Ford",
                Model = "F-150",
                LicensePlate = "XYZ123",
                ModelYear = "2020",
                Color = "Red",
                IsDeleted = false
            };

            var model = new TruckEditInputModel()
            {
                TruckNumber = truck.TruckNumber,
                Make = truck.Make,
                Model = truck.Model,
                LicensePlate = truck.LicensePlate,
                ModelYear = truck.ModelYear,
                Color = truck.Color
            };

            // Act
            var result = this.truckService.GetEditTruckByIdAsync(truckId);

            // Assert
            Assert.That(result, Is.Not.Null);  // Ensure the result is not null
        }


        private TruckService GetServiceWithMockedDependencies()
        {
            var mockPartRepository = new Mock<IPartRepository>();
            var service = new TruckService(mockTruckRepository.Object, mockPartRepository.Object);
            return service;
        }

        private void SetupMockLoadPartsListAsync()
        {
            var mockService = new Mock<TruckService>(mockTruckRepository.Object, null);
            mockService.Setup(s => s.LoadPartsListAsync(It.IsAny<TruckEditInputModel>()))
                .Returns(Task.CompletedTask); // Simulate successful completion of the method
        }

        [Test]
        public void PostEditTruckByIdAsync_ReturnsFalse_WhenTruckNotFound()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Truck>().AsQueryable().BuildMock());

            var model = new TruckEditInputModel
            {
                TruckNumber = "ABC123",
                Make = "TestMake",
                Model = "TestModel",
                LicensePlate = "XYZ456",
                ModelYear = "2022",
                Color = "Blue",
                Parts = new List<PartSelectionViewModel>()
            };

            // Act
            var result = truckService.PostEditTruckByIdAsync(model, truckId).Result;

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void PostEditTruckByIdAsync_ReturnsFalse_WhenTruckIsDeleted()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var deletedTruck = new Truck
            {
                Id = truckId,
                IsDeleted = true,
                TruckNumber = "ABC123",
                Make = "TestMake",
                Model = "TestModel",
                LicensePlate = "XYZ456",
                ModelYear = "2022",
                Color = "Blue",
                TrucksParts = new HashSet<TruckPart>()
            };

            mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Truck> { deletedTruck }.AsQueryable().BuildMock());

            var model = new TruckEditInputModel
            {
                TruckNumber = "ABC123",
                Make = "TestMake",
                Model = "TestModel",
                LicensePlate = "XYZ456",
                ModelYear = "2022",
                Color = "Blue",
                Parts = new List<PartSelectionViewModel>()
            };

            // Act
            var result = truckService.PostEditTruckByIdAsync(model, truckId).Result;

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void PostEditTruckByIdAsync_ReturnsTrue_WhenTruckIsUpdatedSuccessfully()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var truck = new Truck
            {
                Id = truckId,
                IsDeleted = false,
                TruckNumber = "ABC123",
                Make = "TestMake",
                Model = "TestModel",
                LicensePlate = "XYZ456",
                ModelYear = "2022",
                Color = "Blue",
                TrucksParts = new HashSet<TruckPart>()
            };

            mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Truck> { truck }.AsQueryable().BuildMock());

            var model = new TruckEditInputModel
            {
                TruckNumber = "DEF456",
                Make = "UpdatedMake",
                Model = "UpdatedModel",
                LicensePlate = "XYZ789",
                ModelYear = "2023",
                Color = "Red",
                Parts = new List<PartSelectionViewModel>()
            };

            // Act
            var result = truckService.PostEditTruckByIdAsync(model, truckId).Result;

            // Assert
            Assert.That(result, Is.True);
            mockTruckRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Truck>()), Times.Once);
        }

        [Test]
        public void PostEditTruckByIdAsync_ClearsParts_WhenNoPartsSelected()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var truck = new Truck
            {
                Id = truckId,
                IsDeleted = false,
                TruckNumber = "ABC123",
                Make = "TestMake",
                Model = "TestModel",
                LicensePlate = "XYZ456",
                ModelYear = "2022",
                Color = "Blue",
                TrucksParts = new HashSet<TruckPart> { new TruckPart { TruckId = truckId, PartId = Guid.NewGuid() } }
            };

            mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Truck> { truck }.AsQueryable().BuildMock());

            var model = new TruckEditInputModel
            {
                TruckNumber = "ABC123",
                Make = "TestMake",
                Model = "TestModel",
                LicensePlate = "XYZ456",
                ModelYear = "2022",
                Color = "Blue",
                Parts = new List<PartSelectionViewModel>() // No parts selected
            };

            // Act
            var result = truckService.PostEditTruckByIdAsync(model, truckId).Result;

            // Assert
            Assert.That(result, Is.True);
            Assert.That(truck.TrucksParts, Is.Empty);
        }

        [Test]
        public void PostEditTruckByIdAsync_AddsParts_WhenPartsSelected()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var truck = new Truck
            {
                Id = truckId,
                IsDeleted = false,
                TruckNumber = "ABC123",
                Make = "TestMake",
                Model = "TestModel",
                LicensePlate = "XYZ456",
                ModelYear = "2022",
                Color = "Blue",
                TrucksParts = new HashSet<TruckPart>()
            };

            mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Truck> { truck }.AsQueryable().BuildMock());

            var model = new TruckEditInputModel
            {
                TruckNumber = "ABC123",
                Make = "TestMake",
                Model = "TestModel",
                LicensePlate = "XYZ456",
                ModelYear = "2022",
                Color = "Blue",
                Parts = new List<PartSelectionViewModel>
            {
                new PartSelectionViewModel { PartId = Guid.NewGuid(), PartMake = "Cummins", PartType = "Engine", IsSelected = true }
            }
            };

            // Act
            var result = truckService.PostEditTruckByIdAsync(model, truckId).Result;

            // Assert
            Assert.That(result, Is.True);
            Assert.That(truck.TrucksParts.Count, Is.EqualTo(1));
            mockTruckRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Truck>()), Times.Once);
        }

        [Test]
        public void DeleteTruckGetAsync_ReturnsNull_WhenTruckNotFound()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Truck>().AsQueryable().BuildMock());

            // Act
            var result = truckService.DeleteTruckGetAsync(truckId).Result;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void DeleteTruckGetAsync_ReturnsTruckDeleteViewModel_WhenTruckFound()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var truck = new Truck
            {
                Id = truckId,
                TruckNumber = "ABC123",
                IsDeleted = false
            };

            mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Truck> { truck }.AsQueryable().BuildMock());

            // Act
            var result = truckService.DeleteTruckGetAsync(truckId).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(truckId));
            Assert.That(result.TruckNumber, Is.EqualTo("ABC123"));
        }

        [Test]
        public void DeleteTruckGetAsync_ReturnsNull_WhenTruckIsDeleted()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var truck = new Truck
            {
                Id = truckId,
                TruckNumber = "ABC123",
                IsDeleted = true
            };

            mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Truck> { truck }.AsQueryable().BuildMock());

            // Act
            var result = truckService.DeleteTruckGetAsync(truckId).Result;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void DeleteTruckAsync_DoesNotUpdate_WhenTruckNotFound()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var model = new TruckDeleteViewModel { Id = truckId, TruckNumber = "113" };

            mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Truck>().AsQueryable().BuildMock()); // Empty list of trucks

            // Act
            truckService.DeleteTruckAsync(model).Wait();  

            // Assert
            mockTruckRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Truck>()), Times.Never);
        }

        [Test]
        public void DeleteTruckAsync_UpdatesTruck_WhenTruckFound()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var truck = new Truck { Id = truckId, TruckNumber = "123", IsDeleted = false };
            var model = new TruckDeleteViewModel { Id = truckId, TruckNumber = "123" };

            mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Truck> { truck }.AsQueryable().BuildMock());  // Single truck in the list

            // Act
            truckService.DeleteTruckAsync(model).Wait();  // Using .Wait() to run async method synchronously

            // Assert
            mockTruckRepository.Verify(repo => repo.UpdateAsync(It.Is<Truck>(t => t.IsDeleted == true)), Times.Once);
        }

        [Test]
        public void DeleteTruckAsync_DoesNotUpdate_WhenTruckAlreadyDeleted()
        {
            // Arrange
            var truckId = Guid.NewGuid();
            var truck = new Truck { Id = truckId, TruckNumber = "123", IsDeleted = true };
            var model = new TruckDeleteViewModel { Id = truckId, TruckNumber = "123" };

            mockTruckRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Truck> { truck }.AsQueryable().BuildMock());  // Truck is already deleted

            // Act
            truckService.DeleteTruckAsync(model).Wait();  // Using .Wait() to run async method synchronously

            // Assert
            mockTruckRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Truck>()), Times.Never);
        }
    }
}