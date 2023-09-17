using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
// using Core.Specifications;
using SL_API.Dtos;
using AutoMapper;
using SL_API.Errors;
using SL_API.Helpers;

namespace SL_API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        
        public ProductsController(IProductRepository productRepository,
                                  IMapper mapper)
        {
            _productRepository=productRepository;
            _mapper=mapper;
        }
        
        // [HttpGet]
        // public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] string searchTerm, [FromQuery] string orderBy = "nameAsc", [FromQuery] int? pageIndex = 0, [FromQuery] int pageSize = 5, [FromQuery] int? typeId = null, [FromQuery] int? brandId = null)
        // {
        //     var products = await _productRepository.GetProductsAsync(searchTerm, orderBy, pageIndex, pageSize, typeId, brandId);
        //     var data=_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductDto>>(products);
        //     Console.WriteLine(products.Count());
        //     return Ok(new Pagination<ProductDto>((int)pageIndex,pageSize,products.Count(),data));
            
        // }

        [HttpGet]
public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] string searchTerm, [FromQuery] string orderBy = "nameAsc", 
[FromQuery] int? pageIndex = 1, [FromQuery] int pageSize = 5, [FromQuery] int? typeId = null, [FromQuery] int? brandId = null)
{
    
    var (products, totalCount) = await _productRepository.GetProductsAsync(searchTerm, orderBy, pageIndex, pageSize, typeId, brandId);

    var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

    return Ok(new Pagination<ProductDto>(pageIndex.Value, pageSize, totalCount, data));
}
   
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        //We find one entity by providing a Id to search by using FindAsync
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            var data= new ProductDto
            {
                Id=product.Id,
                Name=product.Name,
                Description=product.Description,
                Price=product.Price,
                PictureUrl=product.PictureUrl,
                ProductType=product.ProductType.Name,
                ProductBrand=product.ProductBrand.Name
            };
            return Ok(product);
        }
        
        // GET: api/products/types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productRepository.GetTypesAsync());
        }

        // GET: api/products/brands
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productRepository.GetBrandsAsync());
        }






        // public async Task<ActionResult<ProductDto>> GetProduct(int id)
        // {
        //     var spec=new ProductsWithTypesAndBrandsSpecification(id);
        //     var product= await _productsRepo.GetEntityWithSpec(spec);
        //     /*return new ProductDto
        //     {
        //         Id=product.Id,
        //         Name=product.Name,
        //         Description=product.Description,
        //         Price=product.Price,
        //         PictureUrl=product.PictureUrl,
        //         ProductType=product.ProductType.Name,
        //         ProductBrand=product.ProductBrand.Name
        //     };*/
        //     if(product==null)
        //     {
        //         return NotFound(new ApiResponse(404));
        //     }
        //     return _mapper.Map<Product,ProductDto>(product);
        // }
        // [HttpGet("types")]
        // public async Task<ActionResult<IReadOnlyList<ProductType>>>GetProductTypes(){
        //     var types=await _productsTypeRepo.ListAllAsync();
        //     return Ok(types);
        // }

        // [HttpGet("brands")]
        // public async Task<ActionResult<IReadOnlyList<ProductType>>>GetProductBrands(){
        //     var brands=await _productsBrandRepo.ListAllAsync();
        //     return Ok(brands);
        // }

     
    }
}