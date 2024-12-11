using Moq;
using TruckingSystem.Data.Models;
using MockQueryable.Moq;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data;
using TruckingSystem.Web.ViewModels.Driver;
using MockQueryable;

namespace TruckingSystem.Services.Tests
{
    [TestFixture]
    public class DriverServiceTests
    {
        private Mock<IRepository<Driver>> driverRepositoryMock;
        private Mock<IRepository<DriverManager>> driverManagerRepositoryMock;
        private Mock<IRepository<Truck>> truckRepositoryMock;
        private Mock<IRepository<Trailer>> trailerRepositoryMock;
        private DriverService driverService;

        [SetUp]
        public void SetUp()
        {
            this.driverRepositoryMock = new Mock<IRepository<Driver>>();
            this.driverManagerRepositoryMock = new Mock<IRepository<DriverManager>>();
            this.truckRepositoryMock = new Mock<IRepository<Truck>>();
            this.trailerRepositoryMock = new Mock<IRepository<Trailer>>();

            // Instantiate DriverService with all the required mocks
            this.driverService = new DriverService(
                this.driverRepositoryMock.Object,
                this.driverManagerRepositoryMock.Object,
                this.truckRepositoryMock.Object,
                this.trailerRepositoryMock.Object
                );
        }

        [Test]
        public void GetAllDriversAsync_WhenNoDrivers_ShouldReturnEmptyPaginatedList()
        {
            // Arrange
            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Driver>().AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = this.driverService.GetAllDriversAsync(1, 10).Result;

            // Assert
            Assert.That(result.Items.Count, Is.EqualTo(0));
            Assert.That(result.TotalCount, Is.EqualTo(0));
        }

        [Test]
        public void GetAllDriversAsync_WhenDriversExist_ShouldReturnPaginatedList()
        {
            // Arrange
            var drivers = new List<Driver>
        {
                new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                LicenseNumber = "67890",
                Truck = new Truck { TruckNumber = "T456" },
                Trailer = new Trailer { TrailerNumber = "TR456" },
                DriverManager = new DriverManager { FirstName = "Manager", LastName = "Two" },
                IsDeleted = false
            },

            new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                LicenseNumber = "12345",
                Truck = new Truck { TruckNumber = "T123" },
                Trailer = new Trailer { TrailerNumber = "TR123" },
                DriverManager = new DriverManager { FirstName = "Manager", LastName = "One" },
                IsDeleted = false
            }
        };

            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(drivers.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = this.driverService.GetAllDriversAsync(1, 10).Result;

            // Assert
            Assert.That(result.Items.Count, Is.EqualTo(2));
            Assert.That(result.Items[0].FirstName, Is.EqualTo("Jane"));
            Assert.That(result.TotalCount, Is.EqualTo(2));
        }

