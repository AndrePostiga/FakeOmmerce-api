using System.Collections.Generic;
using System.Threading.Tasks;
using FakeOmmerce.Models;
using FakeOmmerce.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FakeOmmerce.Controllers
{
  [ApiController]
    [Route("api/v1/[Controller]")]
    public class ProductController : ControllerBase
    {   
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]        
        public async Task<ActionResult<(int currentPage, int totalPages, IEnumerable<Product> Products)>> Get([FromQuery]int page = 1, [FromQuery]int pageSize = 10)
        {
            var products = await _repository.FindAll(page, pageSize);
            System.Console.WriteLine(products);
            return Ok(new {products.currentPage, products.totalPages, products.data});
        }
    }
}
