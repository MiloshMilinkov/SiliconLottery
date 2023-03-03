using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;

namespace SL_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        //When the controller is called it creates an instance of this controller and calls our IProductRepository as a service.
        public ProductsController(IProductRepository repo)
        {
            _repo=repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            //Async provides our code more scalability by making it not block a thread.
            //task is created to deal with our request and the thread is freed to do other request untill the task finishes its job.
            var products=await _repo.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        //We find one entity by providing a Id to search by using FindAsync
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var products=await _repo.GetProductByIdAsync(id);
            return Ok(products);
        }
    }
}