

namespace FakeOmmerce.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FakeOmmerce.Models;
    using MongoDB.Bson;

  public interface IProductRepository
    {
      Task<(int currentPage, int totalPages, IEnumerable<Product> data)> FindAll(int page, int pageSize, FilterParameters filters);       

      Task<Product> FindById(ObjectId id);

      Task<Product> Create(Product product);     

      Task<Product> UpdateById(Product product);

      Task<Product> DeleteById(ObjectId id);       
    }
}