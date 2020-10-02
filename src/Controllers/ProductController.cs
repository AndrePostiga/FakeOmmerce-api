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
        public async Task<ActionResult<(int currentPage, int totalPages, IEnumerable<Product> Products)>> Get(            
            [FromQuery]string brand,            
            [FromQuery]string name,
            [FromQuery]List<string>? categories = null,                        
            [FromQuery]double priceGreatherThen = 1,
            [FromQuery]double priceLowerThen = 99999999,
            [FromQuery]int page = 1, 
            [FromQuery]int pageSize = 10           
        )
        {
            var filters = new FilterParameters(
                categories,                
                brand ?? "",
                name ?? "",
                priceGreatherThen,
                priceLowerThen
            );

            try
            {
                var products = await _repository.FindAll(page, pageSize, filters);            
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
            catch (BadRequestException e)
            {
                return BadRequest(e.HttpErrorResponse);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.HttpErrorResponse);
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

            product.Id = ObjectId.GenerateNewId();         

            System.Console.WriteLine($@"
                id : {product.Id}
                name : {product.Name}
                images : {product.Images}
                categories: {product.Categories}
                brand : {product.Brand}
                price : {product.Price}
                description : {product.Description}
            ");

            try
            {                
                await _repository.Create(product);
                return new CreatedResult("Database", product);                
            }
            catch (ConflictException e)
            {
                return Conflict(e.HttpErrorResponse);
            }
            catch (System.Exception)
            {                
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
        }       
    
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Product>> Put(string id, [FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }  

            System.Console.WriteLine($@"
                name : {product.Name}
                images : {product.Images}
                categories: {product.Categories}
                brand : {product.Brand}
                price : {product.Price}
                description : {product.Description}
            ");

            try
            {
                var returnedProduct = await _repository.UpdateById(id, product);
                return Ok(returnedProduct);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.HttpErrorResponse);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.HttpErrorResponse);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Product>> Delete(string id)
        {
            try
            {
                var product = await _repository.DeleteById(id); 
                return Ok(product); 
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.HttpErrorResponse);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.HttpErrorResponse);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
