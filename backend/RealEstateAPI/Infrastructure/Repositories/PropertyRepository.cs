namespace RealEstateAPI.Infrastructure.Repositories
{
    using MongoDB.Driver;
    using MongoDB.Bson;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RealEstateAPI.Domain.Entities;
    using RealEstateAPI.Domain.Repositories;

    public class PropertyRepository : IPropertyRepository
    {
        private readonly IMongoCollection<Property> _propertiesCollection;

        public PropertyRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("RealEstateDB");
            _propertiesCollection = database.GetCollection<Property>("Properties");
        }

        public async Task<List<Property>> GetPropertiesAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice)
        {
            var filterBuilder = Builders<Property>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
                filter &= filterBuilder.Regex("Name", new BsonRegularExpression(name, "i"));

            if (!string.IsNullOrEmpty(address))
                filter &= filterBuilder.Regex("Address", new BsonRegularExpression(address, "i"));

            if (minPrice.HasValue)
                filter &= filterBuilder.Gte("Price", minPrice.Value);

            if (maxPrice.HasValue)
                filter &= filterBuilder.Lte("Price", maxPrice.Value);

            return await _propertiesCollection.Find(filter).ToListAsync();
        }

        public async Task<Property?> GetPropertyByIdAsync(string id)
        {
            return await _propertiesCollection.Find(p => p.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
        }

        public async Task AddPropertyAsync(Property property)
        {
            await _propertiesCollection.InsertOneAsync(property);
        }

        public async Task UpdatePropertyAsync(string id, Property property)
        {
            await _propertiesCollection.ReplaceOneAsync(p => p.Id == ObjectId.Parse(id), property);
        }

        public async Task DeletePropertyAsync(string id)
        {
            await _propertiesCollection.DeleteOneAsync(p => p.Id == ObjectId.Parse(id));
        }
    }
}
