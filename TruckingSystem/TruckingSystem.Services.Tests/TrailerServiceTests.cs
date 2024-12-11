using MockQueryable;
using MockQueryable.Moq;
using Moq;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;
using TruckingSystem.Services.Data;
using TruckingSystem.Web.ViewModels.Trailer;

namespace TruckingSystem.Services.Tests
{
    [TestFixture]
    public class TrailerServiceTests
    {
        private Mock<IRepository<Trailer>> mockTrailerRepository;
        private TrailerService trailerService;

        [SetUp]
        public void SetUp()
        {
            this.mockTrailerRepository = new Mock<IRepository<Trailer>>();
            this.trailerService = new TrailerService(this.mockTrailerRepository.Object);
        }

        [Test]
        public void GetAllTrailersAsync_ReturnsPaginatedList_WhenTrailersExist()
        {
            // Arrange
            int page = 1;
            int pageSize = 2;
            List<Trailer> trailers = new()
        {
            new Trailer { Id = Guid.NewGuid(), TrailerNumber = "TN123", Make = "Make1", Type = "Type1", ModelYear = "2022", IsDeleted = false },
            new Trailer { Id = Guid.NewGuid(), TrailerNumber = "TN124", Make = "Make2", Type = "Type2", ModelYear = "2023", IsDeleted = false },
            new Trailer { Id = Guid.NewGuid(), TrailerNumber = "TN125", Make = "Make3", Type = "Type3", ModelYear = "2021", IsDeleted = false }
        };

            var asyncTrailers = trailers.AsQueryable().BuildMock();

            this.mockTrailerRepository.Setup(r => r.GetAllAttached())
                .Returns(asyncTrailers);

            // Act
            var result = this.trailerService.GetAllTrailersAsync(page, pageSize).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(pageSize));
            Assert.That(result.Items.First().TrailerNumber, Is.EqualTo("TN123"));
            Assert.That(result.TotalCount, Is.EqualTo(3));
        }

