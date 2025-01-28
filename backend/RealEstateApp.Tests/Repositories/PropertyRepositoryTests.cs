using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MongoDB.Driver;
using RealEstateAPI.Domain.Entities;
using RealEstateAPI.Infrastructure.Repositories;
using Xunit;

namespace RealEstateApp.Tests.Repositories
{
    public class PropertyRepositoryTests
    {
        private readonly Mock<IMongoCollection<Property>> _mockCollection;
        private readonly Mock<IMongoDatabase> _mockDatabase;
        private readonly Mock<IMongoClient> _mockClient;
        private readonly PropertyRepository _repository;

        public PropertyRepositoryTests()
        {
            _mockCollection = new Mock<IMongoCollection<Property>>();
            _mockDatabase = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();

            _mockDatabase.Setup(db => db.GetCollection<Property>("Properties", null))
                .Returns(_mockCollection.Object);

            _mockClient.Setup(client => client.GetDatabase("RealEstateDB", null))
                .Returns(_mockDatabase.Object);

            _repository = new PropertyRepository(_mockClient.Object);
        }

        [Fact]
        public async Task GetPropertiesAsync_ShouldReturnProperties()
        {
            var properties = new List<Property>
            {
                new Property { Id = MongoDB.Bson.ObjectId.GenerateNewId(), Name = "Property 1" },
                new Property { Id = MongoDB.Bson.ObjectId.GenerateNewId(), Name = "Property 2" }
            };
            var cursor = new Mock<IAsyncCursor<Property>>();
            cursor.Setup(_ => _.Current).Returns(properties);
            cursor.SetupSequence(_ => _.MoveNext(It.IsAny<System.Threading.CancellationToken>()))
                  .Returns(true)
                  .Returns(false);

            _mockCollection.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<Property>>(),
                                                   It.IsAny<FindOptions<Property, Property>>(),
                                                   It.IsAny<System.Threading.CancellationToken>()))
                           .ReturnsAsync(cursor.Object);

            var result = await _repository.GetPropertiesAsync(null, null, null, null);

            Assert.Equal(2, result.Count);
        }
    }
}
