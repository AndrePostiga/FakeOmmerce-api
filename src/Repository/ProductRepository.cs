using System.Collections.Generic;
using System.Threading.Tasks;
using FakeOmmerce.Data;
using FakeOmmerce.Errors;
using FakeOmmerce.Models;
using MongoDB.Bson;
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

    private ObjectId isValidObjId(string id)
    {
        bool isValid = ObjectId.TryParse(id, out ObjectId objId);
        if (!isValid)
        {
            throw new BadRequestException($@"id: {id}");
        }

        return objId;
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

    public async Task<Product> FindById(string id)
    {        
        var objId = isValidObjId(id);

        var product = await _context.Products.Find(x => x.Id.Equals(objId)).FirstOrDefaultAsync();

        if (product == null)
        {
            throw new NotFoundException(id.ToString());
        }

        return product;
    }

    public async Task<Product> Create(Product product)
    {
      await _context.Products.InsertOneAsync(product);
      return product;
    }    

    public Task<Product> UpdateById(Product product)
    {
      throw new System.NotImplementedException();
    }

    public Task<Product> DeleteById(string id)
    {
      throw new System.NotImplementedException();
    }

    public async Task<IEnumerable<Product>> CreateMany(IEnumerable<Product> products)
    {
      await _context.Products.InsertManyAsync(products);
      return products;
    }
  }
}