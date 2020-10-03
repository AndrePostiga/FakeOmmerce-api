namespace FakeOmmerce.Data
{
    using FakeOmmerce.Models;
    using MongoDB.Driver;
    
    public interface IProductContext
    {
      IMongoCollection<Product> Products { get; }
    }
}