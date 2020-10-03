using System.Collections.Generic;
using System.Threading.Tasks;
using FakeOmmerce.Data;
using FakeOmmerce.Errors;
using FakeOmmerce.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace FakeOmmerce.Repository
{
    class ProductRepository : IProductRepository
    {
        private IProductContext _context;

        public ProductRepository(IProductContext context)
        {
            _context = context;
        }

        public async Task<(int currentPage, int totalPages, IEnumerable<Product> data)> FindAll(int page, int pageSize, FilterParameters filters)
        {
            var filter = filters.makeFilters();
            var data = await _context.Products.Find(filter)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();            

            var count = await _context.Products.CountDocumentsAsync(filter);
            var totalPages = (int)count / pageSize;

            return (page, totalPages, data);
        }

        public async Task<Product> FindById(ObjectId id)
        {            
            var product = await _context.Products.Find(x => x.InternalId.Equals(id)).FirstOrDefaultAsync();
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

        public async Task<Product> UpdateById(Product product)
        {
            var productOnDb = await this.FindById(product.InternalId);

            var filter = Builders<Product>.Filter.Eq(x => x.InternalId, product.InternalId);
            var update = Builders<Product>.Update
              .Set(x => x.Name, product.Name == null ? productOnDb.Name : product.Name)
              .Set(x => x.Images, product.Images == null ? productOnDb.Images : product.Images)
              .Set(x => x.Price, product.Price < 0 ? productOnDb.Price : product.Price)
              .Set(x => x.Brand, product.Brand == null ? productOnDb.Brand : product.Brand)
              .Set(x => x.Description, product.Description == null ? productOnDb.Description : product.Description)
              .Set(x => x.Categories, product.Categories == null ? productOnDb.Categories : product.Categories);

            await _context.Products.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = false });
			
            var updatedProduct = await this.FindById(product.InternalId);
            return updatedProduct;
        }

        public async Task<Product> DeleteById(ObjectId id)
        {
            var productOnDb = await _context.Products.Find(x => x.InternalId.Equals(id)).FirstOrDefaultAsync();
            if (productOnDb == null)
            {
                throw new NotFoundException($@"product id : {id.ToString()}");
            }

            var deleteResult = await _context.Products.DeleteOneAsync(x => x.InternalId.Equals(id));
            return productOnDb;
        }

    }
}