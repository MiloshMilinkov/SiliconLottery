using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;

namespace SL_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        //When the controller is called it creates an instance of this controller and calls our StoreContext as a service.
        public ProductsController(StoreContext context)
        {
            _context=context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            //Async provides our code more scalability by making it not block a thread.
            //task is created to deal with our request and the thread is freed to do other request untill the task finishes its job.
            var products=await _context.Products.ToListAsync();
            return products;
        }

        [HttpGet("{id}")]
        //We find one entity by providing a Id to search by using FindAsync
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}