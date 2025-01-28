namespace RealEstateAPI.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RealEstateAPI.Domain.Entities;

    public interface IPropertyRepository
    {
        Task<List<Property>> GetPropertiesAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice);
        Task<Property?> GetPropertyByIdAsync(string id);
        Task AddPropertyAsync(Property property);
        Task UpdatePropertyAsync(string id, Property property);
        Task DeletePropertyAsync(string id);
    }
}
