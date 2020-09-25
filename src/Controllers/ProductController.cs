using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeOmmerce.Errors;
using FakeOmmerce.Models;
using FakeOmmerce.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
            try
            {
                var products = await _repository.FindAll(page, pageSize);            
                return Ok(new {products.currentPage, products.totalPages, products.data});
            }
            catch (System.Exception)
            {
                
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetById(string id)
        {    
            try
            {
                var product = await _repository.FindById(id); 
                return Ok(product); 
            }
            catch (NotFoundException e)
            {
                return NotFound(e);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }  

            try
            {
                await _repository.Create(product);
                return new CreatedResult("Database", product);
            }
            catch (System.Exception)
            {
                
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
        }  

        // [HttpPost]
        // [Route("many")]
        // public async Task<ActionResult<IEnumerable<Product>>> CreateMany([FromBody]IEnumerable<Product> products)
        // {
        //     await _repository.CreateMany(products);
        //     return new CreatedResult("Database", products);
        // }       
    }
}
