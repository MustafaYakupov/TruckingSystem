
using Microsoft.EntityFrameworkCore;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using System.Globalization;
using System.Runtime.Serialization;
using TruckingSystem.Data.Models;
using TruckingSystem.Data.Models.Enums;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data;
using TruckingSystem.Web.ViewModels.Load;


namespace TruckingSystem.Services.Tests
{
    [TestFixture]
    public class LoadServiceTests
    {
        private Mock<IRepository<Load>> mockLoadRepository;
        private Mock<IRepository<BrokerCompany>> mockBrokerCompanyRepository;
        private Mock<IRepository<Driver>> mockDriverRepository;
        private Mock<IRepository<Dispatch>> mockDispatchRepository;
        private LoadService loadService;

        private const string DateTimeFormat = "yyyy-MM-dd"; 


        [SetUp]
        public void Setup()
        {
            mockLoadRepository = new Mock<IRepository<Load>>();
            mockBrokerCompanyRepository = new Mock<IRepository<BrokerCompany>>();
            mockDriverRepository = new Mock<IRepository<Driver>>();
            mockDispatchRepository = new Mock<IRepository<Dispatch>>();

            loadService = new LoadService(
                mockLoadRepository.Object,
                mockBrokerCompanyRepository.Object,
                mockDriverRepository.Object,
                mockDispatchRepository.Object
            );
        }

        [Test]
        public void GetAllLoadsAsync_ShouldReturnPaginatedList_WhenLoadsExist()
        {
            // Arrange
            var loads = new List<Load>
            {
                new Load
                {
                    Id = Guid.NewGuid(),
                    PickupLocation = "Location A",
                    DeliveryLocation = "Location B",
                    Weight = 1000,
                    Temperature = 25,
                    PickupTime = DateTime.Now,
                    DeliveryTime = DateTime.Now.AddHours(2),
                    Distance = 150,
                    BrokerCompany = new BrokerCompany { CompanyName = "Company A" },
                    Status = DispatchStatus.Available,
                    IsDeleted = false
                },
                new Load
                {
                    Id = Guid.NewGuid(),
                    PickupLocation = "Location C",
                    DeliveryLocation = "Location D",
                    Weight = 2000,
                    Temperature = 30,
                    PickupTime = DateTime.Now.AddDays(1),
                    DeliveryTime = DateTime.Now.AddDays(1).AddHours(2),
                    Distance = 250,
                    BrokerCompany = new BrokerCompany { CompanyName = "Company B" },
                    Status = DispatchStatus.Available,
                    IsDeleted = false
                }
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(loads.AsQueryable().BuildMockDbSet().Object);


            // Act
            var result = loadService.GetAllLoadsAsync(1, 10).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.TotalCount, Is.EqualTo(2));  // Total number of available loads
            Assert.That(result.Items.Count(), Is.EqualTo(2)); // Should return 2 items in the first page
        }

        [Test]
        public void GetAllLoadsAsync_ShouldReturnEmpty_WhenNoAvailableLoads()
        {
            // Arrange
            var loads = new List<Load>();  // Empty list, no loads

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(loads.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = loadService.GetAllLoadsAsync(1, 10).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.TotalCount, Is.EqualTo(0)); // No available loads
            Assert.That(result.Items.Count(), Is.EqualTo(0)); // No items
        }

