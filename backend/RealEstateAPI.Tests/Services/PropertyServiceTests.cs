using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using Moq;
using RealEstateAPI.Application.Services;
using RealEstateAPI.Domain.Entities;
using RealEstateAPI.Domain.Repositories;
using Xunit;

namespace RealEstateApp.Tests.Services
{
    public class PropertyServiceTests
    {
        private readonly Mock<IPropertyRepository> _mockRepository;
        private readonly PropertyService _service;

        public PropertyServiceTests()
        {
            _mockRepository = new Mock<IPropertyRepository>();
            _service = new PropertyService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetProperties_ShouldReturnFilteredProperties()
        {
            var properties = new List<Property>
            {
                new Property { Id = ObjectId.GenerateNewId(), Name = "Property 1", Address = "123 Main St", Price = 500000, ImageUrl = "https://example.com/1.jpg" },
                new Property { Id = ObjectId.GenerateNewId(), Name = "Property 2", Address = "456 Elm St", Price = 750000, ImageUrl = "https://example.com/2.jpg" }
            };

            _mockRepository.Setup(repo => repo.GetPropertiesAsync(null, null, 400000, 800000))
                           .ReturnsAsync(properties);

            var result = await _service.GetPropertiesAsync(null, null, 400000, 800000);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Name == "Property 1");
            Assert.Contains(result, p => p.Name == "Property 2");
        }
    }
}
