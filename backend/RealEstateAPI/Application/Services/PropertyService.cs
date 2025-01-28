namespace RealEstateAPI.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RealEstateAPI.Domain.Entities;
    using RealEstateAPI.Domain.Repositories;

    public class PropertyService
    {
        private readonly IPropertyRepository _repository;

        public PropertyService(IPropertyRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Property>> GetPropertiesAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice)
        {
            return _repository.GetPropertiesAsync(name, address, minPrice, maxPrice);
        }

        public Task<Property?> GetPropertyByIdAsync(string id)
        {
            return _repository.GetPropertyByIdAsync(id);
        }

        public Task AddPropertyAsync(Property property)
        {
            return _repository.AddPropertyAsync(property);
        }

        public Task UpdatePropertyAsync(string id, Property property)
        {
            return _repository.UpdatePropertyAsync(id, property);
        }

        public Task DeletePropertyAsync(string id)
        {
            return _repository.DeletePropertyAsync(id);
        }
    }
}
