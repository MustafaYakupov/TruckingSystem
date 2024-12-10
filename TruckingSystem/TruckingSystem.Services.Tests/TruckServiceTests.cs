using Moq;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data;
using MockQueryable;

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
    }
}