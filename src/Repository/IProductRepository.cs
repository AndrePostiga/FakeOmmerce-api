

namespace FakeOmmerce.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FakeOmmerce.Models;

    public interface IProductRepository
    {
      Task<(int currentPage, int totalPages, IEnumerable<Product> data)> FindAll(int page, int pageSize);       

      Task<Product> FindById(string id);

      Task<Product> Create(Product product);

      Task<IEnumerable<Product>> CreateMany(IEnumerable<Product> products);

      Task<Product> UpdateById(Product product);

      Task<Product> DeleteById(string id);       
    }
}