        [Test]
        public void GetAllLoadsAsync_ShouldReturnCorrectPaginatedData_WhenLoadsExist()
        {
            // Arrange
            var loads = new List<Load>
            {
                new Load
                {
                    Id = Guid.NewGuid(),
                    PickupLocation = "Location A",
                    DeliveryLocation = "Location B",
                    Weight = 1000,
                    Temperature = 25,
                    PickupTime = DateTime.Now,
                    DeliveryTime = DateTime.Now.AddHours(2),
                    Distance = 150,
                    BrokerCompany = new BrokerCompany { CompanyName = "Company A" },
                    Status = DispatchStatus.Available,
                    IsDeleted = false
                },
                new Load
                {
                    Id = Guid.NewGuid(),
                    PickupLocation = "Location C",
                    DeliveryLocation = "Location D",
                    Weight = 2000,
                    Temperature = 30,
                    PickupTime = DateTime.Now.AddDays(1),
                    DeliveryTime = DateTime.Now.AddDays(1).AddHours(2),
                    Distance = 250,
                    BrokerCompany = new BrokerCompany { CompanyName = "Company B" },
                    Status = DispatchStatus.Available,
                    IsDeleted = false
                }
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(loads.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = loadService.GetAllLoadsAsync(1, 1).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.TotalCount, Is.EqualTo(2)); // Total number of available loads
            Assert.That(result.Items.Count(), Is.EqualTo(1)); // Pagination, should return only 1 item on the first page
        }

        [Test]
        public void GetAllLoadsAsync_ShouldReturnLoadsFilteredByStatus()
        {
            // Arrange
            var loads = new List<Load>
            {
                new Load
                {
                    Id = Guid.NewGuid(),
                    PickupLocation = "Location A",
                    DeliveryLocation = "Location B",
                    Weight = 1000,
                    Temperature = 25,
                    PickupTime = DateTime.Now,
                    DeliveryTime = DateTime.Now.AddHours(2),
                    Distance = 150,
                    BrokerCompany = new BrokerCompany { CompanyName = "Company A" },
                    Status = DispatchStatus.Available,
                    IsDeleted = false
                },
                new Load
                {
                    Id = Guid.NewGuid(),
                    PickupLocation = "Location C",
                    DeliveryLocation = "Location D",
                    Weight = 2000,
                    Temperature = 30,
                    PickupTime = DateTime.Now.AddDays(1),
                    DeliveryTime = DateTime.Now.AddDays(1).AddHours(2),
                    Distance = 250,
                    BrokerCompany = new BrokerCompany { CompanyName = "Company B" },
                    Status = DispatchStatus.InProgress,  // This load is not Available
                    IsDeleted = false
                }
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(loads.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = loadService.GetAllLoadsAsync(1, 10).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.TotalCount, Is.EqualTo(1)); // Only 1 available load
            Assert.That(result.Items.Count(), Is.EqualTo(1)); // Should return only 1 available load
        }

        [Test]
        public void GetAllLoadsAsync_ShouldApplyPaginationCorrectly()
        {
            // Arrange
            var loads = new List<Load>
            {
                new Load
                {
                    Id = Guid.NewGuid(),
                    PickupLocation = "Location A",
                    DeliveryLocation = "Location B",
                    Weight = 1000,
                    Temperature = 25,
                    PickupTime = DateTime.Now,
                    DeliveryTime = DateTime.Now.AddHours(2),
                    Distance = 150,
                    BrokerCompany = new BrokerCompany { CompanyName = "Company A" },
                    Status = DispatchStatus.Available,
                    IsDeleted = false
                },
                new Load
                {
                    Id = Guid.NewGuid(),
                    PickupLocation = "Location C",
                    DeliveryLocation = "Location D",
                    Weight = 2000,
                    Temperature = 30,
                    PickupTime = DateTime.Now.AddDays(1),
                    DeliveryTime = DateTime.Now.AddDays(1).AddHours(2),
                    Distance = 250,
                    BrokerCompany = new BrokerCompany { CompanyName = "Company B" },
                    Status = DispatchStatus.Available,
                    IsDeleted = false
                }
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(loads.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = loadService.GetAllLoadsAsync(2, 1).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.TotalCount, Is.EqualTo(2)); // Total number of available loads
            Assert.That(result.Items.Count(), Is.EqualTo(1)); // Should return only 1 item on the second page
            Assert.That(result.Items.First().PickupLocation, Is.EqualTo("Location C")); // The second load should be on the second page
        }

        [Test]
        public void CreateLoadAsync_ShouldReturnTrue_WhenTimesAreValid()
        {
            // Arrange
            var model = new LoadAddInputModel
            {
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = "2024-12-11",  // Valid datetime
                DeliveryTime = "2024-12-11", // Valid datetime
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid()
            };

            mockLoadRepository.Setup(repo => repo.AddAsync(It.IsAny<Load>())).Returns(Task.CompletedTask);

            // Act
            var result = loadService.CreateLoadAsync(model).Result;

            // Assert
            Assert.That(result, Is.True);
            mockLoadRepository.Verify(repo => repo.AddAsync(It.Is<Load>(l =>
                l.PickupLocation == model.PickupLocation &&
                l.DeliveryLocation == model.DeliveryLocation &&
                l.Weight == model.Weight &&
                l.Temperature == model.Temperature &&
                l.PickupTime == DateTime.ParseExact(model.PickupTime, DateTimeFormat, CultureInfo.InvariantCulture) &&
                l.DeliveryTime == DateTime.ParseExact(model.DeliveryTime, DateTimeFormat, CultureInfo.InvariantCulture) &&
                l.Distance == model.Distance &&
                l.BrokerCompanyId == model.BrokerCompanyId
            )), Times.Once);
        }

        [Test]
        public void CreateLoadAsync_ShouldReturnFalse_WhenPickupTimeIsInvalid()
        {
            // Arrange
            var model = new LoadAddInputModel
            {
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = "InvalidTime",  // Invalid datetime
                DeliveryTime = "2024-12-11", // Valid datetime
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid()
            };

            // Act
            var result = loadService.CreateLoadAsync(model).Result;

            // Assert
            Assert.That(result, Is.False);
            mockLoadRepository.Verify(repo => repo.AddAsync(It.IsAny<Load>()), Times.Never);
        }

        [Test]
        public void CreateLoadAsync_ShouldReturnFalse_WhenDeliveryTimeIsInvalid()
        {
            // Arrange
            var model = new LoadAddInputModel
            {
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = "2024-12-11",  // Valid datetime
                DeliveryTime = "InvalidTime", // Invalid datetime
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid()
            };

            // Act
            var result = loadService.CreateLoadAsync(model).Result;

            // Assert
            Assert.That(result, Is.False);
            mockLoadRepository.Verify(repo => repo.AddAsync(It.IsAny<Load>()), Times.Never);
        }

        [Test]
        public void CreateLoadAsync_ShouldReturnFalse_WhenBothTimesAreInvalid()
        {
            // Arrange
            var model = new LoadAddInputModel
            {
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = "InvalidTime",  // Invalid datetime
                DeliveryTime = "InvalidTime", // Invalid datetime
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid()
            };

            // Act
            var result = loadService.CreateLoadAsync(model).Result;

            // Assert
            Assert.That(result, Is.False);
            mockLoadRepository.Verify(repo => repo.AddAsync(It.IsAny<Load>()), Times.Never);
        }

        [Test]
        public void CreateLoadAsync_ShouldHandleNullTemperature()
        {
            // Arrange
            var model = new LoadAddInputModel
            {
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = null,  // Null temperature
                PickupTime = "2024-12-11",  // Valid datetime
                DeliveryTime = "2024-12-11", // Valid datetime
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid()
            };

            mockLoadRepository.Setup(repo => repo.AddAsync(It.IsAny<Load>())).Returns(Task.CompletedTask);

            // Act
            var result = loadService.CreateLoadAsync(model).Result;

            // Assert
            Assert.That(result, Is.True);
            mockLoadRepository.Verify(repo => repo.AddAsync(It.Is<Load>(l =>
                l.PickupLocation == model.PickupLocation &&
                l.DeliveryLocation == model.DeliveryLocation &&
                l.Weight == model.Weight &&
                l.Temperature == null &&  // Temperature should be null
                l.PickupTime == DateTime.ParseExact(model.PickupTime, DateTimeFormat, CultureInfo.InvariantCulture) &&
                l.DeliveryTime == DateTime.ParseExact(model.DeliveryTime, DateTimeFormat, CultureInfo.InvariantCulture) &&
                l.Distance == model.Distance &&
                l.BrokerCompanyId == model.BrokerCompanyId
            )), Times.Once);
        }

        [Test]
        public void GetEditLoadByIdAsync_ShouldReturnLoadEditInputModel_WhenLoadExists()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(4),
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid(),
                IsDeleted = false
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            var expectedModel = new LoadEditInputModel
            {
                PickupLocation = load.PickupLocation,
                DeliveryLocation = load.DeliveryLocation,
                Weight = load.Weight,
                Temperature = load.Temperature ?? null,
                PickupTime = load.PickupTime.ToString("yyyy-MM-dd"),
                DeliveryTime = load.DeliveryTime.ToString("yyyy-MM-dd"),
                Distance = load.Distance,
                BrokerCompanyId = load.BrokerCompanyId
            };

            // Act
            var result = loadService.GetEditLoadByIdAsync(loadId).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PickupLocation, Is.EqualTo(expectedModel.PickupLocation));
            Assert.That(result.DeliveryLocation, Is.EqualTo(expectedModel.DeliveryLocation));
            Assert.That(result.Weight, Is.EqualTo(expectedModel.Weight));
            Assert.That(result.Temperature, Is.EqualTo(expectedModel.Temperature));
            Assert.That(result.PickupTime, Is.EqualTo(expectedModel.PickupTime));
            Assert.That(result.DeliveryTime, Is.EqualTo(expectedModel.DeliveryTime));
            Assert.That(result.Distance, Is.EqualTo(expectedModel.Distance));
            Assert.That(result.BrokerCompanyId, Is.EqualTo(expectedModel.BrokerCompanyId));
        }

        [Test]
        public void GetEditLoadByIdAsync_ShouldReturnNull_WhenLoadNotFound()
        {
            // Arrange
            var loadId = Guid.NewGuid();

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Load>().AsQueryable().BuildMock());

            // Act
            var result = loadService.GetEditLoadByIdAsync(loadId).Result;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetEditLoadByIdAsync_ShouldReturnNull_WhenLoadIsDeleted()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(4),
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid(),
                IsDeleted = true  // Marked as deleted
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            // Act
            var result = loadService.GetEditLoadByIdAsync(loadId).Result;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetEditLoadByIdAsync_ShouldCallRepositoryOnce()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(4),
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid(),
                IsDeleted = false
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            // Act
            var result = loadService.GetEditLoadByIdAsync(loadId).Result;

            // Assert
            mockLoadRepository.Verify(repo => repo.GetAllAttached(), Times.Once);
        }

