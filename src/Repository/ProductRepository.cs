using System.Collections.Generic;
using System.Threading.Tasks;
using FakeOmmerce.Data;
using FakeOmmerce.Errors;
using FakeOmmerce.Models;
using Microsoft.AspNetCore.Http;
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
      
      var productOnDb = await _context.Products.Find(x => x.Name == product.Name).FirstOrDefaultAsync();      
      if (productOnDb != null)
      {
          throw new ConflictException(productOnDb.Name);
      }

      await _context.Products.InsertOneAsync(product);
      return product;
    }    

    public async Task<Product> UpdateById(string id, Product product)
    {
      isValidObjId(id);
      await this.FindById(id);
      
      var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
      var update = Builders<Product>.Update
        .Set(x => x.Name, product.Name)
        .Set(x => x.Images, product.Images)
        .Set(x => x.Price, product.Price)
        .Set(x => x.Brand, product.Brand)
        .Set(x => x.Description, product.Description)
        .Set(x => x.Categories, product.Categories)
        .Set(x => x.Images, product.Images);
      var updatedProduct = await _context.Products.UpdateOneAsync(filter, update, new UpdateOptions {IsUpsert = false});      
      return product;
    }

    public async Task<Product> DeleteById(string id)
    {
        var objId = isValidObjId(id);

        var productOnDb = await _context.Products.Find(x => x.Id.Equals(objId)).FirstOrDefaultAsync();      
        if (productOnDb == null)
        {
            throw new NotFoundException($@"product id : {id}");
    }

        var deleteResult = await _context.Products.DeleteOneAsync(x => x.Id.Equals(objId));
        return productOnDb;
    }

  }
}