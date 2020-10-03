namespace FakeOmmerce.Data
{
  using FakeOmmerce.Models;
  using MongoDB.Driver;


  public class ProductContext : IProductContext
  {
    private readonly IMongoDatabase _db;

    public ProductContext(MongoDbConfig config)
    {
      var client = new MongoClient(config.ConnectionString);
      _db = client.GetDatabase(config.Database);
    }

    public IMongoCollection<Product> Products => _db.GetCollection<Product>("products");
  }
}