        [Test]
        public void PostEditLoadByIdAsync_ShouldReturnTrue_WhenLoadIsSuccessfullyUpdated()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(4),
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid(),
                IsDeleted = false
            };

            var model = new LoadEditInputModel
            {
                PickupLocation = "New Location A",
                DeliveryLocation = "New Location B",
                Weight = 1500,
                Temperature = 30,
                PickupTime = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd"),
                DeliveryTime = DateTime.Now.AddHours(5).ToString("yyyy-MM-dd"),
                Distance = 250,
                BrokerCompanyId = load.BrokerCompanyId
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            // Act
            var result = loadService.PostEditLoadByIdAsync(model, loadId).Result;

            // Assert
            Assert.That(result, Is.True);
            mockLoadRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Load>()), Times.Once);
        }

        [Test]
        public void PostEditLoadByIdAsync_ShouldReturnFalse_WhenLoadIsNotFound()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var model = new LoadEditInputModel
            {
                PickupLocation = "New Location A",
                DeliveryLocation = "New Location B",
                Weight = 1500,
                Temperature = 30,
                PickupTime = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd"),
                DeliveryTime = DateTime.Now.AddHours(5).ToString("yyyy-MM-dd"),
                Distance = 250,
                BrokerCompanyId = Guid.NewGuid()
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Load>().AsQueryable().BuildMock());

            // Act
            var result = loadService.PostEditLoadByIdAsync(model, loadId).Result;

            // Assert
            Assert.That(result, Is.False);
            mockLoadRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Load>()), Times.Never);
        }

        [Test]
        public void PostEditLoadByIdAsync_ShouldReturnFalse_WhenLoadIsDeleted()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(4),
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid(),
                IsDeleted = true
            };

            var model = new LoadEditInputModel
            {
                PickupLocation = "New Location A",
                DeliveryLocation = "New Location B",
                Weight = 1500,
                Temperature = 30,
                PickupTime = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd"),
                DeliveryTime = DateTime.Now.AddHours(5).ToString("yyyy-MM-dd"),
                Distance = 250,
                BrokerCompanyId = load.BrokerCompanyId
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            // Act
            var result = loadService.PostEditLoadByIdAsync(model, loadId).Result;

            // Assert
            Assert.That(result, Is.False);
            mockLoadRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Load>()), Times.Never);
        }

        [Test]
        public void PostEditLoadByIdAsync_ShouldReturnFalse_WhenPickupTimeIsInvalid()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(4),
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid(),
                IsDeleted = false
            };

            var model = new LoadEditInputModel
            {
                PickupLocation = "New Location A",
                DeliveryLocation = "New Location B",
                Weight = 1500,
                Temperature = 30,
                PickupTime = "Invalid Time",  // Invalid PickupTime
                DeliveryTime = DateTime.Now.AddHours(5).ToString("yyyy-MM-dd"),
                Distance = 250,
                BrokerCompanyId = load.BrokerCompanyId
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            // Act
            var result = loadService.PostEditLoadByIdAsync(model, loadId).Result;

            // Assert
            Assert.That(result, Is.False);
            mockLoadRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Load>()), Times.Never);
        }

        [Test]
        public void PostEditLoadByIdAsync_ShouldReturnFalse_WhenDeliveryTimeIsInvalid()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(4),
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid(),
                IsDeleted = false
            };

            var model = new LoadEditInputModel
            {
                PickupLocation = "New Location A",
                DeliveryLocation = "New Location B",
                Weight = 1500,
                Temperature = 30,
                PickupTime = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd"),
                DeliveryTime = "Invalid Time",  // Invalid DeliveryTime
                Distance = 250,
                BrokerCompanyId = load.BrokerCompanyId
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            // Act
            var result = loadService.PostEditLoadByIdAsync(model, loadId).Result;

            // Assert
            Assert.That(result, Is.False);
            mockLoadRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Load>()), Times.Never);
        }

        [Test]
        public void DeleteLoadGetAsync_ShouldReturnNotNull_WhenLoadExists()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(4),
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid(),
                IsDeleted = false
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            mockLoadRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Load>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = loadService.DeleteLoadGetAsync(loadId).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void DeleteLoadGetAsync_ShouldReturnNull_WhenLoadNotFound()
        {
            // Arrange
            var loadId = Guid.NewGuid();

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Load>().AsQueryable().BuildMock());

            // Act
            var result = loadService.DeleteLoadGetAsync(loadId).Result;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void DeleteLoadGetAsync_ShouldReturnNull_WhenLoadAlreadyDeleted()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(4),
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid(),
                IsDeleted = true
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            // Act
            var result = loadService.DeleteLoadGetAsync(loadId).Result;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void DeleteLoadAsync_ShouldDeleteLoad_WhenLoadExistsAndIsNotDeleted()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(4),
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid(),
                IsDeleted = false
            };

            var model = new LoadDeleteViewModel 
            { 
                Id = loadId, 
                PickupLocation = "Location A",
                DeliveryLocation = "Location B"
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            mockLoadRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Load>()))
                .Returns(Task.CompletedTask);

            // Act
            this.loadService.DeleteLoadAsync(model);

            // Assert
            Assert.That(load.IsDeleted, Is.True);
            mockLoadRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Load>()), Times.Once);
        }

        [Test]
        public void DeleteLoadAsync_ShouldNotDeleteLoad_WhenLoadDoesNotExist()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var model = new LoadDeleteViewModel 
            { 
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B"
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Load>().AsQueryable().BuildMock());

            // Act
            this.loadService.DeleteLoadAsync(model);

            // Assert
            mockLoadRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Load>()), Times.Never);
        }

        [Test]
        public void DeleteLoadAsync_ShouldNotDeleteLoad_WhenLoadIsAlreadyDeleted()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(4),
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid(),
                IsDeleted = true
            };

            var model = new LoadDeleteViewModel 
            { 
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B"
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            // Act
            this.loadService.DeleteLoadAsync(model);

            // Assert
            mockLoadRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Load>()), Times.Never);
        }

        [Test]
        public void DeleteLoadAsync_ShouldHandleExceptionGracefully()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var model = new LoadDeleteViewModel 
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B"
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Throws(new Exception("Test exception"));

            // Act & Assert
            Assert.DoesNotThrow(() => this.loadService.DeleteLoadAsync(model));
        }

        [Test]
        public void GetAssignLoadByIdAsync_ShouldReturnViewModel_WhenLoadExists()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(4),
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid(),
                DriverId = Guid.NewGuid(),
                IsDeleted = false
            };

            var viewModel = new LoadAssignInputModel
            {
                LoadId = loadId,
                DriverId = load.DriverId ?? Guid.Empty
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            mockDriverRepository.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Driver>().AsQueryable().BuildMock());

            // Act
            var result = loadService.GetAssignLoadByIdAsync(loadId).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.LoadId, Is.EqualTo(loadId));
            Assert.That(result.DriverId, Is.EqualTo(load.DriverId ?? Guid.Empty));
            mockDriverRepository.Verify(repo => repo.GetAllAttached(), Times.Once);
        }

        [Test]
        public void GetAssignLoadByIdAsync_ShouldReturnNull_WhenLoadDoesNotExist()
        {
            // Arrange
            var loadId = Guid.NewGuid();

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Load>().AsQueryable().BuildMock());

            // Act
            var result = loadService.GetAssignLoadByIdAsync(loadId).Result;

            // Assert
            Assert.That(result, Is.Null);
            mockDriverRepository.Verify(repo => repo.GetAllAttached(), Times.Never);
        }

        [Test]
        public void GetAssignLoadByIdAsync_ShouldReturnViewModel_WhenLoadExists_ButNoAvailableDrivers()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                PickupLocation = "Location A",
                DeliveryLocation = "Location B",
                Weight = 1000,
                Temperature = 25,
                PickupTime = DateTime.Now,
                DeliveryTime = DateTime.Now.AddHours(4),
                Distance = 200,
                BrokerCompanyId = Guid.NewGuid(),
                DriverId = null,
                IsDeleted = false
            };

            var viewModel = new LoadAssignInputModel
            {
                LoadId = loadId,
                DriverId = Guid.Empty
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            mockDriverRepository.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Driver>().AsQueryable().BuildMock());

            // Act
            var result = loadService.GetAssignLoadByIdAsync(loadId).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.LoadId, Is.EqualTo(loadId));
            Assert.That(result.DriverId, Is.EqualTo(Guid.Empty)); // No driver assigned
            mockDriverRepository.Verify(repo => repo.GetAllAttached(), Times.Once);
        }

        [Test]
        public void GetAssignLoadByIdAsync_ShouldHandleExceptionGracefully()
        {
            // Arrange
            var loadId = Guid.NewGuid();

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Throws(new Exception("Test exception"));

            // Act & Assert
            Assert.DoesNotThrow(() => this.loadService.GetAssignLoadByIdAsync(loadId));
        }

        [Test]
        public void PostAssignLoadByIdAsync_ShouldReturnTrue_WhenLoadAndDriverAreValid()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            var load = new Load
            {
                Id = loadId,
                IsDeleted = false,
                DriverId = null,
                Status = DispatchStatus.Available,
                Driver = new Driver { Id = driverId, IsDeleted = false, IsAvailable = true }
            };

            var driver = new Driver
            {
                Id = driverId,
                IsDeleted = false,
                IsAvailable = true,
                TruckId = Guid.NewGuid(),
                TrailerId = Guid.NewGuid()
            };

            var model = new LoadAssignInputModel
            {
                DriverId = driverId
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            mockDriverRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { driver }.AsQueryable().BuildMock());

            // Act
            var result = loadService.PostAssignLoadByIdAsync(model, loadId).Result;

            // Assert
            Assert.That(result, Is.True);
            Assert.That(load.DriverId, Is.EqualTo(driverId));
            Assert.That(load.Status, Is.EqualTo(DispatchStatus.InProgress));
            Assert.That(driver.IsAvailable, Is.False);
            mockDispatchRepository.Verify(repo => repo.AddAsync(It.IsAny<Dispatch>()), Times.Once);
            mockLoadRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Load>()), Times.Once);
            mockDriverRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Driver>()), Times.Once);
        }

        [Test]
        public void PostAssignLoadByIdAsync_ShouldReturnFalse_WhenLoadDoesNotExist()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var model = new LoadAssignInputModel { DriverId = Guid.NewGuid() };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Load>().AsQueryable().BuildMock());

            // Act
            var result = loadService.PostAssignLoadByIdAsync(model, loadId).Result;

            // Assert
            Assert.That(result, Is.False);
            mockDriverRepository.Verify(repo => repo.GetAllAttached(), Times.Never);
        }

        [Test]
        public void PostAssignLoadByIdAsync_ShouldReturnFalse_WhenLoadIsDeleted()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var model = new LoadAssignInputModel { DriverId = Guid.NewGuid() };

            var load = new Load
            {
                Id = loadId,
                IsDeleted = true,
                DriverId = null,
                Status = DispatchStatus.Available
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            // Act
            var result = loadService.PostAssignLoadByIdAsync(model, loadId).Result;

            // Assert
            Assert.That(result, Is.False);
            mockDriverRepository.Verify(repo => repo.GetAllAttached(), Times.Never);
        }

        [Test]
        public void PostAssignLoadByIdAsync_ShouldReturnFalse_WhenDriverDoesNotExist()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            var model = new LoadAssignInputModel { DriverId = driverId };

            var load = new Load
            {
                Id = loadId,
                IsDeleted = false,
                DriverId = null,
                Status = DispatchStatus.Available
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            mockDriverRepository.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Driver>().AsQueryable().BuildMock());

            // Act
            var result = loadService.PostAssignLoadByIdAsync(model, loadId).Result;

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void PostAssignLoadByIdAsync_ShouldReturnFalse_WhenDriverIsDeleted()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var driverId = Guid.NewGuid();
            var model = new LoadAssignInputModel { DriverId = driverId };

            var load = new Load
            {
                Id = loadId,
                IsDeleted = false,
                DriverId = null,
                Status = DispatchStatus.Available
            };

            var driver = new Driver
            {
                Id = driverId,
                IsDeleted = true,
                IsAvailable = true
            };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { load }.AsQueryable().BuildMock());

            mockDriverRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { driver }.AsQueryable().BuildMock());

            // Act
            var result = loadService.PostAssignLoadByIdAsync(model, loadId).Result;

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void PostAssignLoadByIdAsync_ShouldHandleExceptionGracefully()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var model = new LoadAssignInputModel { DriverId = Guid.NewGuid() };

            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Throws(new Exception("Test exception"));

            // Act & Assert
            Assert.DoesNotThrow(() => this.loadService.PostAssignLoadByIdAsync(model, loadId));
        }

        [Test]
        public void LoadAvailableDrivers_ShouldReturnDrivers_WhenValidDriversExist()
        {
            // Arrange
            var model = new LoadAssignInputModel();
            var drivers = new List<Driver>
            {
                new Driver
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Truck = new Truck(),
                    Trailer = new Trailer(),
                    DriverManager = new DriverManager()
                },
                new Driver
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Truck = new Truck(),
                    Trailer = new Trailer(),
                    DriverManager = new DriverManager()
                }
            };

            mockDriverRepository.Setup(repo => repo.GetAllAttached())
                .Returns(drivers.AsQueryable().BuildMock());

            // Act
            loadService.LoadAvailableDrivers(model).Wait();  

            // Assert
            Assert.That(model.Drivers, Is.Not.Null);
            Assert.That(model.Drivers.Count, Is.EqualTo(2));
        }

        [Test]
        public void LoadAvailableDrivers_ShouldReturnNoDrivers_WhenNoDriversMeetCriteria()
        {
            // Arrange
            var model = new LoadAssignInputModel();
            var drivers = new List<Driver>
            {
                new Driver
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Truck = null,   // Invalid truck
                    Trailer = null, // Invalid trailer
                    DriverManager = null // Invalid driver manager
                },
                new Driver
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = true,  // Invalid deleted driver
                    Truck = new Truck(),
                    Trailer = new Trailer(),
                    DriverManager = new DriverManager()
                }
            };

            mockDriverRepository.Setup(repo => repo.GetAllAttached())
                .Returns(drivers.AsQueryable().BuildMock());

            // Act
            loadService.LoadAvailableDrivers(model).Wait();  

            // Assert
            Assert.That(model.Drivers, Is.Not.Null);
            Assert.That(model.Drivers.Count, Is.EqualTo(0));
        }

        [Test]
        public void LoadAvailableDrivers_ShouldReturnFilteredDrivers_WhenSomeDriversDoNotMeetCriteria()
        {
            // Arrange
            var model = new LoadAssignInputModel();
            var drivers = new List<Driver>
            {
                new Driver
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Truck = new Truck(),
                    Trailer = new Trailer(),
                    DriverManager = new DriverManager()
                },
                new Driver
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Truck = null, // Invalid truck
                    Trailer = new Trailer(),
                    DriverManager = new DriverManager()
                },
                new Driver
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = true,  // Invalid deleted driver
                    Truck = new Truck(),
                    Trailer = new Trailer(),
                    DriverManager = new DriverManager()
                }
            };

            mockDriverRepository.Setup(repo => repo.GetAllAttached())
                .Returns(drivers.AsQueryable().BuildMock());

            // Act
            loadService.LoadAvailableDrivers(model).Wait();  

            // Assert
            Assert.That(model.Drivers, Is.Not.Null);
            Assert.That(model.Drivers.Count, Is.EqualTo(1)); // Only one valid driver
        }
    }
}
