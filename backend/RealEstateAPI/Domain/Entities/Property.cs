namespace RealEstateAPI.Domain.Entities
{
    using MongoDB.Bson;
    public class Property
    {
        public ObjectId Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public decimal Price { get; set; }
        public required string ImageUrl { get; set; }
    }
}
