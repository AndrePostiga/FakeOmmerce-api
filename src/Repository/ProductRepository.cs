using System.Collections.Generic;
using System.Threading.Tasks;
using FakeOmmerce.Data;
using FakeOmmerce.Models;
using MongoDB.Driver;

namespace FakeOmmerce.Repository
{
  class ProductRepository : IProductRepository
  {
    private IProductContext _context;

    public ProductRepository(IProductContext context)
    {
        _context = context;
    }

    public async Task<(int currentPage, int totalPages, IEnumerable<Product> data)> FindAll(int page, int pageSize)
    {
        var filter = Builders<Product>.Filter.Empty;

        var data = await _context.Products.Find(filter)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

        var count = await _context.Products.CountDocumentsAsync(filter);
        var totalPages = (int)count/pageSize;

        return (page, totalPages, data);
    }

    public Task<Product> FindById(string id)
    {
      throw new System.NotImplementedException();
    }

    public Task<Product> Create(Product product)
    {
      throw new System.NotImplementedException();
    }    

    public Task<Product> UpdateById(Product product)
    {
      throw new System.NotImplementedException();
    }

    public Task<Product> DeleteById(string id)
    {
      throw new System.NotImplementedException();
    }
  }
}