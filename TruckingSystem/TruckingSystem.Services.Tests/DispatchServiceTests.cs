using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckingSystem.Services.Data;
using TruckingSystem.Web.ViewModels;
using TruckingSystem.Data.Models;
using TruckingSystem.Data.Models.Enums;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Web.ViewModels.Dispatch;
using MockQueryable.Moq;
using MockQueryable;

namespace TruckingSystem.Services.Tests
{
    [TestFixture]
    public class DispatchServiceTests
    {
        private Mock<IRepository<Dispatch>> mockDispatchRepository;
        private Mock<IRepository<Load>> mockLoadRepository;
        private DispatchService dispatchService;

        private const string DateTimeFormat = "yyyy-MM-dd";

        [SetUp]
        public void SetUp()
        {
            mockDispatchRepository = new Mock<IRepository<Dispatch>>();
            mockLoadRepository = new Mock<IRepository<Load>>();
            dispatchService = new DispatchService(mockDispatchRepository.Object, mockLoadRepository.Object);
        }


        [Test]
        public void GetAllDispatchesCompletedAsync_NoDispatches_ReturnsEmptyList()
        {
            // Arrange
            mockDispatchRepository.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Dispatch>().AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = dispatchService.GetAllDispatchesCompletedAsync(string.Empty, 1, 10).Result;

            // Assert
            Assert.That(result.Items, Is.Empty, "The result list should be empty when no dispatches exist.");
            Assert.That(result.TotalCount, Is.EqualTo(0), "Total count should be 0 when no dispatches exist.");
        }

        [Test]
        public void GetAllDispatchesCompletedAsync_WithSearchString_FiltersResults()
        {
            // Arrange
            var driver = new Driver
            {
                FirstName = "John",
                LastName = "Doe",
                Truck = new Truck { TruckNumber = "123" },
                Trailer = new Trailer { TrailerNumber = "456" },
                DriverManager = new DriverManager { FirstName = "Manager", LastName = "One" }
            };
            var dispatch = new Dispatch
            {
                Id = Guid.NewGuid(),
                Driver = driver,
                Status = DispatchStatus.Completed,
                IsDeleted = false,
                Load = new Load { BrokerCompany = new BrokerCompany() }
            };

            mockDispatchRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Dispatch> { dispatch }.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = dispatchService.GetAllDispatchesCompletedAsync("Doe", 1, 10).Result;

            // Assert
            Assert.That(result.Items.Count, Is.EqualTo(1), "The filtered result should contain one item.");
            Assert.That(result.Items[0].Driver, Is.EqualTo("John Doe"), "The driver's name should match the search string.");
        }

        [Test]
        public void GetAllDispatchesCompletedAsync_WithPagination_ReturnsCorrectPage()
        {
            // Arrange
            var drivers = new List<Driver>
            {
                new Driver
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Truck = new Truck { TruckNumber = "123" },
                    Trailer = new Trailer { TrailerNumber = "456" },
                    DriverManager = new DriverManager { FirstName = "Manager", LastName = "One" }
                },
                new Driver
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Truck = new Truck { TruckNumber = "45" },
                    Trailer = new Trailer { TrailerNumber = "21" },
                    DriverManager = new DriverManager { FirstName = "Manager", LastName = "One" }
                },
                new Driver
                {
                    FirstName = "Jack",
                    LastName = "Black",
                    Truck = new Truck { TruckNumber = "32" },
                    Trailer = new Trailer { TrailerNumber = "33" },
                    DriverManager = new DriverManager { FirstName = "Manager", LastName = "One" }
                }
            };

            var dispatches = drivers.Select((driver, index) => new Dispatch
            {
                Id = Guid.NewGuid(),
                Driver = driver,
                Status = DispatchStatus.Completed,
                IsDeleted = false,
                Load = new Load { BrokerCompany = new BrokerCompany() }
            }).ToList();

            mockDispatchRepository.Setup(repo => repo.GetAllAttached())
                .Returns(dispatches.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = dispatchService.GetAllDispatchesCompletedAsync(string.Empty, 2, 2).Result;

            // Assert
            Assert.That(result.Items.Count, Is.EqualTo(1), "Page 2 should contain one item.");
            Assert.That(result.Items[0].Driver, Is.EqualTo("Jack Black"), "The driver on the second page should be Jack Black.");
        }

