using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace SL_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
       //private readonly IProductRepository _repo;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductType> _productsTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productsBrandRepo;
        //When the controller is called it creates an instance of this controller and calls our IProductRepository as a service.
        public ProductsController(IGenericRepository<Product> productsRepo,
                                  IGenericRepository<ProductType> productsTypeRepo,
                                  IGenericRepository<ProductBrand> productsBrandRepo)
        {
            //_repo=repo;
            _productsBrandRepo=productsBrandRepo;
            _productsRepo=productsRepo;
            _productsTypeRepo=productsTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            //Async provides our code more scalability by making it not block a thread.
            //task is created to deal with our request and the thread is freed to do other request untill the task finishes its job.
            var spec=new ProductsWithTypesAndBrandsSpecification();
            var products=await _productsRepo.ListAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        //We find one entity by providing a Id to search by using FindAsync
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec=new ProductsWithTypesAndBrandsSpecification(id);
            return await _productsRepo.GetEntityWithSpec(spec);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>>GetProductTypes(){
            var types=await _productsTypeRepo.ListAllAsync();
            return Ok(types);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>>GetProductBrands(){
            var brands=await _productsBrandRepo.ListAllAsync();
            return Ok(brands);
        }

     
    }
}