        [Test]
        public void GetAllDriversAsync_WhenSomeDriversAreDeleted_ShouldReturnOnlyNonDeleted()
        {
            // Arrange
            var drivers = new List<Driver>
        {
            new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                LicenseNumber = "12345",
                Truck = new Truck { TruckNumber = "T123" },
                Trailer = new Trailer { TrailerNumber = "TR123" },
                DriverManager = new DriverManager { FirstName = "Manager", LastName = "One" },
                IsDeleted = false
            },
            new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                LicenseNumber = "67890",
                Truck = new Truck { TruckNumber = "T456" },
                Trailer = new Trailer { TrailerNumber = "TR456" },
                DriverManager = new DriverManager { FirstName = "Manager", LastName = "Two" },
                IsDeleted = true
            }
        };

            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(drivers.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = this.driverService.GetAllDriversAsync(1, 10).Result;

            // Assert
            Assert.That(result.Items.Count, Is.EqualTo(1));
            Assert.That(result.Items[0].FirstName, Is.EqualTo("John"));
        }

        [Test]
        public void GetAllDriversAsync_ShouldReturnCorrectPagination()
        {
            // Arrange
            var drivers = new List<Driver>
        {
            new Driver { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", LicenseNumber = "12345", IsDeleted = false },
            new Driver { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", LicenseNumber = "67890", IsDeleted = false },
            new Driver { Id = Guid.NewGuid(), FirstName = "Tom", LastName = "Smith", LicenseNumber = "112233", IsDeleted = false },
            new Driver { Id = Guid.NewGuid(), FirstName = "Jerry", LastName = "Brown", LicenseNumber = "445566", IsDeleted = false }
        };

            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(drivers.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = this.driverService.GetAllDriversAsync(2, 2).Result;

            // Assert
            Assert.That(result.Items.Count, Is.EqualTo(2));
            Assert.That(result.Items[0].FirstName, Is.EqualTo("John"));
            Assert.That(result.Items[1].FirstName, Is.EqualTo("Tom"));
        }

        [Test]
        public void CreateDriverAsync_ShouldMapModelToDriverAndCallAddAsync()
        {
            // Arrange
            var model = new DriverAddInputModel
            {
                FirstName = "John",
                LastName = "Doe",
                LicenseNumber = "12345",
                TruckId = Guid.NewGuid(),
                TrailerId = Guid.NewGuid(),
                DriverManagerId = Guid.NewGuid()
            };

            // Act
            this.driverService.CreateDriverAsync(model).Wait(); 

            // Assert
            this.driverRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Driver>(d =>
                d.FirstName == model.FirstName &&
                d.LastName == model.LastName &&
                d.LicenseNumber == model.LicenseNumber &&
                d.TruckId == model.TruckId &&
                d.TrailerId == model.TrailerId &&
                d.DriverManagerId == model.DriverManagerId
            )), Times.Once); // Ensure AddAsync was called once with the mapped driver
        }

        [Test]
        public void GetEditDriverByIdAsync_ShouldReturnNull_WhenDriverDoesNotExist()
        {
            // Arrange
            var driverId = Guid.NewGuid();

            // Mock the repository to return no drivers
            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Driver>().AsQueryable().BuildMock());

            // Act
            var result = this.driverService.GetEditDriverByIdAsync(driverId).Result;

            // Assert
            Assert.Null(result);
        }

        [Test]
        public void PostEditDriverByIdAsync_ShouldReturnFalse_WhenDriverDoesNotExist()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var model = new DriverEditInputModel
            {
                FirstName = "John",
                LastName = "Doe",
                LicenseNumber = "12345"
            };

            // Mock the repository to return null
            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Driver>().AsQueryable().BuildMock());

            // Act
            var result = this.driverService.PostEditDriverByIdAsync(model, driverId).Result;

            // Assert
            Assert.False(result);  // The driver does not exist, so return false
        }

        [Test]
        public void PostEditDriverByIdAsync_ShouldReturnFalse_WhenDriverIsDeleted()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var model = new DriverEditInputModel
            {
                FirstName = "John",
                LastName = "Doe",
                LicenseNumber = "12345"
            };

            // Mock the repository to return a deleted driver
            var driver = new Driver
            {
                Id = driverId,
                FirstName = "Old FirstName",
                LastName = "Old LastName",
                LicenseNumber = "Old License",
                IsDeleted = true
            };

            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { driver }.AsQueryable().BuildMock());

            // Act
            var result = this.driverService.PostEditDriverByIdAsync(model, driverId).Result;

            // Assert
            Assert.False(result);  // The driver is deleted, so return false
        }

        [Test]
        public void PostEditDriverByIdAsync_ShouldUpdateTruckAndTrailerAvailability()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var model = new DriverEditInputModel
            {
                FirstName = "John",
                LastName = "Doe",
                LicenseNumber = "12345",
                TruckId = Guid.NewGuid(),
                TrailerId = Guid.NewGuid()
            };

            var driver = new Driver
            {
                Id = driverId,
                FirstName = "Old FirstName",
                LastName = "Old LastName",
                LicenseNumber = "Old License",
                IsDeleted = false,
                TruckId = Guid.NewGuid(),
                TrailerId = Guid.NewGuid()
            };

            var oldTruck = new Truck { Id = driver.TruckId ?? Guid.NewGuid(), IsAvailable = false };
            var newTruck = new Truck { Id = model.TruckId ?? Guid.NewGuid(), IsAvailable = true };
            var oldTrailer = new Trailer { Id = driver.TrailerId ?? Guid.NewGuid(), IsAvailable = false };
            var newTrailer = new Trailer { Id = model.TrailerId ?? Guid.NewGuid(), IsAvailable = true };

            // Mock the repositories to return data
            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { driver }.AsQueryable().BuildMock());
            this.truckRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { oldTruck, newTruck }.AsQueryable().BuildMock());
            this.trailerRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { oldTrailer, newTrailer }.AsQueryable().BuildMock());

            // Mock UpdateAsync to return the updated entities
            this.driverRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Driver>()))
                .Returns(Task.CompletedTask);
            this.truckRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Truck>()))
                .Returns(Task.CompletedTask);
            this.trailerRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Trailer>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = this.driverService.PostEditDriverByIdAsync(model, driverId).Result;

            // Assert
            Assert.True(result);  // The driver was successfully updated
            Assert.False(newTruck.IsAvailable);  // New truck is marked as unavailable
            Assert.True(oldTruck.IsAvailable);  // Old truck is marked as available
            Assert.False(newTrailer.IsAvailable);  // New trailer is marked as unavailable
            Assert.True(oldTrailer.IsAvailable);  // Old trailer is marked as available
        }

        [Test]
        public void DeleteDriverGetAsync_ShouldReturnDriver_WhenDriverExistsAndIsNotDeleted()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var driver = new Driver
            {
                Id = driverId,
                FirstName = "John",
                LastName = "Doe",
                IsDeleted = false
            };

            // Mock the repository to return a valid driver
            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { driver }.AsQueryable().BuildMock());

            // Act
            var result = this.driverService.DeleteDriverGetAsync(driverId).Result;

            // Assert
            Assert.That(result, Is.Not.Null);  // The driver should be found
            Assert.That(result.Id, Is.EqualTo(driverId));  // The ID should match
            Assert.That(result.FirstName, Is.EqualTo("John"));  // The first name should match
            Assert.That(result.LastName, Is.EqualTo("Doe"));  // The last name should match
        }

        [Test]
        public void DeleteDriverGetAsync_ShouldReturnNull_WhenDriverDoesNotExist()
        {
            // Arrange
            var driverId = Guid.NewGuid();

            // Mock the repository to return no drivers
            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Driver>().AsQueryable().BuildMock());

            // Act
            var result = this.driverService.DeleteDriverGetAsync(driverId).Result;

            // Assert
            Assert.That(result, Is.Null);  // The driver should not be found, so the result should be null
        }

        [Test]
        public void DeleteDriverGetAsync_ShouldReturnNull_WhenDriverIsDeleted()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var driver = new Driver
            {
                Id = driverId,
                FirstName = "John",
                LastName = "Doe",
                IsDeleted = true
            };

            // Mock the repository to return a deleted driver
            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { driver }.AsQueryable().BuildMock());

            // Act
            var result = this.driverService.DeleteDriverGetAsync(driverId).Result;

            // Assert
            Assert.That(result, Is.Null);  // The driver is deleted, so the result should be null
        }

        [Test]
        public void DeleteDriverAsync_ShouldMarkDriverAsDeleted_WhenDriverExistsAndIsNotDeleted()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var driver = new Driver
            {
                Id = driverId,
                FirstName = "John",
                LastName = "Doe",
                IsDeleted = false
            };

            var model = new DriverDeleteViewModel
            {
                Id = driverId,
                FirstName = "John",
                LastName = "Doe"
            };

            // Mock the repository to return a valid driver
            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { driver }.AsQueryable().BuildMock());

            // Act
            this.driverService.DeleteDriverAsync(model).Wait();  // Call the method synchronously using Wait()

            // Assert
            Assert.That(driver.IsDeleted, Is.True);  // The driver should be marked as deleted
            this.driverRepositoryMock.Verify(repo => repo.UpdateAsync(driver), Times.Once);  // UpdateAsync should be called once
        }

        [Test]
        public void DeleteDriverAsync_ShouldNotChangeDriver_WhenDriverDoesNotExist()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var model = new DriverDeleteViewModel
            {
                Id = driverId,
                FirstName = "N/A",
                LastName = "N/A"
            };

            // Mock the repository to return no drivers
            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Driver>().AsQueryable().BuildMock());

            // Act
            this.driverService.DeleteDriverAsync(model).Wait();  // Call the method synchronously using Wait()

            // Assert
            // Since no driver exists, no update should happen. We verify that UpdateAsync was not called.
            this.driverRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Driver>()), Times.Never);
        }

        [Test]
        public void DeleteDriverAsync_ShouldNotChangeDriver_WhenDriverIsAlreadyDeleted()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var driver = new Driver
            {
                Id = driverId,
                FirstName = "John",
                LastName = "Doe",
                IsDeleted = true
            };

            var model = new DriverDeleteViewModel
            {
                Id = driverId,
                FirstName = "John",
                LastName = "Doe"
            };

            // Mock the repository to return the driver that is already deleted
            this.driverRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { driver }.AsQueryable().BuildMock());

            // Act
            this.driverService.DeleteDriverAsync(model).Wait();  // Call the method synchronously using Wait()

            // Assert
            // Since the driver is already deleted, no change should happen. We verify that UpdateAsync was not called.
            this.driverRepositoryMock.Verify(repo => repo.UpdateAsync(driver), Times.Never);
        }

        [Test]
        public void LoadSelectLists_ShouldPopulateAvailableTrucks_WhenCalledWithDriverEditInputModel()
        {
            // Arrange
            var model = new DriverEditInputModel()
            {
                FirstName = "Josh",
                LastName = "Perkins",
                LicenseNumber = "N56622L"
            };

            // Mock the repository methods to return data
            var trucks = new List<Truck>
        {
            new Truck { Id = Guid.NewGuid(), TruckNumber = "T123", IsAvailable = true, IsDeleted = false },
            new Truck { Id = Guid.NewGuid(), TruckNumber = "T124", IsAvailable = true, IsDeleted = false }
        };
            this.truckRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(trucks.AsQueryable().BuildMock());

            var trailers = new List<Trailer>
        {
            new Trailer { Id = Guid.NewGuid(), TrailerNumber = "TR123", IsAvailable = true, IsDeleted = false },
            new Trailer { Id = Guid.NewGuid(), TrailerNumber = "TR124", IsAvailable = true, IsDeleted = false }
        };
            this.trailerRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(trailers.AsQueryable().BuildMock());

            var driverManagers = new List<DriverManager>
        {
            new DriverManager { Id = Guid.NewGuid(), FirstName = "Manager1", LastName = "Test" },
            new DriverManager { Id = Guid.NewGuid(), FirstName = "Manager2", LastName = "Test" }
        };
            this.driverManagerRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(driverManagers.AsQueryable().BuildMock());

            // Act
            this.driverService.LoadSelectLists(model).Wait();  // Call the method synchronously using Wait()

            // Assert
            Assert.That(model.AvailableTrucks.Count(), Is.EqualTo(2));  // Verify that AvailableTrucks has 2 items
            Assert.That(model.AvailableTrailers.Count(), Is.EqualTo(2));  // Verify that AvailableTrailers has 2 items
            Assert.That(model.DriverManagers.Count(), Is.EqualTo(2));  // Verify that DriverManagers has 2 items
        }

        [Test]
        public void LoadSelectLists_ShouldPopulateAvailableTrucks_WhenCalledWithDriverAddInputModel()
        {
            // Arrange
            var model = new DriverAddInputModel()
            {
                FirstName = "Josh",
                LastName = "Perkins",
                LicenseNumber = "N56622L"
            };

            // Mock the repository methods to return data
            var trucks = new List<Truck>
        {
            new Truck { Id = Guid.NewGuid(), TruckNumber = "T125", IsAvailable = true, IsDeleted = false },
            new Truck { Id = Guid.NewGuid(), TruckNumber = "T126", IsAvailable = true, IsDeleted = false }
        };
            this.truckRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(trucks.AsQueryable().BuildMock());

            var trailers = new List<Trailer>
        {
            new Trailer { Id = Guid.NewGuid(), TrailerNumber = "TR125", IsAvailable = true, IsDeleted = false },
            new Trailer { Id = Guid.NewGuid(), TrailerNumber = "TR126", IsAvailable = true, IsDeleted = false }
        };
            this.trailerRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(trailers.AsQueryable().BuildMock());

            var driverManagers = new List<DriverManager>
        {
            new DriverManager { Id = Guid.NewGuid(), FirstName = "Manager3", LastName = "Test" },
            new DriverManager { Id = Guid.NewGuid(), FirstName = "Manager4", LastName = "Test" }
        };
            this.driverManagerRepositoryMock.Setup(repo => repo.GetAllAttached())
                .Returns(driverManagers.AsQueryable().BuildMock());

            // Act
            this.driverService.LoadSelectLists(model).Wait();  // Call the method synchronously using Wait()

            // Assert
            Assert.That(model.AvailableTrucks.Count(), Is.EqualTo(2));  // Verify that AvailableTrucks has 2 items
            Assert.That(model.AvailableTrailers.Count(), Is.EqualTo(2));  // Verify that AvailableTrailers has 2 items
            Assert.That(model.DriverManagers.Count(), Is.EqualTo(2));  // Verify that DriverManagers has 2 items
        }
    }
}