        [Test]
        public void GetAllTrailersAsync_ReturnsEmptyPaginatedList_WhenNoTrailersExist()
        {
            // Arrange
            int page = 1;
            int pageSize = 2;
            List<Trailer> trailers = new();

            var asyncTrailers = trailers.AsQueryable().BuildMock();

            this.mockTrailerRepository.Setup(r => r.GetAllAttached())
                .Returns(asyncTrailers);

            // Act
            var result = this.trailerService.GetAllTrailersAsync(page, pageSize).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(0));
            Assert.That(result.TotalCount, Is.EqualTo(0));
        }

        [Test]
        public void CreateTrailerAsync_AddsNewTrailer()
        {
            // Arrange
            TrailerAddInputModel inputModel = new TrailerAddInputModel
            {
                TrailerNumber = "TN001",
                Make = "Make1",
                Type = "Type1",
                ModelYear = "2022"
            };

            this.mockTrailerRepository
                .Setup(r => r.AddAsync(It.IsAny<Trailer>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var task = this.trailerService.CreateTrailerAsync(inputModel);
            task.Wait(); // Wait for the task to complete.

            // Assert
            this.mockTrailerRepository.Verify(r => r.AddAsync(It.Is<Trailer>(t =>
                t.TrailerNumber == inputModel.TrailerNumber &&
                t.Make == inputModel.Make &&
                t.Type == inputModel.Type &&
                t.ModelYear == inputModel.ModelYear
            )), Times.Once);

            Assert.That(task.IsCompletedSuccessfully, Is.True);
        }

        [Test]
        public void DeleteTrailerAsync_TrailerNotFound_ShouldDoNothing()
        {
            // Arrange
            var trailerId = Guid.NewGuid();
            var trailerList = new List<Trailer>().AsQueryable();

            // Mock repository methods
            mockTrailerRepository.Setup(repo => repo.GetAllAttached())
                .Returns(trailerList);

            // Act
            trailerService.DeleteTrailerAsync(new TrailerDeleteViewModel { Id = trailerId, TrailerNumber = "" });

            // Assert
            mockTrailerRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Trailer>()), Times.Never);
        }

        [Test]
        public void DeleteTrailerAsync_TrailerAlreadyDeleted_ShouldDoNothing()
        {
            // Arrange
            var trailerId = Guid.NewGuid();
            var trailer = new Trailer
            {
                Id = trailerId,
                TrailerNumber = "TN12345",
                IsDeleted = true
            };

            var trailerList = new List<Trailer> { trailer }.AsQueryable();

            // Mock repository methods
            mockTrailerRepository.Setup(repo => repo.GetAllAttached())
                .Returns(trailerList);

            // Act
            trailerService.DeleteTrailerAsync(new TrailerDeleteViewModel { Id = trailerId, TrailerNumber = "" });

            // Assert
            mockTrailerRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Trailer>()), Times.Never);
        }

        [Test]
        public void PostEditTrailerByIdAsync_TrailerExistsAndNotDeleted_ShouldReturnTrue()
        {
            // Arrange
            var trailerId = Guid.NewGuid();
            var trailer = new Trailer
            {
                Id = trailerId,
                TrailerNumber = "TN12345",
                Make = "BrandX",
                Type = "Flatbed",
                ModelYear = "2020",
                IsDeleted = false
            };

            // Mock the repository method for getting the trailer by ID
            mockTrailerRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { trailer }.AsQueryable().BuildMock());

            // Mock UpdateAsync to return a completed task
            mockTrailerRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Trailer>()))
                .Returns(Task.CompletedTask);

            // Act
            var model = new TrailerEditInputModel
            {
                TrailerNumber = "TN54321",
                Make = "BrandY",
                Type = "Refrigerated",
                ModelYear = "2022"
            };

            var result = trailerService.PostEditTrailerByIdAsync(model, trailerId).Result;

            // Assert
            Assert.That(result, Is.True);
            Assert.That(trailer.TrailerNumber, Is.EqualTo("TN54321"));
            Assert.That(trailer.Make, Is.EqualTo("BrandY"));
            Assert.That(trailer.Type, Is.EqualTo("Refrigerated"));
            Assert.That(trailer.ModelYear, Is.EqualTo("2022"));
        }

        [Test]
        public void PostEditTrailerByIdAsync_TrailerNotFound_ShouldReturnFalse()
        {
            // Arrange
            var trailerId = Guid.NewGuid();

            // Mock the repository method to return an empty collection (i.e., no trailers)
            mockTrailerRepository.Setup(repo => repo.GetAllAttached())
                .Returns(Enumerable.Empty<Trailer>().AsQueryable().BuildMock());

            // Mock UpdateAsync to return a completed task
            mockTrailerRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Trailer>()))
                .Returns(Task.CompletedTask);

            // Act
            var model = new TrailerEditInputModel
            {
                TrailerNumber = "TN54321",
                Make = "BrandY",
                Type = "Refrigerated",
                ModelYear = "2022"
            };

            var result = trailerService.PostEditTrailerByIdAsync(model, trailerId).Result;

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void PostEditTrailerByIdAsync_TrailerIsDeleted_ShouldReturnFalse()
        {
            // Arrange
            var trailerId = Guid.NewGuid();
            var trailer = new Trailer
            {
                Id = trailerId,
                TrailerNumber = "TN12345",
                Make = "BrandX",
                Type = "Flatbed",
                ModelYear = "2020",
                IsDeleted = true
            };

            // Mock the repository method to return a deleted trailer
            mockTrailerRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { trailer }.AsQueryable().BuildMock());

            // Mock UpdateAsync to return a completed task
            mockTrailerRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Trailer>()))
                .Returns(Task.CompletedTask);

            // Act
            var model = new TrailerEditInputModel
            {
                TrailerNumber = "TN54321",
                Make = "BrandY",
                Type = "Refrigerated",
                ModelYear = "2022"
            };

            var result = trailerService.PostEditTrailerByIdAsync(model, trailerId).Result;

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void PostEditTrailerByIdAsync_UpdateAsyncShouldBeCalled()
        {
            // Arrange
            var trailerId = Guid.NewGuid();
            var trailer = new Trailer
            {
                Id = trailerId,
                TrailerNumber = "TN12345",
                Make = "BrandX",
                Type = "Flatbed",
                ModelYear = "2020",
                IsDeleted = false
            };

            // Mock the repository method to return the trailer
            mockTrailerRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new[] { trailer }.AsQueryable().BuildMock());

            // Mock UpdateAsync to return a completed task
            mockTrailerRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Trailer>()))
                .Returns(Task.CompletedTask);

            // Act
            var model = new TrailerEditInputModel
            {
                TrailerNumber = "TN54321",
                Make = "BrandY",
                Type = "Refrigerated",
                ModelYear = "2022"
            };

            var result = trailerService.PostEditTrailerByIdAsync(model, trailerId).Result;

            // Assert
            Assert.That(result, Is.True);
            mockTrailerRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Trailer>()), Times.Once);
        }

        [Test]
        public void GetEditTrailerByIdAsync_ReturnsNull_WhenTrailerNotFound()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            this.mockTrailerRepository.Setup(r => r.GetAllAttached())
                .Returns(new List<Trailer>().AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = this.trailerService.GetEditTrailerByIdAsync(id).Result;

            // Assert
            Assert.That(result, Is.Null);
            this.mockTrailerRepository.Verify(r => r.GetAllAttached(), Times.Once);
        }

        [Test]
        public void DeleteTrailerGetAsync_ReturnsTrailerDeleteViewModel_WhenTrailerExistsAndIsNotDeleted()
        {
            // Arrange
            Guid trailerId = Guid.NewGuid();
            var trailers = new List<Trailer>
        {
            new Trailer
            {
                Id = trailerId,
                TrailerNumber = "TR123",
                IsDeleted = false
            }
        }.AsQueryable();

            this.mockTrailerRepository.Setup(r => r.GetAllAttached())
                .Returns(trailers.AsQueryable()
                .BuildMockDbSet().Object);

            // Act
            var result = this.trailerService.DeleteTrailerGetAsync(trailerId).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(trailerId));
            Assert.That(result.TrailerNumber, Is.EqualTo("TR123"));
            this.mockTrailerRepository.Verify(r => r.GetAllAttached(), Times.Once);
        }

        [Test]
        public void DeleteTrailerGetAsync_ReturnsNull_WhenTrailerDoesNotExist()
        {
            // Arrange
            Guid trailerId = Guid.NewGuid();
            var trailers = new List<Trailer>
        {
            new Trailer
            {
                Id = Guid.NewGuid(),
                TrailerNumber = "TR123",
                IsDeleted = false
            }
        }.AsQueryable();

            
            this.mockTrailerRepository.Setup(r => r.GetAllAttached())
                .Returns(trailers.AsQueryable()
                .BuildMockDbSet().Object);

            // Act
            var result = this.trailerService.DeleteTrailerGetAsync(trailerId).Result;

            // Assert
            Assert.That(result, Is.Null);
            this.mockTrailerRepository.Verify(r => r.GetAllAttached(), Times.Once);
        }

        [Test]
        public void GetEditTrailerByIdAsync_ReturnsTrailerEditInputModel_WhenTrailerExistsAndIsNotDeleted()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var trailers = new List<Trailer>
        {
            new Trailer
            {
                Id = id,
                TrailerNumber = "12345",
                Make = "Make1",
                Type = "Type1",
                ModelYear = "2020",
                IsDeleted = false
            }
        }.AsQueryable();

            this.mockTrailerRepository.Setup(r => r.GetAllAttached())
                .Returns(trailers.AsQueryable()
                .BuildMockDbSet().Object);

            // Act
            var result = this.trailerService.GetEditTrailerByIdAsync(id).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.TrailerNumber, Is.EqualTo("12345"));
            Assert.That(result.Make, Is.EqualTo("Make1"));
            Assert.That(result.Type, Is.EqualTo("Type1"));
            Assert.That(result.ModelYear, Is.EqualTo("2020"));
            this.mockTrailerRepository.Verify(r => r.GetAllAttached(), Times.Once);
        }
    }
}