        [Test]
        public void GetAllDispatchesCompletedAsync_WithNoMatchingSearch_ReturnsEmptyList()
        {
            // Arrange
            var driver = new Driver 
            {
                FirstName = "John",
                LastName = "Doe",
                LicenseNumber = "2SS45454",
                Truck = new Truck { TruckNumber = "123" },
                Trailer = new Trailer { TrailerNumber = "456" },
                DriverManager = new DriverManager { FirstName = "Manager", LastName = "One" }
            };

            var dispatch = new Dispatch
            {
                Id = Guid.NewGuid(),
                Driver = driver,
                Status = DispatchStatus.Completed,
                IsDeleted = false,
                Load = new Load { BrokerCompany = new BrokerCompany() }
            };

            mockDispatchRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Dispatch> { dispatch }.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = dispatchService.GetAllDispatchesCompletedAsync("Smith", 1, 10).Result;

            // Assert
            Assert.That(result.Items, Is.Empty, "No items should match the search string.");
            Assert.That(result.Items.Count, Is.EqualTo(0), "Total count should be 0 when no matches are found.");
        }

        [Test]
        public void GetAllDispatchesInProgressAsync_ShouldReturnEmptyList_WhenNoDispatchesInProgress()
        {
            // Arrange
            var searchString = string.Empty;
            var page = 1;
            var pageSize = 10;

            mockDispatchRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Dispatch>().AsQueryable().BuildMockDbSet().Object);
            mockLoadRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Load>().AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = dispatchService.GetAllDispatchesInProgressAsync(searchString, page, pageSize).Result;

            // Assert
            Assert.That(result.Items.Count, Is.EqualTo(0));  // Verify empty result
            Assert.That(result.TotalCount, Is.EqualTo(0));  // Verify total count is 0
        }

        [Test]
        public void CompleteDispatchByIdAsync_DispatchNotFound_ReturnsFalse()
        {
            // Arrange
            var loadId = Guid.NewGuid();

            this.mockDispatchRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(new List<Dispatch>().AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = this.dispatchService.CompleteDispatchByIdAsync(loadId).Result;

            // Assert
            Assert.That(result, Is.False, "If no dispatch is found, the method should return false.");
        }

        [Test]
        public void CompleteDispatchByIdAsync_DispatchIsDeleted_ReturnsFalse()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var deletedDispatch = new Dispatch
            {
                LoadId = loadId,
                IsDeleted = true
            };

            this.mockDispatchRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(new List<Dispatch> { deletedDispatch }.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = this.dispatchService.CompleteDispatchByIdAsync(loadId).Result;

            // Assert
            Assert.That(result, Is.False, "If the dispatch is marked as deleted, the method should return false.");
        }

        [Test]
        public void CompleteDispatchByIdAsync_SuccessfulCompletion_ReturnsTrue()
        {
            // Arrange
            var loadId = Guid.NewGuid();
            var dispatch = new Dispatch
            {
                LoadId = loadId,
                IsDeleted = false,
                Status = DispatchStatus.InProgress
            };
            var load = new Load
            {
                Id = loadId,
                Status = DispatchStatus.InProgress
            };

            this.mockDispatchRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(new List<Dispatch> { dispatch }.AsQueryable().BuildMockDbSet().Object);

            this.mockLoadRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(new List<Load> { load }.AsQueryable().BuildMockDbSet().Object);

            this.mockDispatchRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<Dispatch>()));

            // Act
            var result = this.dispatchService.CompleteDispatchByIdAsync(loadId).Result;

            // Assert
            Assert.That(result, Is.True, "If the dispatch is completed successfully, the method should return true.");
            Assert.That(dispatch.Status, Is.EqualTo(DispatchStatus.Completed), "Dispatch status should be updated to 'Completed'.");
            Assert.That(load.Status, Is.EqualTo(DispatchStatus.Completed), "Load status should be updated to 'Completed'.");
        }
    }
}
